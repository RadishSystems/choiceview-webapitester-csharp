namespace WebApiInterface
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Formatting;
    using System.Net.Http.Headers;
    using ApiTester.Properties;
    using Models;

    public partial class MainForm : Form
    {
        private HttpClient Client;
        private Uri BaseUri;
        private Uri SessionsUri;
        private readonly MediaTypeFormatter jsonFormatter;
        private Uri SessionUri;
        private Uri SessionPropertiesUri;
        private Session cvSession;

        private void UpdateUI(bool connected)
        {
            Invoke((MethodInvoker)(
                () =>
                {
                    btnGetProperties.Enabled = SessionPropertiesUri != null && connected;
                    btnSendUrl.Enabled = false;
                    btnSendText.Enabled = false;
                    btnStartEndSession.Text = connected ? "End Session" : "Start Session";
                    if (!btnStartEndSession.Enabled) btnStartEndSession.Enabled = true;
                    timer1.Enabled = connected;
                    if (!connected) toolStripStatusLabel1.Text = "Not connected.";
                }));
        }

        private void UpdateSessionInfo(Session session)
        {
            Invoke((MethodInvoker)(
                () =>
                {
                    if (!session.status.Equals("disconnected"))
                    {
                        txtSessionID.Text = session.sessionId.ToString();
                        txtIvrID.Text = session.callId;
                        txtNetworkType.Text = session.networkType;
                        txtStatus.Text = session.status;
                        txtQuality.Text = session.networkQuality;
                        txtCallerID.Text = session.callerId;
                        if (SessionPropertiesUri == null ||
                            !SessionPropertiesUri.AbsolutePath.StartsWith(
                                SessionUri.AbsolutePath))
                        {
                            var link =
                                session.links.Find(l => l.rel.EndsWith(Link.PayloadRel));
                            if (link != null) SessionPropertiesUri = new Uri(link.href);
                            else MessageBox.Show("Cannot find the properties uri!");
                        }
                        toolStripStatusLabel1.Text = String.Format("Last update: {0}", DateTime.Now);
                    }
                    else
                    {
                        timer1.Enabled = false;
                        UpdateUI(false);
                        txtSessionID.Text = "Not Connected";
                        txtIvrID.Text = string.Empty;
                        txtNetworkType.Text = string.Empty;
                        txtStatus.Text = string.Empty;
                        txtQuality.Text = string.Empty;
                        txtCallerID.Text = string.Empty;
                        SessionPropertiesUri = null;
                    }
                }));
        }

        private void UpdateSession()
        {
            Client.GetAsync(SessionUri).ContinueWith(
                responseTask =>
                {
                    if (responseTask.Result.IsSuccessStatusCode)
                    {
                        responseTask.Result.Content.ReadAsAsync<Session>(
                            new List<MediaTypeFormatter> { jsonFormatter }).ContinueWith(
                            contentTask =>
                                {
                                    cvSession = contentTask.Result;
                                    if (cvSession != null)
                                    {
                                        UpdateSessionInfo(cvSession);
                                    }
                                    else
                                    {
                                        MessageBox.Show(String.Format("GET {0} did not return recognizable content!",
                                                                      SessionUri.AbsoluteUri));
                                    }
                                });
                    }
                    else
                    {
                        switch (responseTask.Result.StatusCode)
                        {
                            case HttpStatusCode.NotFound:
                                cvSession.status = "disconnected";
                                UpdateSessionInfo(cvSession);
                                break;
                            case HttpStatusCode.NotModified:
                                break;
                            default:
                                MessageBox.Show(String.Format("GET {0} failed!\nStatus Code: {1}",
                                                              SessionUri.AbsoluteUri, responseTask.Result.StatusCode));
                                break;
                        }
                    }
                });
        }

        public MainForm()
        {
            InitializeComponent();
            jsonFormatter = new JsonMediaTypeFormatter();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                BaseUri = new Uri((Settings.Default.SecureTransport ? "https://" : "http://") +
                    Settings.Default.ApiAddress, UriKind.Absolute);
                SessionsUri = new Uri(BaseUri, "/ivr/api/sessions");

                Client = new HttpClient { BaseAddress = BaseUri };
                Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                if(!string.IsNullOrWhiteSpace(Settings.Default.Username))
                {
                    var param =
                        Convert.ToBase64String(
                            System.Text.Encoding.ASCII.GetBytes(string.Format("{0}:{1}",
                                                                              Settings.Default.Username,
                                                                              Settings.Default.Password)));
                    Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("basic", param);
                }
            }
            catch (UriFormatException)
            {
                MessageBox.Show(String.Format("Bad API server address: {0}",
                                              Settings.Default.ApiAddress ?? "[not set]"));
                Close();
            }

            UpdateUI(false);
            txtSessionID.Text = "Not Connected";
            txtNetworkType.Text = string.Empty;
            txtStatus.Text = string.Empty;
            txtQuality.Text = string.Empty;
            txtCallerID.Text = string.Empty;
        }

        private void btnStartEndSession_Click(object sender, EventArgs e)
        {
            if (cvSession == null ||
                cvSession.status.Equals("disconnected"))
            {
                if (!string.IsNullOrWhiteSpace(txtClientID.Text))
                {
                    var newSession = new NewSession
                                         {
                                             callerId = txtClientID.Text,
                                             callId = txtCallID.Text,
                                             newMessageUri = string.Empty,
                                             stateChangeUri = string.Empty
                                         };

                    Invoke((MethodInvoker)(() =>
                    {
                        toolStripStatusLabel1.Text = "Waiting for server at " + Settings.Default.ApiAddress + "...";
                        btnStartEndSession.Text = "Waiting";
                        btnStartEndSession.Enabled = false;
                    }));

                    Client.PostAsJsonAsync<NewSession>(SessionsUri.AbsoluteUri, newSession).ContinueWith(
                        responseTask =>
                        {
                            if (responseTask.Exception != null)
                            {
                                UpdateUI(false);
                                MessageBox.Show(String.Format("POST {0} failed!\nError: {1}",
                                                              SessionsUri.AbsoluteUri,
                                                              responseTask.Exception.InnerException.Message));
                            }
                            else if (responseTask.Result.StatusCode == HttpStatusCode.Created)
                            {
                                SessionUri = responseTask.Result.Headers.Location;
                                responseTask.Result.Content.ReadAsAsync<Session>(
                                    new List<MediaTypeFormatter> { jsonFormatter }).ContinueWith(
                                        contentTask =>
                                        {
                                            if (contentTask.Exception == null)
                                            {
                                                cvSession = contentTask.Result;
                                                if (cvSession != null)
                                                {
                                                    UpdateSessionInfo(cvSession);
                                                }
                                                else
                                                {
                                                    MessageBox.Show(
                                                        String.Format(
                                                            "POST {0} did not return recognizable content!",
                                                            SessionsUri.AbsoluteUri));
                                                }
                                                UpdateUI(cvSession != null &&
                                                         !cvSession.status.Equals("disconnected"));
                                            }
                                            else
                                            {
                                                UpdateUI(false);
                                                MessageBox.Show(
                                                    String.Format(
                                                        "Cannot read the content from POST {0}\nError: {1}",
                                                        SessionsUri.AbsoluteUri, contentTask.Exception.InnerException.Message));
                                            }
                                        }
                                    );
                            }
                            else
                            {
                                UpdateUI(false);
                                MessageBox.Show(String.Format("POST {0} failed!\nStatus Code: {1}",
                                                              SessionsUri.AbsoluteUri,
                                                              responseTask.Result.StatusCode));
                            }
                        });
                }
                else
                {
                    MessageBox.Show("No Client ID!");
                }
            }
            else
            {
                Client.DeleteAsync(SessionUri).ContinueWith(
                    task =>
                        {
                            if (task.Exception != null)
                            {
                                MessageBox.Show(String.Format("DELETE {0} failed!\nError: {1}",
                                    SessionUri.AbsoluteUri, task.Exception.InnerException.Message));
                            }
                            else if (task.Result.IsSuccessStatusCode)
                            {
                                cvSession.status = "disconnected";
                                UpdateSessionInfo(cvSession);
                            }
                            else
                            {
                                MessageBox.Show(String.Format("DELETE {0} failed!\nStatus Code: {1}",
                                    SessionUri.AbsoluteUri, task.Result.StatusCode));
                            }
                        });
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateSession();
        }

        private void btnGetProperties_Click(object sender, EventArgs e)
        {
            if(cvSession != null && cvSession.properties != null)
            {
                var form = new PropertiesForm(cvSession.properties);
                form.ShowDialog(this);
                form.Dispose();
            }
        }
    }
}

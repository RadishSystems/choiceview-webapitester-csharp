namespace WebApiInterface
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
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
                    bool activeSession = SessionUri != null && connected;
                    btnSendUrl.Enabled = activeSession;
                    btnSendText.Enabled = activeSession;
                    btnGetControlMessage.Enabled = activeSession;
                    btnTransfer.Enabled = activeSession;
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
                        txtSessionID.Text = session.sessionId.ToString(CultureInfo.InvariantCulture);
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

        private void displayHttpRequestError(AggregateException ex, String errId)
        {
            if (ex.InnerException is HttpRequestException)
            {
                MessageBox.Show(String.Format("{0}\nError: {1}\n{2}",
                                              errId, ex.InnerException.Message,
                                              ex.InnerException.InnerException.Message));
            }
            else
            {
                MessageBox.Show(String.Format("{0}\nError: {1}",
                                              errId, ex.InnerException.Message));
            }
        }

        private void displayHttpResponseError(HttpResponseMessage msg, String errId)
        {
            MessageBox.Show(String.Format("{0}\n{2} ({1})",
                            errId, Convert.ToInt16(msg.StatusCode), msg.ReasonPhrase));
        }

        private void UpdateSession()
        {
            Client.GetAsync(SessionUri).ContinueWith(
                responseTask =>
                {
                    var errId = String.Format("GET {0} failed!", SessionUri.AbsoluteUri);
                    if (responseTask.Exception != null)
                    {
                        displayHttpRequestError(responseTask.Exception, errId);
                    }
                    else if (responseTask.Result.IsSuccessStatusCode)
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
                                displayHttpResponseError(responseTask.Result, errId);
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

                    Client.PostAsJsonAsync(SessionsUri.AbsoluteUri, newSession).ContinueWith(
                        responseTask =>
                        {
                            var errId = String.Format("POST {0} failed!", SessionsUri.AbsoluteUri);
                            if (responseTask.Exception != null)
                            {
                                UpdateUI(false);
                                displayHttpRequestError(responseTask.Exception, errId);
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
                                displayHttpResponseError(responseTask.Result, errId);
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
                            var errId = String.Format("DELETE {0} failed!", SessionUri.AbsoluteUri);
                            if (task.Exception != null)
                            {
                                displayHttpRequestError(task.Exception, errId);
                            }
                            else if (task.Result.IsSuccessStatusCode)
                            {
                                cvSession.status = "disconnected";
                                UpdateSessionInfo(cvSession);
                            }
                            else displayHttpResponseError(task.Result, errId);
                        });
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateSession();
        }

        private void ShowProperties()
        {
            Invoke((MethodInvoker) (() =>
                                        {
                                            var form = new PropertiesForm(Client, SessionPropertiesUri, jsonFormatter);
                                            form.ShowDialog(this);
                                            form.Dispose();
                                        }));
        }
       
        private void btnGetProperties_Click(object sender, EventArgs e)
        {
            ShowProperties();
        }

        private void btnSendText_Click(object sender, EventArgs e)
        {
            if (SessionUri != null)
            {
                Client.PostAsync(SessionUri, new StringContent("Test Message sent at " + DateTime.Now)).ContinueWith(
                    task =>
                        {
                            var errId = String.Format("POST {0} text/plain failed!", SessionUri.AbsoluteUri);
                            if (task.Exception != null)
                            {
                                UpdateUI(false);
                                displayHttpRequestError(task.Exception, errId);
                            }
                            else if (!task.Result.IsSuccessStatusCode)
                            {
                                displayHttpResponseError(task.Result, errId);
                            }
                        });
            }
        }

        private void btnSendUrl_Click(object sender, EventArgs e)
        {
            if (SessionUri != null)
            {
                var clientUrl = new ClientUrl {url = "http://www.radishsystems.com"};

                Client.PostAsJsonAsync(SessionUri.AbsoluteUri, clientUrl).ContinueWith(
                    task =>
                    {
                        var errId = String.Format("POST {0} application/json failed!", SessionUri.AbsoluteUri);
                        if (task.Exception != null)
                        {
                            UpdateUI(false);
                            displayHttpRequestError(task.Exception, errId);
                        }
                        else if (!task.Result.IsSuccessStatusCode)
                        {
                            displayHttpResponseError(task.Result, errId);
                        }
                    });
            }

        }

        private void btnGetControlMessage_Click(object sender, EventArgs e)
        {
            if (SessionUri != null)
            {
                try
                {
                    var controlMessageUri = new Uri(SessionUri.AbsoluteUri + "/controlmessage");
                    Client.GetAsync(controlMessageUri).ContinueWith(
                        task =>
                            {
                                var errId = String.Format("GET {0} failed!", controlMessageUri.AbsoluteUri);
                                if (task.Exception != null)
                                {
                                    UpdateUI(false);
                                    displayHttpRequestError(task.Exception, errId);
                                }
                                else if (!task.Result.IsSuccessStatusCode)
                                {
                                    displayHttpResponseError(task.Result, errId);
                                }
                                else if (task.Result.StatusCode == HttpStatusCode.NoContent)
                                {
                                    MessageBox.Show("No message available!");
                                }
                                else
                                {
                                    var msg = task.Result.Content.ReadAsStringAsync().Result;
                                    MessageBox.Show(String.Format("Control Message received:\n{0}", msg));
                                }
                            });
                }
                catch (UriFormatException exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            if (SessionUri != null)
            {
                try
                {
                    var transferUri = new Uri(SessionUri.AbsoluteUri + "/transfer/radish1");
                    Client.PostAsync(transferUri, new StringContent("Transfer requested at " + DateTime.Now)).ContinueWith(
                        task =>
                            {
                                var errId = String.Format("POST {0} failed!", transferUri.AbsoluteUri);
                                if (task.Exception != null)
                                {
                                    UpdateUI(false);
                                    displayHttpRequestError(task.Exception, errId);
                                }
                                else if (!task.Result.IsSuccessStatusCode)
                                {
                                    displayHttpResponseError(task.Result, errId);
                                }
                            });
                }
                catch (UriFormatException exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
        }
    }
}

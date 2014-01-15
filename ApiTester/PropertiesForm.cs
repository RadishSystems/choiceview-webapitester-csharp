namespace WebApiInterface
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Formatting;
    using System.Windows.Forms;
    using Models;

    public partial class PropertiesForm : Form
    {
        private readonly HttpClient Switch;
        private readonly Uri PropertiesUri;
        private readonly MediaTypeFormatter Formatter;
        private Models.Properties SessionProperties;

        public PropertiesForm(HttpClient client, Uri propertiesUri, MediaTypeFormatter formatter)
        {
            InitializeComponent();
            Switch = client;
            PropertiesUri = propertiesUri;
            Formatter = formatter;
        }

        private void InitializeList(Payload pairs)
        {
            Invoke((MethodInvoker) (() =>
                {
                    PropertiesList.Items.Clear();
                    foreach (var pair in pairs)
                        PropertiesList.Items.Add(String.Format(pair.ToString()));
                }));
        }

        private void PropertiesForm_Load(object sender, EventArgs e)
        {
            if (PropertiesUri != null)
            {
                Switch.GetAsync(PropertiesUri).ContinueWith(
                    responseTask =>
                    {
                        if (responseTask.Result.IsSuccessStatusCode)
                        {
                            responseTask.Result.Content.ReadAsAsync<Models.Properties>(
                                new List<MediaTypeFormatter> { Formatter }).ContinueWith(
                                contentTask =>
                                {
                                    SessionProperties = contentTask.Result;
                                    if (SessionProperties != null && SessionProperties.properties != null)
                                    {
                                        InitializeList(SessionProperties.properties);
                                    }
                                    else
                                    {
                                        MessageBox.Show(String.Format("GET {0} did not return recognizable content!",
                                                                      PropertiesUri.AbsoluteUri));
                                    }
                                });
                        }
                        else
                        {
                            switch (responseTask.Result.StatusCode)
                            {
                                case HttpStatusCode.NotFound:
                                    MessageBox.Show(String.Format("{0} was not found!", PropertiesUri.AbsoluteUri));
                                    break;
                                case HttpStatusCode.NotModified:
                                    MessageBox.Show("No properties to show!");
                                    break;
                                default:
                                    var msg = responseTask.Result.Content.ReadAsStringAsync().Result;
                                    MessageBox.Show(String.Format("GET {0} failed!\nReason - {1}, {2}\n{3}",
                                                                  PropertiesUri.AbsoluteUri,
                                                                  responseTask.Result.StatusCode,
                                                                  responseTask.Result.ReasonPhrase,
                                                                  msg));
                                    break;
                            }
                        }
                    });
            }
        }

        private void btnAddProperty_Click(object sender, EventArgs e)
        {
            if (PropertiesUri != null)
            {
                var newProperty = new ClientProperty
                    {
                        name = String.Format("Property{0}", SessionProperties.properties.Count),
                        value = String.Format("Value{0}", SessionProperties.properties.Count)
                    };
                Switch.PostAsJsonAsync(PropertiesUri.AbsoluteUri, newProperty).ContinueWith(
                    responseTask =>
                    {
                        if (responseTask.Result.IsSuccessStatusCode)
                        {
                            SessionProperties.properties.Add(newProperty.name, newProperty.value);
                            InitializeList(SessionProperties.properties);
                        }
                        else
                        {
                            var msg = responseTask.Result.Content.ReadAsStringAsync().Result;
                            MessageBox.Show(String.Format("POST {0} failed!\nReason - {1}, {2}\n{3}",
                                                          PropertiesUri.AbsoluteUri,
                                                          responseTask.Result.StatusCode,
                                                          responseTask.Result.ReasonPhrase,
                                                          msg));
                        }
                    });
            }
        }
    }
}

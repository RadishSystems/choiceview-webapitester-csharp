using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WebApiInterface.Models;

namespace WebApiInterface
{
    public partial class PropertiesForm : Form
    {
        public PropertiesForm(Payload pairs)
        {
            InitializeComponent();
            foreach (var pair in pairs)
            {
                PropertiesList.Items.Add(String.Format(pair.ToString()));
            }
        }
    }
}

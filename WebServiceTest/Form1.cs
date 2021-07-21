using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Forms;

namespace WebServiceTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ServicePointManager.ServerCertificateValidationCallback = MyCertHandler;
            WebReference.WebService serv = new WebReference.WebService();
            serv.Credentials = new NetworkCredential("b2b", "HasloBMP2014", "");
            textBox2.Text += serv.b2b_nowyDokumentZam(textBox1.Text) + Environment.NewLine;
        }
        static bool MyCertHandler(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors error)
        {
            // Ignore errors
            return true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpClientApplication
{
    public partial class FormIPServer : Form
    {
        public delegate void ConfiguringServer(string ip, int port);
        public event ConfiguringServer OnConfiguringServer;

        public FormIPServer()
        {
            InitializeComponent();
        }

        public FormIPServer(string ip, int port)
        {
            InitializeComponent();

            if (ip != null)
                this.textBoxIP.Text = ip;

            if (port != -1)
                this.numericPort.Value = port;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (this.textBoxIP.Text.Trim().Length > 0 && this.numericPort.Value != 0)
            {
                this.OnConfiguringServer(this.textBoxIP.Text.Trim(), (int) this.numericPort.Value);

                this.Close();
            }
        }
    }
}

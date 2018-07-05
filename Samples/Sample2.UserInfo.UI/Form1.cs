using Sample2.UserInfo.UI.Callers;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sample2.UserInfo.UI
{
    public partial class Form1 : Form
    {
        const string function = "https://securityexample.azurewebsites.net/api/UserInfo";

        public Form1()
        {
            InitializeComponent();
            progressBar1.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var caller = new UnauthenticatedCaller();
            Run(caller.Call);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var caller = new AuthenticatedCaller();
            Run(caller.Call);
        }

        private async void Run(Func<string, Task<string>> action)
        {
            button1.Enabled = false;
            button2.Enabled = false;
            richTextBox1.Text = string.Empty;
            progressBar1.Style = ProgressBarStyle.Marquee;
            progressBar1.Visible = true;

            // foo
            var data = await action(function);

            richTextBox1.Text = data;
            progressBar1.Visible = false;
            button1.Enabled = true;
            button2.Enabled = true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TopTeam.Gear
{
    public partial class InputForm : Form
    {
        bool btnIsClicked = false;
        public InputForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!btnIsClicked)
            {
                string[] args = Environment.GetCommandLineArgs();
                string taskArgs = " ";
                for (int i = 2; i < (args.Length - 1); i++)
                {
                    taskArgs += args[i];
                    taskArgs += " ";
                }

                string taskParams = string.Format(taskArgs + Application.ExecutablePath + string.Format("\" -alert {0}\"", richTextBox1.Text));
                Process.Start("SCHTASKS.EXE", taskParams);
            }
            btnIsClicked = true;
            Program.TurnOff();
        }
    }
}

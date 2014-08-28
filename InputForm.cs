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
        bool btnIsClicked = false; // To prevent double adding a task to scheduler
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
                // args[0] => app name
                // args[1] => "-input"
                // starts from args[2]: 
                for (int i = 2; i < (args.Length - 1); i++)
                {
                    taskArgs += args[i];
                    taskArgs += " ";
                }
                // Forms a string (with params) that using as a second argument when a task added to scheduler with custom message from textbox 
                string taskParams = string.Format(taskArgs + Application.ExecutablePath + string.Format("\" -alert {0}\"", richTextBox1.Text));

                Process.Start("SCHTASKS.EXE", taskParams);
            }
            btnIsClicked = true;
            Program.TurnOff();
        }

        private void InputForm_Load(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.AutoPopDelay = 7000;
            toolTip1.InitialDelay = 500;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;
            toolTip1.SetToolTip(this.richTextBox1, "Enter your custom message and click OK or anywhere outside the form");
        }
    }
}
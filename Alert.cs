using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TopTeam.Gear
{
    public partial class Alert : Form
    {
        public Alert()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Constructor that sets textbox text recieved value
        /// </summary>
        /// <param name="s"></param>
        public Alert(string s)
        {
            InitializeComponent();
            textBox.Text = s;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Alert_Load(object sender, EventArgs e)
        {
            System.Media.SystemSounds.Beep.Play();
        }
    }
}

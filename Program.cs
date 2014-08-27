namespace TopTeam.Gear
{
    using System;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    using TopTeam.Gear.Parsers;
    using TopTeam.Gear.Model;

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            RunParam runParam = RunParam.standart; // Params for running app ("-alert", "-input", or "")
            
            string[] args = Environment.GetCommandLineArgs();
            if (args.Length > 1)
            {
                if (args[1].Equals("-alert")) runParam = RunParam.alertWindow;
                if (args[1].Equals("-input")) runParam = RunParam.inputTextBox;
            }
            switch (runParam){
                case RunParam.alertWindow:
                    string[] messageArray = new string[args.Length - 2];
                    if (args.Length > 2)
                    {
                        for (int i = 2; i < args.Length; i++)
                        {
                            messageArray[(i - 2)] = args[i];
                        }
                    }

                    Alert alertForm = new Alert(String.Join(" ", messageArray));
                    alertForm.FormClosed += AlertFormClosed;
                    
                    alertForm.Show();

                    break;
                case RunParam.inputTextBox:
                    string taskArgs = "";
                    for (int i = 2; i < args.Length; i++)
                    {
                        taskArgs += args[i];
                        taskArgs += " ";
                    }
                    InputForm inputForm = new InputForm();
                    //inputForm.Deactivate += InputFormDeactivate;
                    
                    inputForm.FormClosed += InputFormClosed;
                    inputForm.Show();
                    break;
                default: 
                    var menuStrip = new ContextMenuStrip();
            
                    var gear = Parser.ReadConfigs();
                    if (gear == null || gear.MenuItems == null || gear.MenuItems.Count == 0)
                    {
                        TurnOffAsync();
                        return;                                                                       
                    }

                    menuStrip.Items.AddRange(gear.MenuItems.ToArray());
                    menuStrip.Closed += MenuStripClosed;
            
                    menuStrip.Show(gear.StartX, gear.StartY);
                    menuStrip.Focus();
                    menuStrip.Items[gear.SelectedIndex].Select();
                    break;
            }

            Application.Run();
        }

        private static void InputFormClosed(object sender, EventArgs e)
        {
            TurnOffAsync();
        }

        private static void AlertFormClosed(object sender, FormClosedEventArgs e)
        {
            TurnOffAsync();
        }

        static void MenuStripClosed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            TurnOffAsync();
        }
        /// <summary>
        /// Turn off the application
        /// </summary>
        public static void TurnOff()
        {
            Application.Exit();
        }

        private static void TurnOffAsync()
        {
            Task.Run(
                async () =>
                {
                    await Task.Delay(150);
                    if (!HandledTurnOFf)
                    {
                        TurnOff();
                    }
                });
        }
        /// <summary>
        /// Used to turn off application if there is no any action is active (for example if pressed alt+tab)
        /// </summary>
        public static bool HandledTurnOFf = false;
    }
}

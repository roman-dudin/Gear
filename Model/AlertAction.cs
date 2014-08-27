using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TopTeam.Gear.Model
{
    using System.Diagnostics;
    using System.IO;

    public class AlertAction: Action
    {
        private static int quickNumber = 1;
        /// <summary>
        /// Prop, reading minutes from config
        /// </summary>
        private string Minutes
        {
            get
            {
                string val;
                return this.Params.TryGetValue(ActionParam.RemindMeIn, out val) ? val : string.Empty;
            }
        }
        /// <summary>
        /// Prop, reading message from config 
        /// </summary>
        private string Message
        {
            get
            {
                string msg;
                return this.Params.TryGetValue(ActionParam.Message, out msg) ? msg : string.Empty;
            }
        }
        
        private string taskParams()
        {
            DateTime saveNow = DateTime.Now;
            string hour;
            string min;
            string taskName = String.Format("GEAR_alert({0})", saveNow.ToString("dd-MMM-HH.mm.ss"));

            try
            {
                saveNow = saveNow.AddMinutes(Convert.ToInt32(this.Minutes, 10)); //Trying to convert string from config to int and add it (mins) to DateTime.Now
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("Unable to parse minutes from config-file. {0}Error: {1}", Environment.NewLine, ex.Message));
            }

            if(saveNow.Minute < 10) min = "0" + saveNow.Minute.ToString(); //adding 0 to minutes under 10 (1-9 => 01-09)
            else min = saveNow.Minute.ToString();
            if(saveNow.Hour < 10) hour = "0" + saveNow.Hour.ToString(); //adding 0 to hours under 10 (1-9 => 01-09)
            else hour = saveNow.Hour.ToString();
            return String.Format("/Create /TN {0} /SD {1} /ST {2}:{3} /SC ONCE /TR {4}", taskName, saveNow.ToString("MM/dd/yyyy"), hour, min, Application.ExecutablePath + "\" -alert " + Message + "\"");
        }

        /// <summary>
        /// Constructor that only call a base constructor
        /// </summary>
        /// <param name="param"></param>
        public AlertAction(Dictionary<ActionParam, string> param)
            : base(param)
        {
        }

        protected override void Execute(object sender, EventArgs e)
        {

            if (Message.Equals(string.Empty)) Process.Start(Application.ExecutablePath, "-input " + taskParams()); // Start app again with -input argument
            else Process.Start("SCHTASKS.EXE", taskParams()); // Adding a task to scheduler
        }

        /// <summary>
        /// Type of action from enum - Alert. 
        /// </summary>
        /// 
        public override ActionType Type
        {
            get
            {
                return ActionType.Alert;
            }
        }

        public override int QuickNumber
        {
            get
            {
                if (AlertAction.quickNumber < 10) return AlertAction.quickNumber;
                else return 0;
            }
            set
            {
                quickNumber = value;
            }
        }
    }
}

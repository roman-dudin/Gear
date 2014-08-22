﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace TopTeam.Gear.Model
{
    using System.Diagnostics;
    using System.IO;
    public class AlertAction: Action
    {
        private static int quickNumber = 1;

        private string Minutes
        {
            get
            {
                string val;
                return this.Params.TryGetValue(ActionParam.RemindMeIn, out val) ? val : string.Empty;

            }
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
            DateTime saveNow = DateTime.Now;
            string hour;
            string min;
            string taskName = String.Format("GEAR_alert({0})", saveNow.ToString("dd-MMM-HH.mm.ss"));
            string[] standartPath = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.exe", SearchOption.TopDirectoryOnly);
            string targetApp = standartPath[0];
            try
            {
                saveNow = saveNow.AddMinutes(Convert.ToInt32(this.Minutes, 10)); //Trying to convert string from config to int and add it (mins) to DateTime.Now
            }
            catch
            {
                MessageBox.Show("Mins");
            }
            if(saveNow.Minute < 10) min = "0" + saveNow.Minute.ToString();
            else min = saveNow.Minute.ToString();
            if (saveNow.Hour < 10) hour = "0" + saveNow.Hour.ToString();
            else hour = saveNow.Hour.ToString();



            MessageBox.Show(Application.ExecutablePath);
            Process.Start("SCHTASKS.EXE", String.Format("/Create /TN {0} /SD {1} /ST {2}:{3} /SC ONCE /TR {4}", taskName, saveNow.ToString("MM/dd/yyyy"), hour, min, targetApp));
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
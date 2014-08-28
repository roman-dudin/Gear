namespace TopTeam.Gear.Model
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Windows.Forms;


    public class WebAction : Action
    {
        private static int quickNumber = 1;
        private string Url
        {
            get
            {
                string val;
                return this.Params.TryGetValue(ActionParam.Url, out val) ? val : string.Empty;
            }
        }
        /// <summary>
        /// Constructor that only call a base constructor
        /// </summary>
        /// <param name="param"></param>
        public WebAction(Dictionary<ActionParam, string> param)
            : base(param)
        {
        }
        /// <summary>
        /// Overrided execute method, that starts the process - opening provided url adress in standart browser. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void Execute(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.Url))
            {
                MessageBox.Show(
                    "URL of site not found. Please fill in a URL adress of site in a config file. "); 
            }
            else if (this.Url.Contains(";"))
            {
                string[] urlArray = this.Url.Split(';');
                for (int i = 0; i < urlArray.Length; i++)
                {
                    Process.Start(urlArray[i].Trim());
                }
            } else
            {
                Process.Start(this.Url.Trim());
            }
        }
        /// <summary>
        /// Type of action from enum - Website. 
        /// </summary>
        public override ActionType Type
        {
            get
            {
                return ActionType.Website;
            }
        }

        public override int QuickNumber
        {
            get
            {
                if (WebAction.quickNumber < 10) return WebAction.quickNumber;
                else return 0;
            }
            set
            {
                quickNumber = value;
            }
        }
    }
}

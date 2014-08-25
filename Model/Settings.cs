using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using TopTeam.Gear.Parsers;

namespace TopTeam.Gear.Model
{
    static class Settings
    {
        static Settings()
        {
            SetNumControl();
        }
        public static bool NumControlEnable;
        private static void SetNumControl()
        {
            var args = Environment.GetCommandLineArgs();
            if (args.Length > 1)
            {
                try
                {
                    for (int i = 1; i < args.Length; i++)
                    {
                        string[] path = Directory.GetFiles(
                            args[i], "*.gear", SearchOption.TopDirectoryOnly);
                        NumControlEnable = ReadNumControlFromFile(path[0]);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(String.Format("Unable to read NumControlEnable from comand line. {0}Error: {1}", Environment.NewLine, ex.Message));
                }
            }
            else
            {
                var standardConfigPaths = new string[0];
                try
                {
                    standardConfigPaths = Directory.GetFiles(
                    Directory.GetCurrentDirectory(), "*.gear", SearchOption.TopDirectoryOnly);
                    for (int i = 0; i < standardConfigPaths.Length; i++)
                    {
                        NumControlEnable = ReadNumControlFromFile(standardConfigPaths[i]);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(String.Format("Unable to read NumControlEnable from standard path. {0}Error: {1}", Environment.NewLine, ex.Message));
                }
            }
        }
        private static bool ReadNumControlFromFile(string path)
        {
            bool NumControl;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path);
            XmlNode s = xmlDoc.DocumentElement;
            string atr = s.Attributes["NumControlEnable"].Value;
            NumControl = atr.ToUpper().Equals("TRUE");
            return NumControl;
        }

    }
}
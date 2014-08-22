using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                        NumControlEnable = ReadNumControlFromFile(args[i]);
                    }
                }
                catch
                {
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
                catch
                {
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
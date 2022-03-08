using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace PigeonDecoder
{
    public class Util
    {
        public static string Language { get; set; }
        public static void ChangeLanguage(string p)
        {
            File.WriteAllText(Path.Combine(FileSystem.AppDataDirectory, "Lang.txt"), p);
        }

        public static string[] DeserialiseEmail(string param)
        {
            string raw = param.Remove(0, 10);
            string[] splitter = Split(';', raw);
            string mailid = splitter[0];
            string nextraw = splitter[1];
            string[] splitter2 = Split(';', nextraw);
            string subject = splitter2[0].Remove(0, 4);
            string body = splitter2[1].Remove(0, 5);
            body = Split(';', body)[0];
            string[] output = new string[]
            {
                mailid,
                subject,
                body
            };
            return output;
        }

        public static string[] DeserialiseSms(string param)
        {
            string raw = param.Remove(0, 6);
            string[] output = Split(':', raw);
            return output;
        }

        public static string[] DeserialiseWIFI(string param)
        {
            param = param.Remove(0, 7);
            string[] raw = Split(';', param);
            string SSID = raw[0].Replace(";", "");
            string Password = Split(';', raw[1])[1].Replace("P:", "").Replace(";", "");
            return new string[] { SSID, Password };
        }

        private static string[] Split(char key, string param)
        {
            char[] spearator = { key, ' ' };
            int count = 2;
            string[] strlist = param.Split(spearator, count, StringSplitOptions.None);
            return strlist;
        }

        public static string SerialiseEmail(string maidid, string sub, string msg)
        {
            string startprefix = "MATMSG:TO:";
            string subprefix = "SUB:";
            string bodyprefix = "BODY:";
            string endprefix = ";";
            string Code = startprefix + maidid + endprefix + subprefix + sub + endprefix + bodyprefix + endprefix + endprefix;
            return Code;
        }

        public static string SerialiseSMS(string phno, string msg)
        {
            string startprefix = "SMSTO:";
            string splitprefix = ":";
            string Code = startprefix + phno + splitprefix + msg;
            return Code;
        }

        public static string SerialiseWifi(string SSID, string password)
        {
            string startprefix = "WIFI:S:";
            string typeprefix = "T:WPA";
            string passwordprefix = "P:";
            string endprefix = ";";
            string Code = startprefix + SSID + endprefix + typeprefix + endprefix + passwordprefix + password + endprefix + endprefix;
            return Code;
        }

        public static void CreateCode(string param, string CodeType)
        {
            MainPage.NavigateCommand.Execute(new CodePage(param, CodeType));
        }

        public static bool ContainsLetter(string s)
        {
            if (String.IsNullOrEmpty(s))
                return false;

            return Regex.IsMatch(s, @"^[a-zA-Z]+$");
        }
    }
}

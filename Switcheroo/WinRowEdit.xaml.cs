using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Switcheroo
{
    /// <summary>
    /// WinRowEdit.xaml 的交互逻辑
    /// </summary>
    public partial class WinRowEdit : Window
    {
        private AppWindowViewModel mWin;

        public WinRowEdit()
        {
            InitializeComponent();
        }

        internal void SetData(AppWindowViewModel win)
        {
            mWin = win;
            TBoxName.Text = mWin.ProcessTitle;
            TBoxHotkey.Text = mWin.OpenHotKey;
            TBoxExePath.Text = mWin.ExePath;
        }

        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            SetOpenHotKey(mWin.ProcessTitle,TBoxHotkey.Text);
            SetExePath(mWin.ProcessTitle, TBoxExePath.Text);
            Close();
        }

        private void SetExePath(string processname, string exepath)
        {
            SetMyAttribute(Properties.Settings.Default.OpenWithHotKey, 2, processname, exepath);
        }

        public static String GetExePath(string processname)
        {
            return GetMyAttribute(Properties.Settings.Default.ExePath, processname,0);
        }
        public static String GetOpenHotKey(string processname)
        {            
            return GetMyAttribute(Properties.Settings.Default.OpenWithHotKey, processname,0);
        }

        public static String GetOpenHotKeyProcessName(string hotkey)
        {
            return GetMyAttribute(Properties.Settings.Default.OpenWithHotKey, hotkey, 1);
        }
        private static string GetMyAttribute(string liststr,string processname,int dataindex)
        {
            foreach (var strarry in liststr.Split(';'))
            {
                var data = strarry.Split(new string[] { "--" }, StringSplitOptions.None);
                if (data.Length > 1)
                {
                    if (data[dataindex].Equals(processname))
                    {
                        return data[1 - dataindex];
                    }
                }
            }
            return "";
        }
        public static void SetOpenHotKey(string processname,string hotkey)
        {
            SetMyAttribute(Properties.Settings.Default.OpenWithHotKey, 1, processname, hotkey);
        }
        public static void SetMyAttribute(string liststr,int type,string processname,string attr)
        {            
            // 首先找到并删除
            var newstr = "";
            foreach (var strarry in liststr.Split(';'))
            {
                var data = strarry.Split(new string[] { "--" }, StringSplitOptions.None);
                if (data.Length > 1)
                {
                    if (data[0].Equals(processname))
                    {
                        continue;
                    }
                    newstr += strarry + ";";
                }
            }
            newstr += processname + "--" + attr;
            switch (type)
            {
                case 1:
                    Properties.Settings.Default.OpenWithHotKey = newstr;
                    break;
                case 2:
                    Properties.Settings.Default.ExePath = newstr;
                    break;
            }
            
            Properties.Settings.Default.Save();
        }
        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        internal static string MyParseKeyString(Key systemKey)
        {
            if (systemKey != Key.Enter)
                return  systemKey.ToString();
            return "";
        }
    }
}

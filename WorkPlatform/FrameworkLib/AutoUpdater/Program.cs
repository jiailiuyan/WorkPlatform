using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.IO;
using System.Xml.Linq;

namespace WorkPlatform.AutoUpdater
{
    class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {

            //WorkPlatform.AutoUpdater.Updater.CheckUpdateStatus();

            if (args.Length == 0)
            {
                //Ezhu.AutoUpdater.App app = new Ezhu.AutoUpdater.App();
                //UI.DownFileProcess downUI = new UI.DownFileProcess("", "", "", "", "", "");
                //app.Run(downUI);


                //MessageBox.Show("无参数");
                //UI.DownFileProcess downUI = new UI.DownFileProcess();

                //Ezhu.AutoUpdater.App app = new Ezhu.AutoUpdater.App();
                //app.Run(downUI);




                //string url = @"D:/update.xml";// 
                //FileStream stream = new FileStream(url, FileMode.Open);

                //XDocument xDoc = XDocument.Load(stream);
                //UpdateInfo updateInfo = new UpdateInfo();
                //XElement root = xDoc.Element("UpdateInfo");
                //updateInfo.AppName = root.Element("AppName").Value;
                //updateInfo.AppVersion = root.Element("AppVersion") == null || string.IsNullOrEmpty(root.Element("AppVersion").Value) ? null : new Version(root.Element("AppVersion").Value);
                //updateInfo.RequiredMinVersion = root.Element("RequiredMinVersion") == null || string.IsNullOrEmpty(root.Element("RequiredMinVersion").Value) ? null : new Version(root.Element("RequiredMinVersion").Value);
                //updateInfo.Desc = root.Element("Desc").Value;
                //updateInfo.MD5 = Guid.NewGuid();

                //Ezhu.AutoUpdater.App app = new Ezhu.AutoUpdater.App();
                //UI.DownFileProcess downUI = new UI.DownFileProcess("", "", "", "", "", "");
                //app.Run(downUI);

                return;
            }
            else if (args[0] == "update")
            {
                try
                {
                    //MessageBox.Show("外部更新");
                    string callExeName = args[1];
                    string updateFileDir = args[2];
                    string appDir = args[3];
                    string appName = args[4];
                    string appVersion = args[5];
                    string desc = args[6];

                    //Check If Have New Vision
                    WorkPlatform.AutoUpdater.App app = new WorkPlatform.AutoUpdater.App();
                    UI.DownFileProcess downUI = new UI.DownFileProcess(callExeName, updateFileDir, appDir, appName, appVersion, desc) { WindowStartupLocation = WindowStartupLocation.CenterScreen };
                    app.Run(downUI);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}

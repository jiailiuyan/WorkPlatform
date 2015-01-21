using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace ServerPlatform
{
    public class StartWork
    {

        public static System.Threading.Mutex Instance = null;

        /// <summary>
        /// Application Entry Point.
        /// </summary>
        [System.STAThreadAttribute()]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public static void Main()
        {
            var name = Application.ResourceAssembly.ManifestModule.Name;
            bool isexsit;
            Instance = new System.Threading.Mutex(true, name, out isexsit);
            if (isexsit)
            {
                PrismServer.App app = new PrismServer.App();
                app.InitializeComponent();
                app.Run();
                Instance.ReleaseMutex();
            }
            else
            {


            }
        }

    }
}

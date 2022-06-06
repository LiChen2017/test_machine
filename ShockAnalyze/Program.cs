using System;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Common;

namespace ShockAnalyze
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //程序只能打开一个
            bool blnIsRunning;
            Mutex mutexApp = new Mutex(false, Assembly.GetExecutingAssembly().FullName, out   blnIsRunning);
            if (!blnIsRunning)
            {
                MessageDxUtil.ShowWarning("程序已经运行");
                return;
            }
            else
            {
                //Check();
                Application.Run(new MainForm());
            }
        }

        private static void Check()
        {
            if (RegistFileHelper.ExistRegistInfofile() == true)
            {
                string info = RegistFileHelper.ReadRegistFile();
                if (UTool.CheckRegist(info) == true)
                {
                    Application.Run(new MainForm());
                }
            }
            else
            {
                MessageBox.Show("缺少系统文件，请与管理员联系");
            }
        }

    }
}

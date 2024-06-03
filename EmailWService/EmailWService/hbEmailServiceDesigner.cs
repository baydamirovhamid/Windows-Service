using System;
using System.IO;
using System.ServiceProcess;
using System.Timers;

namespace EmailWService
{
    public partial class hbEmailServiceDesigner : ServiceBase
    {
        Timer timer = new Timer();  
        public hbEmailServiceDesigner()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            //  base.OnStart(args);

            WriteLog("StartServer");
            timer.Elapsed += new ElapsedEventHandler(OnElapsed);
            timer.Interval = 3000;
            timer.Enabled = true;
        }

        protected override void OnStop()
        {
            WriteLog("End Service...");
        }

        private void OnElapsed(object sender, ElapsedEventArgs e)
        {
            WriteLog("Work");
        }

        public void WriteLog(string log)
        {
            log = $"Time: {DateTime.UtcNow.AddHours(4)}" + log;
            string folderDir = AppDomain.CurrentDomain.BaseDirectory + "/Logs";
            if(Directory.Exists(folderDir)) Directory.CreateDirectory(folderDir);

            string fileDir = folderDir + "/" + "log.txt";
            if (!File.Exists(fileDir))
            {
                using (StreamWriter sw = File.AppendText(fileDir))
                    sw.WriteLine(log);
            }
            else
            {
                using (StreamWriter sw = File.CreateText(fileDir))
                    sw.WriteLine(log);
            }


        }
      
    }
}

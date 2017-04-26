﻿using Logging.Server.Alerting;
using System;
using System.Threading;

namespace Logging.Server
{
    public class BackgroundTask
    {
        public static void InitTasks()
        {
            var t = new Thread(CheckAlerting);
            t.IsBackground = false;
            t.Start();
        }

        private static void CheckAlerting()
        {
            while (true)
            {
                try
                {
                    BaseAlerting alert = new AppErrorthAlerting();
                    alert.Alert();
                }
                catch (ThreadAbortException tae)
                {
                    Thread.ResetAbort();
                    FileLogger.Log(tae);
                }
                catch (Exception ex)
                {
                    FileLogger.Log(ex);
                }
                finally
                {
                    Thread.Sleep(1000 * 60);
                }
            }
        }
    }
}
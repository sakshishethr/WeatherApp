using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WeatherApp.Data
{
    public class Threads
    {

        public static Thread StopThread(Thread thread)
        {
            thread.Abort();
            return thread;
        }

        public static Thread StartThread(Thread thread)
        {
            thread.Start();
            return thread;
        }

        public static ParameterizedThreadStart StartThreadTask(ParameterizedThreadStart thread)
        {
            return thread;
        }

        public static Thread StartThreadWithJoin(Thread thread)
        {
            thread.Start();
            thread.Join();
            return thread;
        }

        public static Thread ThreadWait(Thread thread,int seconds)
        {
            Thread.Sleep(seconds*1000);
            return thread;
        }
    }
}

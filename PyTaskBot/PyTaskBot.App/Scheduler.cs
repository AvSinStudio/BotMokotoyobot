using System;
using System.Timers;

namespace PyTaskBot.App
{
    public class Scheduler
    {
        public Scheduler(TimeSpan interval, ElapsedEventHandler elapsed)
        {
            var timeChecker = new Timer(interval.TotalMilliseconds);
            timeChecker.Elapsed += elapsed;
            timeChecker.Enabled = true;
        }
    }
}
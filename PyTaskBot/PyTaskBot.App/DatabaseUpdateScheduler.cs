using System;
using System.Timers;

namespace PyTaskBot.App
{
    public class Scheduler<T>
    {
        public Scheduler(TimeSpan ts, ElapsedEventHandler elapsed)
        {
            var timeChecker = new Timer(ts.TotalMilliseconds);
            timeChecker.Elapsed += elapsed;
            timeChecker.Enabled = true;
        }
    }
}
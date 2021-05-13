using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmptyWebApplicationASP.NET.Services
{

    public interface ITimer
    {
        string Time { get; }
    }
    public class Timer : ITimer
    {
        public string Time { get; }
        public Timer()
        {
            Time = DateTime.Now.ToString("hh:mm:ss");
        }
    }
    public class TimeService
    {
        private ITimer _timer;
        public TimeService(ITimer timer)
        {
            _timer = timer;
        }
        public string GetTime()
        {
            return _timer.Time;
        }
    }
}

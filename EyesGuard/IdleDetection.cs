using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EyesGuard
{
    public enum IdleDetectorState
    {
        Running, Stopped
    }

    public class IdleDetector
    {
        [DllImport("User32.dll")]
        private static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

        public long TimeDiff { get; set; }

        public IdleDetectorState State { get; private set; } = IdleDetectorState.Stopped;

        public long IdleThreshold { get; set; } = 10;

        public bool SystemIsIdle
        {
            get {
                return TimeDiff >= IdleThreshold;
            }
        }

        public IdleDetector()
        {
            
        }

        internal struct LASTINPUTINFO
        {
            public uint cbSize;

            public uint dwTime;
        }

        public async void Start()
        {
            LASTINPUTINFO lastInPut = new LASTINPUTINFO();
            lastInPut.cbSize = (uint)Marshal.SizeOf(lastInPut);
            State = IdleDetectorState.Running;

            while (true)
            {


                GetLastInputInfo(ref lastInPut);
                var diff = (Environment.TickCount - lastInPut.dwTime) / 1000;

                Console.WriteLine(
                    (diff == 0) ? "User is working with computer!" :
                    $"User is not working with computer for {diff} seconds..."
                    );

                await Task.Delay(1000);
            }

        }

    }
}

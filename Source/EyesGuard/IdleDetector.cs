using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EyesGuard
{
    public enum IdleDetectorState
    {
        Running, Stopped
    }

    public class IdleStateChangedEventArgs : EventArgs
    {
        public bool IdleState { get; set; }
        public long Duration { get; set; }
    }

    public class IdleDetector
    {
        [DllImport("User32.dll")]
        private static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

        public IdleDetectorState State { get; private set; } = IdleDetectorState.Stopped;

        private bool previousSystemInputIdle = false;

        public long IdleDuration { get; private set; }

        public long IdleThreshold { get; set; } = 10;

        public bool DeferUpdate { get; set; } = false;

        private int UpdateInterval => (DeferUpdate) ? 5000 : 1000;

        public bool IsSystemIdle() => IdleDuration > IdleThreshold;

        private bool cancelRequested = false;

        public bool EnableRaisingEvents { get; set; } = false;

        public event EventHandler<IdleStateChangedEventArgs> IdleStateChanged;

        internal struct LASTINPUTINFO
        {
            public uint cbSize;

            public uint dwTime;
        }

        public async Task RequestStart()
        {
            LASTINPUTINFO lastInPut = new LASTINPUTINFO();
            lastInPut.cbSize = (uint)Marshal.SizeOf(lastInPut);
            State = IdleDetectorState.Running;
            int lastTriggerTime = Environment.TickCount;

            while (!cancelRequested)
            {
                try
                {
                    GetLastInputInfo(ref lastInPut);
                }
                catch
                {
                    EnableRaisingEvents = false;
                    break;
                }
                IdleDuration = (Environment.TickCount - lastInPut.dwTime) / 1000;

                if (EnableRaisingEvents && (IsSystemIdle() != previousSystemInputIdle))
                {
                    IdleStateChangedEventArgs lastEventStateArgs = new IdleStateChangedEventArgs()
                    {
                        IdleState = previousSystemInputIdle,
                        Duration = (Environment.TickCount - lastTriggerTime) / 1000
                    };
                    lastTriggerTime = Environment.TickCount;
                    previousSystemInputIdle = IsSystemIdle();
                    IdleStateChanged?.Invoke(null, lastEventStateArgs);
                }
                await Task.Delay(UpdateInterval);
            }

            cancelRequested = false;
            State = IdleDetectorState.Stopped;
        }

        public async Task RequestCancel()
        {
            cancelRequested = true;
            await Task.Run(() =>
            {
                while (State == IdleDetectorState.Running) { }
            });
        }
    }
}

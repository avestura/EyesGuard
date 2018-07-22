using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using static EyesGuard.App;

namespace EyesGuard.Configurations
{
    public partial class Configuration
    {

        #region Config :: Fields :: Internal
        GuardStates _protectionState = GuardStates.Protecting;
        bool _keyTimeVisible = true;
        bool runAtStartup = false;
        bool _systemIdleDetectionEnabled = false;
        #endregion

        #region Config :: Fields :: Public Properties

        public GuardStates ProtectionState
        {
            get { return _protectionState; }
            set
            {
                _protectionState = value;
                UpdateTimeHandlers();
                UpdateLongShortVisibility();
                UpdateTaskbarIcon();
                UpdateIdleActions();

            }
        }

        [XmlIgnore]
        public TimeSpan ShortBreakGap { get; set; } = new TimeSpan(0, 20, 0);

        [XmlIgnore]
        public TimeSpan LongBreakGap { get; set; } = new TimeSpan(1, 0, 0);

        [XmlIgnore]
        public TimeSpan ShortBreakDuration { get; set; } = new TimeSpan(0, 0, 15);

        [XmlIgnore]
        public TimeSpan LongBreakDuration { get; set; } = new TimeSpan(0, 5, 0);

        public string ShortBreakGapString
        {
            get { return ShortBreakGap.ToString(); }
            set { ShortBreakGap = TimeSpan.Parse(value); }
        }
        public string LongBreakGapString
        {
            get { return LongBreakGap.ToString(); }
            set { LongBreakGap = TimeSpan.Parse(value); }
        }
        public string ShortBreakDurationString
        {
            get { return ShortBreakDuration.ToString(); }
            set { ShortBreakDuration = TimeSpan.Parse(value); }
        }
        public string LongBreakDurationString
        {
            get { return LongBreakDuration.ToString(); }
            set { LongBreakDuration = TimeSpan.Parse(value); }
        }

        public bool AlertBeforeLongBreak { get; set; } = true;
        public bool TrayNotificationSaidBefore { get; set; } = false;
        public bool RunMinimized { get; set; } = false;
        public bool ForceUserToBreak { get; set; } = false;
        public bool OnlyOneShortBreak { get; set; } = false;
        public bool SaveStats { get; set; } = true;
        public bool RunAtStartUp { get { return runAtStartup; } set { runAtStartup = value; } }
        public long ShortBreaksCompleted { get; set; } = 0;
        public long LongBreaksCompleted { get; set; } = 0;
        public long LongBreaksFailed { get; set; } = 0;
        public long PauseCount { get; set; } = 0;
        public long StopCount { get; set; } = 0;
        public bool KeyTimesVisible { get { return _keyTimeVisible; } set { _keyTimeVisible = value; UpdateKeyTimeVisible(); } }
        public ScalingType DpiScalingType { get; set; } = ScalingType.UseWindowsDPIScaling;
        public ScalingSize DpiScalingFactor { get; set; } = ScalingSize.Unset;

        public bool SystemIdleDetectionEnabled
        {
            get { return _systemIdleDetectionEnabled; }
            set
            {
                _systemIdleDetectionEnabled = value;

                if (SystemIdleDetector != null)
                {
                    if (value && SystemIdleDetector.State == IdleDetectorState.Stopped)
                    {
                        _ = SystemIdleDetector.RequestStart();
                    }
                    else if (!value && SystemIdleDetector.State == IdleDetectorState.Running)
                    {
                        _ = SystemIdleDetector.RequestCancel();
                    }
                }

            }
        }
        #endregion
    }
}

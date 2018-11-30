using System;

namespace EyesGuard
{
    public partial class App
    {
        public static void PauseProtection(TimeSpan pauseDuration)
        {
            if (CheckIfResting()) return;

            if (Configuration.SaveStats)
            {
                UpdateIntruptOfStats(GuardStates.PausedProtecting);
            }
            PauseProtectionSpan = pauseDuration;
            UpdatePauseTimeString();
            IsProtectionPaused = true;
            PauseHandler.Start();

            CurrentMainPage.ProtectionState = GuardStates.PausedProtecting;
        }

        public static void ResumeProtection()
        {
            PauseProtectionSpan = TimeSpan.Zero;
            IsProtectionPaused = false;
            PauseHandler.Stop();
            CurrentMainPage.ProtectionState = GuardStates.Protecting;
        }
    }
}

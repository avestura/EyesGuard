using EyesGuard.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;

namespace EyesGuard.Localization
{

    public class LocalizedEnvironment
    {
        public Meta Meta { get; set; }
        public Translation Translation { get; set; }
    }

    public class MetaPhantom
    {
        public Meta Meta { get; set; }
    }

    public class Meta
    {
        public Translator[] Translators { get; set; }

        [JsonIgnore]
        public CultureInfo CurrentCulture { get; set; }
    }

    public class Translator
    {
        public string Name { get; set; }
        public string GitHubUsername { get; set; }
        public string Website { get; set; }
        public string Notes { get; set; }
    }

    public class Translation
    {
        public Application Application { get; set; }
        public EyesGuard EyesGuard { get; set; }
        public Shellextensions ShellExtensions { get; set; }
    }

    public class Application
    {
        public string OSTitle { get; set; }
        public string HeaderTitle { get; set; }
        public string LoginWarning { get; set; }
        public string HelpPageText { get; set; }
        public string DoNotRunMultipleInstances { get; set; }

        public ApplicationNotifications Notifications { get; set; }

        public Resources Resources { get; set; }
        public About About { get; set; }
        public Donate Donate { get; set; }
        public Feedback Feedback { get; set; }
    }

    public class ApplicationNotifications
    {
        public FirstLaunch FirstLaunch { get; set; }
    }

    public class FirstLaunch
    {
        public string Title { get; set; }
        public string Message { get; set; }
    }

    public class Resources
    {
        public string PageTitle { get; set; }
        public ResourcesContent Content { get; set; }
    }

    public class ResourcesContent
    {
        public string Icons { get; set; }
        public string UIKit { get; set; }
    }

    public class About
    {
        public string PageTitle { get; set; }
        public AboutContent Content { get; set; }
    }

    public class AboutContent
    {
        public string InnerTitle { get; set; }
        public string PublisherInfo { get; set; }
        public string Repo { get; set; }
    }

    public class Donate
    {
        public string PageTitle { get; set; }
        public BeforeDonation BeforeDonation { get; set; }
        public DonationButtonClicked DonationButtonClicked { get; set; }
    }

    public class BeforeDonation
    {
        public BeforeDonationContent Content { get; set; }
    }

    public class BeforeDonationContent
    {
        public string InnerTitle { get; set; }
        public string Message { get; set; }
        public string ButtonText { get; set; }
    }

    public class DonationButtonClicked
    {
        public DonationButtonClickedContent Content { get; set; }
    }

    public class DonationButtonClickedContent
    {
        public string InnerTitle { get; set; }
        public string Thanks { get; set; }
        public string Redirect { get; set; }
        public string FeedbackNotice { get; set; }
    }

    public class Feedback
    {
        public string PageTitle { get; set; }
        public FeedbackContent Content { get; set; }
    }

    public class FeedbackContent
    {
        public string InnerTitle { get; set; }
        public string Message { get; set; }
        public string ButtonText { get; set; }
    }

    public class EyesGuard
    {
        public ControlPanel ControlPanel { get; set; }
        public GuardStatus GuardStatus { get; set; }
        public TimeRemaining TimeRemaining { get; set; }
        public CustomPause CustomPause { get; set; }
        public WarningPage WarningPage { get; set; }
        public string OperationFailed { get; set; }
        public string CheckInput { get; set; }
        public TimeManipulation TimeManipulation { get; set; }
        public Settings Settings { get; set; }
        public string Resting { get; set; }
        public string Waiting { get; set; }
        public string[] ShortMessageSuggestions { get; set; }
        public string LongWindowMessage { get; set; }
        public string LongWindowCancelButton { get; set; }
        public EyesGuardNotifications Notifications { get; set; }
        public string WaitUnitlEndOfBreak { get; set; }
        public string LongBreakTimeRemaining { get; set; }
        public StatsPage StatsPage { get; set; }
        public AlertPages AlertPages { get; set; }
        public HeaderMenu HeaderMenu { get; set; }
        public UserManager UserManager { get; set; }
    }

    public class ControlPanel
    {
        public string Title { get; set; }
    }

    public class GuardStatus
    {
        public string Running { get; set; }
        public string Paused { get; set; }
        public string Stopped { get; set; }
        public string Idle { get; set; }
    }

    public class TimeRemaining
    {
        public ShortBreak ShortBreak { get; set; }
        public LongBreak LongBreak { get; set; }
        public PauseTime PauseTime { get; set; }
    }

    public class ShortBreak
    {
        public string Seconds { get; set; }
        public string Minutes { get; set; }
    }

    public class LongBreak
    {
        public string Seconds { get; set; }
        public string Minutes { get; set; }
    }

    public class PauseTime
    {
        public string Seconds { get; set; }
        public string Minutes { get; set; }
    }

    public class CustomPause
    {
        public string PageTitle { get; set; }
        public string Message { get; set; }
        public string ButtonText { get; set; }
    }

    public class WarningPage
    {
        public string ButtonText { get; set; }
    }

    public class TimeManipulation
    {
        public string HoursLimit { get; set; }
        public string MinutesLimit { get; set; }
        public string SecondsLimit { get; set; }
        public string SGapMorethanLGap { get; set; }
        public string SDurationMorethanLDuration { get; set; }
        public string SDurationTooHigh { get; set; }
        public string LDurationTooHigh { get; set; }
        public string NotEnoughGapBetweenLandS { get; set; }
        public string ShortGapLow { get; set; }
        public string LongGapLow { get; set; }
        public string ChooseLargerTime { get; set; }
    }

    public class Settings
    {
        public string Title { get; set; }
        public string SavedSuccessfully { get; set; }
        public string AreYouSure { get; set; }
        public string LanguageSection { get; set; }
        public TimeSeparators TimeSeparators { get; set; }
        public string SaveSettings { get; set; }
        public TimeSettings TimeSettings { get; set; }
        public UserSettings UserSettings { get; set; }
        public StatsSettings StatsSettings { get; set; }
        public LanguageSettings LanguageSettings { get; set; }
    }

    public class TimeSeparators
    {
        public string Hour { get; set; }
        public string Minutes { get; set; }
        public string Second { get; set; }
    }

    public class TimeSettings
    {
        public string Title { get; set; }
        public string GapBetweenTwoShortBreak { get; set; }
        public string GapBetweenTwoLongBreak { get; set; }
        public string ShortBreakDuration { get; set; }
        public string LongBreakDuration { get; set; }
    }

    public class StatsSettings
    {
        public string Title { get; set; }
        public string YesDeleteData { get; set; }
        public string NoKeepData { get; set; }
        public string StatsDeleted { get; set; }
        public string StoreStats { get; set; }
        public string StoreStatsToolTip { get; set; }
        public string ClearStatsText { get; set; }
        public string ClearStats { get; set; }
    }

    public class UserSettings
    {
        public string Title { get; set; }
        public string ForceUser { get; set; }
        public string ForceUserToolTip { get; set; }
        public string OnlyOneShortBreak { get; set; }
        public string OnlyOneShortBreakToolTip { get; set; }
        public string StartupApplication { get; set; }
        public string StartupApplicationToolTip { get; set; }
        public string AlertBeforeLongBreak { get; set; }
        public string AlertBeforeLongBreakToolTip { get; set; }
        public string SystemIdle { get; set; }
        public string[] SystemIdleToolTip { get; set; }

        [JsonIgnore]
        public string SystemIdleToolTipJoined => string.Join("\n", SystemIdleToolTip);
    }

    public class LanguageSettings
    {
        public string Title { get; set; }
        public string SelectLanguage { get; set; }
        public string MaintainMessage { get; set; }
        public string RestartRequired { get; set; }
        public string MessagesFromLangFile { get; set; }
        public string MessagesFromLangFileToolTip { get; set; }
        public string AddMessage { get; set; }
        public string RemoveMessage { get; set; }
        public string NoAccount { get; set; }
        public string NoWebsite { get; set; }
        public string NoNotes { get; set; }
        public string EnterNewMessage { get; set; }
        public string Submit { get; set; }
    }

    public class EyesGuardNotifications
    {
        public LongBreakAlert LongBreakAlert { get; set; }
    }

    public class LongBreakAlert
    {
        public string Title { get; set; }
        public string Message { get; set; }
    }

    public class StatsPage
    {
        public string Title { get; set; }
        public string ShortBreak { get; set; }
        public string LongBreak { get; set; }
        public string Pauses { get; set; }
        public string ShortCount { get; set; }
        public string LongCompletedCount { get; set; }
        public string LongFailedCount { get; set; }
        public string PauseCount { get; set; }
        public string StopCount { get; set; }
    }

    public class AlertPages
    {
        public Titles Titles { get; set; }
    }

    public class HeaderMenu
    {
        public Eyesguard EyesGuard { get; set; }
        public Tools Tools { get; set; }
        public Breaks Breaks { get; set; }
        public View View { get; set; }
        public Help Help { get; set; }
    }

    public class UserManager
    {
        public string User { get; set; }
        public string NotLoggedIn { get; set; }
        public string NoTitle { get; set; }
        public string Login { get; set; }
        public string Profile { get; set; }
        public string Logout { get; set; }
    }

    public class Eyesguard
    {
        public string Header { get; set; }
        public string MainMenu { get; set; }
        public string Hide { get; set; }
        public string Exit { get; set; }
    }

    public class Tools
    {
        public string Header { get; set; }
        public string Stats { get; set; }
        public string Settings { get; set; }
    }

    public class Breaks
    {
        public string Header { get; set; }
        public string GoShort { get; set; }
        public string GoLong { get; set; }
    }

    public class View
    {
        public string Header { get; set; }
        public string KeyTimes { get; set; }
    }

    public class Help
    {
        public string Header { get; set; }
        public string EyesGuardHelp { get; set; }
        public string Resources { get; set; }
        public string SendFeedback { get; set; }
        public string Donate { get; set; }
        public string About { get; set; }
    }

    public class Titles
    {
        public string Attention { get; set; }
        public string Successful { get; set; }
        public string Error { get; set; }
        public string Info { get; set; }
        public string AboutSoftware { get; set; }
        public string Donate { get; set; }
    }

    public class Shellextensions
    {
        public TaskbarIcon TaskbarIcon { get; set; }
    }

    public class TaskbarIcon
    {
        public string Protected { get; set; }
        public string NotProtected { get; set; }
        public string PausedProtected { get; set; }
        public Menu Menu { get; set; }
    }

    public class Menu
    {
        public string PauseFor { get; set; }
        public string PausedFor { get; set; }
        public string NextShortBreak { get; set; }
        public string NextLongBreak { get; set; }
        public string ShowMainMenu { get; set; }
        public string StartProtection { get; set; }
        public string StopProtection { get; set; }
        public string FiveMins { get; set; }
        public string TenMins { get; set; }
        public string ThirtyMins { get; set; }
        public string OneHour { get; set; }
        public string TwoHours { get; set; }
        public string Custom { get; set; }
        public string Settings { get; set; }
        public string Exit { get; set; }
    }

}

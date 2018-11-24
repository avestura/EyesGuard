using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyesGuard.Localization
{
    public static class LanguageLoader
    {
        public static string[] SupportedCultures =
            CultureInfo.GetCultures(CultureTypes.AllCultures).Select(x => x.Name).ToArray();

        public static bool IsCultureSupported(this string locale) => SupportedCultures.Contains(locale);

        public static bool IsCultureSupportedAndExists(this string locale) =>
            LocaleFileExists(locale) && IsCultureSupported(locale);

        public static string GetLocalePath(this string locale) =>
            Path.Combine(
                  LocalizationFilesPath,
                  $"{locale}.json"
              );

        public static string GetLocaleContent(this string locale) =>
            File.ReadAllText(GetLocalePath(locale));

        public static bool LocaleFileExists(this string locale) =>
          File.Exists(
              Path.Combine(
                  LocalizationFilesPath,
                  $"{locale}.json"
              )
          );

        public static LocalizedEnvironment CreateEnvironment(string locale)
        {
            try
            {
                var path = locale.GetLocalePath();

                if (locale.IsCultureSupportedAndExists())
                {
                    var content = locale.GetLocaleContent();

                    var localeEnv = JsonConvert.DeserializeObject<LocalizedEnvironment>(content);

                    localeEnv.Meta.CurrentCulture = new CultureInfo(locale);

                    return localeEnv;
                }
            }
            catch{}
            return null;
        }

        public static MetaPhantom LoadMeta(string locale)
        {
            try
            {
                var path = locale.GetLocalePath();

                if (locale.IsCultureSupportedAndExists())
                {
                    var content = locale.GetLocaleContent();

                    var phantom = JsonConvert.DeserializeObject<MetaPhantom>(content);

                    return phantom;
                }
            }
            catch { }
            return null;
        }

        public static readonly string LocalizationFilesPath =
         Path.Combine(
             AppDomain.CurrentDomain.BaseDirectory,
             "Localization",
             "Languages"
         );

        public static LocalizedEnvironment DefaultLocale { get; } = CreateEnvironment("en-US");

        public static string[] GetLocaleFiles() =>
            Directory.GetFiles(LocalizationFilesPath, "*.json", SearchOption.TopDirectoryOnly)
            .Select(x => Path.GetFileNameWithoutExtension(x))
            .ToArray();

        public static Lazy<ObservableCollection<LanguageHolder>> LanguagesBriefData { get; set; }
         = new Lazy<ObservableCollection<LanguageHolder>>(() =>
         {
             var items = from langCode in LanguageLoader.GetLocaleFiles()
                         where langCode.IsCultureSupported()
                         let culture = new CultureInfo(langCode)
                         select new LanguageHolder
                         {
                             Name = culture.Name,
                             NativeName = culture.NativeName
                         };

             return new ObservableCollection<LanguageHolder>(items);
         });

        public class LanguageHolder
        {
            public string Name { get; set; }
            public string NativeName { get; set; }
        }
    }
}

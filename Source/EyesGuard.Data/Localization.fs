namespace EyesGuard.Data

open System.Globalization
open FSharp.Configuration
open System.Collections.ObjectModel
open System.Runtime.InteropServices

module LanguageLoader =

    open System
    open System.IO
    open CompilerExtensions

    let defaultLocale = "en-US"
    [<Literal>]
    let defaultLocaleAddress = "Languages/en-US.yml"

    type LocalizedEnvironment = YamlConfig<defaultLocaleAddress>

    let localizationDirectoryPath designMode =
        if designMode then Path.Combine [| CompilerInfo.CompilerDirectory; "Languages" |]
        else Path.Combine [|AppDomain.CurrentDomain.BaseDirectory; "Languages" |]

    let supportedCultures =
        CultureInfo.GetCultures(CultureTypes.AllCultures)
        |> Array.map (fun x -> x.Name)

    let getLocalePath locale designMode = Path.Combine [|localizationDirectoryPath designMode; sprintf "%s.yml" locale|]

    let localeFileExists locale designMode =
        getLocalePath locale designMode
        |> File.Exists

    let getLocaleContent locale designMode =
        getLocalePath locale designMode |> File.ReadAllText

    let isCultureSupported locale = Array.contains locale supportedCultures
    let isCultureSupportedAndExists locale designMode =
        (localeFileExists locale designMode) && (isCultureSupported locale)

    let createEnvironment locale designmode =
        if isCultureSupportedAndExists locale designmode then
            let lang = LocalizedEnvironment()
            let path = getLocalePath locale designmode
            lang.Load path
            lang
        else
            LocalizedEnvironment()

    let defaultEnvironment = LocalizedEnvironment()

    type LanguageHolder = { Name : string; NativeName : string }

    let localeFiles designMode =
        Directory.GetFiles (localizationDirectoryPath designMode, "*.yml", SearchOption.TopDirectoryOnly)
        |> Array.map Path.GetFileNameWithoutExtension

    let languagesBriefData designMode =
        let items = localeFiles designMode
                    |> Array.filter isCultureSupported
                    |> Array.map CultureInfo
                    |> Array.map (fun x -> {
                        Name = x.Name
                        NativeName = x.NativeName
                    })

        lazy (items |> ObservableCollection<LanguageHolder>)

    // C# Object used to respect .NET conventions
    type FsLanguageLoader() =
        static member DefaultLocale = defaultLocale
        static member CreateEnvironment (locale, [<Optional;DefaultParameterValue(false)>]designMode) = createEnvironment locale designMode
        static member LanguagesBriefData ([<Optional;DefaultParameterValue(false)>]designMode)  = languagesBriefData designMode
        static member DefaultEnvironment = defaultEnvironment
        static member IsCultureSupportedAndExists (locale, [<Optional;DefaultParameterValue(false)>] designMode) = isCultureSupportedAndExists locale designMode

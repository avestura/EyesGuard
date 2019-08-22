Eyes Guard
===
Windows Application for protecting you eyes 

[![BuitlWithDot.Net shield](https://builtwithdot.net/project/41/eyes-guard/badge)](https://builtwithdot.net/project/41/eyes-guard)
[![Build status](https://aryansoftware.visualstudio.com/Eyes%20Guard/_apis/build/status/Eyes%20Guard-.NET%20Desktop-CI)](https://aryansoftware.visualstudio.com/Eyes%20Guard/_build/latest?definitionId=3)

<a href='//www.microsoft.com/store/apps/9PHW0XFKZD7J?ocid=badge'><img width="250" src='https://assets.windowsphone.com/85864462-9c82-451e-9355-a3d5f874397a/English_get-it-from-MS_InvariantCulture_Default.png' alt='English badge'/></a>

## Screenshots
#### Control Panel
![Eyes Guard](Screenshots/Store/main.JPG)
#### Settings
![Eyes Guard Settings](Screenshots/Store/Settings.PNG)
#### Notify Icon
![Eyes Guard NotifyIcon](Screenshots/Store/ContextMenu.png)

---

## Features

- Short break
- Long break
- Timing customization
- Stats
- Pause or Stop Protection
- Customize short-break messages
- Multiple languages support

## Contribute
Prs are very welcome!

You can contribute to software development and/or create translations for the app.

### How to contribute to translating app
**Adding a new Language:**

1. Go to [Languages](https://github.com/0xaryan/EyesGuard/tree/master/Source/EyesGuard.Data/Languages) folder and create a new yaml file with name `{StandardCountryCode}.yml` like `en-US.yml` or `fa-IR.yml`. Here is a [List of country codes](https://azuliadesigns.com/list-net-culture-country-codes/) you can pick.
2. Copy the content of `en-US.yml` to the new created file.
3. In the meta part of yaml edit the `Translators` array. Remove the existing translators in the copied file and just put information of yourself.
4. Translate the `Translation` section of file and make a PR!

**Modifying a translation file:**

1. Go to [Languages](https://github.com/0xaryan/EyesGuard/tree/master/Source/EyesGuard.Data/Languages) and find the language you want to edit using its standard locale.
2. In the meta part of yaml and in the `Translators` array, add yourself as a new translator without removing others from the list.
3. Make your changes in the `Translation` section and make a PR!

**RTL Languages**

For right-to-left languages like Persian, Arabic, etc. It is not needed to explicitly state language layout in the meta section. Simply use a standard country code, and the app automatically detects if it has right-to-left direction or not and changes the design of elements in app.

## Solution Structure

| Project Name   | Language | Description                                  |
|----------------|----------|----------------------------------------------|
| EyesGuard      | C#       | Main Application                             |
| EyesGuard.Data | F#       | Type Provider and Data Access / Translations |
| StorePackage   |          | Used to publish WPF app into Store           |

## Build

You need [Paket Package manager for .NET](https://fsprojects.github.io/Paket) to restore the packages.

```powershell
git clone https://github.com/0xaryan/EyesGuard
cd .\EyesGuard
paket install
cd .\Source\EyesGuard
dotnet run
```
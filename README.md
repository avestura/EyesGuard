# Vista Guard <img align="left" width="120" height="120" src="UWPAssets/150x150.png">




<br>

## What is Eyes Guard?
Eyes Guard is a Windows Application for protecting your eyes. It will help you (also can force you!) to break if you are working with your PC for a long time using configurable times.

## Download

### Windows 10/11 (Official with latest features)

> [!NOTE]  
> EyesGuard will no longer distribute its packages via Microsoft Store. You need to install the 
> package manually using the instruction below:

1. [🟩 Download the latest package form the Releases section](https://github.com/avestura/EyesGuard/releases). 
   - For x64 systems: `EyesGuard.release.x64.zip`
   - For x86 systems: `EyesGuard.release.x86.zip`
2. Unzip the package.
3. Find the certificate file with `.cer` extension and open it.
4. Click `Install Certificate` > `Local Machine` > `Place certificate in following store` > pick `Trusted People`
5. Open the installation package with `.appx` extension and install Eyes Guard.

### Windows 7 (Unofficial Fork, not latest)

[Download from ExplorerOL's GitHub](https://github.com/ExplorerOL/AryanSoftware_EyesGuard/releases/tag/AryanSoftware_EyesGuard_Release_2020_01_14)

## Any Screenshots?

#### Control Panel
![Eyes Guard](Screenshots/Store/main.JPG)
#### Settings
![Eyes Guard Settings](Screenshots/Store/Settings.PNG)
#### Notify Icon
![Eyes Guard NotifyIcon](Screenshots/Store/ContextMenu.png)

---

## What features does it have?

| Feature                  | Implementation State 
|--------------------------|----------------------
| Short Break              | ✔ Implemented       
| Long Break               | ✔ Implemented      
| Timing Customization     | ✔ Implemented      
| Stas                     | ✔ Implemented   
| Pause or Stop protection | ✔ Implemented       
| Windows Tray Integration | ✔ Implemented        
| Auto Start               | ✔ Implemented        
| Customize messages       | ✔ Implemented        
| Multi-language           | ✔ Implemented        


## Can I contribute?

PRs are very welcome!

You can contribute to software development and/or create translations for the app.

### How to contribute to translating app

#### Method 1 : using GitHub
<hr>

Adding a new Language:

1. Go to [Languages](https://github.com/avestura/EyesGuard/tree/master/Source/EyesGuard.Data/Languages) folder and create a new yaml file with name `{StandardCountryCode}.yml` like `en-US.yml` or `fa-IR.yml`. Here is a [List of country codes](CountryCodes.md) you can pick.
2. Copy the content of `en-US.yml` to the new created file.
3. In the meta part of yaml edit the `Translators` array. Remove the existing translators in the copied file and just put information of yourself.
4. Translate the `Translation` section of file and make a PR!

Modifying a translation file:

1. Go to [Languages](https://github.com/avestura/EyesGuard/tree/master/Source/EyesGuard.Data/Languages) and find the language you want to edit using its standard locale.
2. In the meta part of yaml and in the `Translators` array, add yourself as a new translator without removing others from the list.
3. Make your changes in the `Translation` section and make a PR!

#### Method 2 : using Email
<hr>
Use the links mentioned in above method, modify/add translation file, then email it to me: Oxaryan@outlook.com

### 📜 RTL Languages

For right-to-left languages like Persian, Arabic, etc. It is not needed to explicitly state language layout in the meta section. Simply use a standard country code, and the app automatically detects if it has right-to-left direction or not and changes the design of elements in app.

## 📐 Solution Structure

| Project Name   | Language | Description                                  |
|----------------|----------|----------------------------------------------|
| EyesGuard      | C#       | Main Application (at the time of starting project I didn't know F# to write the app entirely in F#)|
| EyesGuard.Data | F#       | Type Provider and Data Access / Translations |
| StorePackage   |          | Used to publish WPF app into Store           |

## 🔨 Build

You need [Paket Package manager for .NET](https://fsprojects.github.io/Paket) to restore the packages.

```powershell
git clone https://github.com/avestura/EyesGuard
cd .\EyesGuard
paket install
cd .\Source\EyesGuard
dotnet run
```

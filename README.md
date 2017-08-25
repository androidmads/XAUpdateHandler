# XAUpdateHandler
Update Checker for Xamarin.Android

##### You can find the Nuget Package [Here](https://www.nuget.org/packages/UpdateHandler/)

# How to Download
Open the Package Manager Console and Paste the following to download the nuget package
```csharp
PM> Install-Package UpdateHandler -Version 1.0.0
```
# How to Use
You can use the following code to use Update Handler in Xamarin.Android
```csharp
...
using UH = Com.Androidmads.UpdateHandler;
...
\\ Check for Newer Version
bool IsNew = UH.Updater.IsNewVersionAvailable(ApplicationContext);
\\ used to get version available in Google Play
string Mversion = UH.Updater.GetMarketVersion(ApplicationContext);
\\ used to get Installed App version
string Aversion = UH.Updater.GetInstalledAppVersion(ApplicationContext);
...
```

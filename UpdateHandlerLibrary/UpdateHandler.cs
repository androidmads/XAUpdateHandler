using System;
using Android.Content;
using Android.OS;
using Android.Net;
using Java.Net;
using Org.Jsoup;

namespace Com.Androidmads.UpdateHandler
{
    public class Updater
    {
        // Check Version Availability
        public static bool IsNewVersionAvailable(Context mContext)
        {
            try
            {
                if (isNetworkAvailable(mContext))
                {
                    CheckUrlExists checkUrlExists = new CheckUrlExists(mContext);
                    bool result = (bool)checkUrlExists.Execute().Get();
                    if (result)
                    {
                        string MarketVersion = (string)new MarketInfo(mContext.PackageName, "softwareVersion").Execute().Get();
                        string AppVersion = (string)new AppInfo(mContext).Execute().Get();
                        var version1 = new Version(MarketVersion);
                        var version2 = new Version(AppVersion);
                        return version1.CompareTo(version2) > 0;
                    }
                }
            }
            catch (Exception e)
            {
            }
            return false;
        }

        public static string GetMarketVersion(Context mContext)
        {
            return (string)new MarketInfo(mContext.PackageName, "softwareVersion").Execute().Get();
        }

        public static string GetInstalledAppVersion(Context mContext)
        {
            return (string)new AppInfo(mContext).Execute().Get();
        }

        // Check Connectivity
        public static bool isNetworkAvailable(Context mContext)
        {
            try
            {
                ConnectivityManager cm = (ConnectivityManager)mContext.GetSystemService(Context.ConnectivityService);
                foreach (var info in cm.GetAllNetworkInfo())
                {
                    if (info == null)
                        continue;

                    if (info.IsConnected)
                        return true;
                }
            }
            catch (Exception e)
            {
            }
            return false;
        }

        // Check Url Exists
        private class CheckUrlExists : AsyncTask
        {
            Context mContext;
            bool IsUrlExist = false;
            public CheckUrlExists(Context context)
            {
                mContext = context;
            }
            protected override Java.Lang.Object DoInBackground(params Java.Lang.Object[] @params)
            {
                try
                {
                    URL url = new URL("https://play.google.com/store/apps/details?id=" + mContext.PackageName);
                    HttpURLConnection huc = (HttpURLConnection)url.OpenConnection();
                    huc.RequestMethod = "GET";
                    huc.Connect();
                    if (isNetworkAvailable(mContext) && huc.ResponseCode == HttpStatus.Ok)
                    {
                        IsUrlExist = true;
                    }
                    else
                    {
                        IsUrlExist = false;
                    }
                }
                catch (Exception e)
                {
                    IsUrlExist = false;
                }
                return IsUrlExist;
            }

            protected override void OnPostExecute(Java.Lang.Object result)
            {
                base.OnPostExecute(result);
            }
        }

        // Check App's Market Version Details
        private class MarketInfo : AsyncTask
        {
            string MarketVersion = string.Empty;
            string PackageName = string.Empty;
            string ItemType = string.Empty;

            public MarketInfo(string PackageName, string ItemType)
            {
                this.PackageName = PackageName;
                this.ItemType = ItemType;
            }

            protected override Java.Lang.Object DoInBackground(params Java.Lang.Object[] @params)
            {
                try
                {
                    string MarketVersion = Jsoup.Connect("https://play.google.com/store/apps/details?id=" + PackageName)
                            .Timeout(60000)
                            .IgnoreHttpErrors(true)
                            .Referrer("http://www.google.com").Get()
                            .Select("div[itemprop=" + ItemType + "]").First() // .recent-change
                            .OwnText();
                    return MarketVersion;
                }
                catch (Exception e)
                {

                }
                return MarketVersion;
            }

            protected override void OnPostExecute(Java.Lang.Object result)
            {
                base.OnPostExecute(result);
            }
        }

        // Check App's Version Details
        private class AppInfo : AsyncTask
        {
            string AppVersion = string.Empty;
            Context mContext;

            public AppInfo(Context context)
            {
                mContext = context;
            }

            protected override Java.Lang.Object DoInBackground(params Java.Lang.Object[] @params)
            {
                try
                {
                    return AppVersion = mContext.PackageManager.GetPackageInfo(mContext.PackageName, 0).VersionName + "";
                }
                catch (Exception e)
                {

                }
                return AppVersion;
            }

            protected override void OnPostExecute(Java.Lang.Object result)
            {
                base.OnPostExecute(result);
            }
        }

    }
}
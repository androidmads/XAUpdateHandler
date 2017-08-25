using Android.App;
using Android.OS;
using Android.Widget;
using UH = Com.Androidmads.UpdateHandler;

namespace UpdateHandlerSample
{
    [Activity(Label = "UpdateHandler", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            bool IsNew = UH.Updater.IsNewVersionAvailable(ApplicationContext);
            string Mversion = UH.Updater.GetMarketVersion(ApplicationContext);
            string Aversion = UH.Updater.GetInstalledAppVersion(ApplicationContext);

            FindViewById<TextView>(Resource.Id.AppVersion).Text = "App Version : "+Aversion + "\nMarket Version : " + Mversion;
        }
    }
}


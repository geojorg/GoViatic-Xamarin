using Android.Gms.Ads;
using Android.Util;
using Goviatic.CustomRenderers;
using Goviatic.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(AdMobInterstitial))]
namespace Goviatic.CustomRenderers
{
    public class AdMobInterstitial : IAdInterstitial
    {
        InterstitialAd interstitialAd;

        public AdMobInterstitial()
        {
            //TODO CHANGE DE CA-APP-PUB CAMBIAR EL CODIGO
            interstitialAd = new InterstitialAd(Android.App.Application.Context);
            interstitialAd.AdUnitId = "ca-app-pub-5943072479494249/4544389012";
            LoadAd();
        }

        void LoadAd()
        {
            var requestbuilder = new AdRequest.Builder();
            interstitialAd.LoadAd(requestbuilder.Build());
        }

        public void ShowAd()
        {
            if (interstitialAd.IsLoaded)
            {
                interstitialAd.Show();
            }
            else
            {
                Log.Debug("TAG", "The interstitial wasn't loaded yet.");
            }
            LoadAd();
        }
    }
}
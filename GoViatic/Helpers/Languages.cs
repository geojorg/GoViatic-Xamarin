using Goviatic.Interfaces;
using GoViatic.Resources;
using Xamarin.Forms;

namespace GoViatic.Helpers
{
    public static class Languages
    {
        static Languages()
        {
            var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            Resource.Culture = ci;
            DependencyService.Get<ILocalize>().SetLocale(ci);
        }
    }
}
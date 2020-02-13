using Goviatic.Interfaces;
using GoViatic.Resources;
using System;
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

        public static object Choose => Resource.Choose;
        public static string NavTittleCreateTripPage => Resource.NavTittleCreateTripPage;
        public static string NavTittleEditTripPage => Resource.NavTittleEditTripPage;
        public static string CreateTripTittle => Resource.CreateTripTittle;
        public static string EditTripTittle => Resource.EditTripTittle;
        public static string Error => Resource.Error;
        public static string Accept => Resource.Accept;
        public static string Edited => Resource.Edited;
        public static string Created => Resource.Created;
        public static string CreateEditTripConfirm => Resource.CreateEditTripConfirm;

        public static string Confirm => Resource.Confirm;
        public static string Question => Resource.Question;
        public static string Yes => Resource.Yes;
        public static string No => Resource.No;
    }
}
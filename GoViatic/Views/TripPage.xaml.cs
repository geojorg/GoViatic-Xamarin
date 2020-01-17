using GoViatic.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GoViatic.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TripPage : ContentPage
    {
        public TripPage()
        {
            InitializeComponent();
            BindingContext = new TripViewModel();
        }

        private void ItemCollection(object sender, EventArgs e)
        {
            MyCollectionView.SelectedItem = (sender as SwipeView).BindingContext;
        }
    }
}
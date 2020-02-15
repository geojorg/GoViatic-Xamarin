using Xamarin.Forms;

namespace GoViatic.ViewModels
{
    [QueryProperty("Type", "type")]
    public class EditViaticViewModel : BaseViewModel
    {
        private string _navTittle;

        public string NavTittle
        {
            get { return _navTittle; }
            set { SetProperty(ref _navTittle, value); }
        }


        public string Type
        {
            set
            {
                if (value == "Create")
                {
                    //Trip = new TripResponse { Date = DateTime.Today, EndDate = DateTime.Today.AddDays(2) };
                    NavTittle = "Create Viatic";
                    //NavTittle = Languages.NavTittleCreateViatic;
                    //Tittle = Languages.CreateTripTittle;
                    //ColumnSpan = "2";
                    //SaveColumnSpan = "2";
                    //DeleteVisible = "False";
                }
                else
                {
                    NavTittle = "Edit Viatic";
                    //NavTittle = Languages.NavTittleEditViatic;
                    //Tittle = Languages.EditTripTittle;
                    //ColumnSpan = "2";
                    //DeleteVisible = "True";
                    //SaveColumnSpan = "1";
                    //IsEdit = true;
                }
            }
        }
    }
}

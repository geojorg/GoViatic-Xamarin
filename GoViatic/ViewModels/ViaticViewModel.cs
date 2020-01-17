using GoViatic.Common.Models;
using GoViatic.Data;
using GoViatic.Models;
using System.Collections.Generic;
using Xamarin.Forms;

namespace GoViatic.ViewModels
{
    [QueryProperty("Trips", "id")]
    public class ViaticViewModel : BaseViewModel
    {
        private IList<ViaticT> _viaticsType;
        public ICollection<ViaticResponse> Viatics { get; private set; }

        public ViaticViewModel()
        {
            ViaticsType = ViaticsData.Viatics;
        }
        

        public IList<ViaticT> ViaticsType
        {
            get { return _viaticsType; }
            set { SetProperty(ref _viaticsType, value); }
        }


        
    }
}

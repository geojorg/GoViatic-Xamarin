using GoViatic.Data;
using GoViatic.Models;
using System.Collections.Generic;

namespace GoViatic.ViewModels
{
    public class ViaticViewModel : BaseViewModel
    {
        private IList<ViaticT> _viaticsType;

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

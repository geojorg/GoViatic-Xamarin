using GoViatic.Web.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace GoViatic.Web.Helpers
{
    public class ComboHelper : IComboHelper
    {
        private readonly DataContext _dataContext;

        public ComboHelper(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        
        public IEnumerable<SelectListItem> GetComboViaticTypes()
        {
            var list = _dataContext.ViaticTypes.Select(vt => new SelectListItem
            {
                Text = vt.Concept,
                Value = $"{vt.Id}"
            })
                .OrderBy(vt => vt.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Select a viatic type...]",
                Value = "0"
            });
            return list;
        }
    }
}

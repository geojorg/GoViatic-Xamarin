using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GoViatic.Web.Helpers
{
    public interface IComboHelper
    {
        IEnumerable<SelectListItem> GetComboViaticTypes();
    }
}
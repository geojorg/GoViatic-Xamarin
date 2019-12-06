using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace GoViatic.Web.Helpers
{
    public interface IImageHelper
    {
        Task<string> UploadImageAsync(IFormFile imageFile);
    }
}

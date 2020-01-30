using GoViatic.Common.Models;
using GoViatic.Web.Data;
using GoViatic.Web.Data.Entities;
using GoViatic.Web.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace GoViatic.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private readonly IMailHelper _mailHelper;

        public AccountController(
            DataContext context,
            IUserHelper userHelper,
            IMailHelper mailHelper)
        {
            _context = context;
            _userHelper = userHelper;
            _mailHelper = mailHelper;
        }

        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] UserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Response<object>
                {
                    IsSuccess = false,
                    Message = "Bad request"
                });
            }

            var user = await _userHelper.GetUserByEmailAsync(request.Email);
            if (user != null)
            {
                return BadRequest(new Response<object>
                {
                    IsSuccess = false,
                    Message = "This email is already registered."
                });
            }

            user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Company = request.Company,
                UserName = request.Email
            };

            var result = await _userHelper.AddUserAsync(user, request.Password);
            if (result != IdentityResult.Success)
            {
                return BadRequest(result.Errors.FirstOrDefault().Description);
            }

            var userNew = await _userHelper.GetUserByEmailAsync(request.Email);
            await _userHelper.AddUserToRoleAsync(userNew, "Traveler");
            _context.Travelers.Add(new Traveler { User = userNew });
            await _context.SaveChangesAsync();

            var myToken = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
            var tokenLink = Url.Action("ConfirmEmail", "Account", new
            {
                userid = user.Id,
                token = myToken
            }, protocol: HttpContext.Request.Scheme);

            _mailHelper.SendMail(request.Email, "Email confirmation",
                    $"<p>&nbsp;</p>" +
                    $"<table style='max-width: 600px; padding: 10px; margin: 0 auto; border-collapse: collapse;'>" +
                    $"<tbody>" +
                    $"<tr>" +
                    $"<td style='background-color: #247d4d; text-align: center; padding: 0;'>&nbsp;</td>" +
                    $"</tr>" +
                    $"<tr>" +
                    $"<td style='background-color: #ecf0f1;'><br />" +
                    $"<div style='color: #34495e; margin: 4% 10% 2%; text-align: justify; font-family: sans-serif;'><br />" +
                    $"<h1 style='color: #e67e22; margin: 0 0 7px;'><span style='color: #247d4d;'>Hola</span></h1>" +
                    $"Bienvenido a GoViatic, es hora de comenzar a viajar y registrar sin problemas tus gastos de viaje:<br />" +
                    $"<h2 style='color: #247d4d; margin: 0 0 7px;'>Email Confirmation</h2>" +
                    $"To allow the user, please click in this link:</div>" +
                    $"<div style='color: #34495e; margin: 4% 10% 2%; font-family: sans-serif; text-align: center;'><a style='text-decoration: none; border-radius: 5px; padding: 11px 23px; color: white; background-color: #247d4d;' href=\"{tokenLink}\">Confirm Email</a> <br />" +
                    $"<p style='color: #b3b3b3; font-size: 12px; text-align: center; margin: 30px 0 0;'>GoViatic by GEOJOR.CO</p>" +
                    $"</div>" +
                    $"</td>" +
                    $"</tr>" +
                    $"</tbody>" +
                    $"</table>" +
                    $"<p>&nbsp;</p>");

            return Ok(new Response<object>
            {
                IsSuccess = true,
                Message = "Confirm your email to Log In"
            });
        }

        [HttpPost]
        [Route("RecoverPassword")]
        public async Task<IActionResult> RecoverPassword([FromBody] EmailRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Response<object>
                {
                    IsSuccess = false,
                    Message = "Bad request"
                });
            }

            var user = await _userHelper.GetUserByEmailAsync(request.Email);
            if (user == null)
            {
                return BadRequest(new Response<object>
                {
                    IsSuccess = false,
                    Message = "This email is not assigned to any user."
                });
            }

            var myToken = await _userHelper.GeneratePasswordResetTokenAsync(user);
            var link = Url.Action("ResetPassword", "Account", new { token = myToken }, protocol: HttpContext.Request.Scheme);
            _mailHelper.SendMail(request.Email, "Password Reset", $"<h1>Recover Password</h1>" +
                $"To reset the password click in this link:</br></br>" +
                $"<a href = \"{link}\">Reset Password</a>");
            return Ok(new Response<object>
            {
                IsSuccess = true,
                Message = "An email with instructions to change the password was sent."
            });
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> PutUser([FromBody] UserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userEntity = await _userHelper.GetUserByEmailAsync(request.Email);
            if (userEntity == null)
            {
                return BadRequest("User not found.");
            }

            userEntity.FirstName = request.FirstName;
            userEntity.LastName = request.LastName;
            userEntity.Company = request.Company;

            var respose = await _userHelper.UpdateUserAsync(userEntity);
            if (!respose.Succeeded)
            {
                return BadRequest(respose.Errors.FirstOrDefault().Description);
            }

            var updatedUser = await _userHelper.GetUserByEmailAsync(request.Email);
            return Ok(updatedUser);
        }

        [HttpPost]
        [Route("ChangePassword")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Response<object>
                {
                    IsSuccess = false,
                    Message = "Bad request"
                });
            }
            var user = await _userHelper.GetUserByEmailAsync(request.Email);
            if (user == null)
            {
                return BadRequest(new Response<object>
                {
                    IsSuccess = false,
                    Message = "This email is not assigned to any user."
                });
            }

            var result = await _userHelper.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);
            if (!result.Succeeded)
            {
                return BadRequest(new Response<object>
                {
                    IsSuccess = false,
                    Message = result.Errors.FirstOrDefault().Description
                });
            }

            return Ok(new Response<object>
            {
                IsSuccess = true,
                Message = "The password was changed successfully!"
            });
        }
    }
}

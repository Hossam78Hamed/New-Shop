using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Shop.API.Helper;
using Shop.Core.DTO.AllAccountDTO;
using Shop.Core.DTO.AllAddressDTO;
using Shop.Core.DTO.Shared;
using Shop.Core.Services.AccountServices;
using System.Security.Claims;

namespace Shop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;
        public AccountController(IAccountService _accountService)
        {
            accountService = _accountService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {

            if (registerDTO != null)
            {
                var result = await accountService.RegisterAsync(registerDTO);
                if (result == "Done")
                    return Ok(new ResponseAPI(200, result));
                else { return BadRequest(new ResponseAPI(400, result)); }

            }
            return BadRequest(new ResponseAPI(400, "RegisterDTO is null"));

        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {


            var result = await accountService.LoginAsync(loginDTO);
            if (result.IsSuccess)
            {

                Response.Cookies.Append("token", result.Msg, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = false,
                    SameSite = SameSiteMode.None,
                    IsEssential = true,
                    //Domain = "localhost",
                    Expires = DateTime.UtcNow.AddDays(1)
                });

                /* ChatGPT
                 SameSite = SameSiteMode.None
Allows the cookie to be sent with cross-site requests (needed for APIs used by front-end apps on a different domain).
Use None if frontend and backend are on different domains.
Use Lax for same-site apps.

                ///
   Problem in Your Code
    1-
Domain = "localhost" is incorrect for local development.

The browser will reject cookies with Domain=localhost.
You should omit Domain entirely or use Domain = "127.0.0.1" if testing on IP.
2-
Secure = true will block cookies if you're using plain HTTP (not HTTPS).

In local development without HTTPS, set it to false.
                 */


                return Ok(new ResponseAPI(200, result.Msg));



            }
            else { return BadRequest(new ResponseAPI(400, result.Msg)); }


            //return BadRequest(new ResponseAPI(400, "RegisterDTO is null"));

        }

        [HttpGet("Send_Email_ForForget_Password")]
        public async Task<IActionResult> SendEmailForForgetPassword(string Email)
        {

            var result = await accountService.SendEmailForForgetPassword(Email);
            if (result == true)
                return Ok(new ResponseAPI(200, "Done"));
            else { return BadRequest(new ResponseAPI(400, "can not found Email ")); }

        }


        [HttpPost("ActiveAccount")]
        public async Task<IActionResult> ActiveAccount(ActiveAccountDTO activeAccountDTO)
        {

            var result = await accountService.ActiveAccount(activeAccountDTO);
            if (result == true)
                return Ok(new ResponseAPI(200, "Active Account is Done"));
            else { return BadRequest(new ResponseAPI(400, "Sorry Active Account is not Done")); }

        }




        //Me
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(RestPasswordDTO restPasswordDTO)
        {

            var result = await accountService.ResetPassword(restPasswordDTO);
            if (result == "Done")
                return Ok(new ResponseAPI(200, "Reset Password is Done"));
            else { return BadRequest(new ResponseAPI(400, result)); }

        }

        //UpdateAddress

        [HttpPut("UpdateAddress")]
        public async Task<IActionResult> UpdateAddress(UpdateAddressDTO updateAddressDTO)
        {

            var email = User.FindFirst(ClaimTypes.Email).Value;

            var result = await accountService.UpdateAddress(email, updateAddressDTO);

            if (result == true) return Ok();
            else return BadRequest();

        }

        ///getUserAddress
        [HttpGet("getUserAddress")]
        public async Task< IActionResult> getUserAddress() {
            var email = User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.Email).Value;
            if (email==null) return BadRequest(new ResponseAPI(400, "Email not found"));
            var addressDTO= await accountService.getUserAddress(email);
            
            if (addressDTO == null) return NotFound(new ResponseAPI(404, "Address not found"));
            return Ok(addressDTO);
        
        
        }



    }
}

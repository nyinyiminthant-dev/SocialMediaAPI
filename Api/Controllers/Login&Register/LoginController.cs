using BAL.IServices.Login_Register;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MODEL.DTOs.Login_RegisterDTO;
using System.Threading.Tasks;

namespace Api.Controllers.Login_Register;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{


    private readonly ILogin _login;

   public LoginController(ILogin loign)
   {
        _login = loign;

   }

    [HttpPost("Login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginRequestModel requestModel)
    {

        try
        {
            var returndata = await _login.UserLogin(requestModel);
          
                return Ok(new LoginResponseModel { IsSuccess = returndata.IsSuccess,
                    Message = returndata.Message,
                    Data = returndata.Data,
                    Email = returndata.Email,
                    UserName = returndata.UserName,
                    User_Id = returndata.User_Id,
                    Token = returndata.Token

                });
            

        } catch (Exception ex)
        {
            return BadRequest(new LoginResponseModel {IsSuccess = false, Message = ex.Message, Data = null });
        }
       
    }


}

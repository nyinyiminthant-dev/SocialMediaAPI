using BAL.IServices.Login_Register;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using MODEL.DTOs.Login_RegisterDTO;

namespace Api.Controllers.Login_Register;

[Route("api/[controller]")]
[AllowAnonymous]
[ApiController]
public class RegisterController : ControllerBase
{
    private readonly IRegister _registerService;

    public RegisterController(IRegister register)
    {
        _registerService = register;
    }


    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestModel requestModel)
    {
       try
        {
            var response = await _registerService.UserRegister(requestModel);
            return Ok(new RegisterResponseModel { IsSuccess = response.IsSuccess, Message = response.Message, Data = response.Data });
           

        } catch (Exception ex)
        {
            return BadRequest(new RegisterResponseModel { Message = ex.Message, Data = null});
        }
    }

    [HttpPost("VerifyAccount")]
    public async Task<IActionResult> VerifyAccount([FromQuery] string email, [FromQuery] string otp)
    {
        try
        {
            var response = await _registerService.VerifyAccount(email, otp);
            return Ok(new RegisterResponseModel { IsSuccess = response.IsSuccess, Message = response.Message, Data = response.Data });
        }
        catch (Exception ex)
        {
            return BadRequest(new RegisterResponseModel { Message = ex.Message, Data = null });
        }
    }

    [HttpPost("ResendOTP")]
    public async Task<IActionResult> ResendOTP([FromQuery] string email)
    {
        try
        {
            var response = await _registerService.ResendOTP(email);
            return Ok(new RegisterResponseModel { IsSuccess = response.IsSuccess, Message = response.Message, Data = response.Data });
        }
        catch (Exception ex)
        {
            return BadRequest(new RegisterResponseModel { Message = ex.Message, Data = null });
        }
    }
}

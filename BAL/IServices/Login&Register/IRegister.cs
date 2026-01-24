using MODEL.DTOs.Login_RegisterDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.IServices.Login_Register;

public interface IRegister
{
    Task<RegisterResponseModel> UserRegister(RegisterRequestModel requestModel);
    Task<RegisterResponseModel> VerifyAccount(string email, string otp);
    Task<RegisterResponseModel> ResendOTP(string email);
}

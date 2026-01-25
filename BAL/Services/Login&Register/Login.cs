using BAL.IServices.Login_Register;
using BAL.Shared;
using MODEL.DTOs.Login_RegisterDTO;
using REPOSITORY.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Login_Register;

public class Login : ILogin
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly CommonAuthentication _commonAuthentication;

    public Login(IUnitOfWork unitOfWork, CommonAuthentication commonAuthentication)
    {
        _unitOfWork = unitOfWork;
        _commonAuthentication = commonAuthentication;
    }



    public async Task<LoginResponseModel> UserLogin(LoginRequestModel requestModel)
    {
       LoginResponseModel model = new LoginResponseModel();


        var userdata = (await _unitOfWork.Users
            .GetByCondition(x => x.Email == requestModel.Email))
            .FirstOrDefault();

        if(userdata is null)
        {
            model.IsSuccess = false;
            model.Message = "Invalid Email";
            return model;
        }


        bool isValid = _commonAuthentication.VerifyPasswordHash(requestModel.Password!, userdata.Password!);

        if(isValid)
        {
            model.User_Id = userdata.User_Id;
           
            model.UserName = userdata.UserName;
            model.Email = userdata.Email;
            model.Token = CommonTokenGenerator.GenerateToken(userdata, "U");
            model.IsSuccess = true;
            model.Message = "Login Successful";
            model.Data = userdata;
            return model;
        }

        else
        {
            model.IsSuccess = false;
            model.Message = "Invalid Password or email.";
            return model;
        }

    }
}

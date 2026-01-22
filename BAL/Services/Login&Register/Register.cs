using BAL.IServices.Login_Register;
using MODEL.DTOs.Login_RegisterDTO;
using MODEL.Entity;
using REPOSITORY.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Login_Register;

public class Register : IRegister
{

    private readonly IUnitOfWork _unitOfWork;

    public Register(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

   public async Task<RegisterResponseModel> UserRegister (RegisterRequestModel requestModel)
    {
        RegisterResponseModel model = new RegisterResponseModel();
        var existingUser = await _unitOfWork.Users.GetByEmail(requestModel.Email!);
        

        if(existingUser is not null)
        {
            model.IsSuccess = false;
            model.Message = "User already exits. Register Failed";
            model.Data = existingUser;
            return model;
        }

        var hashedPassword = HashPassword(requestModel.Password!);

        var User = new User
        {
            UserName = requestModel.userName,
            Email = requestModel.Email,
            Password = hashedPassword,
            Role = "U",
            CreatedAt = DateTime.Now,
        };

        await _unitOfWork.Users.Add(User);
        int result = await _unitOfWork.SaveChangesAsync();
        string message = result > 0 ? "Register Successful " : "Register Failed";

        model.IsSuccess = result > 0;
        model.Message = message;
        model.Data = User;

        return model;


    }

    public static string  HashPassword(string password)
    {
        using SHA256 sha256 = SHA256.Create();
        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(hashedBytes);

    }
}

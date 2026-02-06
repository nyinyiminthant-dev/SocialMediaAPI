using BAL.IServices.Login_Register;
using MODEL.DTOs.Login_RegisterDTO;
using MODEL.Entity;
using REPOSITORY.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.CompilerServices;
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
        var otp = GenerateOTP();
        
        var User = new User
        {
            UserName = requestModel.userName,
            Email = requestModel.Email,
            Password = hashedPassword,
            Role = "U",
            OTP = otp,
            OTP_exp = DateTime.Now.AddMinutes(5),
            CreatedAt = DateTime.Now,
            Status = "N"
        };
             
        await _unitOfWork.Users.Add(User);
        int result = await _unitOfWork.SaveChangesAsync();

        if(result > 0 )
        {
            bool emailSent = SentOTPEmail(User.Email!,User.OTP,User.UserName!);

            if(!emailSent)
            {
                model.IsSuccess = false;
                model.Message = "Failed to send OTP email. Please try again.";
                model.Data = null;
                return model;
            }


            model.IsSuccess = result > 0;
            model.Message = "User Registered Successfully. Please verify OTP sent to your email.";
            model.Data = User;
        } else
        {
            model.IsSuccess = false;
            model.Message = "User Registration Failed.";
            model.Data = null;
        }

            return model;


    }

    public static string  HashPassword(string password)
    {
        using SHA256 sha256 = SHA256.Create();
        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(hashedBytes);

    }


    private string GenerateOTP()
    {
        Random random = new Random();
        return random.Next(100000, 999999).ToString();
    }

    private bool SentOTPEmail(string toEmail, string otp, string userName)
    {
        try
        {

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("nnyi37389@gmail.com", "SocialMedia APP");
            mail.To.Add(toEmail);
            mail.Subject = "Your OTP Code";

            string htmlBody = $@"
            <div style='font-family: Arial, sans-serif; padding: 20px; border: 1px solid #ddd; border-radius: 10px; max-width: 500px; margin: auto; background-color: #f9f9f9;'>
                <h2 style='color: #007bff; text-align: center;'>Your OTP Code</h2>
                <p style='font-size: 16px; color: #333;'>Dear <strong>{userName}</strong>,</p>
                <p style='font-size: 16px; color: #333;'>Your One-Time Password (OTP) for verification is:</p>
                <p style='font-size: 24px; font-weight: bold; color: #28a745; text-align: center; padding: 10px; border: 2px dashed #28a745; display: inline-block;'>{otp}</p>
                <p style='font-size: 14px; color: #ff0000; text-align: center;'>This OTP will expire in 5 minutes.</p>
             
               
                <br>
                <p style='font-size: 14px; color: #666; text-align: center;'>Best regards,</p>
                <p style='font-size: 14px; color: #666; text-align: center;'><strong>SocialMedia APP </strong></p>
            </div>";

            mail.Body = htmlBody;
            mail.IsBodyHtml = true;

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("nnyi37389@gmail.com", "jbrq aqmv ukix sfdv"),
                EnableSsl = true,
            };
            smtpClient.Send(mail);

            return true;
        }         catch (Exception)
        {
            return false;
        }
    }

    public async Task<RegisterResponseModel> VerifyAccount(string email, string otp)
    {
        var model = new RegisterResponseModel();
        var user =    _unitOfWork.Users.GetByExp(x => x.Email == email && x.OTP == otp && x.OTP_exp >= DateTime.Now).FirstOrDefault();

        if(user is null)
        {
            model.IsSuccess = false;
            model.Message = "Invalid OTP or OTP has expired.";
            model.Data = null;
            return model;
        }

        if (user.OTP != otp)
        {
            model.IsSuccess = false;
            model.Message = "Invalid OTP.";
            model.Data = null;

            return model;
        }

        if(user.OTP_exp < DateTime.Now)
        {
            model.IsSuccess = false;
            model.Message = "OTP has expired.";
            model.Data = null;
            return model;
        }

        user.Status = "Y";
        user.OTP = null;
        user.OTP_exp = DateTime.Now;
        _unitOfWork.Users.Update(user);
        int result = await _unitOfWork.SaveChangesAsync();

        string message = result > 0 ? "Account verified successfully." : "Account verification failed.";
        model.IsSuccess = result > 0;
        model.Message = message;
        model.Data = user;
        return model;

    }

    
    public async Task<RegisterResponseModel> ResendOTP (string email)
    {
        var model = new RegisterResponseModel();
        var user = await _unitOfWork.Users.GetByEmail(email);
        if(user is null)
        {
            model.IsSuccess = false;
            model.Message = "User not found.";
            model.Data = null;
            return model;
        }
        var otp = GenerateOTP();
        user.OTP = otp;
        user.OTP_exp = DateTime.Now.AddMinutes(5);
        _unitOfWork.Users.Update(user);
        int result = await _unitOfWork.SaveChangesAsync();
        if(result > 0)
        {
            bool emailSent = SentOTPEmail(user.Email!, user.OTP!, user.UserName!);
            if(!emailSent)
            {
                model.IsSuccess = false;
                model.Message = "Failed to send OTP email. Please try again.";
                model.Data = null;
                return model;
            }
            model.IsSuccess = true;
            model.Message = "OTP resent successfully. Please check your email.";
            model.Data = user;
        }
        else
        {
            model.IsSuccess = false;
            model.Message = "Failed to resend OTP. Please try again.";
            model.Data = null;
        }
        return model;
    }

}

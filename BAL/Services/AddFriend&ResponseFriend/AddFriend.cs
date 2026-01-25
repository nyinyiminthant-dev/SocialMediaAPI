using BAL.IServices.AddFriend_ResponseFriend;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MODEL.DTOs.AddFriend_ResponseFriend;
using MODEL.Entity;
using REPOSITORY.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.AddFriend_ResponseFriend;

public class AddFriend : IAddFriend
{


    private readonly IUnitOfWork _unitOfWork;

    public AddFriend(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<AddFriendResponseModel> MakeFriendRequest(AddFriendRequestModel requestModel)
    {

        try
        {
          
            var toFriend = await _unitOfWork.Users.GetByIdAsync(requestModel.ToUser_Id);
            var fromFriend = await _unitOfWork.Users.GetByIdAsync(requestModel.FromUser_Id);
            if (toFriend == null || fromFriend == null)
            {
                return new AddFriendResponseModel()
                {
                    IsSuccess = false,
                    Message = "User not found",
                   FromUser_Id = 0,
                   ToUser_Id = 0,
                    CreatedAt = DateTime.MinValue,
                    Status = null

                };
            }

            var friendRequest = new Friend()
            {
                FromUser_Id = fromFriend.User_Id,
                ToUser_Id = toFriend.User_Id,
                CreatedAt = DateTime.Now,
                Status = "Pending"
            };

          await  _unitOfWork.Friends.Add(friendRequest);
          int result = await _unitOfWork.SaveChangesAsync();

            string message = result > 0 ? "Friend request sent successfully" : "Failed to send friend request";
            return new AddFriendResponseModel()
            {
                IsSuccess = result > 0,
                Message = message,
                FromUser_Id = friendRequest.FromUser_Id,
                ToUser_Id = friendRequest.ToUser_Id,
                CreatedAt = friendRequest.CreatedAt,
                Status = friendRequest.Status

            };



        }
        catch (Exception ex)
        {
            return new AddFriendResponseModel()
            {
                IsSuccess = false,
                Message = ex.Message,
                FromUser_Id = 0,
                ToUser_Id = 0,
                CreatedAt = DateTime.MinValue,
                Status = null
            };

        }

        }
}

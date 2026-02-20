using BAL.IServices.AddFriend_ResponseFriend;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
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

    public async Task<AcceptFriendResponseModel> AcceptFriendRequest(AcceptFriendRequestModel requestModel)
    {
        try
        {

            var friendRequest = await _unitOfWork.Friends.GetByIdAsync(requestModel.Friend_Id);

            if (friendRequest is null)
            {
                return new AcceptFriendResponseModel
                {
                    IsSuccess = false,
                    Message = "No data",

                };

            }

           

            friendRequest.Status = "Friend";
            friendRequest.CreatedAt = DateTime.Now;
         
            _unitOfWork.Friends.Update(friendRequest);

            int result = await _unitOfWork.SaveChangesAsync();

            string message = result > 0 ? "Approve Friend Successful" : "Approve Friend Failed";

            return new AcceptFriendResponseModel
            {
                IsSuccess = result > 0,
                Message = message

            };
        }
        catch (Exception ex)
        {
            throw ex;
        }
        
    }

    public async Task<CancelFriendResponseModel> CancelFriend(CancelFriendRequestModel requestModel)
    {
       try
        {

            var friendRequest = await _unitOfWork.Friends.GetByIdAsync(requestModel.Friend_Id);

           if(friendRequest is null)
            {
                return new CancelFriendResponseModel
                {
                    IsSuccess = false,
                    Message = "No data"

                };

            }

            
            _unitOfWork.Friends.Delete(friendRequest);
            int result = await _unitOfWork.SaveChangesAsync();

            string message = result > 0 ? "Cancel Friend Successful" : "Cancel Friend Failed";

            return new CancelFriendResponseModel
            {
                IsSuccess = result > 0,
                Message = message
            };

        } catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<GetFriendsByIdResponseModel> GetFriendsById(GetFriendByIdRequestModel requestModel)
    {
        try
        {

            var friends = await _unitOfWork.Friends.GetByCondition(
        x => x.User_Id == requestModel.User_Id &&
             x.Status == "Friend"
    );


            if (friends is null)
            {
                return new GetFriendsByIdResponseModel
                {
                    IsSuccess = false,
                    Message = "No data",
                    Data = null
                };
            }

            return new GetFriendsByIdResponseModel
            {
                IsSuccess = true,
                Message = "Successful",
                Data = friends.ToList()
            };
        }
        catch (Exception ex)
        {
            throw ex;
        }
        
    }

    public async Task<AddFriendResponseModel> MakeFriendRequest(AddFriendRequestModel requestModel)
    {

        try
        {
          
            var toFriend = await _unitOfWork.Users.GetByIdAsync(requestModel.ToUser_Id);
            var fromFriend = await _unitOfWork.Users.GetByIdAsync(requestModel.FromUser_Id);

            var friendExist = _unitOfWork.Friends.GetByExp(x =>
           (x.FromUser_Id == fromFriend!.UserId && x.ToUser_Id == toFriend!.UserId)
        || (x.FromUser_Id == toFriend!.UserId && x.ToUser_Id == fromFriend!.UserId)
       ).FirstOrDefault();

            if (friendExist != null)
            {
                if (friendExist.Status == "Friend")
                {
                    return new AddFriendResponseModel
                    {
                        IsSuccess = false,
                        Message = "Already Friends"
                    };
                }

                if (friendExist.Status == "Pending")
                {
                    return new AddFriendResponseModel
                    {
                        IsSuccess = false,
                        Message = "Friend request already sent"
                    };
                }
            }

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
                FromUser_Id = fromFriend.UserId,
                ToUser_Id = toFriend.UserId,
                CreatedAt = DateTime.Now,
                Status = "Pending",
                User_Id= fromFriend.UserId

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
                Status = friendRequest.Status,
                User_Id = friendRequest.User_Id


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

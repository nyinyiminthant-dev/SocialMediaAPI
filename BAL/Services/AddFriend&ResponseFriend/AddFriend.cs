using BAL.IServices.AddFriend_ResponseFriend;
using MODEL.DTOs.AddFriend_ResponseFriend;
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

    
    Task<AddFriendResponseModel> IAddFriend.AddFriend(AddFriendRequestModel requestModel)
    {
       throw new NotImplementedException();
    }
}

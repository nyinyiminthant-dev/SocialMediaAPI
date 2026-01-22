using MODEL.DTOs.AddFriend_ResponseFriend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.IServices.AddFriend_ResponseFriend;

public interface IAddFriend
{
    Task<AddFriendResponseModel> AddFriend (AddFriendRequestModel requestModel);
}

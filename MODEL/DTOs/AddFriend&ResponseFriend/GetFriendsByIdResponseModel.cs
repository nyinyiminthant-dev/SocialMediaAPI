using MODEL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.DTOs.AddFriend_ResponseFriend;

public class GetFriendsByIdResponseModel : Common
{
    public List<Friend>? Data { get; set; }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.DTOs.AddFriend_ResponseFriend;

public class AddFriendRequestModel
{
    public int From_User_Id { get; set; }
    public int To_User_Id { get; set; }
    public DateTime Created_At { get; set; }
    public string? Status { get; set; }
}




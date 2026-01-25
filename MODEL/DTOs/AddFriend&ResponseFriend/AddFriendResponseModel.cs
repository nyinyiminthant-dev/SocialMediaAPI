using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.DTOs.AddFriend_ResponseFriend;

public class AddFriendResponseModel : Common
{
    public int Friend_Id { get; set; }
    public int FromUser_Id { get; set; }
    public int ToUser_Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? Status { get; set; }
}

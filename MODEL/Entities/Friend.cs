using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Entity;

[Table("FriendTbl")]
public class Friend
{
    [Key]
    public int Friend_Id { get; set; }
    public int From_User_Id { get; set; }
    public int To_User_Id { get; set; }
    public DateTime Created_At { get; set; }
    public string ? Status { get; set; }
}

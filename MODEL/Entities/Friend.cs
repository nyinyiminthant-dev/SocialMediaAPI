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
    public int FromUser_Id { get; set; }
    public int ToUser_Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string ? Status { get; set; }
}

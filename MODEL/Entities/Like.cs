using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Entity;

[Table("LikeTbl")]
public class Like
{
    [Key]
    public int Like_Id { get; set; }
    public int Post_Id { get; set; }
    public int User_Id { get; set; }
    public DateTime Liked_At { get; set; }
}

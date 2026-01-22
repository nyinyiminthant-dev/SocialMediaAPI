using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Entity;

[Table("CommentTbl")]
public class Comment
{
    [Key]
    public int Comment_Id { get; set; }
    public int Post_Id { get; set; }
    public int User_Id { get; set; }
    public string? Content { get; set; }
    public DateTime Created_At { get; set; }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Entity;

[Table("PostTbl")]
public class Post
{
    [Key]
    public int Post_Id { get; set; }
    public int User_Id { get; set; }
    public string? content { get; set; }
    public DateTime Created_At { get; set; }
}

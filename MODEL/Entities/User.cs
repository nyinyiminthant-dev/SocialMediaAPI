using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Entity;

[Table("User_Tbl")]
public class User
{ 
    [Key]
    public int User_Id { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }

    public string? OTP {get; set; }
    public DateTime OTP_exp {get; set; }

    public DateTime CreateAt { get; set; }
    public string? Role { get; set; }

    public string? Status { get; set; }
    }

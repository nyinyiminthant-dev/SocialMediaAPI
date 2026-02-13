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
    public int UserId { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }

    public string? OTP {get; set; }
    public DateTime OTPExp {get; set; }

    public DateTime CreatedAt { get; set; }
    public string? Role { get; set; }

    public string? Status { get; set; }
    }

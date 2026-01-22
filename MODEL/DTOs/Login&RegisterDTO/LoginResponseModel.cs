using MODEL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.DTOs.Login_RegisterDTO;

public class LoginResponseModel : Common
{
    public int User_Id { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? Token { get; set; }
    public User? Data { get; set; }
    }

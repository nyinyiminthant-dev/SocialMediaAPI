using MODEL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.DTOs.Login_RegisterDTO;

public class RegisterResponseModel : Common
{
    public User? Data { get; set; }
}

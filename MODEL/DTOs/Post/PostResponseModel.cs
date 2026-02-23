using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.DTOs.Post
{
    public class PostResponseModel : Common
    {
     
        public object? Data { get; set; }
        public int? Post_Id { get; set; }
        public int? User_Id { get; set; }
    }
}

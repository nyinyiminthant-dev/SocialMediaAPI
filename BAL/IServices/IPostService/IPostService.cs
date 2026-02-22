using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MODEL.DTOs.Post;

namespace BAL.IServices.IPostService
{
    public interface IPostService
    {
        Task<PostResponseModel> CreatePost(CreatePostRequestModel requestModel, ClaimsPrincipal user);
        Task<PostResponseModel> GetMyPosts(ClaimsPrincipal user);
        Task<PostResponseModel> UpdatePost(int postId, UpdatePostRequestModel requestModel, ClaimsPrincipal user);
        Task<PostResponseModel> DeletePost(int postId, ClaimsPrincipal user);
    }
}

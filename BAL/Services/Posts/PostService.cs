using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BAL.IServices.IPostService;
using MODEL.DTOs.Post;
using MODEL.Entity;
using REPOSITORY.UnitOfWork;

namespace BAL.Services.Posts
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PostService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private int GetUserIdFromToken(ClaimsPrincipal user)
        {
            var userIdClaim = user.FindFirst("User_Id")?.Value
                              ?? user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userIdClaim, out int userId))
                throw new UnauthorizedAccessException("Invalid token.");

            return userId;
        }

        public async Task<PostResponseModel> CreatePost(CreatePostRequestModel requestModel, ClaimsPrincipal user)
        {
            PostResponseModel model = new PostResponseModel();

            try
            {
                int userId = GetUserIdFromToken(user);

                if (string.IsNullOrWhiteSpace(requestModel.Content))
                {
                    model.IsSuccess = false;
                    model.Message = "Post content is required.";
                    return model;
                }

                var post = new Post
                {
                    User_Id = userId,
                    content = requestModel.Content.Trim(),
                    Created_At = DateTime.UtcNow,
                    Updated_At = DateTime.UtcNow
                };

                await _unitOfWork.Posts.Add(post);
                await _unitOfWork.SaveChangesAsync();

                model.IsSuccess = true;
                model.Message = "Post created successfully.";
                model.Post_Id = post.Post_Id;
                model.User_Id = post.User_Id;
                model.Data = post;
                return model;
            }
            catch (Exception ex)
            {
                model.IsSuccess = false;
                model.Message = ex.Message;
                model.Data = null;
                return model;
            }
        }

        public async Task<PostResponseModel> GetMyPosts(ClaimsPrincipal user)
        {
            PostResponseModel model = new PostResponseModel();

            try
            {
                int userId = GetUserIdFromToken(user);

                var posts = (await _unitOfWork.Posts.GetByCondition(x => x.User_Id == userId))
                    .OrderByDescending(x => x.Created_At)
                    .ToList();

                model.IsSuccess = true;
                model.Message = "Posts fetched successfully.";
                model.User_Id = userId;
                model.Data = posts;
                return model;
            }
            catch (Exception ex)
            {
                model.IsSuccess = false;
                model.Message = ex.Message;
                model.Data = null;
                return model;
            }
        }

        public async Task<PostResponseModel> UpdatePost(int postId, UpdatePostRequestModel requestModel, ClaimsPrincipal user)
        {
            PostResponseModel model = new PostResponseModel();

            try
            {
                int userId = GetUserIdFromToken(user);

                var post = (await _unitOfWork.Posts.GetByCondition(x => x.Post_Id == postId)).FirstOrDefault();

                if (post is null)
                {
                    model.IsSuccess = false;
                    model.Message = "Post not found.";
                    return model;
                }

                if (post.User_Id != userId)
                {
                    model.IsSuccess = false;
                    model.Message = "You are not authorized to update this post.";
                    return model;
                }

                if (string.IsNullOrWhiteSpace(requestModel.Content))
                {
                    model.IsSuccess = false;
                    model.Message = "Post content is required.";
                    return model;
                }

                post.content = requestModel.Content.Trim();
                post.Updated_At = DateTime.UtcNow;

                _unitOfWork.Posts.Update(post);
                await _unitOfWork.SaveChangesAsync();

                model.IsSuccess = true;
                model.Message = "Post updated successfully.";
                model.Post_Id = post.Post_Id;
                model.User_Id = post.User_Id;
                model.Data = post;
                return model;
            }
            catch (Exception ex)
            {
                model.IsSuccess = false;
                model.Message = ex.Message;
                model.Data = null;
                return model;
            }
        }

        public async Task<PostResponseModel> DeletePost(int postId, ClaimsPrincipal user)
        {
            PostResponseModel model = new PostResponseModel();

            try
            {
                int userId = GetUserIdFromToken(user);

                var post = (await _unitOfWork.Posts.GetByCondition(x => x.Post_Id == postId)).FirstOrDefault();

                if (post is null)
                {
                    model.IsSuccess = false;
                    model.Message = "Post not found.";
                    return model;
                }

                if (post.User_Id != userId)
                {
                    model.IsSuccess = false;
                    model.Message = "You are not authorized to delete this post.";
                    return model;
                }

                _unitOfWork.Posts.Delete(post);
                await _unitOfWork.SaveChangesAsync();

                model.IsSuccess = true;
                model.Message = "Post deleted successfully.";
                model.Post_Id = postId;
                model.User_Id = userId;
                model.Data = null;
                return model;
            }
            catch (Exception ex)
            {
                model.IsSuccess = false;
                model.Message = ex.Message;
                model.Data = null;
                return model;
            }
        }
    }
}

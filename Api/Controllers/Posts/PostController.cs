using BAL.IServices.IPostService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MODEL.DTOs.Post;

namespace Api.Controllers.Posts
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreatePostRequestModel requestModel)
        {
            var returndata = await _postService.CreatePost(requestModel, User);

            if (returndata.IsSuccess)
                return Ok(returndata);

            return BadRequest(returndata);
        }

        [HttpGet("GetMyPosts")]
        public async Task<IActionResult> GetMyPosts()
        {
            var returndata = await _postService.GetMyPosts(User);

            if (returndata.IsSuccess)
                return Ok(returndata);

            return BadRequest(returndata);
        }

        [HttpPut("Update/{postId}")]
        public async Task<IActionResult> Update(int postId, [FromBody] UpdatePostRequestModel requestModel)
        {
            var returndata = await _postService.UpdatePost(postId, requestModel, User);

            if (returndata.IsSuccess)
                return Ok(returndata);

            return BadRequest(returndata);
        }

        [HttpDelete("Delete/{postId}")]
        public async Task<IActionResult> Delete(int postId)
        {
            var returndata = await _postService.DeletePost(postId, User);

            if (returndata.IsSuccess)
                return Ok(returndata);

            return BadRequest(returndata);
        }
    }
}

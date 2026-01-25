using BAL.IServices.AddFriend_ResponseFriend;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MODEL.DTOs.AddFriend_ResponseFriend;

namespace Api.Controllers.AddFriend_ResponseFriend;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class FriendController : ControllerBase
{
    private readonly IAddFriend _addFirendService;

    public FriendController(IAddFriend addfriend)
    {
        _addFirendService = addfriend;
    }

    [HttpPost("MakeFriendRequest")]
   
    public async Task<IActionResult> MakeFriendRequest([FromBody] AddFriendRequestModel requestModel)
    {
        try
        {
            var response = await _addFirendService.MakeFriendRequest(requestModel);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(new MODEL.DTOs.AddFriend_ResponseFriend.AddFriendResponseModel { IsSuccess = false, Message = ex.Message, FromUser_Id = 0, ToUser_Id = 0, CreatedAt = DateTime.MinValue, Status = null });
        }
    }
}

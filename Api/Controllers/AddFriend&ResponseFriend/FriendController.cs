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
            return BadRequest(new AddFriendResponseModel { IsSuccess = false, Message = ex.Message, FromUser_Id = 0, ToUser_Id = 0, CreatedAt = DateTime.MinValue, Status = null });
        }
    }

    [HttpPatch("AcceptFriendRequest")]
    public async Task<IActionResult> AcceptFriendRequest([FromBody] AcceptFriendRequestModel requestModel)
    {
        try
        {
            var response = await _addFirendService.AcceptFriendRequest(requestModel);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(new AcceptFriendResponseModel { IsSuccess = false, Message = ex.Message });
        }
    }


    [HttpDelete("CancelFriend")]
    public async Task<IActionResult> CancelFriend([FromBody] CancelFriendRequestModel requestModel)
    {
        try
        {
            var response = await _addFirendService.CancelFriend(requestModel);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(new CancelFriendResponseModel { IsSuccess = false, Message = ex.Message });
        }
    }

     [HttpGet("GetFriendsById")]
     public async Task<IActionResult> GetFriendsById([FromQuery] GetFriendByIdRequestModel requestModel)
     {
         try
         {
             var response = await _addFirendService.GetFriendsById(requestModel);
             return Ok(response);
         }
         catch (Exception ex)
         {
             return BadRequest(new GetFriendsByIdResponseModel { IsSuccess = false, Message = ex.Message });
         }
    }
}

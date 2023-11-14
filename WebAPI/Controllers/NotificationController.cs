using Business.Abstract;
using Entities.Concrete.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NotificationController : ControllerBase
{
    private INotificationService _notificationService;

    public NotificationController(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    [HttpGet]
    [Route("getFriendRequests")]
    public IActionResult GetFriendRequests(string token)
    {
        var result = _notificationService.GetFriendRequestNotifications(token);
        if (result.Success)
        {
            return Ok(result.Data);
        }

        return Ok();
    }

    [Authorize]
    [HttpPost]
    [Route("recordNotification")]
    public IActionResult RecordNotification([FromBody] NotificationDto dto)
    {
        var result = _notificationService.RecordNotification(dto);
        if (result.Success)
        {
            return Ok(result.Data);
        }

        return BadRequest();
    }

    [HttpGet]
    [Route("getNotifications")]
    public IActionResult GetNotifications(string token)
    {
        var result = _notificationService.GetNotifications(token);
        if (result.Success)
        {
            return Ok(result.Data);
        }

        return Ok();
    }
}
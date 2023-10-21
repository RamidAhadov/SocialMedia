using DataContracts.Models;
using Microsoft.AspNetCore.Mvc;
using MvcWebUI.Extensions;
using Newtonsoft.Json;

namespace MvcWebUI.Controllers;

public class TokenController : Controller
{
    [HttpPost]
    public IActionResult SendToken([FromBody] string createdToken)
    {
        var token = JsonConvert.DeserializeObject<TokenModel>(createdToken);
        if (token!=null)
        {
            HttpContext.Session.SetObject("token",token);
            return Ok(token);
        }

        return BadRequest("Token is not created");
    }
}
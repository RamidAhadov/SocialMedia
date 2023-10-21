using Microsoft.AspNetCore.Mvc;

namespace MvcWebUI.Controllers;

public class MessageController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}
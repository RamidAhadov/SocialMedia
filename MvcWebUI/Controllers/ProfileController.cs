using Microsoft.AspNetCore.Mvc;

namespace MvcWebUI.Controllers;

public class ProfileController : Controller
{
    // GET
    public IActionResult Index(string userName)
    {
        return View();
    }
}
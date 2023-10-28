using Microsoft.AspNetCore.Mvc;

namespace MvcWebUI.Controllers;

public class TestController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}
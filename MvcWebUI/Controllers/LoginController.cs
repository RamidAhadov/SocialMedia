using Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace MvcWebUI.Controllers;

public class LoginController : Controller
{
    private IAuthService _authService;

    public LoginController(IAuthService authService)
    {
        _authService = authService;
    }

    // GET
    public IActionResult Index()
    {
        return View();
    }
}
using System.Net.Http.Headers;
using System.Text;
using DataContracts.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MvcWebUI.Controllers;

public class AuthController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }

    
    public  IActionResult Log()
    {
        // string token =
        //     ".eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6WyIxOCIsImZhcmlkMjAwMCJdLCJlbWFpbCI6ImZhcmlkMjAwMEBnbWFpbC5jb20iLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiRmFyaWQgRGphdmFkb3YiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJVc2VyIiwibmJmIjoxNjk0Mzg1MjI4LCJleHAiOjE2OTQ0MjEyMjUsImlzcyI6Ind3dy5lbmdpbi5jb20iLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjUxMjkifQ.wnlhx-5KZzF5YeM5Z09dydiNAtNxA0g-zaJQkQwPvg4";
        //
        //
         // using (var client = new HttpClient())
         // {
         //     var baseAddress = "http://localhost:5129";
         //     client.BaseAddress = new Uri(baseAddress);
         //
         //     var contentType = new MediaTypeWithQualityHeaderValue("application/json");
         //
         //     client.DefaultRequestHeaders.Accept.Add(contentType);
         //     client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
         //     string url = "http://localhost:5129/post/index";
         //     
         //      var request = new HttpRequestMessage(HttpMethod.Get, url);
         //      
         //      HttpResponseMessage response = await client.SendAsync(request);
         //     if (response.IsSuccessStatusCode)
         //     {
         //         return RedirectToAction("Index", "Post");
         //     }
        
             return View();
    }

    //[Authorize]
    public async Task<IActionResult> Logg()
    {
        // using (var client = new HttpClient())
        // {
        //     client.DefaultRequestHeaders.Accept.Clear();
        //     client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //
        //     HttpResponseMessage response = await client.PostAsync("api/auth/login", null);
        //
        //     if (response.IsSuccessStatusCode)
        //     {
        //         var tokenResponse = await response.Content.ReadFromJsonAsync<AccessToken>();
        //         var token = tokenResponse.Token;
        //
        //         return Ok(token);
        //     }
        //     else
        //     {
        //         return View("Error");
        //     }
        // }
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] UserLoginModel model)
    {
        string jsonData = JsonConvert.SerializeObject(model);

        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri("http://localhost:5015");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.PostAsync("api/login", new StringContent(jsonData, Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                var responseJson = await response.Content.ReadAsStringAsync();
                return RedirectToAction("Log");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
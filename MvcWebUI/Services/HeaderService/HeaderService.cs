using System.Net.Http.Headers;
using System.Text;
using DataContracts.Models;
using Newtonsoft.Json;

namespace MvcWebUI.Services.HeaderService;

public static class HeaderService
{
    public static async Task<HttpResponseMessage> GetResponseGet(string receiverBase, string senderUrl,TokenModel model)
    {
        using (var client = new HttpClient())
        {
            var token = model;

            var baseAddress = receiverBase;
            client.BaseAddress = new Uri(baseAddress);
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            string url = senderUrl;

            client.DefaultRequestHeaders.Accept.Add(contentType);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token.Token);
            
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            HttpResponseMessage response = await client.SendAsync(request);

            return response;
        }
    }
    public static async Task<HttpResponseMessage> GetResponsePost(string receiverBase, string senderUrl, TokenModel model, object data)
    {
        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri(receiverBase);
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            string url = senderUrl;

            client.DefaultRequestHeaders.Accept.Add(contentType);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", model.Token.Token);

            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, contentType);
            var postRequest = new HttpRequestMessage(HttpMethod.Post, url);
            postRequest.Content = content;

            var postResponse = await client.SendAsync(postRequest);

            if (postResponse.IsSuccessStatusCode)
            {
                
            }

            return postResponse;
        }
    }

}
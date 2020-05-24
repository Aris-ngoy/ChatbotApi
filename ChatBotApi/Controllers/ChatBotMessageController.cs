using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ChatBotApi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ChatBotApi.Controllers
{
    [Route("chatbotmessage")]
    public class ChatBotMessageController : Controller
    {
        private readonly HttpClient HttpClient;

        public ChatBotMessageController()
        {
            HttpClient = new HttpClient();
        }

        private async Task<MessageResponseModel> GetMessage(SendMessageModel content)
        {
            MessageResponseModel messageResponseModel = new MessageResponseModel();
            try
            {
                string strPayload = JsonConvert.SerializeObject(content);
                HttpContent httpContent = new StringContent(strPayload, Encoding.UTF8, "application/json");
                HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "ba260ac8e4714764b31a837be09adcd1");

                HttpResponseMessage response = await HttpClient.PostAsync("https://api.dialogflow.com/v1/query?v=20150910", httpContent);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                // Above three lines can be replaced with new helper method below

                messageResponseModel = JsonConvert.DeserializeObject<MessageResponseModel>(responseBody);
                
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
            return messageResponseModel;
        }
        // POST api/<controller>
        [HttpPost]
        public Task<MessageResponseModel> Post([FromBody]SendMessageModel param)
        {
            return GetMessage(param);

        }

       
    }
}

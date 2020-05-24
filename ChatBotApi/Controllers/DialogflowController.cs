using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Google.Cloud.Dialogflow.V2;
using Google.Protobuf;
using System.IO;
using System.Text;

namespace ChatBotApi.Controllers
{
    [Route("webhook")]
    public class DialogflowController : ControllerBase
    {
        // A Protobuf JSON parser configured to ignore unknown fields. This makes
        // the action robust against new fields being introduced by Dialogflow.
        private static readonly JsonParser jsonParser =
            new JsonParser(JsonParser.Settings.Default.WithIgnoreUnknownFields(true));

        string phoneNumber;
        public async Task<ContentResult> DialogAction()
        {

            // Read the request JSON asynchronously, as the Google.Protobuf library
            // doesn't (yet) support asynchronous parsing.
            string requestJson;
            using (TextReader reader = new StreamReader(Request.Body))
            {
                requestJson = await reader.ReadToEndAsync();
            }

            // Parse the body of the request using the Protobuf JSON parser,
            // *not* Json.NET.
            WebhookRequest request = jsonParser.Parse<WebhookRequest>(requestJson);

            if (request.QueryResult.Intent.DisplayName.Equals("ContactDetails"))
            {
                phoneNumber = "Get it from database or other sources";
            }

            var message = new Intent.Types.Message
            {
                Card = new Intent.Types.Message.Types.Card
                {
                    Title = "Oh Yeah !",
                    ImageUri = "https://www.google.be/images/branding/googlelogo/2x/googlelogo_color_272x92dp.png",

                }
            };
            // Note: you should authenticate the request here.

            // Populate the response
            WebhookResponse response = new WebhookResponse
            {
                // ...
                FulfillmentMessages = { message },
                FulfillmentText = "Emergency Number is : " + phoneNumber
            };

            // Ask Protobuf to format the JSON to return.
            // Again, we don't want to use Json.NET - it doesn't know how to handle Struct
            // values etc.
            string responseJson = response.FulfillmentMessages.ToString();
            return Content(responseJson, "application/json");
        }
    }
}
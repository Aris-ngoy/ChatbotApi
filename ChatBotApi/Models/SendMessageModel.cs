using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatBotApi.Models
{
    public class SendMessageModel
    {
        public string lang { get; set; }
        public string query { get; set; }
        public string sessionId { set; get; } = "raise-my-hand";
    }
}

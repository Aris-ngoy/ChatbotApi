using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatBotApi.Models
{
    public class MessageResponseModel
    {
        public string id { set; get; }
        public string lang { set; get; }
        public string timestamp { set; get; }

        public Result result { get; set; }

        public Status status { set; get; }

    }

    public class Result
    {
        public string source { set; get;  }
        public string resolveQuery { set; get;  }
        public string action { set; get;  }
        public string actionIncomplete { set; get;  }
        public double score { set; get;  }
        public Dictionary<string, string> parameters { set; get;  }

        public Metadata metadata { set; get; }
        public Fulfillment fulfillment { set; get; }

    }

    public class Metadata
    {
        public string intentId { set; get; }
        public string intentName { set; get; }
        public string webhookUsed { set; get; }
        public string webhookForSlotFillingUsed { set; get; }
        public string isFallbackIntent { set; get; }
    }

    public class Fulfillment
    {
        public string speech { set; get; }
        public List<Message> messages { set; get; }
    }

    public class Message
    {
        public string lang { set; get; }
        public int type { set; get; }
        public string speech { set; get; }
    }

    public class Status
    {
        public int code { set; get; }
        public string errorType { set; get; }
    }
}

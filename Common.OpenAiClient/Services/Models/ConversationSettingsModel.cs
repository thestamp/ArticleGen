using System.Collections.Generic;

namespace Common.OpenAiClient.Services.Models
{
    public class ConversationSettingsModel
    {
        public ConversationSettingsModel()
        {
            ExampleDialogs = new List<ExampleDialog>();
        }

        public class ExampleDialog
        {
            public string UserInput { get; set; }
            public string BotResponse { get; set; }
        }

        public string SystemMessage { get; set; }
        public List<ExampleDialog> ExampleDialogs { get; set; }
    }
}

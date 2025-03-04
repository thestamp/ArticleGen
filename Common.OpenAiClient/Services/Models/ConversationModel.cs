using OpenAI_API.Chat;

namespace Common.OpenAiClient.Services.Models
{
    public class ConversationModel
    {
        private readonly Conversation _gptConversation;

        public ConversationModel(Conversation gptConversation)
        {
            _gptConversation = gptConversation;
        }
    }
}

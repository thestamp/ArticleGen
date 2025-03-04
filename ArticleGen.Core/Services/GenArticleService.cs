using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Common.OpenAiClient.Configuration;
using Common.OpenAiClient.Services.Models;
using Enlighten.Gpt.Client.Services;

namespace ArticleGen.Core.Services
{
    public class GenArticleService
    {
        private readonly GptClientSettingsModel _clientSettings;

        public GenArticleService(GptClientSettingsModel clientSettings)
        {
            _clientSettings = clientSettings;
        }

        public async Task<IAsyncEnumerable<string>> GenerateArticle(string articleTopic, string articleName, string articleHeadline)
        {
            var client = new GptClientService(_clientSettings);

            client.Connect();

            var conversationSettings = InitializeConversation();


            // The bot is requested to generate a short-answer question based on the textbook content
            var response = await client.StreamResponse(conversationSettings, $"Based on {articleTopic}, generate a magazine article about 500 words long, named \"{articleName}\".");
            return response;
        }

        public ConversationSettingsModel InitializeConversation()
        {
            //todo topic settings support
            var conversationSettings = new ConversationSettingsModel()
            {
                SystemMessage = "You are an AI article generator. Your goal is to help users understand and remember their articles."//gptPromptSettings.InquireSystemMessage
            };

            return conversationSettings;


        }


    }
}

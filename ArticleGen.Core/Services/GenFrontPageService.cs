using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;
using ArticleGen.Core.Models;
using Common.OpenAiClient.Configuration;
using Common.OpenAiClient.Services.Models;
using Enlighten.Gpt.Client.Services;

namespace ArticleGen.Core.Services
{
    public class GenFrontPageService
    {
        private readonly GptClientSettingsModel _clientSettings;

        public GenFrontPageService(GptClientSettingsModel clientSettings)
        {
            _clientSettings = clientSettings;
        }

        public async Task<FrontPageModel> GenerateFrontPage(string industry)
        {
            var client = new GptClientService(_clientSettings);

            client.Connect();

            var conversationSettings = InitializeConversation();

            var prompt = $"Based on {industry}, a list of 5 categories and 3 article names each.";
            // The bot is requested to generate a short-answer question based on the textbook content
            var response = await client.GetResponseAsync(conversationSettings, prompt);

            var model = JsonSerializer.Deserialize<FrontPageModel>(response)!;
            return model;
        }

        public ConversationSettingsModel InitializeConversation()
        {

            var example = new FrontPageModel()
            {

                Industry = "industry",
                Categories = new[]
                {
                    new FrontPageModel.FrontPageCategoryModel()
                    {
                        Category = "Category1Name",
                        CategoryArticleNames = new string[] { "ArticleName1", "ArticleName2", "ArticleName3" },
                    },
                    new FrontPageModel.FrontPageCategoryModel()
                    {
                        Category = "Category2Name",
                        CategoryArticleNames = new string[] { "ArticleName1", "ArticleName2", "ArticleName3" },
                    }
                }

            };

            var jsonExample = JsonSerializer.Serialize(example); ;

            //todo topic settings support
            var conversationSettings = new ConversationSettingsModel()
            {
                SystemMessage = $"You are an AI article generator, your goal is to help users understand and remember their articles. you only return properly escaped json (use \\n instead of 0x0A) in the following structure: {jsonExample} "
            };

            return conversationSettings;


        }


    }
}

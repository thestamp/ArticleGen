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
    public class GenCategoryService
    {
        private readonly GptClientSettingsModel _clientSettings;

        public GenCategoryService(GptClientSettingsModel clientSettings)
        {
            _clientSettings = clientSettings;
        }

        public async Task<CategoryModel> GenerateCategoryArticles(string industry, string category)
        {
            var client = new GptClientService(_clientSettings);

            client.Connect();

            var conversationSettings = InitializeConversation();

            var response = await client.GetResponseAsync(conversationSettings, $"Based on {industry} industry and {category} category, generate of 10 articles.");
            
            var model = JsonSerializer.Deserialize<CategoryModel>(response);

            return model;
        }

        public ConversationSettingsModel InitializeConversation()
        {
            var example = new CategoryModel()
            {
                Name = "CategoryName",
                Articles = new[]
                {
                    new CategoryModel.ArticleModel()
                    {
                        Name = "ArticleName",
                        Headline = "ArticleHeadline"
                    },
                    new CategoryModel.ArticleModel()
                    {
                        Name = "ArticleName",
                        Headline = "ArticleHeadline"
                    }
                }

            };

            var jsonExample = JsonSerializer.Serialize(example); ;

            var conversationSettings = new ConversationSettingsModel()
            {
                SystemMessage = $"You are an AI article generator, your goal is to help users understand and remember their articles. you only return properly escaped json (use \\n instead of 0x0A) in the following structure: {jsonExample} "
            };

            return conversationSettings;


        }


    }
}

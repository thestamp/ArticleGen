﻿using System;
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
    public class GenArticleService
    {
        private readonly GptClientSettingsModel _clientSettings;

        public GenArticleService(GptClientSettingsModel clientSettings)
        {
            _clientSettings = clientSettings;
        }

        public async Task<ArticleModel> GenerateArticle(string industry, string articleCategory, string articleName, string articleHeadline)
        {
            var client = new GptClientService(_clientSettings);

            client.Connect();

            var conversationSettings = InitializeConversation();

           

            // The bot is requested to generate a short-answer question based on the textbook content
            var response = await client.GetResponseAsync(conversationSettings, $"Based on {industry} industry and {articleCategory} category, generate a magazine article about 500 words long, named \"{articleName}\".");

            var model = JsonSerializer.Deserialize<ArticleModel>(response);
            return model;
        }

        public ConversationSettingsModel InitializeConversation()
        {
            var example = new ArticleModel()
            {
                Name = "ArticleName",
                Body = "ArticleBody, in markdown syntax"

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

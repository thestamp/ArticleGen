using System.Text.Json;
using System.Threading.Tasks;
using Common.OpenAiClient.Configuration;
using Common.OpenAiClient.Services.Models;
using Enlighten.Gpt.Client.Services;

namespace ArticleGen.Core.Services
{
    public class DomainAnalysisService
    {
        private readonly GptClientSettingsModel _clientSettings;

        public DomainAnalysisService(GptClientSettingsModel clientSettings)
        {
            _clientSettings = clientSettings;
        }

        public async Task<string> AnalyzeDomainForIndustry(string domainName)
        {
            var client = new GptClientService(_clientSettings);
            client.Connect();

            var conversationSettings = InitializeConversation();

            var prompt = $"Based on the domain name '{domainName}', what industry or business category would be most relevant for self-help articles? Return only the industry name.";
            
            var response = await client.GetResponseAsync(conversationSettings, prompt);

            // Clean up the response to extract just the industry name
            var industry = response.Trim().Trim('"');
            
            return industry;
        }

        private ConversationSettingsModel InitializeConversation()
        {
            var conversationSettings = new ConversationSettingsModel()
            {
                SystemMessage = "You are an AI assistant that analyzes domain names to determine the most relevant industry or business category. You should respond with a single, clear industry name that would be appropriate for self-help articles. For example: 'Health', 'Technology', 'Finance', 'Education', 'Fitness', 'Business', 'Career', etc. Keep responses concise and relevant."
            };

            return conversationSettings;
        }
    }
}
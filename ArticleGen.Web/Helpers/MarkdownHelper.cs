using Markdig;

namespace ArticleGen.Web.Helpers
{
    public static class MarkdownHelper
    {
        public static string ConvertMarkdownToHtml(string markdown)
        {
            var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
            return Markdown.ToHtml(markdown, pipeline);
        }
    }
}


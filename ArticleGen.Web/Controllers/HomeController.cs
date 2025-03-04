using System.Diagnostics;
using ArticleGen.Core.Services;
using Microsoft.AspNetCore.Mvc;
using ArticleGen.Web.Models;
using Common.OpenAiClient.Services.Models;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Enlighten.Gpt.Client.Services;

namespace ArticleGen.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly GenArticleService _articleService;


    public HomeController(ILogger<HomeController> logger, GenArticleService articleService)
    {
        _logger = logger;
        _articleService = articleService;
    }

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> Article(string? topic = null, string? name = null)
    {
        var response = await _articleService.GenerateArticle(topic, name, "n/a");
        var botResponse = "";
        await foreach (var res in response)
        {
            botResponse += res;
        }
        return Content(botResponse);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

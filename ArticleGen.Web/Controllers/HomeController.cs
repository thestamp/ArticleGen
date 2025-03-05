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
    private readonly GenCategoryService _categoryService;
    private readonly GenFrontPageService _frontPageService;


    public HomeController(ILogger<HomeController> logger, GenArticleService articleService, GenCategoryService categoryService, GenFrontPageService frontPageService)
    {
        _logger = logger;
        _articleService = articleService;
        _categoryService = categoryService;
        _frontPageService = frontPageService;
    }

    public IActionResult Index()
    {

        return View();
    }

    public async Task<IActionResult> FrontPage(string industry = null)
    {
        var model = await _frontPageService.GenerateFrontPage(industry);

        return View(model);
    }

    public async Task<IActionResult> Article(string industry, string category, string article)
    {
        var model = await _articleService.GenerateArticle(industry, category, article);

        return View(model);
    }

    public async Task<IActionResult> Category(string industry, string category)
    {
        var model = await _categoryService.GenerateCategoryArticles(industry, category);
 
        return View(model);
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

using System.Diagnostics;
using Csnp.Presentation.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Csnp.Presentation.Web.Controllers;

/// <summary>
/// Controller for handling homepage and static views.
/// </summary>
public class HomeController : Controller
{
    #region -- Methods --

    /// <summary>
    /// Initializes a new instance of the <see cref="HomeController"/> class.
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Displays the default home page.
    /// </summary>
    /// <returns>The index view.</returns>
    public IActionResult Index()
    {
        return View();
    }

    /// <summary>
    /// Displays the privacy policy page.
    /// </summary>
    /// <returns>The privacy view.</returns>
    public IActionResult Privacy()
    {
        return View();
    }

    /// <summary>
    /// Displays the error page with request trace information.
    /// </summary>
    /// <returns>The error view.</returns>
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    #endregion

    #region -- Fields --

    private readonly ILogger<HomeController> _logger;

    #endregion
}

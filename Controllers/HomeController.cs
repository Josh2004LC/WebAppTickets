using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TicketsMVC.Models;
using TicketsMVC.Services;

namespace TicketsMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITicketService _ticketService;

        public HomeController(ILogger<HomeController> logger, ITicketService ticketService)
        {
            _logger = logger;
            _ticketService = ticketService;
        }

        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    var tickets = await _ticketService.GetAllTicketsAsync();
                    return View(tickets);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error retrieving tickets");
                    return View(new List<Ticket>());
                }
            }

            return View();
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
}

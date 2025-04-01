using Microsoft.AspNetCore.Mvc;
using MVCProject.Models;
using MVCProject.Services;

namespace MVCProject.Controllers
{
    public class TicketController : Controller
    {
        private readonly ITicketService _ticketService;

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        // Acción para mostrar la lista de tiquetes
        public async Task<IActionResult> Index()
        {
            var tickets = await _ticketService.GetTiquetesAsync();
            return View(tickets);
        }

        // Acción para mostrar los detalles de un tiquete
        public async Task<IActionResult> Details(int id)
        {
            var ticket = await _ticketService.GetTiqueteByIdAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            return View(ticket);
        }

        // GET: Ticket/Create - Muestra el formulario para crear un tiquete
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ticket/Create - Procesa el formulario de creación
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Tiquetes tiquete)
        {
            if (ModelState.IsValid)
            {
                await _ticketService.CreateTiqueteAsync(tiquete);
                return RedirectToAction(nameof(Index));
            }
            return View(tiquete);
        }
    }
}

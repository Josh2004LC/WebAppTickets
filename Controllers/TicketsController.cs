using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TicketsMVC.Models;
using TicketsMVC.Services;

namespace TicketsMVC.Controllers
{
    [Authorize]
    public class TicketsController : Controller
    {
        private readonly ITicketService _ticketService;
        private readonly ICategoryService _categoryService;
        private readonly IUrgencyService _urgencyService;
        private readonly IImportanceService _importanceService;
        private readonly IUserService _userService;
        private readonly ILogger<TicketsController> _logger;

        public TicketsController(
            ITicketService ticketService,
            ICategoryService categoryService,
            IUrgencyService urgencyService,
            IImportanceService importanceService,
            IUserService userService,
            ILogger<TicketsController> logger)
        {
            _ticketService = ticketService;
            _categoryService = categoryService;
            _urgencyService = urgencyService;
            _importanceService = importanceService;
            _userService = userService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var tickets = await _ticketService.GetAllTicketsAsync();
                return View(tickets);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving tickets");
                TempData["ErrorMessage"] = "Error al cargar los tickets.";
                return View(new List<Ticket>());
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var ticket = await _ticketService.GetTicketByIdAsync(id);
                if (ticket == null)
                {
                    return NotFound();
                }
                return View(ticket);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving ticket with ID {id}");
                TempData["ErrorMessage"] = "Error al cargar el ticket.";
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Create()
        {
            await LoadDropdownData();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ticket.ti_adicionado_por = User.Identity.Name;
                    ticket.ti_fecha_adicion = DateTime.Now;
                    ticket.ti_estado = "Pendiente";

                    var createdTicket = await _ticketService.CreateTicketAsync(ticket);
                    TempData["SuccessMessage"] = "Ticket creado exitosamente.";
                    return RedirectToAction(nameof(Details), new { id = createdTicket.ti_identificador });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error creating ticket");
                    ModelState.AddModelError(string.Empty, "Error al crear el ticket.");
                }
            }

            await LoadDropdownData();
            return View(ticket);
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var ticket = await _ticketService.GetTicketByIdAsync(id);
                if (ticket == null)
                {
                    return NotFound();
                }

                await LoadDropdownData();
                return View(ticket);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving ticket with ID {id} for edit");
                TempData["ErrorMessage"] = "Error al cargar el ticket para editar.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Ticket ticket)
        {
            if (id != ticket.ti_identificador)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    ticket.ti_modificado_por = User.Identity.Name;
                    ticket.ti_fecha_modificacion = DateTime.Now;

                    var result = await _ticketService.UpdateTicketAsync(id, ticket);
                    if (result)
                    {
                        TempData["SuccessMessage"] = "Ticket actualizado exitosamente.";
                        return RedirectToAction(nameof(Details), new { id });
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Error al actualizar el ticket.");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error updating ticket with ID {id}");
                    ModelState.AddModelError(string.Empty, "Error al actualizar el ticket.");
                }
            }

            await LoadDropdownData();
            return View(ticket);
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var ticket = await _ticketService.GetTicketByIdAsync(id);
                if (ticket == null)
                {
                    return NotFound();
                }

                return View(ticket);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving ticket with ID {id} for delete");
                TempData["ErrorMessage"] = "Error al cargar el ticket para eliminar.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var result = await _ticketService.DeleteTicketAsync(id);
                if (result)
                {
                    TempData["SuccessMessage"] = "Ticket eliminado exitosamente.";
                }
                else
                {
                    TempData["ErrorMessage"] = "Error al eliminar el ticket.";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting ticket with ID {id}");
                TempData["ErrorMessage"] = "Error al eliminar el ticket.";
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeStatus(int id, string newStatus)
        {
            try
            {
                var result = await _ticketService.ChangeTicketStatusAsync(id, newStatus);
                if (result)
                {
                    TempData["SuccessMessage"] = "Estado del ticket actualizado exitosamente.";
                }
                else
                {
                    TempData["ErrorMessage"] = "Error al actualizar el estado del ticket.";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error changing status for ticket with ID {id}");
                TempData["ErrorMessage"] = "Error al actualizar el estado del ticket.";
            }

            return RedirectToAction(nameof(Details), new { id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateSolution(int id, string solution)
        {
            try
            {
                var result = await _ticketService.UpdateTicketSolutionAsync(id, solution);
                if (result)
                {
                    TempData["SuccessMessage"] = "Solución del ticket actualizada exitosamente.";
                }
                else
                {
                    TempData["ErrorMessage"] = "Error al actualizar la solución del ticket.";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating solution for ticket with ID {id}");
                TempData["ErrorMessage"] = "Error al actualizar la solución del ticket.";
            }

            return RedirectToAction(nameof(Details), new { id });
        }

        private async Task LoadDropdownData()
        {
            try
            {
                var categories = await _categoryService.GetAllCategoriesAsync();
                var urgencies = await _urgencyService.GetAllUrgenciesAsync();
                var importances = await _importanceService.GetAllImportancesAsync();
                var analysts = await _userService.GetUsersByRoleAsync(2); // Assuming 2 is the role ID for analysts

                ViewBag.Categories = new SelectList(categories, "ca_identificador", "ca_descripcion");
                ViewBag.Urgencies = new SelectList(urgencies, "ur_identificador", "ur_descripcion");
                ViewBag.Importances = new SelectList(importances, "im_identificador", "im_descripcion");
                ViewBag.Analysts = new SelectList(analysts, "us_identificador", "us_nombre_completo");
                ViewBag.Statuses = new SelectList(new[] { "Pendiente", "En Proceso", "Resuelto", "Cerrado" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading dropdown data");
            }
        }
    }
}

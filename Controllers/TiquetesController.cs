using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaTickets.Models;
using SistemaTickets.Services;

namespace SistemaTickets.Controllers
{
    [Authorize]
    public class TiquetesController : Controller
    {
        private readonly ApiService _apiService;

        public TiquetesController(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index(string estado = null, string urgencia = null, int? usuarioId = null)
        {
            ViewBag.Usuarios = await ObtenerUsuariosSelectList();

            try
            {
                List<Tiquete> tiquetes;
                if (!string.IsNullOrEmpty(estado))
                {
                    tiquetes = await _apiService.GetListAsync<Tiquete>($"Tiquetes/Estado/{estado}");
                }
                else if (!string.IsNullOrEmpty(urgencia))
                {
                    tiquetes = await _apiService.GetListAsync<Tiquete>($"Tiquetes/Urgencia/{urgencia}");
                }
                else if (usuarioId.HasValue)
                {
                    tiquetes = await _apiService.GetListAsync<Tiquete>($"Tiquetes/Usuario/{usuarioId}");
                }
                else
                {
                    tiquetes = await _apiService.GetListAsync<Tiquete>("Tiquetes");
                }

                return View(tiquetes);
            }
            catch (Exception)
            {
                TempData["Error"] = "Error al cargar los tiquetes.";
                return View(new List<Tiquete>());
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await CargarListas();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Tiquete tiquete)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    tiquete.ti_adicionado_por = User.FindFirst(ClaimTypes.Name)?.Value;
                    tiquete.ti_modificado_por = User.FindFirst(ClaimTypes.Name)?.Value;

                    await _apiService.PostAsync<Tiquete>("Tiquetes", tiquete);
                    TempData["Success"] = "Tiquete creado exitosamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    TempData["Error"] = "Error al crear el tiquete.";
                }
            }

            await CargarListas();
            return View(tiquete);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var tiquete = await _apiService.GetAsync<Tiquete>($"Tiquetes/{id}");
                await CargarListas();
                return View(tiquete);
            }
            catch (Exception)
            {
                TempData["Error"] = "Error al cargar el tiquete.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Tiquete tiquete)
        {
            if (id != tiquete.ti_identificador)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    tiquete.ti_modificado_por = User.FindFirst(ClaimTypes.Name)?.Value;
                    await _apiService.PutAsync($"Tiquetes/{id}", tiquete);
                    TempData["Success"] = "Tiquete actualizado exitosamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    TempData["Error"] = "Error al actualizar el tiquete.";
                }
            }

            await CargarListas();
            return View(tiquete);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var tiquete = await _apiService.GetAsync<Tiquete>($"Tiquetes/{id}");
                return View(tiquete);
            }
            catch (Exception)
            {
                TempData["Error"] = "Error al cargar el tiquete.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _apiService.DeleteAsync($"Tiquetes/{id}");
                TempData["Success"] = "Tiquete eliminado exitosamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                TempData["Error"] = "Error al eliminar el tiquete.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public async Task<IActionResult> CambiarEstado(int id)
        {
            try
            {
                var tiquete = await _apiService.GetAsync<Tiquete>($"Tiquetes/{id}");
                ViewBag.Estados = new SelectList(new[] { "Abierto", "En Proceso", "Resuelto", "Cerrado" });
                return View(tiquete);
            }
            catch (Exception)
            {
                TempData["Error"] = "Error al cargar el tiquete.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CambiarEstado(int id, string nuevoEstado)
        {
            try
            {
                await _apiService.PatchAsync($"Tiquetes/{id}/CambiarEstado", nuevoEstado);
                TempData["Success"] = "Estado del tiquete actualizado exitosamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                TempData["Error"] = "Error al cambiar el estado del tiquete.";
                return RedirectToAction(nameof(Index));
            }
        }

        private async Task CargarListas()
        {
            ViewBag.Usuarios = await ObtenerUsuariosSelectList();
            ViewBag.Categorias = new SelectList(new[] { "Hardware", "Software", "Red", "Otro" });
            ViewBag.Estados = new SelectList(new[] { "Abierto", "En Proceso", "Resuelto", "Cerrado" });
            ViewBag.Urgencias = new SelectList(new[] { "Alta", "Media", "Baja" });
            ViewBag.Importancias = new SelectList(new[] { "Alta", "Media", "Baja" });
        }

        private async Task<SelectList> ObtenerUsuariosSelectList()
        {
            try
            {
                var usuarios = await _apiService.GetListAsync<Usuario>("Usuarios");
                return new SelectList(usuarios, "us_identificador", "us_nombre_completo");
            }
            catch
            {
                return new SelectList(new List<Usuario>());
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaTickets.Models;
using SistemaTickets.Services;

namespace SistemaTickets.Controllers
{
    [Authorize]
    public class RolesController : Controller
    {
        private readonly ApiService _apiService;

        public RolesController(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var roles = await _apiService.GetListAsync<Rol>("Roles");
                return View(roles);
            }
            catch (Exception)
            {
                TempData["Error"] = "Error al cargar los roles.";
                return View(new List<Rol>());
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Rol rol)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    rol.ro_adicionado_por = User.FindFirst(ClaimTypes.Name)?.Value;
                    rol.ro_modificado_por = User.FindFirst(ClaimTypes.Name)?.Value;

                    await _apiService.PostAsync<Rol>("Roles", rol);
                    TempData["Success"] = "Rol creado exitosamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    TempData["Error"] = "Error al crear el rol.";
                }
            }
            return View(rol);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var rol = await _apiService.GetAsync<Rol>($"Roles/{id}");
                return View(rol);
            }
            catch (Exception)
            {
                TempData["Error"] = "Error al cargar el rol.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Rol rol)
        {
            if (id != rol.ro_identificador)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    rol.ro_modificado_por = User.FindFirst(ClaimTypes.Name)?.Value;
                    await _apiService.PutAsync($"Roles/{id}", rol);
                    TempData["Success"] = "Rol actualizado exitosamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    TempData["Error"] = "Error al actualizar el rol.";
                }
            }
            return View(rol);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var rol = await _apiService.GetAsync<Rol>($"Roles/{id}");
                return View(rol);
            }
            catch (Exception)
            {
                TempData["Error"] = "Error al cargar el rol.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _apiService.DeleteAsync($"Roles/{id}");
                TempData["Success"] = "Rol eliminado exitosamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                TempData["Error"] = "Error al eliminar el rol.";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
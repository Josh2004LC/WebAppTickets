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
    public class UsuariosController : Controller
    {
        private readonly ApiService _apiService;

        public UsuariosController(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index(int? rolId = null, string estado = null)
        {
            ViewBag.Roles = await ObtenerRolesSelectList();

            try
            {
                List<Usuario> usuarios;
                if (rolId.HasValue)
                {
                    usuarios = await _apiService.GetListAsync<Usuario>($"Usuarios/Rol/{rolId}");
                }
                else if (!string.IsNullOrEmpty(estado))
                {
                    usuarios = await _apiService.GetListAsync<Usuario>($"Usuarios/Estado/{estado}");
                }
                else
                {
                    usuarios = await _apiService.GetListAsync<Usuario>("Usuarios");
                }

                return View(usuarios);
            }
            catch (Exception)
            {
                TempData["Error"] = "Error al cargar los usuarios.";
                return View(new List<Usuario>());
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Roles = await ObtenerRolesSelectList();
            ViewBag.Estados = new SelectList(new[] { new { Value = "A", Text = "Activo" }, new { Value = "I", Text = "Inactivo" } }, "Value", "Text");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    usuario.us_adicionado_por = User.FindFirst(ClaimTypes.Name)?.Value;
                    usuario.us_modificado_por = User.FindFirst(ClaimTypes.Name)?.Value;

                    await _apiService.PostAsync<Usuario>("Usuarios", usuario);
                    TempData["Success"] = "Usuario creado exitosamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    TempData["Error"] = "Error al crear el usuario.";
                }
            }

            ViewBag.Roles = await ObtenerRolesSelectList();
            ViewBag.Estados = new SelectList(new[] { new { Value = "A", Text = "Activo" }, new { Value = "I", Text = "Inactivo" } }, "Value", "Text");
            return View(usuario);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var usuario = await _apiService.GetAsync<Usuario>($"Usuarios/{id}");
                ViewBag.Roles = await ObtenerRolesSelectList();
                ViewBag.Estados = new SelectList(new[] { new { Value = "A", Text = "Activo" }, new { Value = "I", Text = "Inactivo" } }, "Value", "Text");
                return View(usuario);
            }
            catch (Exception)
            {
                TempData["Error"] = "Error al cargar el usuario.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Usuario usuario)
        {
            if (id != usuario.us_identificador)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    usuario.us_modificado_por = User.FindFirst(ClaimTypes.Name)?.Value;
                    await _apiService.PutAsync($"Usuarios/{id}", usuario);
                    TempData["Success"] = "Usuario actualizado exitosamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    TempData["Error"] = "Error al actualizar el usuario.";
                }
            }

            ViewBag.Roles = await ObtenerRolesSelectList();
            ViewBag.Estados = new SelectList(new[] { new { Value = "A", Text = "Activo" }, new { Value = "I", Text = "Inactivo" } }, "Value", "Text");
            return View(usuario);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var usuario = await _apiService.GetAsync<Usuario>($"Usuarios/{id}");
                return View(usuario);
            }
            catch (Exception)
            {
                TempData["Error"] = "Error al cargar el usuario.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _apiService.DeleteAsync($"Usuarios/{id}");
                TempData["Success"] = "Usuario eliminado exitosamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                TempData["Error"] = "Error al eliminar el usuario.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public async Task<IActionResult> CambiarEstado(int id)
        {
            try
            {
                var usuario = await _apiService.GetAsync<Usuario>($"Usuarios/{id}");
                ViewBag.Estados = new SelectList(new[] { new { Value = "A", Text = "Activo" }, new { Value = "I", Text = "Inactivo" } }, "Value", "Text");
                return View(usuario);
            }
            catch (Exception)
            {
                TempData["Error"] = "Error al cargar el usuario.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CambiarEstado(int id, string nuevoEstado)
        {
            try
            {
                await _apiService.PatchAsync($"Usuarios/{id}/CambiarEstado", nuevoEstado);
                TempData["Success"] = "Estado del usuario actualizado exitosamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                TempData["Error"] = "Error al cambiar el estado del usuario.";
                return RedirectToAction(nameof(Index));
            }
        }

        private async Task<SelectList> ObtenerRolesSelectList()
        {
            try
            {
                var roles = await _apiService.GetListAsync<Rol>("Roles");
                return new SelectList(roles, "ro_identificador", "ro_decripcion");
            }
            catch
            {
                return new SelectList(new List<Rol>());
            }
        }
    }
}
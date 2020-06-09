using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PWABlog.Models.Blog.Etiqueta;
using PWABlog.RequestModels.AdminEtiquetas;

namespace PWABlog.Controllers.Admin
{
    public class AdminEtiquetasController : Controller
    {
        private readonly EtiquetaOrmService _etiquetaOrmService;

        public AdminEtiquetasController(
            EtiquetaOrmService etiquetaOrmService
            )
        { }

        [HttpGet]
        public IActionResult Listar()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Detalhar()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Criar()
        {
            ViewBag.erro = TempData["erro-msg"];
            return View();
        }

        public RedirectToActionResult Criar(AdminEtiquetasCriarRequestModel request)
        {
            var nome = request.Nome;
            var idCategoria = Convert.ToInt32(request.IdCategoria);

            try
            {
                _etiquetaOrmService.CriarEtiqueta(nome, idCategoria);
            }
            catch (Exception exception)
            {
                TempData["error-msg"] = exception.Message;
                return RedirectToAction("Criar");
            }
            return RedirectToAction("Listar");
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            ViewBag.id = id;
            ViewBag.erro = TempData["erro-msg"];

            return View();
        }

        [HttpPost]

        public RedirectToActionResult Editar(AdminEtiquetasEditarRequestModel request)
        {
            var id = request.Id;
            var nome = request.Nome;
            var idCategoria = Convert.ToInt32(request.IdCategoria);

            try
            {
                _etiquetaOrmService.EditarEtiqueta(id, nome, idCategoria);
            }
            catch (Exception exception)
            {
                TempData["erro-msg"] = exception.Message;
                return RedirectToAction("Editar", new { id = id });
            }

            return RedirectToAction("Listar");
        }

        [HttpGet]
        public IActionResult Remover(int id)
        {
            ViewBag.id = id;
            ViewBag.erro = TempData["erro-msg"];
            return View();

        }

        [HttpPost]
        public RedirectToActionResult Remover(AdminEtiquetasRemoverRequesteModel request)
        {
            var id = request.Id;

            try
            {
                _etiquetaOrmService.RemoverEtiqueta(id);
            }
            catch (Exception exception)
            {
                TempData["Erro-msg"] = exception.Message;
                return RedirectToAction("Remover", new { id = id });
            }
            return RedirectToAction("Listar");
        }

    }
}
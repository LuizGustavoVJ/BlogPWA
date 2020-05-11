using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using PWABlog.Models.Blog.Categoria;
using PWABlog.RequestModels;
using PWABlog.RequestModels.AdminCategorias;

namespace PWABlog.Controllers.Admin
{
    public class AdminCategoriasController : Controller
    {

        private readonly CategoriaOrmService _categoriasOrmService;

        public AdminCategoriasController(
           CategoriaOrmService categoriaOrmService)
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

        [HttpPost]
        public RedirectToActionResult Criar(AdminCategoriasCriarRequestModel request)
        {
            var nome = request.Nome;


            try
            {
                _categoriasOrmService.CriarCategoria(nome);
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
        public RedirectToActionResult Editar(AdminCategoriasEditarRequestModel request)
        {
            var id = request.Id;
            var nome = request.Nome;

            try
            {
                _categoriasOrmService.EditarCategoria(id, nome);
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
        public RedirectToActionResult Remover(AdminCategoriasRemoverRequesteModel request)
        {
            var id = request.Id;

            try
            {
                _categoriasOrmService.RemoverCategoria(id);
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
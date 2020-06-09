using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PWABlog.Models.Blog.Autor;
using PWABlog.RequestModels.AdminAutores;

namespace PWABlog.Controllers.Admin
{
    public class AdminAutoresController : Controller
    {

        private readonly AutorOrmService _autorOrmService;

        public AdminAutoresController(
            AutorOrmService autorOrmService
        )
        {
            _autorOrmService = autorOrmService; 
        }

        [HttpGet]
        [Route("admin/autor")]
        [Route("admin/autores/Listar")]
        public IActionResult Listar()
        {
            return View();
        }

        [HttpGet]
        [Route("admin/autores/{id}")]
        public IActionResult Detalhar(int id)
        {
            return View();
        }

        [HttpGet]
        [Route("admin/autores/criar")]
        public IActionResult Criar()
        {
            ViewBag.erro = TempData["erro-msg"];
            return View();
        }

        [HttpPost]
        [Route("admin/autores/criar")]
        public RedirectToActionResult Criar(AdminAutoresCriarRequestModel request)
        {
            var nome = request.Nome;

            try
            {
                _autorOrmService.CriarAutor(nome);
            }
            catch (Exception exception)
            {

                TempData["erro-msg"] = exception.Message;
                return RedirectToAction("Criar");
            }

            return RedirectToAction("Listar");
        }

        [HttpGet]
        [Route("admin/autores/editar/{id?}")]
        public IActionResult Editar(int id)
        {
            return View();
        }

        [HttpPost]
        [Route("admin/autores/editar/{id}")]
        public RedirectToActionResult Editar(AdminAutoresEditarRequestModel request)
        {
            var id = request.Id;
            var nome = request.Nome;

            try
            {
                _autorOrmService.EditarAutor(id, nome);
            }
            catch (Exception exception)
            {

                TempData["erro-msg"] = exception.Message;
                return RedirectToAction("Editar", new { id = id });
            }

            return RedirectToAction("Listar");
        }

        [HttpGet]
        [Route("admin/autores/remover/{id?}")]
        public IActionResult Remover(int id)
        {
            return View();
        }

        [HttpPost]
        [Route("admin/autores/remover/{id?}")]
        public RedirectToActionResult Remover(AdminAutoresRemoverRequesteModel request)
        {
            var id = request.Id;

            try
            {
                _autorOrmService.RemoverAutor(id);
            }
            catch (Exception exception)
            {
                TempData["erro-msg"] = exception.Message;
                return RedirectToAction("Remover", new {id = id});
            }

            return RedirectToAction("Listar");
        }
    }
}
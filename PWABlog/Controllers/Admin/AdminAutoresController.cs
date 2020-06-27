using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PWABlog.Models.Blog.Autor;
using PWABlog.RequestModels.AdminAutores;
using PWABlog.ViewModels.Admin;

namespace PWABlog.Controllers.Admin
{
    [Authorize]
    public class AdminAutoresController : Controller
    {
        private readonly AutorOrmService _autoresOrmService;

        public AdminAutoresController(AutorOrmService autoresOrmService)
        {
            _autoresOrmService = autoresOrmService;
        }

        [HttpGet]
        [Route("admin/autores")]
        [Route("admin/autores/listar")]
        public IActionResult Listar()
        {
            AdminAutoresListarViewModel model = new AdminAutoresListarViewModel();

            // Obter as Etiquetas
            var listarAtores = _autoresOrmService.ObterAutores();

            foreach (var AutorEntity in listarAtores)
            {
                var autoresAdminAutores = new AutoresAdminAutores();
                autoresAdminAutores.Id = AutorEntity.Id;
                autoresAdminAutores.Nome = AutorEntity.Nome;


                model.Autores.Add(autoresAdminAutores);
            }

            return View(model);
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
            AdminAutoresCriarViewModel model = new AdminAutoresCriarViewModel();

            // Definir possível erro de processamento (vindo do post do criar)
            ViewBag.erro = TempData["erro-msg"];

            return View(model);
        }

        [HttpPost]
        [Route("admin/autores/criar")]
        public RedirectToActionResult Criar(AdminAutoresCriarRequestModel request)
        {
            var nome = request.Nome;

            try
            {
                _autoresOrmService.CriarAutor(nome);
            }
            catch (Exception exception)
            {
                TempData["erro-msg"] = exception.Message;
                return RedirectToAction("Criar");
            }

            return RedirectToAction("Listar");
        }

        [HttpGet]
        [Route("admin/autores/editar/{id}")]
        public IActionResult Editar(int id)
        {
            AdminAutoresEditarViewModel model = new AdminAutoresEditarViewModel();

            // Obter autor a editar
            var autorEditar = _autoresOrmService.ObterAutorPorId(id);
            if (autorEditar == null)
            {
                return RedirectToAction("Listar");
            }

            // Define possível erro no processamento (vindo do post do criar)
            model.Erro = (string)TempData["erro-msg"];

            model.Id = autorEditar.Id;
            model.Nome= autorEditar.Nome;
            model.TituloPagina += model.Nome;

            return View(model);
        }

        [HttpPost]
        [Route("admin/autores/editar/{id}")]
        public RedirectToActionResult Editar(AdminAutoresEditarRequestModel request)
        {
            var id = request.Id;
            var nome = request.Nome;

            try
            {
                _autoresOrmService.EditarAutor(id, nome);
            }
            catch (Exception exception)
            {
                TempData["erro-msg"] = exception.Message;
                return RedirectToAction("Editar", new { id = id });
            }

            return RedirectToAction("Listar");
        }

        [HttpGet]
        [Route("admin/autores/remover/{id}")]
        public IActionResult Remover(int id)
        {
            AdminAutoresRemoverViewModel model = new AdminAutoresRemoverViewModel();

            // Obter etiqueta a editar
            var autorRemover = _autoresOrmService.ObterAutorPorId(id);
            if (autorRemover == null)
            {
                return RedirectToAction("Listar");
            }

            // Alimentar o model com os dados da etiqueta a ser editada
            model.Id = autorRemover.Id;
            model.Nome = autorRemover.Nome;
            model.TituloPagina += model.Nome;

            return View();
        }

        [HttpPost]
        [Route("admin/autores/remover/{id}")]
        public RedirectToActionResult Remover(AdminAutoresRemoverRequestModel request)
        {
            var id = request.Id;

            try
            {
                _autoresOrmService.RemoverAutor(id);
            }
            catch (Exception exception)
            {
                TempData["erro-msg"] = exception.Message;
                return RedirectToAction("Remover", new { id = id });
            }

            return RedirectToAction("Listar");
        }
    }
}
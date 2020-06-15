using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PWABlog.Models.Blog.Postagem;
using PWABlog.RequestModels.AdminPostagens;
using PWABlog.ViewModels.Admin;

namespace PWABlog.Controllers.Admin
{
    [Authorize]
    public class AdminPostagensController : Controller
    {
        private readonly PostagemOrmService _postagemOrmService;

        public AdminPostagensController(
            PostagemOrmService postagemOrmService
        )
        {
            _postagemOrmService = postagemOrmService;
        }

        [HttpGet]
        public IActionResult Listar()
        {
            AdminPostagensListarViewModel model = new AdminPostagensListarViewModel();

            return View(model);
        }

        [HttpGet]
        public IActionResult Detalhar(int id)
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
        public RedirectToActionResult Criar(AdminPostagensCriarRequestModel request)
        {
            var titulo = request.Texto;
            var descricao = request.Descricao;
            var idAutor = request.IdAutor;
            var idCategoria = request.IdCategoria;
            var texto = request.Texto;
            var dataPostagem = DateTime.Parse(request.DataPostagem);

            try
            {
                _postagemOrmService.CriarPostagem(titulo, descricao, idAutor, idCategoria, texto, dataPostagem);
            }
            catch (Exception exception)
            {
                TempData["erro-msg"] = exception.Message;
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
        public RedirectToActionResult Editar(AdminPostagensEditarRequestModel request)
        {
            var id = request.Id;
            var titulo = request.Texto;
            var descricao = request.Descricao;
            var idCategoria = Convert.ToInt32(request.IdCategoria);
            var texto = request.Texto;
            var dataPostagem= DateTime.Parse(request.DataPostagem);

            try
            {
                _postagemOrmService.EditarPostagem(id, titulo, descricao, idCategoria, texto, dataPostagem);
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
        public RedirectToActionResult Remover(AdminPostagensRemoverRequestModel request)
        {
            var id = request.Id;

            try
            {
                _postagemOrmService.RemoverPostagem(id);
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
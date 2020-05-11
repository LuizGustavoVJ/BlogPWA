using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PWABlog.Models.Blog.Postagem.Revisao;
using PWABlog.RequestModels.AdminRevisao;

namespace PWABlog.Controllers.Admin
{
    public class AdminRevisaoController : Controller
    {
        private readonly RevisaoOrmService _revisaoOrmService;

        public AdminRevisaoController(

            RevisaoOrmService revisaoOrmService
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
        public RedirectToActionResult Criar(AdminRevisaoCriarRequestModel request)
        {
            var postagem = request.Postagem;
            var texto = request.Texto;
            var versao = request.Versao;
            var dataCriacao = request.DataCriacao;

            try
            {
                _revisaoOrmService.CriarRevisao(postagem, texto, versao, dataCriacao);
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
        public RedirectToActionResult Editar(AdminRevisaoAlterarRequestModel request)
        {
            var id = request.Id;
            var postagem = request.Postagem;
            var texto = request.Texto;
            var versao = request.Versao;
            var dataCriacao = request.DataCriacao;

            try
            {
                _revisaoOrmService.EditarRevisão(id, postagem, texto, versao, dataCriacao);
            }
            catch (Exception exception)
            {
                TempData["error-msg"] = exception.Message;
                return RedirectToAction("Criar");
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
        public RedirectToActionResult Remover(AdminRevisaoRemoverRequestModel request)
        {
            var id = request.Id;

            try
            {
                _revisaoOrmService.RemoverRevisao(id);
            }
            catch (Exception exception)
            {
                TempData["error-msg"] = exception.Message;
                return RedirectToAction("Criar");
            }
            return RedirectToAction("Listar");
        }


    }
}
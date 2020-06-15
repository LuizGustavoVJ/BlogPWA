using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using PWABlog.Models.Blog.Categoria;
using PWABlog.RequestModels;
using PWABlog.RequestModels.AdminCategorias;
using PWABlog.ViewModels.Admin;

namespace PWABlog.Controllers.Admin
{
    [Authorize]
    public class AdminCategoriasController : Controller
    {
        private readonly CategoriaOrmService _categoriaOrmService;

        public AdminCategoriasController(
          CategoriaOrmService categoriaOrmService
        )
        {
            _categoriaOrmService = categoriaOrmService;
        }

        [HttpGet]
        public IActionResult Listar()
        {
            AdminCategoriasListarViewModel model = new AdminCategoriasListarViewModel();

            // Obter as Categorias
            var listaCategorias = _categoriaOrmService.ObterCategorias();

            // Alimentar o model com as categorias que serão listadas
            foreach (var categoriaEntity in listaCategorias)
            {
                var categoriaAdminEtiquetas = new CategoriaAdminCategorias();
                categoriaAdminEtiquetas.Id = categoriaEntity.Id;
                categoriaAdminEtiquetas.Nome = categoriaEntity.Nome;

                model.Categorias.Add(categoriaAdminEtiquetas);
            }

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
            AdminCategoriasCriarViewModel model = new AdminCategoriasCriarViewModel();

            // Define possível erro no processamento (vindo do post do criar)
            model.Erro = (string)TempData["erro-msg"];

            // Obter as Categorias
            var listaCategorias = _categoriaOrmService.ObterCategorias();

            // Alimentar o model com as categorias que serão colocadas no <select> do formulário
            foreach (var categoriaEntity in listaCategorias)
            {
                var categoriaAdminetiquetas = new CategoriaAdminCategorias();
                categoriaAdminetiquetas.Id = categoriaEntity.Id;
                categoriaAdminetiquetas.Nome = categoriaEntity.Nome;

                model.Categorias.Add(categoriaAdminetiquetas);
            }

            return View(model);
        }

        [HttpPost]
        public RedirectToActionResult Criar(AdminCategoriasCriarRequestModel request)
        {
            var nome = request.Nome;

            try
            {
                _categoriaOrmService.CriarCategoria(nome);
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
            AdminCategoriasEditarViewModel model = new AdminCategoriasEditarViewModel();

            // Obter categoria a Editar
            var categoriaAEditar = _categoriaOrmService.ObterCategoriaPorId(id);
            if (categoriaAEditar == null)
            {
                return RedirectToAction("Listar");
            }

            // Define possível erro no processamento (vindo do post do criar)
            model.Erro = (string)TempData["erro-msg"];

            // Alimentar o model com os dados da categoria a ser editada
            model.IdCategoria = categoriaAEditar.Id;
            model.NomeCategoria = categoriaAEditar.Nome;
            model.TituloPagina += model.NomeCategoria;

            return View(model);
        }

        [HttpPost]
        public RedirectToActionResult Editar(AdminCategoriasEditarRequestModel request)
        {
            var id = request.Id;
            var nome = request.Nome;

            try
            {
                _categoriaOrmService.EditarCategoria(id, nome);
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
                _categoriaOrmService.RemoverCategoria(id);
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
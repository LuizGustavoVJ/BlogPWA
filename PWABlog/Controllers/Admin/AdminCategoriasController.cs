using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using PWABlog.Models.Blog.Categoria;
using PWABlog.Models.Blog.Etiqueta;
using PWABlog.Models.Blog.Postagem;
using PWABlog.RequestModels;
using PWABlog.RequestModels.AdminCategorias;
using PWABlog.ViewModels.Admin;

namespace PWABlog.Controllers.Admin
{
    [Authorize]
    public class AdminCategoriasController : Controller
    {
        private readonly CategoriaOrmService _categoriaOrmService;
        private readonly EtiquetaOrmService _etiquetaOrmService;
        private readonly PostagemOrmService _postagensOrmService;

        public AdminCategoriasController(
            CategoriaOrmService categoriaOrmService,
            EtiquetaOrmService etiquetaOrmService,
            PostagemOrmService postagemOrmService
        )
        {
            _categoriaOrmService = categoriaOrmService;
            _etiquetaOrmService = etiquetaOrmService;
            _postagensOrmService = postagemOrmService;
        }

        [HttpGet]
        public IActionResult Listar()
        {
            AdminCategoriasListarViewModel model = new AdminCategoriasListarViewModel();

            // Obter as Categorias para aparecer na lista
            var listaCategorias = _categoriaOrmService.ObterCategorias();

            // Alimentar o model com as categorias que serão listadas
            foreach (var categoriaEntity in listaCategorias)
            {
                var categoriaAdminCategorias = new CategoriaAdminCategorias();
                categoriaAdminCategorias.Id = categoriaEntity.Id;
                categoriaAdminCategorias.Nome = categoriaEntity.Nome;


                model.Categorias.Add(categoriaAdminCategorias);
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
                TempData["erro-msg"] = exception.Message;
                return RedirectToAction("Criar");
            }

            return RedirectToAction("Listar");
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            AdminCategoriasEditarViewModel model = new AdminCategoriasEditarViewModel();

            //Obter as categorias a editar
            var categoriaEditar = _categoriaOrmService.ObterCategoriaPorId(id);
            if (categoriaEditar == null)
            {
                return RedirectToAction("Listar");
            }
            // Obter as Etiquetas
            var listaEtiquetas = _etiquetaOrmService.ObterEtiquetas();

            // Alimentar o model com as etiquetas que serão colocadas no <select> do formulário
            foreach (var etiquetaEntity in listaEtiquetas)
            {
                var etiquetasAdminCategorias = new EtiquetasAdminCategorias();
                etiquetasAdminCategorias.IdEtiqueta = etiquetaEntity.Id;
                etiquetasAdminCategorias.NomeEtiqueta = etiquetaEntity.Nome;

                model.Etiquetas.Add(etiquetasAdminCategorias);
            }

            // Alimentar o model com as postagens que serão colocadas no <select> do formulário
            var listaPostagens = _postagensOrmService.ObterPostagens();
            foreach (var postagemEntity in listaPostagens)
            {
                var postagensAdminCategorias = new PostagensAdminCategorias();
                postagensAdminCategorias.IdPostagens = postagemEntity.Id;
                postagensAdminCategorias.NomePostagens = postagemEntity.Titulo;


                model.Postagens.Add(postagensAdminCategorias);
            }

            // Alimentar o model com os dados da categoria a ser editada
            model.IdCategoria = categoriaEditar.Id;
            model.NomeCategoria = categoriaEditar.Nome;
            model.IdEtiquetaCategoria = categoriaEditar.Id;
            model.IdPostagensCategoria = categoriaEditar.Id;

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
            AdminCategoriasRemoverViewModel model = new AdminCategoriasRemoverViewModel();

            //obtendo categorias
            var categoriaRemover = _categoriaOrmService.ObterCategoriaPorId(id);

            if (categoriaRemover == null)
            {
                return RedirectToAction("Listar");
            }

            // Alimentar o model com os dados da categoria a ser editada
            model.IdCategoria = categoriaRemover.Id;
            model.NomeCategoria = categoriaRemover.Nome;

            return View(model);
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
                TempData["erro-msg"] = exception.Message;
                return RedirectToAction("Remover", new { id = id });
            }

            return RedirectToAction("Listar");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PWABlog.Models.Blog.Categoria;
using PWABlog.Models.Blog.Etiqueta;
using PWABlog.RequestModels.AdminEtiquetas;
using PWABlog.ViewModels.Admin;

namespace PWABlog.Controllers.Admin
{
    [Authorize]
    public class AdminEtiquetasController : Controller
    {
        private readonly EtiquetaOrmService _etiquetaOrmService;
        private readonly CategoriaOrmService _categoriaOrmService;

        public AdminEtiquetasController(
            EtiquetaOrmService etiquetaOrmService,
            CategoriaOrmService categoriaOrmService
        )
        {
            _etiquetaOrmService = etiquetaOrmService;
            _categoriaOrmService = categoriaOrmService;
        }

        [HttpGet]
        public IActionResult Listar()
        {
            AdminEtiquetasListarViewModel model = new AdminEtiquetasListarViewModel();

            // Obter as Etiquetas
            var listaEtiquetas = _etiquetaOrmService.ObterEtiquetas();

            // Alimentar o model com as etiquetas que serão listadas
            foreach (var etiquetaEntity in listaEtiquetas)
            {
                var etiquetaAdminEtiquetas = new EtiquetaAdminEtiquetas();
                etiquetaAdminEtiquetas.Id = etiquetaEntity.Id;
                etiquetaAdminEtiquetas.Nome = etiquetaEntity.Nome;
                etiquetaAdminEtiquetas.NomeCategoria = etiquetaEntity.Categoria.Nome;

                model.Etiquetas.Add(etiquetaAdminEtiquetas);
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Detalhar()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Criar()
        {
            AdminEtiquetasCriarViewModel model = new AdminEtiquetasCriarViewModel();

            // Define possível erro no processamento (vindo do post do criar)
            model.Erro = (string) TempData["erro-msg"];

            // Obter as Categorias
            var listaCategorias = _categoriaOrmService.ObterCategorias();

            // Alimentar o model com as categorias que serão colocadas no <select> do formulário
            foreach (var categoriaEntity in listaCategorias)
            {
                var categoriaAdminetiquetas= new CategoriaAdminEtiquetas();
                categoriaAdminetiquetas.IdCategoria = categoriaEntity.Id;
                categoriaAdminetiquetas.NomeCategoria = categoriaEntity.Nome;

                model.Categorias.Add(categoriaAdminetiquetas);
            }

            return View(model);
        }

        [HttpPost]
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
                TempData["erro-msg"] = exception.Message;
                return RedirectToAction("Criar");
            }

            return RedirectToAction("Listar");
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            AdminEtiquetasEditarViewModel model = new AdminEtiquetasEditarViewModel();

            // Obter etiqueta a Editar
            var etiquetaAEditar= _etiquetaOrmService.ObterEtiquetaPorId(id);
            if (etiquetaAEditar == null)
            {
                return RedirectToAction("Listar");
            }

            // Define possível erro no processamento (vindo do post do criar)
            model.Erro = (string)TempData["erro-msg"];

            // Obter as Categorias
            var listaCategorias = _categoriaOrmService.ObterCategorias();

            // Alimentar o model com as categorias que serão colocadas no <select> do formulário
            foreach (var categoriaEntity in listaCategorias)
            {
                var categoriaAdminetiquetas = new CategoriaAdminEtiquetas();
                categoriaAdminetiquetas.IdCategoria = categoriaEntity.Id;
                categoriaAdminetiquetas.NomeCategoria = categoriaEntity.Nome;

                model.Categorias.Add(categoriaAdminetiquetas);
            }

            // alimentar o model com os dados da etiqueta a ser editada
            model.IdEtiqueta = etiquetaAEditar.Id;
            model.NomeEtiqueta = etiquetaAEditar.Nome;
            model.IdCategoriaEtiqueta = etiquetaAEditar.Categoria.Id;
            model.TituloPagina += model.NomeEtiqueta;

            return View(model);
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
            AdminEtiquetasRemoverViewModel model = new AdminEtiquetasRemoverViewModel();

            // Obter etiqueta para Remoção
            var etiquetaARemover = _etiquetaOrmService.ObterEtiquetaPorId(id);
            if (etiquetaARemover == null)
            {
                return RedirectToAction("Listar");
            }

            // Define possível erro no processamento (vindo do post do criar)
            model.Erro = (string)TempData["erro-msg"];

            // Alimentar o model com os dados da etiqueta a ser removida
            model.IdEtiqueta = etiquetaARemover.Id;
            model.NomeEtiqueta = etiquetaARemover.Nome;
            model.TituloPagina += model.NomeEtiqueta;

            return View(model);

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
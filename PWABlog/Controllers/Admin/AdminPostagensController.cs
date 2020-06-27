using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PWABlog.Models.Blog.Autor;
using PWABlog.Models.Blog.Categoria;
using PWABlog.Models.Blog.Etiqueta;
using PWABlog.Models.Blog.Postagem;
using PWABlog.RequestModels.AdminPostagens;
using PWABlog.ViewModels.Admin;

namespace PWABlog.Controllers.Admin
{
    [Authorize]
    public class AdminPostagensController : Controller
    {
        private readonly PostagemOrmService _postagemOrmService;
        private readonly CategoriaOrmService _categoriaOrmService;
        private readonly AutorOrmService _autoresOrmService;
        private readonly EtiquetaOrmService _etiquetaOrmService;

        public AdminPostagensController(
            PostagemOrmService postagemOrmService,
            AutorOrmService autoresOrmService,
             CategoriaOrmService categoriaOrmService,
             EtiquetaOrmService etiquetaOrmService
        )
        {
            _postagemOrmService = postagemOrmService;
            _autoresOrmService = autoresOrmService;
            _categoriaOrmService = categoriaOrmService;
            _etiquetaOrmService = etiquetaOrmService;
        }

        [HttpGet]
        public IActionResult Listar()
        {
            AdminPostagensListarViewModel model = new AdminPostagensListarViewModel();

            //Obter as Postagens
            var listarPostagens = _postagemOrmService.ObterPostagens();

            //Alimentar a model com as postagens que serão Listadas
            foreach (var postagemEntity in listarPostagens)
            {
                var postagemAdminPostagens = new PostagemAdminPostagens();
                postagemAdminPostagens.IdPostagem = postagemEntity.Id;
                postagemAdminPostagens.NomePostagem = postagemEntity.Descricao;
                postagemAdminPostagens.NomeAutor = postagemEntity.Autor.Nome;
                postagemAdminPostagens.NomeCategoria = postagemEntity.Categoria.Nome;
                postagemAdminPostagens.DataPostagem = postagemEntity.DataPostagem;

                model.Postagens.Add(postagemAdminPostagens);

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
            AdminPostagensCriarViewModel model = new AdminPostagensCriarViewModel();

            ViewBag.erro = TempData["erro-msg"];
            model.Erro = (string)TempData["erro-msg"];

            // Obter categoria da postagem
            var listaCategorias = _categoriaOrmService.ObterCategorias();

            // Alimentar o model com as categorias que serão colocadas no <select> do formulário
            foreach (var categoriaEntity in listaCategorias)
            {
                var categoriaAdmPostagens = new CategoriaAdminPostagens();
                categoriaAdmPostagens.IdCategorias = categoriaEntity.Id;
                categoriaAdmPostagens.NomeCategorias = categoriaEntity.Nome;

                model.Categorias.Add(categoriaAdmPostagens);
            }

            // Obter autor da postagem
            var listaAutores = _autoresOrmService.ObterAutores();

            // Alimentar o model com os autores que serão colocadas no <select> do formulário
            foreach (var autorEntity in listaAutores)
            {
                var autorAdminPostagens = new AutorAdminPostagens();
                autorAdminPostagens.IdAutores = autorEntity.Id;
                autorAdminPostagens.NomeAutores = autorEntity.Nome;

                model.Autores.Add(autorAdminPostagens);
            }

            // Obter etiqueta da postagem
            var listaEtiquetas = _etiquetaOrmService.ObterEtiquetas();

            // Alimentar o model com as etiquetas que serão colocadas no <select> do formulário
            foreach (var etiquetaEntity in listaEtiquetas)
            {
                var etiquetaAdminPostagens = new EtiquetaAdminPostagens();
                etiquetaAdminPostagens.IdEtiqueta = etiquetaEntity.Id;
                etiquetaAdminPostagens.NomeEtiqueta = etiquetaEntity.Nome;

                model.Etiquetas.Add(etiquetaAdminPostagens);
            }

            return View(model);
        }

        [HttpPost]
        public RedirectToActionResult Criar(AdminPostagensCriarRequestModel request)
        {
            var titulo = request.Titulo;
            var descricao = request.Descricao;
            var idAutor = request.idAutor;
            var idCategoria = request.idCategoria;
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
            AdminPostagensEditarViewModel model = new AdminPostagensEditarViewModel();
                       
            // Definir possível erro de processamento (vindo do post do criar)
            model.Erro = (string)TempData["erro-msg"];

            // Obter etiqueta a editar
            var postagemEditar = _postagemOrmService.ObterPostagemPorId(id);
            if (postagemEditar == null)
            {
                return RedirectToAction("Listar");
            }

            // Obter as Categorias
            var listaCategorias = _categoriaOrmService.ObterCategorias();

            // Alimentar o model com as categorias que serão colocadas no <select> do formulário
            foreach (var categoriaEntity in listaCategorias)
            {
                var categoriaAdminPostagem = new CategoriaAdminPostagens();
                categoriaAdminPostagem.IdCategorias = categoriaEntity.Id;
                categoriaAdminPostagem.NomeCategorias = categoriaEntity.Nome;

                model.Categorias.Add(categoriaAdminPostagem);
            }

            //Obter Autores
            var listaAutores = _autoresOrmService.ObterAutores();

            //Alimentar o model com os autores que serão colocadas no select
            foreach (var autorEntity in listaAutores)
            {
                var autorAdminPostagens = new AutorAdminPostagens();
                autorAdminPostagens.IdAutores = autorEntity.Id;
                autorAdminPostagens.NomeAutores = autorEntity.Nome;

                model.Autores.Add(autorAdminPostagens);
            }

            // Alimentar o model com os dados da etiqueta a ser editada
            model.IdPostagem = postagemEditar.Id;
            model.NomePostagem = postagemEditar.Descricao;
            model.IdAutorPostagem = postagemEditar.Autor.Id;
            model.IdCategoriaPostagem = postagemEditar.Categoria.Id;
            model.TituloPagina += model.NomePostagem;

            return View(model);
        }

        [HttpPost]
        public RedirectToActionResult Editar(AdminPostagensEditarRequestModel request)
        {
            var id = request.Id;
            var titulo = request.Texto;
            var descricao = request.Descricao;
            var CategoriaId = Convert.ToInt32(request.CategoriaId);
            var texto = request.Texto;
            var dataExibicao = DateTime.Parse(request.DataPostagem);

            try
            {
                _postagemOrmService.EditarPostagem(id, titulo, descricao, CategoriaId, texto, dataExibicao);
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
            AdminPostagensRemoverViewModel model = new AdminPostagensRemoverViewModel();

            // Obter etiqueta a remover
            var postagemRemover = _postagemOrmService.ObterPostagemPorId(id);
            if (postagemRemover == null)
            {
                return RedirectToAction("Listar");
            }

            // Definir possível erro de processamento (vindo do post do criar)
            model.Erro = (string)TempData["erro-msg"];

            // Alimentar o model com os dados da etiqueta a ser editada
            model.IdPostagem = postagemRemover.Id;
            model.NomePostagem = postagemRemover.Descricao;
            model.TituloPagina += model.NomePostagem;

            return View(model);
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
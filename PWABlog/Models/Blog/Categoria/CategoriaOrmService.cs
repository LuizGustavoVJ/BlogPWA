using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PWABlog.Models.Blog.Categoria
{
    public class CategoriaOrmService
    {
        private readonly DatabaseContext _databaseContext;

        public CategoriaOrmService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public List<CategoriaEntity> ObterCategorias()
        {
            return _databaseContext.Categorias.ToList();
        }

        public CategoriaEntity ObterCategoriaPorId(int idCategoria)
        {
            var categoria = _databaseContext.Categorias.Find(idCategoria);

            return categoria;
        }

        public List<CategoriaEntity> PesquisarCategoriasPorNome(string nomeCategoria)
        {
            return _databaseContext.Categorias.Where(c => c.Nome.Contains(nomeCategoria)).ToList();
            
        }

        public CategoriaEntity CriarCategoria(string nome)
        {
            var novaCategoria = new CategoriaEntity { Nome = nome };
            _databaseContext.Categorias.Add(novaCategoria);
            _databaseContext.SaveChanges();

            return novaCategoria;

        }

        public CategoriaEntity EditarCategoria(int id, string nome)
        {
            // obter a categoria para edição
            var categoria = _databaseContext.Categorias.Find(id);

            if (categoria == null)
            {
                throw new Exception("Categoria não encontrada");
            }

            // Atualizar os dados da categoria
            categoria.Nome = nome;
            _databaseContext.SaveChanges();

            return categoria;
        }

        public bool RemoverCategoria(int id)
        {
            // obter a categoria para remoção
            var categoria = _databaseContext.Categorias.Find(id);

            if (categoria == null)
            {
                throw new Exception("Categoria não encontrada");
            }

            // Remover a categoria
            _databaseContext.Categorias.Remove(categoria);
            _databaseContext.SaveChanges();

            return true;

        }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWABlog.Models.Blog.Autor
{
    public class AutorOrmService
    {
        private readonly DatabaseContext _databaseContext;

        public AutorOrmService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public List<AutorEntity> ObterAutores()
        {
            return _databaseContext.Autores.ToList();

        }

        public AutorEntity ObterAutorPorId(int idAutor)
        {
            var autores = _databaseContext.Autores.Find(idAutor);

            return autores;
        }

        public List<AutorEntity> PesquisarAutorPorNome(string nomeAutor)
        {
            return _databaseContext.Autores.Where(c => c.Nome.Contains(nomeAutor)).ToList();

        }

        public AutorEntity CriarAutor(string nome)
        {
            var novoAutor = new AutorEntity { Nome = nome };
            _databaseContext.Autores.Add(novoAutor);
            _databaseContext.SaveChanges();

            return novoAutor;

        }

        public AutorEntity EditarAutor(int id, string nome)
        {
            // obter o autor para edição
            var autor = _databaseContext.Autores.Find(id);

            if (autor == null)
            {
                throw new Exception("Autor não encontrado");
            }

            // Atualizar os dados do autor
            autor.Nome = nome;
            _databaseContext.SaveChanges();

            return autor;
        }

        public bool RemoverAutor(int id)
        {
            // obter o autor para remoção
            var autor = _databaseContext.Autores.Find(id);

            if (autor == null)
            {
                throw new Exception("Autor não encontrado");
            }

            // Remover o autor
            _databaseContext.Autores.Remove(autor);
            _databaseContext.SaveChanges();

            return true;

        }
    }
}

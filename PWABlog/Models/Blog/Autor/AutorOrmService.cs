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

        public AutorEntity ObterAutorPorId(int idCategoria)
        {
            var autor = _databaseContext.Autores.Find(idCategoria);

            return autor;
        }

        public List<AutorEntity> PesquisarAutoresPorNome(string nomeAutor)
        {
            return _databaseContext.Autores.Where(c => c.Nome.Contains(nomeAutor)).ToList();
        }
    }
}

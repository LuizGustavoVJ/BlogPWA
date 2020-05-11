using Microsoft.EntityFrameworkCore;
using PWABlog.Models.Blog.Categoria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWABlog.Models.Blog.Etiqueta
{
    public class EtiquetaOrmService
    {
        private readonly DatabaseContext _databaseContext;

        public EtiquetaOrmService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }


        public List<EtiquetaEntity> ObterEtiquetas()
        {
            return _databaseContext.Etiquetas.ToList();
        }

        public EtiquetaEntity CriarEtiqueta(string nome, CategoriaEntity categoria)
        {
            var novaEtiqueta = new EtiquetaEntity { Nome = nome, Categoria = categoria };
            _databaseContext.Etiquetas.Add(novaEtiqueta);
            _databaseContext.SaveChanges();

            return novaEtiqueta;

        }

        public EtiquetaEntity EditarEtiqueta(int id, string nome, CategoriaEntity categoria)
        {
            var etiqueta = _databaseContext.Etiquetas.Find(id);
            if (etiqueta == null)
            {
                throw new Exception("Etiqueta não encontrada");
            }
            etiqueta.Nome = nome;
            etiqueta.Categoria = categoria;
            _databaseContext.SaveChanges();

            return etiqueta;
        }

        public bool RemoverEtiqueta(int id)
        {
            var etiqueta = _databaseContext.Etiquetas.Find(id);
            if (etiqueta == null)
            {
                throw new Exception("Etiqueta não encontrada");
            }

            _databaseContext.Etiquetas.Remove(etiqueta);
            _databaseContext.SaveChanges();

            return true;

        }

        public EtiquetaOrmService()
        {

        }
    }
}

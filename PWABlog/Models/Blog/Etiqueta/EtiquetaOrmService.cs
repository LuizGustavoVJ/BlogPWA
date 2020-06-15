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
            return _databaseContext.Etiquetas
                .Include(e => e.Categoria)
                .ToList();
        }

        public EtiquetaEntity ObterEtiquetaPorId(int idEtiqueta)
        {
            var etiqueta = _databaseContext.Etiquetas.Find(idEtiqueta);

            return etiqueta;
        }

        public List<EtiquetaEntity> PesquisarEtiquetaPorNome(string nomeEtiqueta)
        {
            return _databaseContext.Etiquetas.Where(c => c.Nome.Contains(nomeEtiqueta)).ToList();

        }

        public EtiquetaEntity CriarEtiqueta(string nome, int idCategoria)
        {
            // Verifica a existencia do Nome da Etiqueta
            if (nome == null)
            {
                throw new Exception("O nome para a Etiqueta não foi informado!");
            }

            // Verifica a existencia da Categoria da Etiqueta
            var categoria = _databaseContext.Categorias.Find(idCategoria);

            if (categoria == null)
            {
                throw new Exception("A Categoria informada para a Etiqueta não existe!");
            }

            //Criar nova Etiqueta
            var novaEtiqueta = new EtiquetaEntity
            { 
                Nome = nome,
                Categoria = categoria 
            };

            _databaseContext.Etiquetas.Add(novaEtiqueta);
            _databaseContext.SaveChanges();

            return novaEtiqueta;

        }

        public EtiquetaEntity EditarEtiqueta(int id, string nome, int idCategoria)
        {
            // obter a etiqueta para edição
            var etiqueta = _databaseContext.Etiquetas.Find(id);

            if (etiqueta == null)
            {
                throw new Exception("Etiqueta não encontrada");
            }

            // Verifica  existencia da Categoria da Etiqueta
            var categoria = _databaseContext.Categorias.Find(idCategoria);

            if (categoria == null)
            {
                throw new Exception("A Categoria informada para a Etiqueta não existe!");
            }

            // Atualizar os dados da etiqueta
            etiqueta.Nome = nome;
            etiqueta.Categoria = categoria;
            _databaseContext.SaveChanges();

            return etiqueta;
        }

        public bool RemoverEtiqueta(int id)
        {
            // obter a etiqueta para remoção
            var etiqueta = _databaseContext.Etiquetas.Find(id);

            if (etiqueta == null)
            {
                throw new Exception("Etiqueta não encontrada");
            }

            // Remover a etiqueta
            _databaseContext.Etiquetas.Remove(etiqueta);
            _databaseContext.SaveChanges();

            return true;

        }
    }
}

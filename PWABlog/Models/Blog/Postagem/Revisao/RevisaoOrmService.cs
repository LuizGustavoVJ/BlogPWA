using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWABlog.Models.Blog.Postagem.Revisao
{
    public class RevisaoOrmService
    {
        private readonly DatabaseContext _databaseContext;

        public RevisaoOrmService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public List<RevisaoEntity> ObterRevisao()
        {
            return _databaseContext.Revisoes.ToList();
        }

        public RevisaoEntity CriarRevisao(PostagemEntity postagem, string texto, int versao, DateTime dataCriacao)
        {
            var revisao = new RevisaoEntity { Postagem = postagem, Texto = texto, Versao = versao, DataCriacao = dataCriacao };
            _databaseContext.Revisoes.Add(revisao);
            _databaseContext.SaveChanges();

            return revisao;
        }

        public RevisaoEntity EditarRevisão(int id, PostagemEntity postagem, string texto, int versao, DateTime dataCriacao)
        {
            var revisao = _databaseContext.Revisoes.Find(id);

            if (revisao == null)
            {
                throw new Exception("Revisão não encontrada");
            }
            revisao.Postagem = postagem;
            revisao.Texto = texto;
            revisao.Versao = versao;
            revisao.DataCriacao = dataCriacao;

            _databaseContext.SaveChanges();

            return revisao;
        }

        public bool RemoverRevisao(int id)
        {
            var revisao = _databaseContext.Revisoes.Find(id);
            if (revisao == null)
            {
                throw new Exception("Revisão não encontrada");
            }
            _databaseContext.Revisoes.Remove(revisao);
            _databaseContext.SaveChanges();

            return true;

        }

        public RevisaoOrmService()
        {

        }
    }
}

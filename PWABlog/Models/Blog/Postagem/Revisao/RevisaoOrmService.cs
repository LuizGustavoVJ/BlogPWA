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

        public RevisaoEntity ObterRevisaoPorId(int id)
        {
            // Obter a revisão 
            var revisao = _databaseContext.Revisoes.Find(id);

            return revisao;
        }

        public RevisaoEntity CriarRevisao(int IdPostagem, string texto)
        {
            // Verifica a Existencia de Postagem da Revisão
            var postagem = _databaseContext.Postagens.Find(IdPostagem);

            if (postagem == null)
            {
                throw new Exception("A Postagem informada para a Revisão não existe!");
            }

            // Criar nova Revisão
            var novaRevisao = new RevisaoEntity
            {
                Postagem = postagem,
                Texto = texto,
                Versao = postagem.ObterUltimaRevisao().Versao + 1,
                DataCriacao = new DateTime()
            };

            _databaseContext.Revisoes.Add(novaRevisao);
            _databaseContext.SaveChanges();

            return novaRevisao;
        }

        public RevisaoEntity EditarRevisão(int id, string texto)
        {
            // obter a revisão para edição
            var revisao = _databaseContext.Revisoes.Find(id);

            if (revisao == null)
            {
                throw new Exception("Revisão não encontrada");
            }

            // Atualiza os dados da revisão
            revisao.Id = id;
            revisao.Texto = texto;

            _databaseContext.SaveChanges();

            return revisao;
        }

        public bool RemoverRevisao(int id)
        {
            // Obter revisão a ser removida
            var revisao = _databaseContext.Revisoes.Find(id);

            if (revisao == null)
            {
                throw new Exception("Revisão não encontrada");
            }

            // Remover a Revisão
            _databaseContext.Revisoes.Remove(revisao);
            _databaseContext.SaveChanges();

            return true;
        }
    }
}

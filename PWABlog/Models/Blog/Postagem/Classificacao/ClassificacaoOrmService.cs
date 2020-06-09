using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWABlog.Models.Blog.Postagem.Classificacao
{
    public class ClassificacaoOrmService
    {
        private readonly DatabaseContext _databaseContext;

        public ClassificacaoOrmService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public ClassificacaoEntity ObterClassificacaoPorId(int id)
        {
            var classificacao = _databaseContext.Classificacoes.Find(id);

            return classificacao;
        }

        //public ClassificacaoEntity CriarClassificacao(int idPostagem, bool classsificacaoDada)
        //{
        //    return "";
        //}

        //public ClassificacaoEntity EditarClasssificacao(int id, bool classsificacaoDada)
        //{

        //}

        //public ClassificacaoEntity RemoverClasssificacao(int id, bool classsificacaoDada)
        //{

        //}
    }
}

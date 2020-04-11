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

        public List<ClassificacaoEntity> ObterClassificacoes()
        {
            // INÍCIO DOS EXEMPLOS

            /**********************************************************************************************************/
            /*** OBTER UM ÚNICO OBJETO                                                                                */
            /**********************************************************************************************************/

            // First = Obter a primeira categoria retornada pela consulta
            //var primeiraCategoria = _databaseContext.Categorias.First();

            // FirstOrDefault = Mesmo do First, porém retorna nulo caso não encontre nenhuma
            var primeiraClassficacaoOuNulo = _databaseContext.Classificacoes.FirstOrDefault();

            // Single = Obter um único registro do banco de dados
            // var algumaCategoriaEspecifica = _databaseContext.Categorias.Single(c => c.Id == 3);

            // SingleOrDefault = Mesmo do Sigle, porém retorna nulo caso não encontre nenhuma
            var algumaClassificacaoOuNulo = _databaseContext.Classificacoes.SingleOrDefault(c => c.Id == 3);

            // Find = Equivalente ao SingleOrDefault, porém fazendo uma busca por uma propriedade chave
            var encontrarClassificacao = _databaseContext.Classificacoes.Find(3);


            /**********************************************************************************************************/
            /*** OBTER MÚLTIPLOS OBJETOS                                                                              */
            /**********************************************************************************************************/

            // ToList
            var todasClassificacoes = _databaseContext.Classificacoes.ToList();

            /**************************/
            /* ENTIDADES RELACIONADAS */
            /**************************/

            var classificacoesESuaPostagem = _databaseContext.Classificacoes
                .Include(c => c.Postagem)
                .ToList();

            // FIM DOS EXEMPLOS



            // Código de fato necessário para o método
            return _databaseContext.Classificacoes
                .Include(c => c.Postagem)
                .ToList();
        }

        public ClassificacaoEntity ObterClassificacaoPorId(int idClassificacao)
        {
            var classificacao = _databaseContext.Classificacoes.Find(idClassificacao);

            return classificacao;
        }
    }
}

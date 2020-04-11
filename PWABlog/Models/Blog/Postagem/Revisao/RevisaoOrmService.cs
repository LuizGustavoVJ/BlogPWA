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
            // INÍCIO DOS EXEMPLOS

            /**********************************************************************************************************/
            /*** OBTER UM ÚNICO OBJETO                                                                                */
            /**********************************************************************************************************/

            // First = Obter a primeira categoria retornada pela consulta
            //var primeiraCategoria = _databaseContext.Categorias.First();

            // FirstOrDefault = Mesmo do First, porém retorna nulo caso não encontre nenhuma
            var primeiraRevisaoOuNulo = _databaseContext.Revisoes.FirstOrDefault();

            // Single = Obter um único registro do banco de dados
            // var algumaCategoriaEspecifica = _databaseContext.Categorias.Single(c => c.Id == 3);

            // SingleOrDefault = Mesmo do Sigle, porém retorna nulo caso não encontre nenhuma
            var algumaRevisaoOuNulo = _databaseContext.Revisoes.SingleOrDefault(c => c.Id == 3);

            // Find = Equivalente ao SingleOrDefault, porém fazendo uma busca por uma propriedade chave
            var encontrarRevisao = _databaseContext.Revisoes.Find(3);


            /**********************************************************************************************************/
            /*** OBTER MÚLTIPLOS OBJETOS                                                                              */
            /**********************************************************************************************************/

            // ToList
            var todasRevisoes = _databaseContext.Revisoes.ToList();


            /***********/
            /* FILTROS */
            /***********/

            var revisoesComALetraG = _databaseContext.Revisoes.Where(c => c.Texto.StartsWith("G")).ToList();
            var revisoesComALetraMouL = _databaseContext.Revisoes
                .Where(c => c.Texto.StartsWith("M") || c.Texto.StartsWith("L"))
                .ToList();



            /*************/
            /* ORDENAÇÃO */
            /*************/

            var revisoesEmOrdemAlfabetica = _databaseContext.Revisoes.OrderBy(c => c.Texto).ToList();
            var revisoesEmOrdemAlfabeticaInversa = _databaseContext.Revisoes.OrderByDescending(c => c.Texto).ToList();


            /**************************/
            /* ENTIDADES RELACIONADAS */
            /**************************/

            var revisoesESuaPostagem = _databaseContext.Revisoes
                .Include(c => c.Postagem);

            // FIM DOS EXEMPLOS



            // Código de fato necessário para o método
            return _databaseContext.Revisoes
                .Include(c => c.Postagem)
                .ToList();
        }

        public RevisaoEntity ObterRevisaoPorId(int idRevisao)
        {
            var revisao = _databaseContext.Revisoes.Find(idRevisao);

            return revisao;
        }

        /*public List<RevisaoEntity> PesquisaRevisoesPorNome(string nomeRevisao)
        {
            return _databaseContext.Categorias.Where(c => c.Nome.Contains(nomeCategoria)).ToList();

        }*/
    }
}

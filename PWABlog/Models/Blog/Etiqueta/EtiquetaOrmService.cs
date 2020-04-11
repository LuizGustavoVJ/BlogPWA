using Microsoft.EntityFrameworkCore;
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

        public List<EtiquetaEntity> ObterEtiqueta()
        {
            // INÍCIO DOS EXEMPLOS

            /**********************************************************************************************************/
            /*** OBTER UM ÚNICO OBJETO                                                                                */
            /**********************************************************************************************************/

            // First = Obter a primeira categoria retornada pela consulta
            //var primeiraCategoria = _databaseContext.Categorias.First();

            // FirstOrDefault = Mesmo do First, porém retorna nulo caso não encontre nenhuma
            var primeiraEtiquetaOuNulo = _databaseContext.Etiquetas.FirstOrDefault();

            // Single = Obter um único registro do banco de dados
            // var algumaCategoriaEspecifica = _databaseContext.Categorias.Single(c => c.Id == 3);

            // SingleOrDefault = Mesmo do Sigle, porém retorna nulo caso não encontre nenhuma
            var algumaEtiquetaOuNulo = _databaseContext.Etiquetas.SingleOrDefault(c => c.Id == 3);

            // Find = Equivalente ao SingleOrDefault, porém fazendo uma busca por uma propriedade chave
            var encontrarEtiqueta = _databaseContext.Etiquetas.Find(3);


            /**********************************************************************************************************/
            /*** OBTER MÚLTIPLOS OBJETOS                                                                              */
            /**********************************************************************************************************/

            // ToList
            var todasEtiquetas = _databaseContext.Etiquetas.ToList();


            /***********/
            /* FILTROS */
            /***********/

            var etiquetasComALetraG = _databaseContext.Etiquetas.Where(c => c.Nome.StartsWith("G")).ToList();
            var etiquetasComALetraMouL = _databaseContext.Etiquetas
                .Where(c => c.Nome.StartsWith("M") || c.Nome.StartsWith("L"))
                .ToList();



            /*************/
            /* ORDENAÇÃO */
            /*************/

            var etiquetasEmOrdemAlfabetica = _databaseContext.Etiquetas.OrderBy(c => c.Nome).ToList();
            var etiquetasEmOrdemAlfabeticaInversa = _databaseContext.Etiquetas.OrderByDescending(c => c.Nome).ToList();


            /**************************/
            /* ENTIDADES RELACIONADAS */
            /**************************/

            var etiquetasESuaCategoria = _databaseContext.Etiquetas
                .Include(c => c.Categoria);
            // FIM DOS EXEMPLOS



            // Código de fato necessário para o método
            return _databaseContext.Etiquetas
                .Include(c => c.Categoria)
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
    }
}

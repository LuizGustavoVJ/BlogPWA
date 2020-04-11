using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWABlog.Models.Blog.Postagem.Comentario
{
    public class ComentarioOrmService
    {
        private readonly DatabaseContext _databaseContext;

        public ComentarioOrmService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public List<ComentarioEntity> ObterComentarios()
        {
            // INÍCIO DOS EXEMPLOS

            /**********************************************************************************************************/
            /*** OBTER UM ÚNICO OBJETO                                                                                */
            /**********************************************************************************************************/

            // First = Obter a primeira categoria retornada pela consulta
            //var primeiraCategoria = _databaseContext.Categorias.First();

            // FirstOrDefault = Mesmo do First, porém retorna nulo caso não encontre nenhuma
            var primeiroComentarioOuNulo = _databaseContext.Comentarios.FirstOrDefault();

            // Single = Obter um único registro do banco de dados
            // var algumaCategoriaEspecifica = _databaseContext.Categorias.Single(c => c.Id == 3);

            // SingleOrDefault = Mesmo do Sigle, porém retorna nulo caso não encontre nenhuma
            var algumComentarioOuNulo = _databaseContext.Comentarios.SingleOrDefault(c => c.Id == 3);

            // Find = Equivalente ao SingleOrDefault, porém fazendo uma busca por uma propriedade chave
            var encontrarComentario = _databaseContext.Comentarios.Find(3);


            /**********************************************************************************************************/
            /*** OBTER MÚLTIPLOS OBJETOS                                                                              */
            /**********************************************************************************************************/

            // ToList
            var todosComentarios = _databaseContext.Comentarios.ToList();


            /***********/
            /* FILTROS */
            /***********/

            var comentariosComALetraG = _databaseContext.Comentarios.Where(c => c.Texto.StartsWith("G")).ToList();
            var comentariosComALetraMouL = _databaseContext.Comentarios
                .Where(c => c.Texto.StartsWith("M") || c.Texto.StartsWith("L"))
                .ToList();



            /*************/
            /* ORDENAÇÃO */
            /*************/

            var comentariosEmOrdemAlfabetica = _databaseContext.Comentarios.OrderBy(c => c.Texto).ToList();
            var comentariosEmOrdemAlfabeticaInversa = _databaseContext.Comentarios.OrderByDescending(c => c.Texto).ToList();


            /**************************/
            /* ENTIDADES RELACIONADAS */
            /**************************/

            var comentariosESuaPostagem = _databaseContext.Comentarios
                .Include(c => c.Postagem)
                .ToList();

            // FIM DOS EXEMPLOS



            // Código de fato necessário para o método
            return _databaseContext.Comentarios
                .Include(c => c.Postagem)
                .ToList();
        }

        public ComentarioEntity ObterComentarioPorId(int idComentario)
        {
            var comentario = _databaseContext.Comentarios.Find(idComentario);

            return comentario;
        }

        public List<ComentarioEntity> PesquisarComentarioPorNome(string TextoComentario)
        {
            return _databaseContext.Comentarios.Where(c => c.Texto.Contains(TextoComentario)).ToList();

        }
    }
}

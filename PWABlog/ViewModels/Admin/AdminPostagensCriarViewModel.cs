using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWABlog.ViewModels.Admin
{
    public class AdminPostagensCriarViewModel : ViewModelAreaAdministrativa
    {
        public string Erro { get; set; }

        public ICollection<AutorAdminPostagens> Autores { get; set; }

        public ICollection<CategoriaAdminPostagens> Categorias { get; set; }

        public AdminPostagensCriarViewModel()
        {
            TituloPagina = "Criar nova Etiqueta";
            Categorias = new List<CategoriaAdminPostagens>();
            Autores = new List<AutorAdminPostagens>();
        }

    }

    public class CategoriaAdminPostagens
    {
        public int IdCategorias { get; set; }
        public string NomeCategorias { get; set; }
    }

    public class AutorAdminPostagens
    {
        public int IdAutores { get; set; }
        public string NomeAutores { get; set; }
    }
}

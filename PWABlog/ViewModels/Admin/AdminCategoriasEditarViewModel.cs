using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWABlog.ViewModels.Admin
{
    public class AdminCategoriasEditarViewModel : ViewModelAreaAdministrativa
    {
        public int IdCategoria { get; set; }

        public string NomeCategoria { get; set; }

        public string Erro { get; set; }

        public ICollection<CategoriaAdminCategorias> Categorias { get; set; }

        public AdminCategoriasEditarViewModel()
        {
            TituloPagina = "Editar Categoria: ";

            Categorias = new List<CategoriaAdminCategorias>();
        }
    }
}

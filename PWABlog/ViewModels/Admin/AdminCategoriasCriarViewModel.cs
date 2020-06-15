using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWABlog.ViewModels.Admin
{
    public class AdminCategoriasCriarViewModel : ViewModelAreaAdministrativa
    {
        public string Erro { get; set; }

        public ICollection<CategoriaAdminCategorias> Categorias { get; set; }

        public AdminCategoriasCriarViewModel()
        {
            TituloPagina = "Categorias - Administrador";

            Categorias = new List<CategoriaAdminCategorias>();
        }
    }
}

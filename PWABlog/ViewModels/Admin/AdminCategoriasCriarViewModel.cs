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

        public ICollection<EtiquetasAdminCategorias> Etiquetas { get; set; }

        public ICollection<PostagensAdminCategorias> Postagens { get; set; }

        public AdminCategoriasCriarViewModel()
        {
            TituloPagina = "Criar nova Etiqueta";
            Etiquetas = new List<EtiquetasAdminCategorias>();
            Postagens = new List<PostagensAdminCategorias>();
        }

    }

    public class EtiquetasAdminCategorias
    {
        public int IdEtiqueta { get; set; }
        public string NomeEtiqueta { get; set; }
    }

    public class PostagensAdminCategorias
    {
        public int IdPostagens { get; set; }
        public string NomePostagens { get; set; }
    }
}

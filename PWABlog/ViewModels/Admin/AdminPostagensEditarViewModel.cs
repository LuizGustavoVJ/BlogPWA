using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWABlog.ViewModels.Admin
{
    public class AdminPostagensEditarViewModel : ViewModelAreaAdministrativa
    {
        public string Erro { get; set; }

        public int IdPostagem { get; set; }

        public string NomePostagem { get; set; }

        public DateTime DataPostagem { get; set; }

        public int IdCategoriaPostagem { get; set; }

        public int IdAutorPostagem { get; set; }

        public ICollection<CategoriaAdminPostagens> Categorias { get; set; }

        public ICollection<AutorAdminPostagens> Autores { get; set; }

        public ICollection<EtiquetaAdminPostagens> Etiquetas { get; set; }

        public AdminPostagensEditarViewModel()
        {
            TituloPagina = "Criar nova Postagem";
            Categorias = new List<CategoriaAdminPostagens>();
            Autores = new List<AutorAdminPostagens>();
            Etiquetas = new List<EtiquetaAdminPostagens>();
        }
    }
}

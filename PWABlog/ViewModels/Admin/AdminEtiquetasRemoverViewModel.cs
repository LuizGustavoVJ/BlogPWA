using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWABlog.ViewModels.Admin
{
    public class AdminEtiquetasRemoverViewModel : ViewModelAreaAdministrativa
    {
        public int IdEtiqueta { get; set; }

        public string NomeEtiqueta { get; set; }

        public string Erro { get; set; }

        public AdminEtiquetasRemoverViewModel()
        {
            TituloPagina = "Remover Etiqueta: ";
        }
    }
}

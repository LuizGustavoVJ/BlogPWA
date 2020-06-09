using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWABlog.ViewModels.Admin
{
    public class AdminEtiquetasListarViewModel : ViewModelAreaAdministrativa
    {
        public ICollection<EtiquetaAdminEtiquetas> Etiquetas { get; set; }

        public AdminEtiquetasListarViewModel()
        {
            TituloPagina = "Etiquetas - Administrador";
            Etiquetas = new List<EtiquetaAdminEtiquetas>();
        }
    }

    public class EtiquetaAdminEtiquetas
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string NomeCategoria { get; set; }
    }
}

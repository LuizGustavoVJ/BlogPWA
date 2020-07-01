using System;
using System.Collections.Generic;

namespace PWABlog.ViewModels.Admin
{
    public class AdminPainelViewModel : ViewModelAreaAdministrativa
    {
        public ICollection<PainelAdminPainel> Postagens { get; set; }

        public AdminPainelViewModel()
        {
            TituloPagina = "Painel - Administrador";
            Postagens = new List<PainelAdminPainel>();
        }
    }

    public class PainelAdminPainel
    {
        public int IdPostagem { get; set; }

        public string NomePostagem { get; set; }

        public DateTime DataPostagem { get; set; }

        public string NomeCategoria { get; set; }

        public string NomeAutor { get; set; }

        public string NomeEtiqueta { get; set; }

        public string Versao { get; set; }
    }
}

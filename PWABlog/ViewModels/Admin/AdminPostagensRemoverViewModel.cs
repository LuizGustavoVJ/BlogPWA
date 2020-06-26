using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWABlog.ViewModels.Admin
{
    public class AdminPostagensRemoverViewModel : ViewModelAreaAdministrativa
    {
        public int IdPostagem { get; set; }

        public string NomePostagem { get; set; }

        public DateTime DataPostagem { get; set; }

        public string Erro { get; set; }

        public AdminPostagensRemoverViewModel()
        {
            TituloPagina = "Remover Postagem ";
        }
    }
}

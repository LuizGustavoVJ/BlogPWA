using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWABlog.ViewModels.Admin
{
    public class AdminAutoresListarViewModel : ViewModelAreaAdministrativa
    {
        public ICollection<AutoresAdminAutores> Autores { get; set; }

        public AdminAutoresListarViewModel()
        {
            TituloPagina = "Autores - Administrador";

            Autores = new List<AutoresAdminAutores>();
        }

    }

    public class AutoresAdminAutores
    {
        public int Id { get; set; }

        public string Nome { get; set; }

    }
}

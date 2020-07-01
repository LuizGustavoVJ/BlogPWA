using System;
using System.Collections.Generic;

namespace PWABlog.ViewModels.Admin
{
    public class AdminPostagensListarViewModel : ViewModelAreaAdministrativa
    {
        public ICollection<PostagemAdminPostagens> Postagens { get; set; }

        public AdminPostagensListarViewModel()
        {
            TituloPagina = "Postagens - Administrador";
            Postagens = new List<PostagemAdminPostagens>();
        }
    }

    public class PostagemAdminPostagens
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

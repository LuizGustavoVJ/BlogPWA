using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWABlog.ViewModels.ControleDeAcesso
{
    public class ControleDeAcessoLoginViewModel : ViewModelControleDeAcesso
    {
        public string MensagemRegistro { get; set; }

        public string MensagemLogin { get; set; }

        public string Destino { get; set; }

        public ControleDeAcessoLoginViewModel()
        {
            TituloPagina = "Login - Administrador";
        }
    }
}

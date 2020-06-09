using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWABlog.Models.ControledeAcesso
{
    public class RegistrarUsuarioException : Exception
    {
        public IEnumerable<IdentityError> Erros { get; set; }

        public RegistrarUsuarioException(IEnumerable<IdentityError> erros)
        {
            Erros = erros;
        }
    }
}

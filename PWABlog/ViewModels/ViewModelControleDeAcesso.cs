using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWABlog.ViewModels
{
    public abstract class ViewModelControleDeAcesso
    {
        public string Layout = "_LayoutControleDeAcesso";
        public string TituloPagina { get; set; }
    }
}

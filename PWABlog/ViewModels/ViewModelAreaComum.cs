using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWABlog.ViewModels
{
    public abstract class ViewModelAreaComum
    {
        public string Layout = "_Layout";
        public string TituloPagina { get; set; }
    }
}

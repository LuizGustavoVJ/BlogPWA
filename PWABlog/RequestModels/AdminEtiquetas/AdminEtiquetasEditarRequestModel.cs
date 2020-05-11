using PWABlog.Models.Blog.Categoria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWABlog.RequestModels.AdminEtiquetas
{
    public class AdminEtiquetasEditarRequestModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public CategoriaEntity Categoria { get; set; }
    }
}

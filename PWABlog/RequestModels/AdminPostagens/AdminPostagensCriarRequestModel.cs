using PWABlog.Models.Blog.Autor;
using PWABlog.Models.Blog.Categoria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWABlog.RequestModels.AdminPostagens
{
    public class AdminPostagensCriarRequestModel
    {
        public int Id { get; set; }

        public int idAutor { get; set; }

        public int idCategoria { get; set; }

        public string DataPostagem { get; set; }

        public string Titulo { get; set; }

        public string Descricao { get; set; }

        public string Texto { get; set; }

    }
}

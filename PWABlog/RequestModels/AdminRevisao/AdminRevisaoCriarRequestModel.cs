using PWABlog.Models.Blog.Postagem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWABlog.RequestModels.AdminRevisao
{
    public class AdminRevisaoCriarRequestModel
    {
        public int Id { get; set; }

        public PostagemEntity Postagem { get; set; }


        public string Texto { get; set; }


        public int Versao { get; set; }


        public DateTime DataCriacao { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PWABlog.Models.Blog.Autor;

namespace PWABlog.Controllers.Admin
{
    public class AdminAutoresController : Controller
    {

        private readonly AutorOrmService _autorOrmService;

        public AdminAutoresController(
            AutorOrmService autorOrmService
            )
        { _autorOrmService = autorOrmService; }

        [HttpGet]
        [Route("admin/autor")]
        [Route("admin/autor/Listar")]
        public string Listar()
        {
            return "Listar autores";
        }
        [HttpPost]
        [Route("admin/autor/criar")]
        public string Criar()
        {
            return "Criar autor";
        }
        [HttpPost]
        [Route("admin/autor/editar/{id?}")]
        public string Editar(int id)
        {
            return "Editar autor";
        }

        [HttpPost]
        [Route("admin/autor/remover/{id?}")]
        public string Remover(int id)
        {
            return "Remover autor";
        }
    }
}
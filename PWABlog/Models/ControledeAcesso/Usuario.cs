﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWABlog.ControleDeAcesso
{
    public class Usuario : IdentityUser<int>
    {
        public string Apelido { get; set; }

    }
}

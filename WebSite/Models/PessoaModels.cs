using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSite.Models
{
    public class PessoaModels
    {
        public int Codigo{ get; set; }

        public string Cpf { get; set; }

        public string Nome { get; set; }

        public string Sobrenome { get; set; }

        public DateTime DataNascimento { get; set; } 

        public string Email { get; set; }

        public string Telefone { get; set; }
    }
}
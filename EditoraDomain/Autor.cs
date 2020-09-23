using System;
using System.Collections.Generic;

namespace Editora.Domain
{
    public class Autor
    {
        public int id { get; set; }
        public String nome { get; set; }
        public String sobrenome { get; set; }
        public String email { get; set; }
        public String datanascimento { get; set; }
        public List<Livro> livros { get; set; }
    }
}

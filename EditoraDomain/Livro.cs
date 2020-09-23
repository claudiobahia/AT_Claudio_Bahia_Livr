using System;
using System.Text.Json.Serialization;

namespace Editora.Domain
{
    public class Livro
    {
        public int id { get; set; }

        [JsonIgnore]
        public Autor autor { get; set; }
        public String titulo { get; set; }
        public String isbn { get; set; }
        public String ano { get; set; } 
        public int autorid { get; set; }
    }
}

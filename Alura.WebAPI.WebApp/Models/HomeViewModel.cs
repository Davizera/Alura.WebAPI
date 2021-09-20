using System.Collections.Generic;
using Alura.ListaLeitura.Modelos;

namespace Alura.ListaLeitura.WebApp.Models
{
    public class HomeViewModel
    {
        public IEnumerable<LivroApi> ParaLer { get; set; }
        public IEnumerable<LivroApi> Lendo { get; set; }
        public IEnumerable<LivroApi> Lidos { get; set; }
    }
}
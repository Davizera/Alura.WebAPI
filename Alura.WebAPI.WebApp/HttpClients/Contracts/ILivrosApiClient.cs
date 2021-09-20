using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Alura.ListaLeitura.Modelos;
using Lista = Alura.ListaLeitura.Modelos.ListaLeitura;
using Refit;

namespace Alura.ListaLeitura.WebApp.HttpClients.Contracts
{
    public interface ILivrosApiClient
    {
        [Get("/livros/{id}")]
        Task<LivroApi> RecuperarLivroAsync(int id);

        [Get("/livros/{id}/capa")]
        Task<HttpResponseMessage> RecuperarCapaAsync(int id);

        [Get("/listasleitura/{tipo}")]
        Task<Lista> RecuperarListaLivroTipo(string tipo);

        [Delete("/livros/{id}")]
        Task RemoverLivroAsync(int id);
    }
}
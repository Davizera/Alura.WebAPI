using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Alura.ListaLeitura.Modelos;
using Alura.ListaLeitura.Persistencia;
using Alura.ListaLeitura.WebApp.HttpClients.Contracts;
using Microsoft.AspNetCore.Mvc;
using Refit;

namespace Alura.ListaLeitura.WebApp.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class LivroController : Controller
    {
        private readonly IRepository<Livro> _repo;
        private readonly ILivrosApiClient _apiClient;

        public LivroController(IRepository<Livro> repository, ILivrosApiClient apiClient)
        {
            _repo = repository;
            _apiClient = apiClient;
        }

        [HttpGet]
        public IActionResult Novo()
        {
            return View(new LivroUpload());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Novo(LivroUpload model)
        {
            if (ModelState.IsValid)
            {
                _repo.Incluir(model.ToLivro());
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ImagemCapa(int id)
        {
            HttpResponseMessage response = null;
            try
            {
                response = await _apiClient.RecuperarCapaAsync(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            var img = await response.Content.ReadAsByteArrayAsync();

            if (img != null) return File(img, "image/png");

            return File("image/png", "~/images/capas/capa-vazia.png");
        }

        [HttpGet]
        public async Task<IActionResult> Detalhes(int id)
        {
            LivroApi livro = null;
            try
            {
                livro = await _apiClient.RecuperarLivroAsync(id);
            }
            catch (ApiException e)
            {
                return NotFound();
            }
            return View(livro.ToModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Detalhes(LivroUpload model)
        {
            if (ModelState.IsValid)
            {
                var livro = model.ToLivro();
                if (model.Capa == null)
                    livro.ImagemCapa = _repo.All
                        .Where(l => l.Id == livro.Id)
                        .Select(l => l.ImagemCapa)
                        .FirstOrDefault();

                _repo.Alterar(livro);
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remover(int id)
        {
            var model = await _apiClient.RecuperarLivroAsync(id);
            if (model == null) return NotFound();

            await _apiClient.RemoverLivroAsync(id);
            return RedirectToAction("Index", "Home");
        }
    }
}
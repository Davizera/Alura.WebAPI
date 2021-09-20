using System.Linq;
using Alura.ListaLeitura.Modelos;
using Alura.ListaLeitura.Persistencia;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Alura.WebAPI.Endpoints.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class LivrosController : ControllerBase
    {
        private readonly IRepository<Livro> _repo;

        public LivrosController(IRepository<Livro> repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult RecuperarLivros()
        {
            return Ok(_repo.All.Select(l => l.ToApi()).ToList());
        }

        [HttpGet("{id}")]
        public ActionResult Recuperar([FromRoute] int id)
        {
            var livro = _repo.Find(id);

            if (livro == null) return NotFound();

            return Ok(livro.ToApi());
        }

        [HttpPost]
        public IActionResult Inserir([FromBody] LivroUpload model)
        {
            if (!ModelState.IsValid) return BadRequest();

            var livro = model.ToLivro();
            _repo.Incluir(livro);
            return CreatedAtAction(nameof(Recuperar), new {livro.Id}, livro.ToApi());
        }

        [HttpPut("{id}")]
        public IActionResult Alterar([FromRoute] int id, [FromBody] LivroUpload model)
        {
            if (!ModelState.IsValid) return BadRequest();

            var livro = model.ToLivro();
            if (model.Capa == null)
                livro.ImagemCapa = _repo.All
                    .Where(l => l.Id == livro.Id)
                    .Select(l => l.ImagemCapa)
                    .FirstOrDefault();

            _repo.Alterar(livro);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            var model = _repo.Find(id);
            if (model == null) return NotFound();

            _repo.Excluir(model);
            return NoContent();
        }

        [HttpGet("{id}/capa")]
        public IActionResult Capa(int id)
        {
            byte[] img = _repo.All
                .Where(l => l.Id == id)
                .Select(l => l.ImagemCapa)
                .FirstOrDefault();

            if (img != null )
                return File(img, "image/png");

            return File("~/images/capas/capa-vazia.png", "image/png");
        }
    }
}
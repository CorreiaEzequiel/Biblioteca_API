using Biblioteca.Data;
using Biblioteca.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BibliotecaController : ControllerBase
    {
        private readonly Contexto _contexto;

        public BibliotecaController(Contexto contexto)
        {
            _contexto = contexto;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Livro>>> Get()
        {
            return await _contexto.Livro.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Livro>> Get(int id)
        {
            var livro = await _contexto.Livro.FindAsync(id);
            if (livro == null)
            {
                return NotFound();
            }
            return Ok(livro);
        }
        [HttpPost]
        public async Task<ActionResult<Livro>> Post([FromBody] Livro livro)
        {
            if (livro == null)
            {
                return BadRequest("Dados invalidos");
            }
            _contexto.Livro.Add(livro);
            await _contexto.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new object[] { livro });
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Livro livroAtualizado)
        {
            if (livroAtualizado == null || id != livroAtualizado.Id)
            {
                return BadRequest("Dados invalidos");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var livro = await _contexto.Livro.FindAsync(id);
            if (livro == null)
            {
                return NotFound();
            }
            livro.Titulo = livroAtualizado.Titulo;
            livro.Autor = livroAtualizado.Autor;
            livro.Paginas = livroAtualizado.Paginas;
            livro.Preco = livroAtualizado.Preco;
            
            

            _contexto.Entry(livro).State = EntityState.Modified;
            await _contexto.SaveChangesAsync();
            return NoContent();

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var livro = await _contexto.Livro.FindAsync(id);
            if (livro == null)
            {
                return NotFound();
            }
            _contexto.Livro.Remove(livro);
            await _contexto.SaveChangesAsync();
            return NoContent();

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Editora.Domain;
using Editora.Repository;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //Authorize
    public class AutorController : ControllerBase
    {
        private readonly EditoraContext _context;

        public AutorController(EditoraContext context)
        {
            _context = context;
        }

        // GET: api/Autor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Autor>>> GetAutores()
        {
            return await _context.Autores.Include(x => x.livros).ToListAsync();
        }

        // GET: api/Autor/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Autor>> GetAutor(int id)
        {
            var autor = await _context.Autores.Include(x => x.livros).FirstOrDefaultAsync(x => x.id == id);

            if (autor == null)
            {
                return NotFound();
            }

            return autor;
        }

        // PUT: api/Autor/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAutor(int id, Autor autor)
        {
            if (id != autor.id)
            {
                return BadRequest();
            }

            _context.Entry(autor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AutorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Autor
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Autor>> PostAutor(Autor autor)
        {
            _context.Autores.Add(autor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAutor", new { id = autor.id }, autor);
        }

        // DELETE: api/Autor/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Autor>> DeleteAutor(int id)
        {
            var autor = await _context.Autores.FindAsync(id);

            {
                var author = await _context.Autores
                                           .Include(x => x.livros)
                                           .FirstOrDefaultAsync(x => x.id == id);
                if (author == null)
                {
                    return NotFound();
                }
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {

                        foreach (var item in author.livros)
                        {
                            _context.Livros.Remove(item);
                        } 
                        _context.Autores.Remove(author);

                        await _context.SaveChangesAsync();

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                    }
                }
                return NoContent();
            }
        }

        private bool AutorExists(int id)
        {
            return _context.Autores.Any(e => e.id == id);
        }
    }
}

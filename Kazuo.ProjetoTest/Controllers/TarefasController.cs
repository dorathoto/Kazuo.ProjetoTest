using Kazuo.ProjetoTest.Data;
using Kazuo.ProjetoTest.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Kazuo.ProjetoTest.Controllers
{
    [Authorize] //somente logado
    public class TarefasController : Controller
    {
        private readonly UserManager<Usuario> _userManager; //injetando o userManager
        private readonly ApplicationDbContext _context;

        public TarefasController(ApplicationDbContext context
            , UserManager<Usuario> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Tarefas
        public async Task<IActionResult> Index()
        {
            var usuario = await _userManager.GetUserAsync(User); //pegando o usuario logado

            var tarefasDoUser = await _context.Tarefas
                .Include(t => t.Usuario)
                .Where(w => w.UsuarioId == usuario.Id)
                .ToListAsync();


            return View(tarefasDoUser);
        }

        // GET: Tarefas/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Tarefas == null)
            {
                return NotFound();
            }

            var tarefa = await _context.Tarefas
                .Include(t => t.Usuario)
                .FirstOrDefaultAsync(m => m.TarefaId == id);
            if (tarefa == null)
            {
                return NotFound();
            }

            return View(tarefa);
        }

        // GET: Tarefas/Create
        public IActionResult Create()
        {
            ViewData["UsuarioId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Tarefas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TarefaId,DataRealizacaoTarefa,UsuarioId")] Tarefa tarefa)
        {
            if (ModelState.IsValid)
            {
                tarefa.TarefaId = Guid.NewGuid();
                _context.Add(tarefa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UsuarioId"] = new SelectList(_context.Users, "Id", "Id", tarefa.UsuarioId);
            return View(tarefa);
        }

        // GET: Tarefas/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Tarefas == null)
            {
                return NotFound();
            }

            var tarefa = await _context.Tarefas.FindAsync(id);
            if (tarefa == null)
            {
                return NotFound();
            }
            ViewData["UsuarioId"] = new SelectList(_context.Users, "Id", "Id", tarefa.UsuarioId);
            return View(tarefa);
        }

        // POST: Tarefas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("TarefaId,DataRealizacaoTarefa,UsuarioId")] Tarefa tarefa)
        {
            if (id != tarefa.TarefaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tarefa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TarefaExists(tarefa.TarefaId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UsuarioId"] = new SelectList(_context.Users, "Id", "Id", tarefa.UsuarioId);
            return View(tarefa);
        }

        // GET: Tarefas/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Tarefas == null)
            {
                return NotFound();
            }

            var tarefa = await _context.Tarefas
                .Include(t => t.Usuario)
                .FirstOrDefaultAsync(m => m.TarefaId == id);
            if (tarefa == null)
            {
                return NotFound();
            }

            return View(tarefa);
        }

        // POST: Tarefas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Tarefas == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Tarefas'  is null.");
            }
            var tarefa = await _context.Tarefas.FindAsync(id);
            if (tarefa != null)
            {
                _context.Tarefas.Remove(tarefa);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TarefaExists(Guid id)
        {
            return (_context.Tarefas?.Any(e => e.TarefaId == id)).GetValueOrDefault();
        }
    }
}

using CrudPedidos.Data;
using CrudPedidos.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
//teste
namespace CrudPedidos.Controllers
{
    public class PedidosController : Controller
    {

        private readonly CrudPedidosContext _context;
        private decimal? valortotal;

        public PedidosController(CrudPedidosContext context)
        {
            _context = context;
        }
        // GET: PedidosController
        public async Task<ActionResult> Index()
        {
            return View(await _context.Pedido.ToListAsync());
        }


        // GET: PedidosController/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedido
                .FirstOrDefaultAsync(p => p.Id == id);
            if (pedido == null)
            {
                return NotFound();
            }
            
         

            return View(pedido);
        }

        // GET: PedidosController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PedidosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("Id,Nome_Produto,Valor,Desconto,ValorTotal,Data_Vencimento")] Pedido pedido)
        {
            try
            {
                pedido.ValorTotal = pedido.Valor;
                if (ModelState.IsValid)
                {
                    _context.Add(pedido);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(pedido);

            }
            catch
            {
                return View(pedido);
            }
        }

        // GET: PedidosController/Edit/5
        public async Task<ActionResult> Edit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedido.FindAsync(Id);
            if (pedido == null)
            {
                return NotFound();
            }
            return View(pedido);
        }

        // POST: PedidosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind("Id,Nome_Produto,Valor,Desconto, Data_Vencimento,ValorTotal")] Pedido pedido)
        {
            if (id != pedido.Id)
            {
                return NotFound();
            }
            else if (pedido.Valor < pedido.Desconto)
            {
                TempData["Mensagem"] = "O valor do pedido nao pode ser menor que o desconto!";
                return RedirectToAction("Edit");
            }
            if (ModelState.IsValid)
            {
                try
                {

                    pedido.ValorTotal = CalcularDesconto(pedido.Valor, pedido.Desconto, pedido.ValorTotal);
                    _context.Update(pedido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PedidoExists(pedido.Id))
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
            return View(pedido);
        }

        private decimal CalcularDesconto(decimal valor, decimal? desconto, decimal valorTotal)
        {
            throw new NotImplementedException();
        }

        // GET: PedidosController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedido
                .FirstOrDefaultAsync(p => p.Id == id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pedido = await _context.Pedido.FindAsync(id);
            _context.Pedido.Remove(pedido);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PedidoExists(int id)
        {
            return _context.Pedido.Any(p => p.Id == id);
        }


        //desconto
        public async Task<ActionResult> Desconto(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }


            var pedido = await _context.Pedido.FindAsync(Id);
            if (pedido == null)
            {
                return NotFound();
            }
            return View(pedido);


        }

        // POST: PedidosController/Desconto
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Desconto(int id,decimal desconto, decimal valor, [Bind("Id,Nome_Produto,Valor,Desconto, Data_Vencimento")] Pedido pedido)
        {
            //Validação desconto

            if (pedido.Data_Vencimento < DateTime.Now)
            {
                TempData["Mensagem"] = "Pedido inválido!!! Não é possível aplicar desconto.";
                return RedirectToAction("Index");
            }

            if (pedido.Desconto >= pedido.Valor || pedido.Desconto < 0)
            {
                TempData["Mensagem"] = "O valor do desconto não pode ser maior que o valor do pedido!";
                return RedirectToAction("Desconto");              
            }
           
            if (id != pedido.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
               pedido.ValorTotal= CalcularDesconto(pedido.Valor, pedido.Desconto, pedido.ValorTotal);

                    _context.Update(pedido);
                    valor = pedido.Valor;
                    valor = valor - desconto;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PedidoExists(pedido.Id))
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
            
           
                     
            return View(pedido);
        }

        private decimal? CalcularDesconto(decimal valor, decimal? desconto, decimal? valorTotal)
        {
            try
            {
                valortotal = valor - desconto;
                return valortotal;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private decimal CalcularDesconto(decimal valor, decimal desconto, decimal valortotal)
        {
           
            try
            {
              valortotal=  valor - desconto;
                return valortotal;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

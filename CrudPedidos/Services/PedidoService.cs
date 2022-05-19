using Microsoft.EntityFrameworkCore;
using CrudPedidos.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using CrudPedidos.Data;
using CrudPedidos.Services.Exceptions;

namespace CrudPedidos.Services
{
    public class PedidoService
    {
        private readonly CrudPedidosContext _context;

        public PedidoService(CrudPedidosContext context)
        {
            _context = context;
        }

        public async Task<List<Pedido>> FindAllAsync()
        {
            return await _context.Pedido.OrderBy(x => x.Id).ToListAsync();
        }



        public async Task UpdateAsync(Pedido obj)
        {
            bool hasAny = await _context.Pedido.AnyAsync(x => x.Id == obj.Id);
            if (!hasAny)
            {
                throw new NotFoundException("Id not found");
            }
            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}

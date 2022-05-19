using CrudPedidos.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudPedidos.Data
{
    public class CrudPedidosContext : DbContext
    {
        public CrudPedidosContext (DbContextOptions<CrudPedidosContext> options ) : base(options)
        {

        }
        public DbSet<Pedido> Pedido { get; set; }


    }


}

using CrudPedidos.Data;
using CrudPedidos.Models;
using System.Linq;

namespace CrudPedidos.Services

{
    public class DadosService
    {
        private Data.CrudPedidosContext _context;

        public DadosService(CrudPedidosContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if (_context.Pedido.Any())

            {
                return; // banco de dados já contém informação
            }

            Pedido p1 = new Pedido( 0,"Computador", 3500,new System.DateTime(2022, 5, 29),0,3500);
            Pedido p2 = new Pedido(0, "HD", 500, new System.DateTime(2022, 5, 15),0, 500);
            Pedido p3 = new Pedido( 0,"Monitor", 980, new System.DateTime(2022,5, 30),0,980);
            Pedido p4 = new Pedido( 0,"Impressora", 450,  new System.DateTime(2022, 5, 01),0,450);

            _context.Pedido.AddRange(p1, p2, p3, p4);

            _context.SaveChanges();
        }
    }
}

using DataAnnotationsExtensions;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrudPedidos.Models
{
    public class Pedido
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50,MinimumLength = 1, ErrorMessage = "{0} deve ter {2} e {1}")]
        [Display(Name = "Nome Produto")]
        public string Nome_Produto { get; set; }

        [Display(Name = "Valor Produto")]
        [Required(ErrorMessage = "Digite o {0}!!")]
        [Range(1, 9999999999999999.99, ErrorMessage = "{0} deve ser entre {1} e {2}")]
       // [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Valor { get; set; }


        [Required(ErrorMessage = "Digite {0}!!")]
        [Display(Name = "Data Vencimento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Data_Vencimento { get; set; }


        [Range(0, 9999999999999999.99, ErrorMessage = "{0} deve ser entre {1} e {2}")]
        //[DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Desconto { get; set; }

        [Display(Name = "Valor Total")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? ValorTotal { get; set; }
        public Pedido()
        {

        }

        public Pedido(int id, string nome_Produto, decimal valor, DateTime data_Vencimento, decimal desconto,decimal valortotal)
        {
            Id = id;
            Nome_Produto = nome_Produto;
            Valor = valor;
            Data_Vencimento = data_Vencimento;
            Desconto = desconto;
            ValorTotal = valortotal;
        }
    }
}

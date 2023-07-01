using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL
{
    [Table("Clientes")]

    public class Clientes
    {

        [Key]
        public short IdCliente { get; set; }

        [MaxLength(200), Required]
        public string Nombre_Completo { get; set; }

        [MaxLength(200), Required]
        public string Correo { get; set; }

        [MaxLength(10), Required]
        public string Celular { get; set; }

        [Required]

        public bool Activo { get; set; }
        [Required]
        public int IdUsuarioRegistra { get; set; }
        [Required]
        public DateTime FechaRegistro { get; set; }
        public int? IdUsuarioActualiza { get; set; }
        public DateTime? FechaActualizacion { get; set; }

        public Clientes()
        {
            Nombre_Completo = string.Empty;
            Correo = string.Empty;
     
        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Digital_Test.Entities
{
    public class Factura
    {
        [Key]
        public int Id_Factura { get; set; }
        public DateTime Fecha{ get; set; }
        public int Id_Cliente { get; set; }
        public decimal Total { get; set; }

        public virtual Cliente Cliente{ get; set; }

    }
}

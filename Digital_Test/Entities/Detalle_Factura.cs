using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Digital_Test.Entities
{
    public class Detalle_Factura
    {
    
        [Key]
        public int Id_DetalleFact { get; set; }
        
        public int Id_Factura { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio_Unitario { get; set; }
        public int Id_Producto { get; set; }
        public virtual  Factura Factura { get; set; }

        public virtual Producto Producto{ get; set; }


    }
}

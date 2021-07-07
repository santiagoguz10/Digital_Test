using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Digital_Test.Entities
{
    public class Cliente
    {
        [Key]
        public int Id_Cliente { get; set; }

        public string Nombre { get; set; }

        public int Edad { get; set; }



    }
}

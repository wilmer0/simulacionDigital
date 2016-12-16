using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulacionCajeroBanco.clases
{
    public class problema
    {
        public string nombre { get; set; }
        public double tiempo_aumenta { get; set; }
        public double tiempo_porciento_aumenta { get; set; }
        public double probabilidad_ocurrencia_inicial { get; set; }
        public double probabilidad_ocurrencia_final { get; set; }
    }
}

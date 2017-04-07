using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulacionCajeroBancoV2.objetos
{
    public class problemasLogs
    {
        public int idcliente { get; set; }
        public string operacion { get; set; }
        public string fase { get; set; }
        public double tiempo_antes { get; set; }
        public double tiempo_despues { get; set; }
        public string nombreProblema { get; set; }
        public int cantidad_intentos { get; set; }
        public string respuesta { get; set; }
        public bool problema_encontrado { get; set; }
    }
}

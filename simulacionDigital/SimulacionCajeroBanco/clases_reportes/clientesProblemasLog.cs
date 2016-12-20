using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulacionCajeroBanco.clases_reportes
{
    public class clientesProblemasLog
    {
        public int codigocliente { get; set; }
        public string operacion { get; set; }
        public double tiempo_antes { get; set; }
        public double tiempo_despues { get; set; }
        public string nombreProblema { get; set; }
        public int cantidad_intentos { get; set; }
        public string respuesta { get; set; }
        public bool problema_encontrado { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulacionCajeroBanco.clases
{
    public class cajero
    {
        public int codigo { get; set; }
        public int clientesAtendidos { get; set; }
        public double tiempoPromedioEnServcio { get; set; }
    }
}

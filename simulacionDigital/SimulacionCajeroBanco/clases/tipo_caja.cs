using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulacionCajeroBanco.clases
{
    public class tipo_caja
    {
        public string nombre { get; set; }

        public int total_clientes { get; set; }
        public int clientes_atendidos { get; set; }
        public int clientes_abandono { get; set; }
    }
}

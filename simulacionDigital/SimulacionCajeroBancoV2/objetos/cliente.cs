using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulacionCajeroBancoV2.objetos
{
    public class cliente
    {

        public int id { get; set; }
        public float intervalo_inicial { get; set; }
        public float intervalo_final { get; set; }
        public int idTemporada { get; set; }
        public int idTanda { get; set; }
        public int idOperacion { get; set; }
    }
}

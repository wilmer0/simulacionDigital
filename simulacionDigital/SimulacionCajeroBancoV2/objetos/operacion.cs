using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulacionCajeroBancoV2.objetos
{
    public class operacion
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public double intervalo_inicial { get; set; }
        public double intervalo_final { get; set; }

        //para modificar dependiendo de la temporada escogida
        public double tiempoEsperadoCola { get; set; }
        public double tiempoEsperadoEntregaDatos { get; set; }
        public double tiempoEsperadoProcesoSolicitud { get; set; }
        public double tiempoEsperadoServicio { get; set; }
        public double tiempoEsperadoTotal { get; set; }

    }
}

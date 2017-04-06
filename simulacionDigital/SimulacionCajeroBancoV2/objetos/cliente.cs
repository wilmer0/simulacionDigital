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
        public int idTemporada { get; set; }
        public int idTanda { get; set; }
        public string tanda { get; set; }
        public int idOperacion { get; set; }
        public string operacion { get; set; }
        public int idCajero { get; set; }
        public string tipoOperacion { get; set; }
        public double tiempoEsperadoCola { get; set; }
        public double tiempoEsperadoEntregaDatos { get; set; }
        public double tiempoEsperadoProcesoSolicitud { get; set; }
        public double tiempoCola { get; set; }
        public double tiempoEntregaDatos { get; set; }
        public double tiempoProcesoSolicitud { get; set; }
        public double tiempoEsperadoServicio { get; set; }
        public double tiempoTotalServicio { get; set; }
        public bool aceptaCuentaAhorro { get; set; }
        public bool aceptaDepositarRetiro { get; set; }
        public bool cambioDolaresApesos { get; set; }
        public bool cambioPesosADolares { get; set; }
        public bool aceptaCuentaAhorroCambioMoneda { get; set; }
        public decimal montoTransaccion { get; set; }

        public bool abandono { get; set; }
        public List<problema> listaProblema { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simulacionDigital.clases
{
    public class estudiante
    {
        public double indice { get; set; }
        public double indice_procesado { get; set; }
        public double jugadas { get; set; }
        public int indiceCarrera { get; set; }
        public string Nombrecarrera { get; set; }
        public bool tieneBeca { get; set; }
        public int cantidadPremios { get; set; }
        public string tipoCarrera { get; set; }
        public double tiempoLLegada { get; set; }
        public double tiempoServicioMiu { get; set; }
        public double tiempoServicioBase { get; set; }
        public double tiempoServicioFinal { get; set; }
        public int cantidadJugadasParaElegirCarra { get; set; }
        public bool eligioCarrera { get; set; }
        public List<premios> premios { get; set; }
        public List<evento_retraso> eventosRestraso { get; set; }
        public int cantidadEventosRestraso { get; set; }
        public int cambiosPremiosExtraPorNuevaoportunidad { get; set; }
    }
}

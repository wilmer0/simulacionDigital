using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simulacionDigital.clases
{
    public class panel
    {
        public int posicion { get; set; }
        public string contenido { get; set; }
        public bool pulsado { get; set; }
        public int cantidadPulsada { get; set; }
        public double intervaloInicial { get; set; }
        public double intervaloFinal { get; set; }
    }
}

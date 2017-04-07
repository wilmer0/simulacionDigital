using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace SimulacionCajeroBanco.clases
{
    public class cliente
    {
        public int codigo { get; set; }
        public double tiempo_servicio_esperado { get; set; } //el tiempo que deberia ser segun la operacion en la temp y tanda
        public double tiempo_servicio_base { get; set; }
        public double tiempo_servicio_final { get; set; }
        public string temporada { get; set; }
        public string tanda { get; set; }
        public string tipo_cuenta { get; set; }//cuenta corriente y cuenta ahorro
        public string operacion_deseada { get; set; } // para saber la operacion que desea realizar
        public bool operacion_completada { get; set; } //para saber si completo la operacion
        public List<problema> problemas { get; set; }
        public bool atendiendo { get; set; }
        public bool atendido { get; set; } 
        public bool abandono { get; set; } //para saber si cuando se presento un problema el cliente abandono la fila
        public int intentos { get; set; } //para saber la cantidad de intentos que realizo cuando se presento un problema
        public string numeroCajeroAntendio { get; set; }
        public string tipo_cajero { get; set; }



    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulacionCajeroBanco.clases_reportes
{
    public class reporte_grafico_cliente_encabezado
    {
        public double tiempo_maximo_servicio_esperado { get; set; }
        public double tiempo_maximo_servicio_final { get; set; }
        public double tiempo_minimo_servicio_esperado { get; set; }
        public double tiempo_minimo_servicio_final { get; set; }
        public int cantidad_clientes_retiraron { get; set; }
        public int cantidad_clientes_completaron_operacion { get; set; }

    }
}

﻿using System;
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
        public double tiempo_servicio_base { get; set; }
        public double tiempo_servicio_final { get; set; }
        public string temporada { get; set; }
        public string tanda { get; set; }
        public string tipo_cuenta { get; set; }//cuenta corriente y cuenta ahorro

    }
}

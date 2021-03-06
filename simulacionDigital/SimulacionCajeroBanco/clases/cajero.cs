﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulacionCajeroBanco.clases
{
    public class cajero
    {
        public int codigo { get; set; }
        public int clientesAtendidos { get; set; } //para saber cuales clientes fueron atendidos
        public int clientesCola { get; set; }//para saber todos los clientes que llegaron
        public double tiempoPromedioEnServcio { get; set; }//para saber el tiempo promedio servicio
        public string operacion { get; set; }// tipo de caja que maneja el cajero
        private List<cliente> clientes { get; set; }
        public int cantidad_cajeros_esta_operacion { get; set; }
    }
}

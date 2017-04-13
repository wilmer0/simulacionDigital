using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulacionCajeroBancoV2.objetos
{
    public class reporte_encabezado
    {
        public string temporada { get; set; }
        public int cantidadCajeros { get; set; }
        public int cantidadClientes { get; set; }
        
        //tiempos promedios
        public decimal tiempoPromedioFase1Esperado { get; set; }
        public decimal tiempoPromedioFase2Esperado { get; set; }
        public decimal tiempoPromedioFase3Esperado { get; set; }
        public decimal tiempoPromedioTotalEsperado { get; set; }

        //tiempos obtenidos
        public decimal tiempoPromedioFase1 { get; set; }
        public decimal tiempoPromedioFase2 { get; set; }
        public decimal tiempoPromedioFase3 { get; set; }
        public decimal tiempoPromedioTotal { get; set; }

        //tiempos limites maximos y minimos
        public decimal tiempoMaximoTotal { get; set; }
        public decimal tiempoMinimoTotal { get; set; }

        //tiempos limites por fase maximos y minimos
        public decimal tiempoMaximoFase1 { get; set; }
        public decimal tiempoMaximoFase2 { get; set; }
        public decimal tiempoMaximoFase3 { get; set; }

        //tiempos minimos por fase
        public decimal tiempoMinimoFase1 { get; set; }
        public decimal tiempoMinimoFase2 { get; set; }
        public decimal tiempoMinimoFase3 { get; set; }

        public List<problema_tiempo> listaProblemaTiempo { get; set; } //para guardar el tiempo de problema por cada problema


        public reporte_encabezado()
        {
            
        }

        public reporte_encabezado(temporada temporada,int cajeros,int clientes,List<problemasLogs> listaProblemaLogs,List<cliente> listaCliente )
        {
            

            this.temporada = temporada.nombre;
            cantidadCajeros = cajeros;
            cantidadClientes = clientes;

            //tiempos promedios esperado
            #region
            tiempoPromedioFase1Esperado = Math.Round(listaCliente.Average(x=>x.tiempoPromedioFase1),2);
            tiempoPromedioFase2Esperado = Math.Round(listaCliente.Average(x=>x.tiempoPromedioFase2),2);
            tiempoPromedioFase3Esperado = Math.Round(listaCliente.Average(x=>x.tiempoPromedioFase3),2);
            //this.tiempoPromedioTotalEsperado = Math.Round(listaCliente.Average(x=>x.tiempoPromedioTotal),2);
            tiempoPromedioTotalEsperado = Math.Round((this.tiempoPromedioFase1Esperado + this.tiempoPromedioFase2Esperado + this.tiempoPromedioFase3Esperado), 2);
            #endregion
            
            //tiempos promedio obtenidos
            #region
            tiempoPromedioFase1 = Math.Round(listaCliente.Average(x=>x.tiempoFase1),2);
            tiempoPromedioFase2 = Math.Round(listaCliente.Average(x=>x.tiempoFase2),2);
            tiempoPromedioFase3 = Math.Round(listaCliente.Average(x=>x.tiempoFase3),2);
            //this.tiempoPromedioTotal = Math.Round(listaCliente.Average(x=>x.tiempoTotal),2);
            tiempoPromedioTotal = Math.Round((this.tiempoPromedioFase1 + this.tiempoPromedioFase2 + this.tiempoPromedioFase3), 2);
            #endregion
            
           
            //tiempos obtenido limites por fase maximos
            #region
            tiempoMaximoFase1 = Math.Round(listaCliente.Max(x => x.tiempoFase1),2);
            tiempoMaximoFase2 = Math.Round(listaCliente.Max(x => x.tiempoFase2),2);
            tiempoMaximoFase3 = Math.Round(listaCliente.Max(x => x.tiempoFase3),2);
            tiempoMaximoTotal = Math.Round((tiempoMaximoFase1 + tiempoMaximoFase2 + tiempoMaximoFase3), 2);
            #endregion

            //tiempos obtenido limites por fase minimo
            #region
            tiempoMinimoFase1 = Math.Round(listaCliente.Min(x => x.tiempoFase1),2);
            tiempoMinimoFase2 = Math.Round(listaCliente.Min(x => x.tiempoFase2),2);
            tiempoMinimoFase3 = Math.Round(listaCliente.Min(x => x.tiempoFase3),2);
            tiempoMinimoTotal = Math.Round((tiempoMinimoFase1 + tiempoMinimoFase2 + tiempoMinimoFase3), 2);
            #endregion


            //recomendaciones
            //tiempo que acumulo cada problema promedio
            #region
            listaProblemaTiempo = new List<problema_tiempo>();
            foreach (var x in listaProblemaLogs)
            {
                if (listaProblemaTiempo.Where(p => p.problema == x.nombreProblema).Count() == 0)
                {
                    problema_tiempo problemaTiempo=new problema_tiempo();
                    problemaTiempo.problema = x.nombreProblema;
                    problemaTiempo.recomendacion = new problema_recomendacion().getRecomendacionByNombreProblema(x.nombreProblema);
                    problemaTiempo.cantidadCliente = listaProblemaLogs.Where(w => w.nombreProblema == x.nombreProblema).Count();
                    problemaTiempo.tiempoPromedio = listaProblemaLogs.FindAll(w=> w.nombreProblema==x.nombreProblema).Average(s=> s.tiempo_problema);
                    problemaTiempo.tiempoPromedio = Math.Round(problemaTiempo.tiempoPromedio, 2);
                    listaProblemaTiempo.Add(problemaTiempo);
                }
            }
            listaProblemaTiempo = listaProblemaTiempo.OrderByDescending(x => x.tiempoPromedio).ThenBy(x=> x.cantidadCliente).ToList();

            #endregion



        }




    }
}

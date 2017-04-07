using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SimulacionCajeroBancoV2.objetos;

namespace SimulacionCajeroBancoV2.modelos
{
    public class modeloTemporada
    {
        public List<temporada> getListaTemporada()
        {
            try
            {
                temporada temporada;
                List<temporada> lista=new List<temporada>();



                //creado caa temporada
                //1
                temporada=new temporada();
                temporada.id = 1;
                temporada.nombre = "temporada 1";
                lista.Add(temporada);

                //2
                temporada = new temporada();
                temporada.id = 2;
                temporada.nombre = "temporada 2";
                lista.Add(temporada);

                //3
                temporada = new temporada();
                temporada.id = 3;
                temporada.nombre = "temporada 3";
                lista.Add(temporada);

                int contador = 1;
                double probabilidad = 0;
                double intervaloTemporal = 0;
                double cantidad = lista.Count;
                probabilidad = Math.Round((1/cantidad),2);
                //asignando intervalos
                foreach (var x in lista)
                {
                    x.id = contador;
                    x.nombre = "temporada " + contador;
                    x.intervalo_inicial = intervaloTemporal;
                    intervaloTemporal += probabilidad;
                    x.intervalo_final = intervaloTemporal;
                    contador++;
                }


                return lista;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getListaTemporada.:" + ex.ToString(), "", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return null;
            }
        }



    }
}

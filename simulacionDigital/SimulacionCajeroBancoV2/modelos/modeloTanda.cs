using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SimulacionCajeroBancoV2.objetos;

namespace SimulacionCajeroBancoV2.modelos
{
    public class modeloTanda
    {
        public List<tanda> getListaTanda(int temporadaId)
        {
            try
            {
                tanda tanda;
                List<tanda> lista = new List<tanda>();



                //creado cada tanda
                //temporada 1
                if (temporadaId == 1)
                {
                    //1
                    tanda = new tanda();
                    tanda.id = 1;
                    tanda.nombre = "matutina";
                    tanda.intervalo_inicial = 0;
                    tanda.intervalo_final = 0;
                    lista.Add(tanda);

                    //2
                    tanda = new tanda();
                    tanda.id = 2;
                    tanda.nombre = "vespertina";
                    tanda.intervalo_inicial = 0;
                    tanda.intervalo_final = 0;
                    lista.Add(tanda);
                }
                else if (temporadaId == 2)
                {
                    //1
                    tanda = new tanda();
                    tanda.id = 1;
                    tanda.nombre = "matutina";
                    tanda.intervalo_inicial = 0;
                    tanda.intervalo_final = 0;
                    lista.Add(tanda);

                    //2
                    tanda = new tanda();
                    tanda.id = 2;
                    tanda.nombre = "vespertina";
                    tanda.intervalo_inicial = 0;
                    tanda.intervalo_final = 0;
                    lista.Add(tanda);
                }
                else if (temporadaId == 3)
                {
                    //1
                    tanda = new tanda();
                    tanda.id = 1;
                    tanda.nombre = "matutina";
                    tanda.intervalo_inicial = 0;
                    tanda.intervalo_final = 0;
                    lista.Add(tanda);

                    //2
                    tanda = new tanda();
                    tanda.id = 2;
                    tanda.nombre = "vespertina";
                    tanda.intervalo_inicial = 0;
                    tanda.intervalo_final = 0;
                    lista.Add(tanda);
                }
               

               
               

                return lista;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getListaTanda.:" + ex.ToString(), "", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return null;
            }
        }

        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SimulacionCajeroBancoV2.objetos;

namespace SimulacionCajeroBancoV2.modelos
{
    public class modeloCajero
    {
        public List<cajero> getListaCajero()
        {
            try
            {
                cajero cajero;
                List<cajero> lista = new List<cajero>();

                //creado cada cajero
               
                //1
                cajero = new cajero();
                cajero.id = 1;
                cajero.nombre = "Cajero 1";
                lista.Add(cajero);

                ////2
                //cajero = new cajero();
                //cajero.id = 2;
                //cajero.nombre = "Retiro";
                //lista.Add(cajero);


                ////3
                //cajero = new cajero();
                //cajero.id = 3;
                //cajero.nombre = "Cambio moneda";
                //lista.Add(cajero);
                

                return lista;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getListaCajero.:" + ex.ToString(), "", MessageBoxButtons.OK,MessageBoxIcon.Error);
                return null;
            }
        }

    }
}

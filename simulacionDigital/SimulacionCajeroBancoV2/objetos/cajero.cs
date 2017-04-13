using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimulacionCajeroBancoV2.objetos
{
    public class cajero
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public double intervalo_inicial { get; set; }
        public double intervalo_final { get; set; }

        private utilidades utilidades=new utilidades();


        public void agregarCajero(double cantidadCajeros)
        {
            try
            {

                //limpia tabla
                string sql = "delete from cajero;";
                utilidades.ejecutarcomando_mysql(sql);


                double probabilidad = 0;
                double probabilidadTemporal = 0;
                int contador = 1;
                probabilidad = 1 / cantidadCajeros;

                for (int f = 0; f < cantidadCajeros; f++)
                {
                    cajero cajero=new cajero();
                    cajero.id = contador;
                    cajero.nombre = "cajero " + contador;
                    cajero.intervalo_inicial = probabilidadTemporal;
                    probabilidadTemporal += probabilidad;
                    cajero.intervalo_final = probabilidadTemporal;
                    sql = "insert into cajero(id,nombre,intervalo_inicial,intervalo_final) values('" + cajero.id + "','" + cajero.nombre + "','" + cajero.intervalo_inicial + "','" + cajero.intervalo_final + "');";
                    utilidades.ejecutarcomando_mysql(sql);
                    contador++;
                }
               
            }
            catch (Exception)
            {
                
            }
        }


        public List<cajero> getListaCajero()
        {
            try
            {
                List<cajero> lista = new List<cajero>();
                string sql = "select id,nombre,intervalo_inicial,intervalo_final from cajero";
                DataSet ds = utilidades.ejecutarcomando_mysql(sql);
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    cajero cajero = new cajero();
                    cajero.id = Convert.ToInt16(row[0]);
                    cajero.nombre = row[1].ToString();
                    cajero.intervalo_inicial = Convert.ToDouble(row[2]);
                    cajero.intervalo_final = Convert.ToDouble(row[3]);
                    lista.Add(cajero);
                }

                return lista;
            }
            catch (Exception ex)
            {
                MessageBox.Show("error getListaCajero .:" + ex.ToString());
                return null;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimulacionCajeroBancoV2.objetos
{
    public class temporada
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public double intervalo_inicial { get; set; }
        public double intervalo_final { get; set; }

        utilidades utilidades=new utilidades();

        public void agregarTemporadas()
        {
            string sql = "delete from temporada";
            utilidades.ejecutarcomando_mysql(sql);

            sql = "insert into temporada(id,nombre,intervalo_inicial,intervalo_final) values('1','temporada 1','0.00','0.43');";
            utilidades.ejecutarcomando_mysql(sql);
            sql = "insert into temporada(id,nombre,intervalo_inicial,intervalo_final) values('2','temporada 2','0.43','1');";
            utilidades.ejecutarcomando_mysql(sql);
        }

        public List<temporada> getListaTemporada()
        {
            try
            {
                List<temporada> lista = new List<temporada>();
                string sql = "select id,nombre,intervalo_inicial,intervalo_final from temporada";
                DataSet ds = utilidades.ejecutarcomando_mysql(sql);
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    temporada temporada = new temporada();
                    temporada.id = Convert.ToInt16(row[0]);
                    temporada.nombre = row[1].ToString();
                    temporada.intervalo_inicial = Convert.ToDouble(row[2]);
                    temporada.intervalo_final = Convert.ToDouble(row[3]);
                    lista.Add(temporada);
                }

                return lista;
            }
            catch (Exception ex)
            {
                MessageBox.Show("error getListaTemporada .:" + ex.ToString());
                return null;
            }
        }

        public temporada getemporadaById(int id)
        {
            temporada temporada=new temporada();
            string sql = "select id,nombre,intervalo_inicial,intervalo_final from temporada where id='"+id+"'";
            DataSet ds = utilidades.ejecutarcomando_mysql(sql);
            if (ds.Tables[0].Rows[0][0].ToString() != "")
            {
                temporada=new temporada();
                temporada.id = Convert.ToInt16(ds.Tables[0].Rows[0][0]);
                temporada.nombre = ds.Tables[0].Rows[0][1].ToString();
                temporada.intervalo_inicial = Convert.ToDouble(ds.Tables[0].Rows[0][2]);
                temporada.intervalo_final = Convert.ToDouble(ds.Tables[0].Rows[0][3]);
                
                return temporada;
            }
            return null;

        }
    }
}

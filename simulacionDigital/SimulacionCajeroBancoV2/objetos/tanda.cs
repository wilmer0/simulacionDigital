using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimulacionCajeroBancoV2.objetos
{
    public class tanda
    {

        public int id { get; set; }
        public string nombre { get; set; }
        public double intervalo_inicial { get; set; }
        public double intervalo_final { get; set; }

        utilidades utilidades=new utilidades();
        public void agregarTandas(int idTemporada)
        {
            string sql = "delete from tanda";
            utilidades.ejecutarcomando_mysql(sql);

            if (idTemporada == 1)
            {
                sql = "insert into tanda(id,nombre,intervalo_inicial,intervalo_final) values('1','tanda 1','0.00','0.60');";
                utilidades.ejecutarcomando_mysql(sql);
                sql = "insert into tanda(id,nombre,intervalo_inicial,intervalo_final) values('2','tanda 2','0.60','1');";
                utilidades.ejecutarcomando_mysql(sql);
            }
            else if (idTemporada == 2)
            {
                sql = "insert into tanda(id,nombre,intervalo_inicial,intervalo_final) values('1','tanda 1','0.00','0.40');";
                utilidades.ejecutarcomando_mysql(sql);
                sql = "insert into tanda(id,nombre,intervalo_inicial,intervalo_final) values('2','tanda 2','0.40','1');";
                utilidades.ejecutarcomando_mysql(sql);
            }else if (idTemporada == 3)
            {
                sql = "insert into tanda(id,nombre,intervalo_inicial,intervalo_final) values('1','tanda 1','0.00','0.55');";
                utilidades.ejecutarcomando_mysql(sql);
                sql = "insert into tanda(id,nombre,intervalo_inicial,intervalo_final) values('2','tanda 2','0.55','1');";
                utilidades.ejecutarcomando_mysql(sql);
            }
            
        }

        public List<tanda> getListaTanda()
        {
            try
            {
                List<tanda> lista = new List<tanda>();
                string sql = "select id,nombre,intervalo_inicial,intervalo_final from tanda";
                DataSet ds = utilidades.ejecutarcomando_mysql(sql);
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    tanda tanda = new tanda();
                    tanda.id = Convert.ToInt16(row[0]);
                    tanda.nombre = row[1].ToString();
                    tanda.intervalo_inicial = Convert.ToDouble(row[2]);
                    tanda.intervalo_final = Convert.ToDouble(row[3]);
                    lista.Add(tanda);
                }

                return lista;
            }
            catch (Exception ex)
            {
                MessageBox.Show("error getListaTanda .:" + ex.ToString());
                return null;
            }
        }
    }
}

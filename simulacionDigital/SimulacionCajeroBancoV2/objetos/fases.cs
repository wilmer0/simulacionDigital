using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimulacionCajeroBancoV2.objetos
{
    public class fases
    {
        public int id { get; set; }
        public string nombre { get; set; }

        utilidades utilidades=new utilidades();
        
        public void agregarFases()
        {
            try
            {
                //limpia tabla
                string sql = "delete from fases;";
                utilidades.ejecutarcomando_mysql(sql);

                sql = "insert into fases(id,nombre) values('1','cola espera');";
                utilidades.ejecutarcomando_mysql(sql);

                sql = "insert into fases(id,nombre) values('2','entrega datos');";
                utilidades.ejecutarcomando_mysql(sql);

                sql = "insert into fases(id,nombre) values('3','proceso solicitud');";
                utilidades.ejecutarcomando_mysql(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show("error agregarFases .:" + ex.ToString());
            }
        }

        public List<fases> getListaFases()
        {
            try
            {
                List<fases> lista=new List<fases>();
                string sql = "select id,nombre from fases";
                DataSet ds = utilidades.ejecutarcomando_mysql(sql);
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    fases fase=new fases();
                    fase.id = Convert.ToInt16(row[0]);
                    fase.nombre = row[1].ToString();
                    lista.Add(fase);
                }

                return lista;

            }
            catch (Exception ex)
            {
                MessageBox.Show("error getListaFases .:" + ex.ToString());
                return null;
            }
        }
    }
}

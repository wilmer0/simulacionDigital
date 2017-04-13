using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimulacionCajeroBancoV2.objetos
{
    public class problemasLogs
    {
        public int idcliente { get; set; }
        public string operacion { get; set; }
        public string fase { get; set; }
        public decimal tiempo_antes { get; set; }
        public decimal tiempo_despues { get; set; }
        public decimal tiempo_problema { get; set; }
        public string nombreProblema { get; set; }
        public int cantidad_intentos { get; set; }
        public string respuesta { get; set; }
        public int problema_encontrado { get; set; }
        public int idCajero { get; set; }

        utilidades utilidades=new utilidades();

        public void agregarLog(problemasLogs problemasLogs)
        {
            string sql = "insert into problema_logs(id_cliente,operacion,fase,tiempo_antes,tiempo_problema,tiempo_despues,nombre_problema,cantidad_intentos,repuesta,problema_encontrado,id_cajero) values('" + problemasLogs.idcliente + "','" + problemasLogs.operacion + "','" + problemasLogs.fase + "','" + problemasLogs.tiempo_antes + "','" + problemasLogs.tiempo_problema + "','" + problemasLogs.tiempo_despues + "','" + problemasLogs.nombreProblema + "','" + problemasLogs.cantidad_intentos + "','" + problemasLogs.respuesta + "','" + problemasLogs.problema_encontrado + "','" + problemasLogs.idCajero + "');";
            utilidades.ejecutarcomando_mysql(sql);
        }

        public void eliminarProblemasLogs()
        {
            string sql = "delete from problema_logs;";
            utilidades.ejecutarcomando_mysql(sql);
        }

        public List<problemasLogs> getListaProblemaLogs()
        {
            try
            {
                List<problemasLogs> lista = new List<problemasLogs>();
                string sql = "select id_cliente,operacion,fase,tiempo_antes,tiempo_problema,tiempo_despues,nombre_problema,cantidad_intentos,repuesta,problema_encontrado,id_cajero from problema_logs;";
                DataSet ds = utilidades.ejecutarcomando_mysql(sql);
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    problemasLogs problemasLogs = new problemasLogs();
                    problemasLogs.idcliente = Convert.ToInt16(row[0]);
                    problemasLogs.operacion = row[1].ToString();
                    problemasLogs.fase = row[2].ToString();
                    problemasLogs.tiempo_antes = Convert.ToDecimal(row[3]);
                    problemasLogs.tiempo_problema = Convert.ToDecimal(row[4]);
                    problemasLogs.tiempo_despues = Convert.ToDecimal(row[5]);
                    problemasLogs.nombreProblema = row[6].ToString();
                    problemasLogs.cantidad_intentos = Convert.ToInt16(row[7]);
                    problemasLogs.respuesta = row[8].ToString();
                    problemasLogs.problema_encontrado = Convert.ToInt16(row[9]);
                    problemasLogs.idCajero = Convert.ToInt16(row[10]);
                    lista.Add(problemasLogs);
                }

                return lista;
            }
            catch (Exception ex)
            {
                MessageBox.Show("error getListaProblemaLogs .:" + ex.ToString());
                return null;
            }
        }
    }
}

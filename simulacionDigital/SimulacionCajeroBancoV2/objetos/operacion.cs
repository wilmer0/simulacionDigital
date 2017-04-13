using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimulacionCajeroBancoV2.objetos
{
    public class operacion
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public int idTemporada { get; set; }
        public decimal tiempoFase1 { get; set; }
        public decimal tiempoFase2 { get; set; }
        public decimal tiempoFase3 { get; set; }
        public decimal tiempoTotalFases { get; set; }

        public utilidades utilidades=new utilidades();

        public void agregarOperacion(int idTemporada)
        {
            string sql = "delete from operacion";
            utilidades.ejecutarcomando_mysql(sql);

            //temporada 1
            if (idTemporada == 1)
            {
                sql = "insert into operacion(id,nombre,tiempo_fase1,tiempo_fase2,tiempo_fase3,tiempo_total_fases,id_temporada) values('1','deposito','1.9','2.4','1.8','5.7','" + idTemporada + "');";
                utilidades.ejecutarcomando_mysql(sql);

                sql = "insert into operacion(id,nombre,tiempo_fase1,tiempo_fase2,tiempo_fase3,tiempo_total_fases,id_temporada) values('2','retiro','2.5','2.3','1.8','6.3','" + idTemporada + "');";
                utilidades.ejecutarcomando_mysql(sql);

                sql = "insert into operacion(id,nombre,tiempo_fase1,tiempo_fase2,tiempo_fase3,tiempo_total_fases,id_temporada) values('3','cambio moneda','2.2','2.6','1.9','6.7','" + idTemporada + "');";
                utilidades.ejecutarcomando_mysql(sql);


            }else if (idTemporada == 2)
            {
                sql = "insert into operacion(id,nombre,tiempo_fase1,tiempo_fase2,tiempo_fase3,tiempo_total_fases,id_temporada) values('1','deposito','2.1','2.4','2.3','6.8','"+idTemporada+"');";
                utilidades.ejecutarcomando_mysql(sql);

                sql = "insert into operacion(id,nombre,tiempo_fase1,tiempo_fase2,tiempo_fase3,tiempo_total_fases,id_temporada) values('2','retiro','2.7','2.3','2.0','7','" + idTemporada + "');";
                utilidades.ejecutarcomando_mysql(sql);

                sql = "insert into operacion(id,nombre,tiempo_fase1,tiempo_fase2,tiempo_fase3,tiempo_total_fases,id_temporada) values('3','cambio moneda','2.4','2.5','2.3','7.2','" + idTemporada + "');";
                utilidades.ejecutarcomando_mysql(sql);

            }else if (idTemporada == 3)
            {
                sql = "insert into operacion(id,nombre,tiempo_fase1,tiempo_fase2,tiempo_fase3,tiempo_total_fases,id_temporada) values('1','deposito','2.3','2.7','2.5','7.5','" + idTemporada + "');";
                utilidades.ejecutarcomando_mysql(sql);

                sql = "insert into operacion(id,nombre,tiempo_fase1,tiempo_fase2,tiempo_fase3,tiempo_total_fases,id_temporada) values('2','retiro','3.1','2.6','2.8','8.5','" + idTemporada + "');";
                utilidades.ejecutarcomando_mysql(sql);

                sql = "insert into operacion(id,nombre,tiempo_fase1,tiempo_fase2,tiempo_fase3,tiempo_total_fases,id_temporada) values('3','cambio moneda','2.6','2.3','2.8','7.7','" + idTemporada + "');";
                utilidades.ejecutarcomando_mysql(sql);

            }
        }

        public List<operacion> getListaOperacion()
        {
            try
            {
                List<operacion> lista = new List<operacion>();
                string sql = "select id,nombre,tiempo_fase1,tiempo_fase2,tiempo_fase3,tiempo_total_fases,id_temporada from operacion";
                DataSet ds = utilidades.ejecutarcomando_mysql(sql);
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    operacion operacion = new operacion();
                    operacion.id = Convert.ToInt16(row[0]);
                    operacion.nombre = row[1].ToString();
                    operacion.tiempoFase1 = Convert.ToDecimal(row[2]);
                    operacion.tiempoFase2 = Convert.ToDecimal(row[3]);
                    operacion.tiempoFase3 = Convert.ToDecimal(row[4]);
                    operacion.tiempoTotalFases = Convert.ToDecimal(row[5]);
                    operacion.idTemporada = Convert.ToInt16(row[6]);
                    lista.Add(operacion);
                }

                return lista;
            }
            catch (Exception ex)
            {
                MessageBox.Show("error getListaOperacion .:" + ex.ToString());
                return null;
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimulacionCajeroBancoV2.objetos
{
    public class operacion_tipo
    {
        public int idTipo { get; set; }
        public int idOperacion { get; set; }
        public string tipoOperacion { get; set; }
        public decimal intervaloInicial { get; set; }
        public decimal intercaloFinal { get; set; }
        public Int32 montoIncial { get; set; }
        public Int32 montofinal { get; set; }


        utilidades utilidades=new utilidades();

        public List<operacion_tipo> getListaOperacionTipo()
        {
            try
            {
                List<operacion_tipo> lista = new List<operacion_tipo>();
                string sql = "select id_operacion,id_tipo,tipo_operacion,intervalo_inicial,intervalo_final,monto_inicial,monto_final from operacion_tipo";
                DataSet ds = utilidades.ejecutarcomando_mysql(sql);
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    operacion_tipo operacion_tipo = new operacion_tipo();
                    operacion_tipo.idOperacion = Convert.ToInt16(row[0]);
                    operacion_tipo.idTipo = Convert.ToInt16(row[1]);
                    operacion_tipo.tipoOperacion = row[2].ToString();
                    operacion_tipo.intervaloInicial = Convert.ToDecimal(row[3]);
                    operacion_tipo.intercaloFinal = Convert.ToDecimal(row[4]);
                    operacion_tipo.montoIncial = Convert.ToInt32(row[5]);
                    operacion_tipo.montofinal = Convert.ToInt32(row[6]);
                   
                    lista.Add(operacion_tipo);
                }

                return lista;
            }
            catch (Exception ex)
            {
                MessageBox.Show("error getListaOperacionTipo .:" + ex.ToString());
                return null;
            }
        }


        public List<operacion_tipo> getListaOperacionTipoByOperacionId(int id)
        {
            try
            {
                List<operacion_tipo> lista = new List<operacion_tipo>();
                string sql = "select id_operacion,id_tipo,tipo_operacion,intervalo_inicial,intervalo_final,monto_inicial,monto_final from operacion_tipo where id_operacion='"+id+"'";
                DataSet ds = utilidades.ejecutarcomando_mysql(sql);
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    operacion_tipo operacion_tipo = new operacion_tipo();
                    operacion_tipo.idOperacion = Convert.ToInt16(row[0]);
                    operacion_tipo.idTipo = Convert.ToInt16(row[1]);
                    operacion_tipo.tipoOperacion = row[2].ToString();
                    operacion_tipo.intervaloInicial = Convert.ToDecimal(row[3]);
                    operacion_tipo.intercaloFinal = Convert.ToDecimal(row[4]);
                    operacion_tipo.montoIncial = Convert.ToInt32(row[5]);
                    operacion_tipo.montofinal = Convert.ToInt32(row[6]);

                    lista.Add(operacion_tipo);
                }

                return lista;
            }
            catch (Exception ex)
            {
                MessageBox.Show("error getListaOperacionTipo .:" + ex.ToString());
                return null;
            }
        }




    }
}

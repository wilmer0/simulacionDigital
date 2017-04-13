using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimulacionCajeroBancoV2.objetos
{
    public class cliente
    {

        public int id { get; set; }
        public int idTemporada { get; set; }
        public int idTanda { get; set; }
        public string tanda { get; set; }
        public int idOperacion { get; set; }
        public string operacion { get; set; }
        public int idCajero { get; set; }
        public string tipoOperacion { get; set; }
        public decimal tiempoPromedioFase1 { get; set; }
        public decimal tiempoPromedioFase2 { get; set; }
        public decimal tiempoPromedioFase3 { get; set; }
        public decimal tiempoPromedioTotal { get; set; }
        public decimal tiempoFase1 { get; set; }
        public decimal tiempoFase2 { get; set; }
        public decimal tiempoFase3 { get; set; }
        public decimal tiempoTotal { get; set; }
        public int aceptaCuentaAhorro { get; set; }
        public int aceptaDepositarRetiro { get; set; }
        public int cambioDolaresApesos { get; set; }
        public int cambioPesosADolares { get; set; }
        public int aceptaCuentaAhorroCambioMoneda { get; set; }
        public decimal montoTransaccion { get; set; }
        public int abandono { get; set; }
        public int cantidad_problemas { get; set; }



        utilidades utilidades=new utilidades();

        public void agregarCliente(cliente cliente)
        {
            string sql = "insert into cliente (idcliente,id_temporada,id_tanda,tanda,id_operacion,operacion, id_cajero,tipo_operacion,tiempo_promedio_fase1, tiempo_promedio_fase2,tiempo_promedio_fase3, tiempo_promedio_total,tiempo_fase1, tiempo_fase2,tiempo_fase3,tiempo_total,acepta_cuenta_ahorro, acepta_deposito_retiro,acepta_cuenta_ahorro_cambio_moneda, monto_transaccion,abandono,cantidad_problemas) values ('" + cliente.id + "','" + cliente.idTemporada + "','" + cliente.idTanda + "','" + cliente.tanda + "','" + cliente.idOperacion + "','" + cliente.operacion + "','" + cliente.idCajero + "','" + cliente.tipoOperacion + "','" + cliente.tiempoPromedioFase1 + "', '" + cliente.tiempoPromedioFase2 + "','" + cliente.tiempoPromedioFase3 + "','" + cliente.tiempoPromedioTotal + "','" + cliente.tiempoFase1 + "','" + cliente.tiempoFase2 + "','" + cliente.tiempoFase3 + "','" + cliente.tiempoTotal + "', '" + cliente.aceptaCuentaAhorro + "','" + cliente.aceptaDepositarRetiro + "','" + cliente.aceptaCuentaAhorroCambioMoneda + "','" + cliente.montoTransaccion + "','" + cliente.abandono + "','"+cliente.cantidad_problemas+"');";
            utilidades.ejecutarcomando_mysql(sql);
        }

        public List<cliente> getListaCliente()
        {
            try
            {
                List<cliente> lista = new List<cliente>();
                string sql = "select  idcliente,id_temporada,id_tanda,tanda,id_operacion,operacion,id_cajero,tipo_operacion,tiempo_promedio_fase1,tiempo_promedio_fase2,tiempo_promedio_fase3,tiempo_promedio_total,tiempo_fase1,tiempo_fase2,tiempo_fase3,tiempo_total,acepta_cuenta_ahorro,acepta_deposito_retiro,acepta_cuenta_ahorro_cambio_moneda,monto_transaccion,abandono,cantidad_problemas from cliente;";
                DataSet ds = utilidades.ejecutarcomando_mysql(sql);
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    cliente cliente = new cliente();
                    cliente.id = Convert.ToInt16(row[0]);
                    cliente.idTemporada = Convert.ToInt16(row[1]);
                    cliente.idTanda = Convert.ToInt16(row[2]);
                    cliente.tanda = row[3].ToString();
                    cliente.idOperacion = Convert.ToInt16(row[4]);
                    cliente.operacion = row[5].ToString();
                    cliente.idCajero = Convert.ToInt16(row[6]);
                    cliente.tipoOperacion = row[7].ToString();
                    cliente.tiempoPromedioFase1 = Convert.ToDecimal(row[8]);
                    cliente.tiempoPromedioFase2 = Convert.ToDecimal(row[9]);
                    cliente.tiempoPromedioFase3 = Convert.ToDecimal(row[10]);
                    cliente.tiempoPromedioTotal = Convert.ToDecimal(row[11]);
                    cliente.tiempoFase1 = Convert.ToDecimal(row[12]);
                    cliente.tiempoFase2 = Convert.ToDecimal(row[13]);
                    cliente.tiempoFase3 = Convert.ToDecimal(row[14]);
                    cliente.tiempoTotal = Convert.ToDecimal(row[15]);
                    cliente.aceptaCuentaAhorro = Convert.ToInt16(row[16]);
                    cliente.aceptaDepositarRetiro = Convert.ToInt16(row[17]);
                    cliente.aceptaCuentaAhorroCambioMoneda = Convert.ToInt16(row[18]);
                    cliente.montoTransaccion = Convert.ToDecimal(row[19]);
                    cliente.abandono = Convert.ToInt16(row[20]);
                    cliente.cantidad_problemas = Convert.ToInt16(row[21]);
                    
                    lista.Add(cliente);
                }

                return lista;
            }
            catch (Exception ex)
            {
                MessageBox.Show("error getListaCliente .:" + ex.ToString());
                return null;
            }
        }

        public void eliminarClientes()
        {
            string sql = "truncate table cliente;";
            utilidades.ejecutarcomando_mysql(sql);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimulacionCajeroBancoV2.objetos
{
    public class problema
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public double intervalo_final { get; set; }
        public int idFase { get; set; }
        public int idOperacion { get; set; }
        public int tiempoInicial { get; set; }
        public int tiempoFinal { get; set; }

        utilidades utilidades=new utilidades();

        public void agregarProblemas()
        {
            try
            {
                string sql = "truncate problema";
                utilidades.ejecutarcomando_mysql(sql);


                //deposito
                #region
                //fase1
                //nombre,intervalo_final,id_fase,id_operacion,tiempo_inicial,tiempo_final
                sql = "insert into problema(nombre,intervalo_final,id_fase,id_operacion,tiempo_inicial,tiempo_final)values('falla sistema','13','0','1','5','10');";
                utilidades.ejecutarcomando_mysql(sql);
                sql = "insert into problema(nombre,intervalo_final,id_fase,id_operacion,tiempo_inicial,tiempo_final)values('falla computadora','15','0','1','4','8');";
                utilidades.ejecutarcomando_mysql(sql);
                sql = "insert into problema(nombre,intervalo_final,id_fase,id_operacion,tiempo_inicial,tiempo_final)values('falla energia electrica','20','0','1','3','7');";
                utilidades.ejecutarcomando_mysql(sql);
                
             
                //fase2
                //nombre,intervalo_final,id_fase,id_operacion,tiempo_inicial,tiempo_final
                sql = "insert into problema(nombre,intervalo_final,id_fase,id_operacion,tiempo_inicial,tiempo_final)values('falta numero cuenta','13','2','1','2','4');";
                utilidades.ejecutarcomando_mysql(sql);
                sql = "insert into problema(nombre,intervalo_final,id_fase,id_operacion,tiempo_inicial,tiempo_final)values('numero cuenta incorrecto','25','1','2','3','4');";
                utilidades.ejecutarcomando_mysql(sql);
                sql = "insert into problema(nombre,intervalo_final,id_fase,id_operacion,tiempo_inicial,tiempo_final)values('falta dinero cliente','27','2','1','2','7');";
                utilidades.ejecutarcomando_mysql(sql);
                sql = "insert into problema(nombre,intervalo_final,id_fase,id_operacion,tiempo_inicial,tiempo_final)values('dinero mal estado','22','2','1','3','5');";
                utilidades.ejecutarcomando_mysql(sql);

                //fase3
                //nombre,intervalo_final,id_fase,id_operacion,tiempo_inicial,tiempo_final
                sql = "insert into problema(nombre,intervalo_final,id_fase,id_operacion,tiempo_inicial,tiempo_final)values('falla sistema','13','3','1','5','10');";
                utilidades.ejecutarcomando_mysql(sql);
                sql = "insert into problema(nombre,intervalo_final,id_fase,id_operacion,tiempo_inicial,tiempo_final)values('falla computadora','15','3','1','4','8');";
                utilidades.ejecutarcomando_mysql(sql);
                sql = "insert into problema(nombre,intervalo_final,id_fase,id_operacion,tiempo_inicial,tiempo_final)values('falla energia electrica','20','3','1','3','7');";
                utilidades.ejecutarcomando_mysql(sql);
                #endregion


                

                //retiro
                #region
                //fase1
                //nombre,intervalo_final,id_fase,id_operacion,tiempo_inicial,tiempo_final
                sql = "insert into problema(nombre,intervalo_final,id_fase,id_operacion,tiempo_inicial,tiempo_final)values('falla sistema','13','0','2','5','10');";
                utilidades.ejecutarcomando_mysql(sql);
                sql = "insert into problema(nombre,intervalo_final,id_fase,id_operacion,tiempo_inicial,tiempo_final)values('falla computadora','15','0','2','4','8');";
                utilidades.ejecutarcomando_mysql(sql);
                sql = "insert into problema(nombre,intervalo_final,id_fase,id_operacion,tiempo_inicial,tiempo_final)values('falla energia electrica','20','0','2','3','7');";
                utilidades.ejecutarcomando_mysql(sql);
               
                //fase2
                //nombre,intervalo_final,id_fase,id_operacion,tiempo_inicial,tiempo_final
                sql = "insert into problema(nombre,intervalo_final,id_fase,id_operacion,tiempo_inicial,tiempo_final)values('falta cedula','21','2','2','2','4');";
                utilidades.ejecutarcomando_mysql(sql);
                sql = "insert into problema(nombre,intervalo_final,id_fase,id_operacion,tiempo_inicial,tiempo_final)values('cedula mal estado','15','2','2','3','4');";
                utilidades.ejecutarcomando_mysql(sql);
                sql = "insert into problema(nombre,intervalo_final,id_fase,id_operacion,tiempo_inicial,tiempo_final)values('monto excede balance','27','2','2','2','7');";
                utilidades.ejecutarcomando_mysql(sql);
                sql = "insert into problema(nombre,intervalo_final,id_fase,id_operacion,tiempo_inicial,tiempo_final)values('dinero mal estado','15','2','2','3','5');";
                utilidades.ejecutarcomando_mysql(sql);

               
                //fase3
                //nombre,intervalo_final,id_fase,id_operacion,tiempo_inicial,tiempo_final
                sql = "insert into problema(nombre,intervalo_final,id_fase,id_operacion,tiempo_inicial,tiempo_final)values('falla sistema','13','3','0','5','10');";
                utilidades.ejecutarcomando_mysql(sql);
                sql = "insert into problema(nombre,intervalo_final,id_fase,id_operacion,tiempo_inicial,tiempo_final)values('falla computadora','15','3','0','4','8');";
                utilidades.ejecutarcomando_mysql(sql);
                sql = "insert into problema(nombre,intervalo_final,id_fase,id_operacion,tiempo_inicial,tiempo_final)values('falla energia electrica','20','0','2','3','7');";
                utilidades.ejecutarcomando_mysql(sql);
                #endregion
                


                //cambio moneda
                #region
                //fase1
                //nombre,intervalo_final,id_fase,id_operacion,tiempo_inicial,tiempo_final
                sql = "insert into problema(nombre,intervalo_final,id_fase,id_operacion,tiempo_inicial,tiempo_final)values('falla sistema','13','0','3','5','10');";
                utilidades.ejecutarcomando_mysql(sql);
                sql = "insert into problema(nombre,intervalo_final,id_fase,id_operacion,tiempo_inicial,tiempo_final)values('falla computadora','15','0','3','4','8');";
                utilidades.ejecutarcomando_mysql(sql);
                sql = "insert into problema(nombre,intervalo_final,id_fase,id_operacion,tiempo_inicial,tiempo_final)values('falla energia electrica','20','0','3','3','7');";
                utilidades.ejecutarcomando_mysql(sql);
                
                //fase2
                //nombre,intervalo_final,id_fase,id_operacion,tiempo_inicial,tiempo_final
                sql = "insert into problema(nombre,intervalo_final,id_fase,id_operacion,tiempo_inicial,tiempo_final)values('dinero mal estado','35','2','3','4','6');";
                utilidades.ejecutarcomando_mysql(sql);
                sql = "insert into problema(nombre,intervalo_final,id_fase,id_operacion,tiempo_inicial,tiempo_final)values('cliente no tiene monto pensado','20','2','3','3','4');";
                utilidades.ejecutarcomando_mysql(sql);
                sql = "insert into problema(nombre,intervalo_final,id_fase,id_operacion,tiempo_inicial,tiempo_final)values('el banco no tiene moneda de cambio','5','2','3','6','10');";
                utilidades.ejecutarcomando_mysql(sql);
                sql = "insert into problema(nombre,intervalo_final,id_fase,id_operacion,tiempo_inicial,tiempo_final)values('dinero mal estado','15','2','3','3','5');";
                utilidades.ejecutarcomando_mysql(sql);
                
                //fase3
                //nombre,intervalo_final,id_fase,id_operacion,tiempo_inicial,tiempo_final
                sql = "insert into problema(nombre,intervalo_final,id_fase,id_operacion,tiempo_inicial,tiempo_final)values('falla sistema','13','3','3','6','10');";
                utilidades.ejecutarcomando_mysql(sql);
                sql = "insert into problema(nombre,intervalo_final,id_fase,id_operacion,tiempo_inicial,tiempo_final)values('falla computadora','15','3','3','2','8');";
                utilidades.ejecutarcomando_mysql(sql);
                sql = "insert into problema(nombre,intervalo_final,id_fase,id_operacion,tiempo_inicial,tiempo_final)values('falla energia electrica','20','0','3','4','7');";
                utilidades.ejecutarcomando_mysql(sql);
                sql = "insert into problema(nombre,intervalo_final,id_fase,id_operacion,tiempo_inicial,tiempo_final)values('falla maquina contadora billete','35','0','3','4','7');";
                utilidades.ejecutarcomando_mysql(sql);
                #endregion



            }
            catch (Exception ex)
            {
                MessageBox.Show("error agregarProblemass .:" + ex.ToString());
            }
        }


        public List<problema> getListaProblemaByFaseAndOperacion(int idFase,int idOperacion)
        {
            try
            {
                List<problema> lista = new List<problema>();
                string sql = "SELECT id,nombre,intervalo_final,id_fase,id_operacion,tiempo_inicial,tiempo_final FROM problema where (id_fase='" + idFase + "' or id_fase='0') and id_operacion='" + idOperacion + "';";
                DataSet ds = utilidades.ejecutarcomando_mysql(sql);
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    problema problema = new problema();
                    problema.id = Convert.ToInt16(row[0]);
                    problema.nombre = row[1].ToString();
                    problema.intervalo_final = Convert.ToDouble(row[2]);
                    problema.idFase = Convert.ToInt16(row[3]);
                    problema.idOperacion = Convert.ToInt16(row[4]);
                    problema.tiempoInicial = Convert.ToInt16(row[5]);
                    problema.tiempoFinal = Convert.ToInt16(row[6]);
                    lista.Add(problema);
                }

                return lista;
            }
            catch (Exception ex)
            {
                MessageBox.Show("error getListaProblemaByFaseAndOperacion .:" + ex.ToString());
                return null;
            }
        }



        public List<problema> getListaProblemaByOperacion(int idOperacion)
        {
            try
            {
                List<problema> lista = new List<problema>();
                string sql = "SELECT id,nombre,intervalo_final,id_fase,id_operacion,tiempo_inicial,tiempo_final FROM problema where id_operacion='" + idOperacion + "';";
                DataSet ds = utilidades.ejecutarcomando_mysql(sql);
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    problema problema = new problema();
                    problema.id = Convert.ToInt16(row[0]);
                    problema.nombre = row[1].ToString();
                    problema.intervalo_final = Convert.ToDouble(row[2]);
                    problema.idFase = Convert.ToInt16(row[3]);
                    problema.idOperacion = Convert.ToInt16(row[4]);
                    problema.tiempoInicial = Convert.ToInt16(row[5]);
                    problema.tiempoFinal = Convert.ToInt16(row[6]);
                    lista.Add(problema);
                }

                return lista;
            }
            catch (Exception ex)
            {
                MessageBox.Show("error getListaProblemaByOperacion .:" + ex.ToString());
                return null;
            }
        }

    }
}

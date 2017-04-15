using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using SimulacionCajeroBancoV2.modelos;
using SimulacionCajeroBancoV2.objetos;
using SimulacionCajeroBancoV2.reporte;
using SimulacionCajeroBancoV2.views;

namespace SimulacionCajeroBancoV2
{
    public partial class Form1 : Form
    {
        //objetos
        private temporada temporada; // objeto con todos los atributos de la temporada
        private cliente cliente; // objeto con todos los atributos del cliente
        private tanda tanda; // objeto con todos los atributos de la tanda
        private problema problema; // objeto con todos los atributos del problema
        private cajero cajero; // objeto con todos los atributos del cajero
        private fases fase; //objeto con todos los atributos de las fases
        private operacion operacion;
        private operacion_tipo operacionTipo;

        //listas
        private List<tanda> listaTanda; // lista de las tandas disponibles
        private List<cliente> listaCliente; // lista de los clientes donde se almacena cada corrida
        private List<temporada> listaTemporada; // lista de las temporadas disponibles
        private List<cajero> listaCajero; // lista de los cajeros disponibles
        private problemasLogs problemasLogs; // lista de los problemas con detalles encontrado en la corrida
        private List<operacion> listaOperaciones;
        private List<fases> listaFases;
        private List<operacion_tipo> listaOperacionTipo;

        //lista de problema
        private List<problema> listaProblemaDeposito;
        private List<problema> listaProblemaRetiro;
        private List<problema> listaProblemaCambio;
        private List<problemasLogs> listaProblemaLogs;
        private List<problema> listaProblemas;

        //variables
        public temporada temporadaSeleccionada;
        public tanda tandaSeleccionada;
        private Int64 cantidadClientes = 0;
        private double randomDouble = 0;
        private int randomEntero = 0;
        private Random random;


        public Form1()
        {
            InitializeComponent();
            loadVentana();
        }

        public void loadVentana()
        {
            try
            {
                cajero = new cajero();
                fase = new fases();
                getListaFases();
                getListaTemporadas();
                getListaTandas();
                getListaOperaciones();




                //eliminando el historico
                cliente = new cliente();
                cliente.eliminarClientes();

                problemasLogs = new problemasLogs();
                problemasLogs.eliminarProblemasLogs();

                //load operaciones en base a la temporada seleccionada
                operacion.agregarOperacion(temporadaSeleccionada.id);

                //get lista de problemas intervalos en base a la temporada
                getListasProblema();

                //instanciando la lista de cliente
                listaCliente = new List<cliente>();

                problema = new problema();
                //instanciando la lista de los problemas log para el reporte
                listaProblemaLogs = new List<problemasLogs>();
                decimal tiempoTotal = 0;
                cliente.eliminarClientes();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loadVentana.:" + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }





        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            cajero.agregarCajero(0);
            MessageBox.Show("Se ha blanqueado la lista de cajeros", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (cantidadCajerosText.Text == "")
            {
                MessageBox.Show("Falta cantidad de cajeros");
                return;
            }
            listaCajero = new List<cajero>();

            cajero.agregarCajero(Convert.ToDouble(cantidadCajerosText.Text));

            listaCajero = cajero.getListaCajero();
            MessageBox.Show("Cajeros agregados");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        //proceso padre
        public void getAction()
        {
            try
            {
                if (validarGetAction() == false)
                {
                    return;
                }

                cliente.eliminarClientes();
                problemasLogs.eliminarProblemasLogs();

                for (int f = 1; f <= cantidadClientes; f++)
                {
                    //llenando los primeros datos de cliente actual
                    cliente = new cliente();
                    cliente.id = f;
                    cliente.abandono = 0;
                    cliente.idTemporada = temporadaSeleccionada.id;

                    //saber que cajero escogio el cliente
                    #region

                    cliente.idCajero = getIdCajeroByRandom(getNumeroRandom(1, 100));

                    #endregion


                    //obteniendo la tanda del cliente
                    #region

                    //tanda matutina con probabilidad de 60% y 40% tanda vespertina
                    if (getNumeroRandom(1, 100) <= 60)
                    {
                        //tanda matutina
                        cliente.idTanda = 1;
                        cliente.tanda = "matutina";
                    }
                    else
                    {
                        //tanda vespertina
                        cliente.idTanda = 2;
                        cliente.tanda = "vespertina";
                    }

                    #endregion


                    //obteniendo la operacion y el tipo de operacion
                    #region

                    operacionTipo = new operacion_tipo();
                    listaOperacionTipo = new List<operacion_tipo>();
                    randomEntero = 0;
                    randomEntero = getNumeroRandom(1, 100);
                    if (randomEntero >= 1 && randomEntero <= 43)
                    {
                        //deposito
                        cliente.idOperacion = 1;
                        cliente.operacion = "deposito";
                        //el deposito puede ser efectivo 60,cheque 30, transferencia 10
                        listaOperacionTipo = operacionTipo.getListaOperacionTipoByOperacionId(1);
                        randomEntero = getNumeroRandom(1, 100);
                        foreach (var x in listaOperacionTipo)
                        {
                            if (x.intervaloInicial <= randomEntero && x.intercaloFinal >= randomEntero)
                            {
                                cliente.tipoOperacion = x.tipoOperacion;
                                cliente.montoTransaccion = getNumeroRandom(x.montoIncial, x.montofinal);
                                break;
                            }
                        }

                    }
                    else if (randomEntero >= 43 && randomEntero <= 80)
                    {
                        //retiro
                        cliente.idOperacion = 2;
                        cliente.operacion = "retiro";
                        //para este caso el retiro mas bajo fue de 10,500 y el mayor fue de 122,000
                        listaOperacionTipo = operacionTipo.getListaOperacionTipoByOperacionId(2);
                        randomEntero = getNumeroRandom(1, 100);
                        foreach (var x in listaOperacionTipo)
                        {
                            if (x.intervaloInicial <= randomEntero && x.intercaloFinal >= randomEntero)
                            {
                                cliente.tipoOperacion = x.tipoOperacion;
                                cliente.montoTransaccion = getNumeroRandom(x.montoIncial, x.montofinal);
                                break;
                            }
                        }
                    }
                    else if (randomEntero >= 80 && randomEntero <= 100)
                    {
                        //cambio moneda
                        cliente.idOperacion = 3;
                        cliente.operacion = "cambio moneda";
                        listaOperacionTipo = operacionTipo.getListaOperacionTipoByOperacionId(3);
                        randomEntero = getNumeroRandom(1, 100);
                        foreach (var x in listaOperacionTipo)
                        {
                            if (x.intervaloInicial <= randomEntero && x.intercaloFinal >= randomEntero)
                            {
                                cliente.tipoOperacion = x.tipoOperacion;
                                cliente.montoTransaccion = getNumeroRandom(x.montoIncial, x.montofinal);
                                break;
                            }
                        }
                    }

                    #endregion


                    //simulando problemas
                    #region

                    decimal tiempoTotal = 0;
                    listaOperaciones = operacion.getListaOperacion();
                    listaOperaciones=listaOperaciones.Where(x => x.id == cliente.idOperacion).ToList();
                    foreach (var operacionActual in listaOperaciones)
                    {
                        //tiempos promedios
                        cliente.tiempoPromedioFase1 = operacionActual.tiempoFase1;
                        cliente.tiempoPromedioFase2 = operacionActual.tiempoFase2;
                        cliente.tiempoPromedioFase3 = operacionActual.tiempoFase3;
                        cliente.tiempoPromedioTotal = operacionActual.tiempoTotalFases;
                        cliente.tiempoFase1 = cliente.tiempoPromedioFase1;
                        cliente.tiempoFase2 = cliente.tiempoPromedioFase2;
                        cliente.tiempoFase3 = cliente.tiempoPromedioFase3;
                        cliente.tiempoTotal = cliente.tiempoPromedioTotal;

                        //operacion que selecciono el cliente
                        foreach (var faseActual in listaFases)
                        {
                            //las fases de la operacion actual incluyendo fase 0
                            listaProblemas = problema.getListaProblemaByFaseAndOperacion(faseActual.id,operacionActual.id);
                            foreach (var problemaActual in listaProblemas)
                            {
                                //los problemas de la fase y operacion actual
                                if (cliente.abandono == 0 && getNumeroRandom(1, 100) <= problemaActual.intervalo_final && (problemaActual.id==0 || problemaActual.idFase == faseActual.id) && problemaActual.idOperacion == operacionActual.id)
                                {
                                    //hay problema
                                    cliente.cantidad_problemas += 1;
                                    problemasLogs = new problemasLogs();
                                    problemasLogs.problema_encontrado = 1;
                                    problemasLogs.idcliente = cliente.id;
                                    problemasLogs.operacion = operacionActual.nombre;
                                    problemasLogs.fase = faseActual.nombre;
                                    problemasLogs.nombreProblema = problemaActual.nombre;
                                    problemasLogs.idCajero = cliente.idCajero;

                                    //el cliente decide si quedarse ante el problema o abandona la cola
                                    if (getNumeroRandom(1, 2) == 1)
                                    {
                                        //el cliente se queda en la fila
                                        cliente.abandono = 0;
                                        problemasLogs.cantidad_intentos += 1;
                                        problemasLogs.respuesta = "cliente se queda esperando";
                                    }
                                    else
                                    {
                                        //el cliente abandona la fila
                                        cliente.abandono = 1;
                                        problemasLogs.respuesta = "cliente abandona";
                                    }
                                    //saber el tiempo antes del problema que era el esperado en que termine dicha fase
                                    if (tiempoTotal == 0)
                                    {
                                        //es el primer problema, por lo tanto el tiempo hay que generarlo
                                        problemasLogs.tiempo_antes = (faseActual.id == 1)? operacionActual.tiempoFase1: operacionActual.tiempoFase2;
                                        problemasLogs.tiempo_antes = (faseActual.id == 2)? operacionActual.tiempoFase2: operacionActual.tiempoFase3;
                                    }
                                    else
                                    {
                                        //ya existia un problema entonce se coje el tiempo anterior
                                        problemasLogs.tiempo_antes += tiempoTotal;
                                    }
                                    tiempoTotal += problemasLogs.tiempo_antes;
                                    
                                    problemasLogs.tiempo_problema =(getNumeroRandom(problemaActual.tiempoInicial, problemaActual.tiempoFinal));
                                    //problemasLogs.tiempo_problema /= 100;
                                    problemasLogs.tiempo_despues = problemasLogs.tiempo_antes + problemasLogs.tiempo_problema;
                                    problemasLogs.tiempo_despues = Math.Round(problemasLogs.tiempo_despues, 2);
                                    tiempoTotal = problemasLogs.tiempo_despues;
                                    if (faseActual.id == 1)
                                    {
                                        cliente.tiempoFase1 += tiempoTotal;
                                    }
                                    else if (faseActual.id == 2)
                                    {
                                        cliente.tiempoFase2 += tiempoTotal;
                                    }
                                    else if (faseActual.id == 3)
                                    {
                                        cliente.tiempoFase3 += tiempoTotal;
                                    }
                                    cliente.tiempoTotal = cliente.tiempoFase1 + cliente.tiempoFase2 + cliente.tiempoFase3;

                                    problemasLogs.agregarLog(problemasLogs);
                                }
                            }
                        }
                    }

                    #endregion







                    cliente.agregarCliente(cliente);

                }

                loadListaCliente();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getAction.:" + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //boton getAction
        private void button1_Click(object sender, EventArgs e)
        {
            getAction();
        }

        //carga la lista de cliente en el dataGridView
        public void loadListaCliente()
        {
            try
            {
                listaCliente = new List<cliente>();
                listaCliente = cliente.getListaCliente();

                listaProblemaLogs = new List<problemasLogs>();
                listaProblemaLogs = problemasLogs.getListaProblemaLogs();


                if (listaCliente == null || listaCliente.Count == 0)
                {
                    MessageBox.Show("Error lista cliente esta nula, debe generar la corrida", "", MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return;
                }
                dataGridView1.Rows.Clear();
                dataGridView2.Rows.Clear();



                //llenando corrida de los clientes
                foreach (var x in  listaCliente)
                {
                    //cliente.tiempoTotal = cliente.tiempoFase1 + cliente.tiempoFase2 + cliente.tiempoFase3;
                    dataGridView1.Rows.Add(x.id, x.operacion + "-" + x.tipoOperacion, x.tanda, x.tiempoPromedioTotal,x.montoTransaccion.ToString("N"), x.cantidad_problemas, x.tiempoTotal, x.abandono, x.idCajero);
                }


                //llenando los problemas log de todos los clientes
                listaProblemaLogs = listaProblemaLogs.FindAll(x => x.problema_encontrado == 1);
                foreach (var x in listaProblemaLogs)
                {
                    dataGridView2.Rows.Add(x.idcliente, x.operacion, x.fase, x.tiempo_antes, x.tiempo_despues,x.nombreProblema, x.respuesta);
                }

                MessageBox.Show("Finalizó", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loadListaCliente.: " + ex.ToString(), "", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        //mostar ventana popup de los problemas
        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                Int64 idCliente = Convert.ToInt64(dataGridView1.CurrentRow.Cells[0].Value);
                //MessageBox.Show(idCliente.ToString());
                visor_problemas ventana = new visor_problemas(idCliente, listaProblemaLogs);
                ventana.Owner = this;
                ventana.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error doble click.: " + ex.ToString());
            }
        }

        //cara las temporadas
        public void getListaTemporadas()
        {
            try
            {
                temporada temporada = new temporada();

                temporada.agregarTemporadas();

                listaTemporada = new List<temporada>();
                listaTemporada = temporada.getListaTemporada();

                comboBoxTemporada.DataSource = listaTemporada;
                comboBoxTemporada.DisplayMember = "nombre";
                comboBoxTemporada.ValueMember = "id";

                temporadaSeleccionada = new temporada();
                temporadaSeleccionada = temporada.getemporadaById(Convert.ToInt16(comboBoxTemporada.SelectedValue));
                if (temporadaSeleccionada == null)
                {
                    MessageBox.Show("temporada seleccionada esta nula");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getTemporadas.: " + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //carga las tandas
        public void getListaTandas()
        {
            try
            {
                tanda tanda = new tanda();

                tanda.agregarTandas(temporadaSeleccionada.id);

                listaTanda = new List<tanda>();
                listaTanda = tanda.getListaTanda();

                comboBoxTanda.DataSource = listaTanda;
                comboBoxTanda.DisplayMember = "nombre";
                comboBoxTanda.ValueMember = "id";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getTandas.: " + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //get lista operaciones
        public void getListaOperaciones()
        {
            try
            {
                //lista de operaciones

                #region

                operacion = new operacion();
                operacion.agregarOperacion(Convert.ToInt16(comboBoxTemporada.SelectedValue));

                listaOperaciones = new List<operacion>();
                listaOperaciones = operacion.getListaOperacion();

                #endregion


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getListaOperaciones.:" + ex.ToString(), "", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        //get lista fases
        public void getListaFases()
        {
            try
            {
                fase.agregarFases();
                //lista de fases
                listaFases = new List<fases>();
                listaFases = fase.getListaFases();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getFases.:" + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //geera un numero random entre un rango de numero inicial y numero final
        public int getNumeroRandom(Int32 inicio, Int32 final)
        {
            random = new Random();
            Thread.Sleep(5);
            return random.Next(inicio, final + 1);
        }

        //validando antes de iniciar el proceso
        public bool validarGetAction()
        {
            try
            {
                //validar temporada
                if (comboBoxTemporada.Text == "")
                {
                    comboBoxTemporada.Focus();
                    comboBoxTemporada.SelectAll();
                    MessageBox.Show("Falta seleccionar la temporada", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                temporadaSeleccionada = new temporada();
                temporadaSeleccionada.id = Convert.ToInt16(comboBoxTemporada.SelectedValue);
                temporadaSeleccionada.nombre = comboBoxTemporada.Text;



                //validar cantidad cliente
                Int64 ca;
                if (Int64.TryParse(cantidadCajerosText.Text, out ca) == false)
                {
                    cantidadCajerosText.Focus();
                    cantidadCajerosText.SelectAll();
                    MessageBox.Show("falta cantidad de cajeros", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //validar cantidad cliente
                Int64 c;
                if (Int64.TryParse(cantidadClienteText.Text, out c) == false)
                {
                    cantidadClienteText.Focus();
                    cantidadClienteText.SelectAll();
                    MessageBox.Show("Formato de numero no es correcto en la cantidad de clientes", "",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                cantidadClientes = Convert.ToInt64(cantidadClienteText.Text);

                cajero.agregarCajero(3);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error validarGetAction.:" + ex.ToString(), "", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }
        }

        //obtiene un id cajero en base a un numero random con los intervalos del cajero
        public int getIdCajeroByRandom(double random)
        {
            try
            {
                random = random/100;
                foreach (var x in listaCajero)
                {
                    if (x.intervalo_inicial <= random && x.intervalo_final >= random)
                    {
                        return x.id;
                    }
                }
                return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getIdByRandom.: " + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
        }

        //get lista de problemas dependiendo del tipo de operacion
        public void getListasProblema()
        {
            try
            {
                //para asignar los intervalos de los problemas dependiendo de la temporada del cliente
                listaProblemaDeposito = new List<problema>();
                listaProblemaRetiro = new List<problema>();
                listaProblemaCambio = new List<problema>();

                //0-cualquier fase
                //1-cola espera
                //2-entrega datos
                //3-proceso solicitud

                problema = new problema();
                problema.agregarProblemas();

                listaProblemas = new List<problema>();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getListaProblema.: " + ex.ToString(), "", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        //reporte
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

                if (listaProblemaLogs == null)
                {
                    return;
                }

                listaProblemaLogs = listaProblemaLogs.FindAll(x => x.problema_encontrado == 1);


                //datos generales
                String reporte = "SimulacionCajeroBancoV2.reporte.reporte1.rdlc";
                List<ReportDataSource> listaReportDataSource = new List<ReportDataSource>();
                
                //encabezado
                reporte_encabezado reporteE=new reporte_encabezado(temporadaSeleccionada,Convert.ToInt16(cantidadCajerosText.Text),Convert.ToInt16(cantidadClienteText.Text),listaProblemaLogs,listaCliente);
                List<reporte_encabezado> listaReporteEncabezado=new List<reporte_encabezado>();
                listaReporteEncabezado.Add(reporteE);
                ReportDataSource reporteEncabezado = new ReportDataSource("reporte_encabezado", listaReporteEncabezado);
                listaReportDataSource.Add(reporteEncabezado);

                //llenar detalle
                ReportDataSource reporteDetalle1 = new ReportDataSource("reporte_detalle",listaProblemaLogs);
                listaReportDataSource.Add(reporteDetalle1);

                //llenar detalle problema y sus tiempo
                List<problema_tiempo> listaProblemaTiempo=new List<problema_tiempo>();
                listaProblemaTiempo = reporteE.listaProblemaTiempo;
                ReportDataSource reporteDetalle2 = new ReportDataSource("reporte_detalle_problema_tiempo", listaProblemaTiempo);
                listaReportDataSource.Add(reporteDetalle2);


                //llenar detalle cliente
                ReportDataSource reporteDetalle3 = new ReportDataSource("reporte_detalle_cliente", listaCliente);
                listaReportDataSource.Add(reporteDetalle3);


                List<ReportParameter> ListaReportParameter = new List<ReportParameter>();
                VisorReporteComun ventana = new VisorReporteComun(reporte, listaReportDataSource, ListaReportParameter);
                ventana.ShowDialog();
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error imprimiendo.:" + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }

}

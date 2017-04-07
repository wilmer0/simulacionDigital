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
using SimulacionCajeroBancoV2.modelos;
using SimulacionCajeroBancoV2.objetos;
using SimulacionCajeroBancoV2.views;

namespace SimulacionCajeroBancoV2
{
    public partial class Form1 : Form
    {


        //modelos
        modeloTemporada modeloTemporada=new modeloTemporada(); // toda la programacion con respecto a la temporada
        modeloTanda modeloTanda = new modeloTanda(); // toda la programacion con respecto a la tanda
        modeloCajero modeloCajero = new modeloCajero();// toda la programacion con respecto al cajero

        //objetos
        private temporada temporada; // objeto con todos los atributos de la temporada
        private cliente cliente;// objeto con todos los atributos del cliente
        private tanda tanda;// objeto con todos los atributos de la tanda
        private problema problema;// objeto con todos los atributos del problema
        private cajero cajero;// objeto con todos los atributos del cajero
        private fases fase;//objeto con todos los atributos de las fases
        private operacion operacion;
        private operacion operacionDeposito;
        private operacion operacionRetiro;
        private operacion operacionCambioMoneda;

        //listas
        private List<tanda> listaTanda; // lista de las tandas disponibles
        private List<cliente> listaCliente;// lista de los clientes donde se almacena cada corrida
        private List<temporada> listaTemporada;// lista de las temporadas disponibles
        private List<cajero> listaCajero;// lista de los cajeros disponibles
        private problemasLogs problemasLogs;// lista de los problemas con detalles encontrado en la corrida
        private List<operacion> listaOperaciones; 
      

        //lista de problema
        private List<problema> listaProblemaDeposito;
        private List<problema> listaProblemaRetiro;
        private List<problema> listaProblemaCambio;
        private List<int> listaNumero;
        private List<problemasLogs> listaProblemaLogs;
        private List<fases> listaFases;
        private List<fases> listaFasesDeposito;
        private List<fases> listaFasesRetiro;
        private List<fases> listaFasesCambioMoneda;
        private List<problema> listaProblemaSistema;
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
                listaCajero=new List<cajero>();
                getTemporadas();
                getTandas();
                getCajeros();
                getListaFases();
                getListaOperaciones();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loadVentana.:" + ex.ToString(), "", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

      

        public void getTemporadas()
        {
            try
            {
                listaTemporada = new List<temporada>();
                listaTemporada = modeloTemporada.getListaTemporada();

                comboBoxTemporada.DataSource = listaTemporada;
                comboBoxTemporada.DisplayMember = "nombre";
                comboBoxTemporada.ValueMember = "id";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getTemporadas.: " + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //carga las tandas
        public void getTandas()
        {
            try
            {
                listaTanda = new List<tanda>();
                listaTanda = modeloTanda.getListaTanda(Convert.ToInt16(comboBoxTemporada.SelectedValue.ToString()));

                comboBoxTanda.DataSource = listaTanda;
                comboBoxTanda.DisplayMember = "nombre";
                comboBoxTanda.ValueMember = "id";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getTandas.: " + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //carga los cajeros
        public void getCajeros()
        {
            try
            {
                listaCajero = new List<cajero>();
                listaCajero = modeloCajero.getListaCajero();

                comboBoxTipoCaja.DataSource = listaCajero;
                comboBoxTipoCaja.DisplayMember = "nombre";
                comboBoxTipoCaja.ValueMember = "id";

                loadListaCajeros();
                getCantidadCajeros();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getCajeros.: " + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
           getCajeros();
           MessageBox.Show("Se ha blanqueado la lista de cajeros","",MessageBoxButtons.OK,MessageBoxIcon.Warning);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                cajero= new cajero();
                cajero.id = (listaCajero.Count) + 1;
                cajero.nombre = "Cajero "+cajero.id;
                listaCajero.Add(cajero);
                loadListaCajeros();
                

            }
            catch (Exception ex)
            {

                MessageBox.Show("Error agregando el cajero.:" + ex.ToString(), "", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        //carga la cantidad de cajeros
        public void getCantidadCajeros()
        {
            try
            {
                cantidadCajerosLabel.Text = "cajeros: " + listaCajero.Count;
            }
            catch (Exception)
            {
                
            }
        }

        //carga la lista de cajeros
        public void loadListaCajeros()
        {
            double probabilidad = 0;
            double probabilidadTemporal = 0;
            double cantidadCajeros = 0;

            cantidadCajeros = listaCajero.Count;
            probabilidad = Math.Round((1/cantidadCajeros),2);

            foreach (var x in listaCajero)
            {
                x.intervalo_inicial = probabilidadTemporal;
                probabilidadTemporal += probabilidad;
                x.intervalo_final = probabilidadTemporal;
            }

            comboBoxTipoCaja.DataSource = listaCajero.ToList();
            comboBoxTipoCaja.DisplayMember = "nombre";
            comboBoxTipoCaja.ValueMember = "id";
            comboBoxTipoCaja.SelectedIndex = 0;
            getCantidadCajeros();
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
                    MessageBox.Show("Falta seleccionar la temporada", "", MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return false;
                }
                temporadaSeleccionada=new temporada();
                temporadaSeleccionada.id = Convert.ToInt16(comboBoxTemporada.SelectedValue);
                temporadaSeleccionada.nombre = comboBoxTemporada.Text;

                ////validar tanda 
                //if (comboBoxTanda.Text == "")
                //{
                //    comboBoxTanda.Focus();
                //    comboBoxTanda.SelectAll();
                //    MessageBox.Show("Falta seleccionar la tanda", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return false;
                //}
                //tandaSeleccionada=new tanda();
                //tandaSeleccionada.id = Convert.ToInt16(comboBoxTanda.ValueMember);
                //tandaSeleccionada.nombre = comboBoxTanda.Text;


                //valdar cajeros
                if (listaCajero.Count == 0)
                {
                    comboBoxTipoCaja.Focus();
                    comboBoxTipoCaja.SelectAll();
                    MessageBox.Show("Falta seleccionar los cajeros", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //validar cantidad cliente
                Int64 c;
                if (Int64.TryParse(cantidadClienteText.Text, out c) == false)
                {
                    cantidadClienteText.Focus();
                    cantidadClienteText.SelectAll();
                    MessageBox.Show("Formato de numero no es correcto en la cantidad de clientes", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                cantidadClientes = Convert.ToInt64(cantidadClienteText.Text);


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
                    if (x.intervalo_inicial<= random && x.intervalo_final >= random)
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

        //proceso padre
        public void getAction()
        {
            try
            {
                if (validarGetAction() == false)
                {
                    return;
                }
                //get lista de problemas intervalos en base a la temporada
                getListasProblema();


                //asignando los tiempos promedios esperado y finales bases cada fase de cada operacion en base a la temporada seleccionada
                #region
                operacionDeposito=new operacion();
                operacionRetiro=new operacion();
                operacionCambioMoneda=new operacion();
                foreach (var operacionActual in listaOperaciones)
                {
                    if (temporadaSeleccionada.id == 1)
                    {
                        //temporada 1
                        #region
                        if (operacionActual.id == 1)
                        {
                            //deposito
                            operacionActual.tiempoEsperadoCola = getNumeroRandom(100, 250); //de 1.0 a 2.5
                            operacionActual.tiempoEsperadoCola /= 100;

                            operacionActual.tiempoEsperadoEntregaDatos = getNumeroRandom(100, 180); //de 1 a 1.8
                            operacionActual.tiempoEsperadoEntregaDatos /= 100;

                            operacionActual.tiempoEsperadoProcesoSolicitud = getNumeroRandom(100, 190); //de 1.0 a 1.9
                            operacionActual.tiempoEsperadoProcesoSolicitud /= 100;

                            operacionActual.tiempoEsperadoTotal = operacionActual.tiempoEsperadoCola + operacionActual.tiempoEsperadoEntregaDatos + operacionActual.tiempoEsperadoProcesoSolicitud;
                        
                        }else if (operacionActual.id == 2)
                        {
                            //retiro
                            operacionActual.tiempoEsperadoCola = getNumeroRandom(120, 260); // 1.2 a 2.60
                            operacionActual.tiempoEsperadoCola /= 100;

                            operacionActual.tiempoEsperadoEntregaDatos = getNumeroRandom(180, 290); //de 1.8 a 2.9
                            operacionActual.tiempoEsperadoEntregaDatos /= 100;

                            operacionActual.tiempoEsperadoProcesoSolicitud = getNumeroRandom(200, 340); //de 2 a 3.4
                            operacionActual.tiempoEsperadoProcesoSolicitud /= 100;

                            operacionActual.tiempoEsperadoTotal = operacionActual.tiempoEsperadoCola + operacionActual.tiempoEsperadoEntregaDatos + operacionActual.tiempoEsperadoProcesoSolicitud;

                        }
                        else if (operacionActual.id == 3)
                        {
                            //cambio moneda
                            operacionActual.tiempoEsperadoCola = getNumeroRandom(120, 320); //de 1.2 a 3.20
                            operacionActual.tiempoEsperadoCola /= 100;

                            operacionActual.tiempoEsperadoEntregaDatos = getNumeroRandom(120, 350); // de 1.2 a 3.5
                            operacionActual.tiempoEsperadoEntregaDatos /= 100;

                            operacionActual.tiempoEsperadoProcesoSolicitud = getNumeroRandom(180, 220); //de  1.8 a 2.2
                            operacionActual.tiempoEsperadoProcesoSolicitud /= 100;

                            operacionActual.tiempoEsperadoTotal = operacionActual.tiempoEsperadoCola + operacionActual.tiempoEsperadoEntregaDatos + operacionActual.tiempoEsperadoProcesoSolicitud;
                        }
                        #endregion
                    }
                    else if (temporadaSeleccionada.id == 2)
                    {
                        //temporada 2
                        #region
                        #endregion

                    }
                    else if (temporadaSeleccionada.id == 3)
                    {
                        //temporada 3
                        #region
                        #endregion
                    }
                }
                
           
                #endregion


                //instanciando la lista de cliente
                listaCliente=new List<cliente>();

                //instanciando la lista de los problemas log para el reporte
                listaProblemaLogs = new List<problemasLogs>();
                problemasLogs = new problemasLogs();
                double tiempoTotal = 0;
                for (int f = 1; f <= cantidadClientes; f++)
                {
                    //llenando los primeros datos de cliente actual
                    cliente=new cliente();
                    cliente.id = f;
                    cliente.abandono = false;
                    cliente.idTemporada = temporadaSeleccionada.id;

                    //instanciando la lista de problemas del cliene
                    cliente.listaProblema = new List<problema>();
                    
                    //saber que cajero escogio el cliente
                    #region
                    cliente.idCajero = getIdCajeroByRandom(getNumeroRandom(1, 100));
                    #endregion

                    //obteniendo la tanda del cliente
                    #region
                    //tanda matutina con probabilidad de 41% y 59% tanda vespertina
                    //Thread.Sleep(30);
                    if (getNumeroRandom(1, 100) <= 41)
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

                    //obteniendo la operacion
                    #region
                    randomEntero = 0;
                    randomEntero = getNumeroRandom(1, 100);
                    if (randomEntero >= 1 && randomEntero<=43)
                    {
                        //deposito
                        cliente.idOperacion = 1;
                        cliente.operacion = "deposito";
                        #region
                        //el deposito puede ser efectivo 60,cheque 30, transferencia 10
                        randomEntero = 0;
                        randomEntero = getNumeroRandom(1, 100);
                        if (randomEntero >= 1 && randomEntero <= 60)
                        {
                            //deposito en efectivo
                            cliente.tipoOperacion = "efectivo";
                            //como el deposito es en efectivo si es monto > 45,000 se ofrece una cuenta de ahorro que el 38% acepta
                            //para este caso el deposito mas bajo fue de 5,000 y el mayor fue de 150,0000
                            cliente.montoTransaccion = getNumeroRandom(5000, 150000);
                            if (cliente.montoTransaccion > 45000)
                            {
                                //monto de transaccion > 45,000 se oferta cuenta de ahorro. 
                                randomEntero = getNumeroRandom(1, 100);
                                if (randomEntero >= 1 && randomEntero <= 38)
                                {
                                    //acepta cuenta de ahorro aumenta tiempo formula (-1/landa * LN(random()))
                                    cliente.aceptaCuentaAhorro = true;
                                    cliente.tiempoProcesoSolicitud += Math.Round((-1*0.03)*(Math.Log(random.NextDouble())),4);
                                }
                                else
                                {
                                    cliente.aceptaCuentaAhorro = false;
                                }
                            }
                        }else if (randomEntero >= 60 && randomEntero <= 90)
                        {
                            //deposito en cheque
                            cliente.tipoOperacion = "cheque";
                            //cuando es cheque el monto minimo es de 5000 y la maxima es de 85,000
                            cliente.montoTransaccion = getNumeroRandom(5000, 85000);
                        }else if (randomEntero >= 90 && randomEntero <= 100)
                        {
                            //deposito en transferencia
                            cliente.tipoOperacion = "transferencia";
                            //cuando es transferencia el monto minimo es de 3500 y la maxima es de 160,000
                            cliente.montoTransaccion = getNumeroRandom(3500, 160000);
                        }

                        #endregion
                    }
                    else if (randomEntero >= 43 && randomEntero <= 80)
                    {
                        //retiro
                        cliente.idOperacion = 2;
                        cliente.operacion = "retiro";
                        //para este caso el retiro mas bajo fue de 10,500 y el mayor fue de 122,000
                        cliente.montoTransaccion = getNumeroRandom(10500, 122000);
                        #region
                        //existe un 34% de que lo quiera depositar en otra cuenta
                        randomEntero = 0;
                        randomEntero = getNumeroRandom(1, 100);
                        if (randomEntero >= 1 && randomEntero <= 34)
                        {
                            //elije depositar el retiro en una cuenta de banco aumenta tiempo de proceso de solicitud
                            cliente.tipoOperacion= "deposita cuenta de banco";
                            cliente.aceptaDepositarRetiro = true;
                            cliente.tiempoProcesoSolicitud += Math.Round((-1 * 0.05) * (Math.Log(random.NextDouble())), 4);
                        }
                        else
                        {
                            //cliente hace retiro en efectivo
                            cliente.tipoOperacion = "efectivo";
                            cliente.aceptaDepositarRetiro = false;
                        }

                        #endregion
                    }
                    else if (randomEntero >= 80 && randomEntero <= 100)
                    {
                        //cambio moneda
                        cliente.idOperacion = 3;
                        cliente.operacion = "cambio moneda";
                        #region
                        //saber si cambio pesos a dolares o dolares a pesos
                        randomEntero = 0;
                        randomEntero = getNumeroRandom(1, 100);
                        if (randomEntero >= 1 && randomEntero <= 43)
                        {
                            cliente.tipoOperacion = "DP";
                            //dolares a pesos
                            cliente.cambioDolaresApesos = true;
                            cliente.cambioPesosADolares = false;
                            //para este caso el cambio mas bajo fue de 50 y el mayor fue de 2000
                            cliente.montoTransaccion = getNumeroRandom(50, 2000);
                        }
                        else
                        {
                            //pesos a dolares
                            cliente.tipoOperacion ="PD";
                            cliente.cambioPesosADolares = true;
                            cliente.cambioDolaresApesos = false;
                            //para este caso el deposito mas bajo fue de 4500 y el mayor fue de 80000
                            cliente.montoTransaccion = getNumeroRandom(4500, 80000);
                        }

                        //ofreciendo cuenta de ahorro en moneda que cambio un 20% acepta
                        randomEntero = 0;
                        randomEntero = getNumeroRandom(1, 100);
                        if (randomEntero >= 1 && randomEntero <= 20)
                        {
                            cliente.aceptaCuentaAhorroCambioMoneda = true;
                            //aumenta el tiempo de proceso
                            cliente.tiempoProcesoSolicitud += Math.Round((-1 * 0.07) * (Math.Log(random.NextDouble())), 4);
                        }
                        else
                        {
                            cliente.aceptaCuentaAhorroCambioMoneda = false;
                        }

                        #endregion
                    }
                    #endregion

                    
                    //simulando problemas
                    #region

                    tiempoTotal = 0;
                    foreach (var operacionActual in listaOperaciones.Where(x => x.id == cliente.idOperacion))
                    {
                       
                        //operacion que selecciono el cliente
                        foreach (var faseActual in listaFases)
                        {
                            
                            //las fases de la operacion actual
                            foreach (var problemaActual in listaProblemas.Where(p=> p.idFase==faseActual.id))
                            {
                                
                                //los problemas de la fase y operacion actual
                                if (getNumeroRandom(1, 100) <= problemaActual.intervalo_final && problemaActual.idFase==faseActual.id && problemaActual.idOperacion==operacionActual.id && cliente.abandono==false)
                                {
                                    //hay problema
                                    cliente.listaProblema.Add(problemaActual);
                                    problemasLogs =new problemasLogs();
                                    problemasLogs.problema_encontrado = true;
                                    problemasLogs.idcliente = cliente.id;
                                    problemasLogs.operacion = operacionActual.nombre;
                                    problemasLogs.fase = faseActual.nombre;
                                    problemasLogs.nombreProblema = problemaActual.nombre;
                                    problemasLogs.idCajero = cliente.idCajero;

                                    //saber el tiempo antes del problema que era el esperado en que termine dicha fase
                                    if (tiempoTotal == 0)
                                    {
                                        problemasLogs.tiempo_antes = (faseActual.id == 1)? operacionActual.tiempoEsperadoCola: operacionActual.tiempoEsperadoEntregaDatos;
                                        problemasLogs.tiempo_antes = (faseActual.id == 2)? operacionActual.tiempoEsperadoEntregaDatos: operacionActual.tiempoEsperadoProcesoSolicitud;
                                    }
                                    else
                                    {
                                        problemasLogs.tiempo_antes += tiempoTotal;
                                    }
                                    tiempoTotal += problemasLogs.tiempo_antes;
                                    
                                    problemasLogs.tiempo_problema =(getNumeroRandom(problemaActual.tiempoInicial, problemaActual.tiempoFinal));
                                    problemasLogs.tiempo_problema /= 100;
                                    problemasLogs.tiempo_despues = tiempoTotal + problemasLogs.tiempo_problema;
                                    problemasLogs.tiempo_despues = Math.Round(problemasLogs.tiempo_despues, 2);
                                    tiempoTotal = problemasLogs.tiempo_despues;

                                    listaProblemaLogs.Add(problemasLogs);

                                }
                            }

                        }

                    }
                    #endregion
                   









                    listaCliente.Add(cliente);
                    //loadListaCliente();
                }
            
            loadListaCliente();
            
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getAction.:" + ex.ToString(), "", MessageBoxButtons.OK,MessageBoxIcon.Error);
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
                if (listaCliente==null || listaCliente.Count == 0)
                {
                    MessageBox.Show("Error lista cliente esta nula, debe generar la corrida","", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
               

                dataGridView1.Rows.Clear();
                dataGridView2.Rows.Clear();
                //llenando corrida de los clientes
                foreach (var x in  listaCliente)
                {
                    if (x.idOperacion == null || x.operacion == "")
                    {
                        MessageBox.Show("cliente no tiene operacion-->" + x.id);
                    }
                    cliente.tiempoTotalServicio = cliente.tiempoCola + cliente.tiempoEntregaDatos + cliente.tiempoProcesoSolicitud;
                    dataGridView1.Rows.Add(x.id,x.operacion+"-"+x.tipoOperacion,x.tanda,x.tiempoEsperadoServicio,x.montoTransaccion.ToString("N"),x.listaProblema.Count.ToString("N"),x.tiempoTotalServicio,x.abandono,x.idCajero);
                }


                //llenando los problemas log de todos los clientes
                listaProblemaLogs = listaProblemaLogs.FindAll(x => x.problema_encontrado == true);
                foreach (var x in listaProblemaLogs)
                {
                    dataGridView2.Rows.Add(x.idcliente, x.operacion,x.fase,x.tiempo_antes,x.tiempo_despues,x.nombreProblema,x.respuesta);
                }

                MessageBox.Show("Finalizó", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loadListaCliente.: " + ex.ToString(), "", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        //geera un numero random entre un rango de numero inicial y numero final
        public int getNumeroRandom(int inicio, int final)
        {
            random = new Random();
            Thread.Sleep(5);
            return random.Next(inicio, final +1);
        }

        //get lista de problemas dependiendo del tipo de operacion
        public void getListasProblema()
        {
            try
            {
                //para asignar los intervalos de los problemas dependiendo de la temporada del cliente
                listaProblemaSistema = new List<problema>();
                listaProblemaDeposito = new List<problema>();
                listaProblemaRetiro = new List<problema>();
                listaProblemaCambio = new List<problema>();
                listaProblemas=new List<problema>();
                
                //0-cualquier fase
                //1-cola espera
                //2-entrega datos
                //3-proceso solicitud

                //temporada ==1
                if (temporadaSeleccionada.id == 1)
                {
                    //deposito
                    #region

                    //fase cola espera
                    #region
                    //falla computadora 13% tiempo de 3-10 minutos
                    problema = new problema();
                    problema.id = 1;
                    problema.nombre = "falla computadora";
                    problema.intervalo_inicial = 0;
                    problema.intervalo_final = 13;
                    problema.idFase = 1;
                    problema.idOperacion = 1;
                    problema.tiempoInicial = 300;
                    problema.tiempoFinal = 1000;
                    listaProblemas.Add(problema);
                    //falla sistema 12% tiempo de 5-30 minutos
                    problema = new problema();
                    problema.id = 2;
                    problema.nombre = "falla sistema";
                    problema.intervalo_inicial = 0;
                    problema.intervalo_final = 12;
                    problema.idFase = 1;
                    problema.idOperacion = 1;
                    problema.tiempoInicial = 500;
                    problema.tiempoFinal = 3000;
                    listaProblemas.Add(problema);
                    //falla energia electrica 15% tiempo de 3-8 minutos
                    problema = new problema();
                    problema.id = 3;
                    problema.nombre = "falla energia electrica";
                    problema.intervalo_inicial = 0;
                    problema.intervalo_final = 15;
                    problema.idFase = 1;
                    problema.idOperacion = 1;
                    problema.tiempoInicial = 300;
                    problema.tiempoFinal = 800;
                    listaProblemas.Add(problema);
                    #endregion
                    
                    //entrega datos
                    #region
                    //-falto numero cuenta-13% tiempo de 1-4 minutos
                    problema = new problema();
                    problema.id = 4;
                    problema.nombre = "falta numero cuenta";
                    problema.intervalo_inicial = 0;
                    problema.intervalo_final = 13;
                    problema.idFase = 2;
                    problema.idOperacion = 1;
                    problema.tiempoInicial = 100;
                    problema.tiempoFinal = 400;
                    listaProblemas.Add(problema);
                    //-numero cuenta incorrecto -25% tiempo de 3-5 minutos
                    problema = new problema();
                    problema.id = 5;
                    problema.nombre = "numero cuenta incorrecto";
                    problema.intervalo_inicial = 0;
                    problema.intervalo_final = 25;
                    problema.idFase = 2;
                    problema.idOperacion = 1;
                    problema.tiempoInicial = 300;
                    problema.tiempoFinal = 500;
                    listaProblemas.Add(problema);
                    //-dinero en efectivo en mal estado -22% tiempo de 1-3 minutos
                    problema = new problema();
                    problema.id = 6;
                    problema.nombre = "dinero mal estado";
                    problema.intervalo_inicial = 0;
                    problema.intervalo_final = 22;
                    problema.idFase = 2;
                    problema.idOperacion = 1;
                    problema.tiempoInicial = 100;
                    problema.tiempoFinal = 300;
                    listaProblemas.Add(problema);
                    #endregion

                    //proceso solicitud
                    #region
                    //-falla sistema -15% tiempo de 1-15 minutos
                    problema = new problema();
                    problema.id = 7;
                    problema.nombre = "falla sistema";
                    problema.intervalo_inicial = 0;
                    problema.intervalo_final = 15;
                    problema.idFase = 3;
                    problema.idOperacion = 1;
                    problema.tiempoInicial = 100;
                    problema.tiempoFinal = 1500;
                    listaProblemas.Add(problema);
                    //-dinero en efectivo en mal estado -22% tiempo de 1-15 minutos
                    problema = new problema();
                    problema.id = 7;
                    problema.nombre = "dinero mal estado";
                    problema.intervalo_inicial = 0;
                    problema.intervalo_final = 22;
                    problema.idFase = 3;
                    problema.idOperacion = 1;
                    problema.tiempoInicial = 100;
                    problema.tiempoFinal = 1500;
                    listaProblemas.Add(problema);
                    //-falla computadora -19% tiempo de 1-10 minutos
                    problema = new problema();
                    problema.id = 7;
                    problema.nombre = "falla computadora";
                    problema.intervalo_inicial = 0;
                    problema.intervalo_final = 19;
                    problema.idFase = 3;
                    problema.idOperacion = 1;
                    problema.tiempoInicial = 100;
                    problema.tiempoFinal = 10000;
                    listaProblemas.Add(problema);

                    #endregion








                    #endregion
                }
                else if (temporadaSeleccionada.id == 2)
                {
                    //problemas generales del sistema
                    #region
                    
                    #endregion

                    //problemas deposito
                    #region
                   
                    #endregion

                    //problemas retiro
                    #region
                    #endregion

                    //problemas cambio moneda
                    #region
                    #endregion

                }
                else if (temporadaSeleccionada.id == 3)
                {

                    //problemas generales del sistema
                    #region
                   
                    #endregion

                    //problemas deposito
                    #region
                    #endregion

                    //problemas retiro
                    #region
                    #endregion

                    //problemas cambio moneda
                    #region
                    #endregion

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getListaProblema.: " + ex.ToString(), "", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        //get lista fases por operacion
        public void getListaFases()
        {
            try
            {
                //lista de fases
                #region
                listaFases = new List<fases>();
                //cola de espera
                fase = new fases();
                fase.id = 1;
                fase.nombre = "cola espera";
                listaFases.Add(fase);

                //entrega de datos
                fase = new fases();
                fase.id = 2;
                fase.nombre = "entrega datos";
                listaFases.Add(fase);
                
                //proceso de solicitud
                fase = new fases();
                fase.id = 3;
                fase.nombre = "proceso solicitud";
                listaFases.Add(fase);
                #endregion


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getFases.:" + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //get lista operaciones
        public void getListaOperaciones()
        {
            try
            {
                //lista de operaciones
                #region
                listaOperaciones = new List<operacion>();
                //deposito
                operacion = new operacion();
                operacion.id = 1;
                operacion.nombre = "deposito";
                listaOperaciones.Add(operacion);

                //retiro
                operacion = new operacion();
                operacion.id = 2;
                operacion.nombre = "retiro";
                listaOperaciones.Add(operacion);
                
                //cambio moneda
                operacion = new operacion();
                operacion.id = 3;
                operacion.nombre = "cambio moneda";
                listaOperaciones.Add(operacion);
                
                operacionDeposito=new operacion();
                operacionRetiro=new operacion();
                operacionCambioMoneda=new operacion();

                #endregion


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getListaOperaciones.:" + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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
    }
}

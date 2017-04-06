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

        //listas
        private List<tanda> listaTanda; // lista de las tandas disponibles
        private List<cliente> listaCliente;// lista de los clientes donde se almacena cada corrida
        private List<temporada> listaTemporada;// lista de las temporadas disponibles
        private List<cajero> listaCajero;// lista de los cajeros disponibles
        private problemasLogs problemasLogs;// lista de los problemas con detalles encontrado en la corrida

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
                getListaNumeros();
                getListaFases();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loadVentana.:" + ex.ToString(), "", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        public void getListaNumeros()
        {
            listaNumero=new List<int>();
            for (int f = 1; f <=100; f++)
            {
                listaNumero.Add(f);
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


                listaCliente=new List<cliente>();

                for (int f = 1; f <= cantidadClientes; f++)
                {
                    //llenando los primeros datos de cliente actual
                    cliente=new cliente();
                    cliente.id = f;
                    cliente.abandono = false;
                    cliente.idTemporada = temporadaSeleccionada.id;


                    //saber que cajero escogio el cliente
                    #region
                    randomEntero = getNumeroRandom(1, 100);
                    cliente.idCajero = getIdCajeroByRandom(randomEntero);
                    #endregion


                    //obteniendo la tanda del cliente
                    #region
                    //tanda matutina con probabilidad de 41% y 59% tanda vespertina
                    //Thread.Sleep(30);
                    randomEntero = getNumeroRandom(1, 100);
                    if (randomEntero <= 41)
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
                    randomEntero = getNumeroRandom(1, 100);
                    if (randomEntero >= 1 && randomEntero<=43)
                    {
                        //deposito
                        cliente.idOperacion = 1;
                        cliente.operacion = "deposito";
                        #region
                        //el deposito puede ser efectivo 60,cheque 30, transferencia 10
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
                        randomEntero = getNumeroRandom(1, 100);
                        if (randomEntero >= 1 && randomEntero <= 34)
                        {
                            //elije depositar el retiro en una cuenta de banco aumenta tiempo de proceso de solicitud
                            cliente.aceptaDepositarRetiro = true;
                            cliente.tiempoProcesoSolicitud += Math.Round((-1 * 0.05) * (Math.Log(random.NextDouble())), 4);
                        }
                        else
                        {
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
                        randomEntero = getNumeroRandom(1, 100);
                        if (randomEntero >= 1 && randomEntero <= 43)
                        {
                            cliente.operacion += "-DP";
                            //dolares a pesos
                            cliente.cambioDolaresApesos = true;
                            cliente.cambioPesosADolares = false;
                            //para este caso el cambio mas bajo fue de 50 y el mayor fue de 2000
                            cliente.montoTransaccion = getNumeroRandom(50, 2000);
                        }
                        else
                        {
                            //pesos a dolares
                            cliente.operacion += "-PD";
                            cliente.cambioPesosADolares = true;
                            cliente.cambioDolaresApesos = false;
                            //para este caso el deposito mas bajo fue de 4500 y el mayor fue de 80000
                            cliente.montoTransaccion = getNumeroRandom(4500, 80000);
                        }

                        //ofreciendo cuenta de ahorro en moneda que cambio un 20% acepta
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


                    //asignando los tiempos promedios esperado y finales bases cada fase de cada operacion en base a la temporada
                    #region
                    if (cliente.idTemporada == 1)
                    {
                        //temporada 1 normal (media cliente dura un tiempo promedio normal)
                        #region
                        if (cliente.idOperacion == 1)
                        {
                            //deposito
                            cliente.tiempoEsperadoCola = getNumeroRandom(200,250); //de 2.0 a 2.5
                            cliente.tiempoEsperadoCola /= 100;
                            cliente.tiempoCola = cliente.tiempoEsperadoCola;
                            
                            cliente.tiempoEsperadoEntregaDatos = getNumeroRandom(100, 180); //de 1 a 1.8
                            cliente.tiempoEsperadoEntregaDatos /= 100;
                            cliente.tiempoEntregaDatos = cliente.tiempoEsperadoEntregaDatos;
                            
                            cliente.tiempoEsperadoProcesoSolicitud = getNumeroRandom(100, 150); //de 1.0 a 1.5
                            cliente.tiempoEsperadoProcesoSolicitud /= 100;
                            cliente.tiempoProcesoSolicitud = cliente.tiempoEsperadoProcesoSolicitud;
                            
                            cliente.tiempoEsperadoServicio = cliente.tiempoEsperadoCola+ cliente.tiempoEsperadoEntregaDatos + cliente.tiempoEsperadoProcesoSolicitud;
                        }
                        else if (cliente.idOperacion == 2)
                        {
                            //retiro
                            cliente.tiempoEsperadoCola = getNumeroRandom(200, 220); // 2.0 a 2.2
                            cliente.tiempoEsperadoCola /= 100;
                            cliente.tiempoCola = cliente.tiempoEsperadoCola;
                            
                            cliente.tiempoEsperadoEntregaDatos = getNumeroRandom(180, 210); //de 1.8 a 2.1
                            cliente.tiempoEsperadoEntregaDatos /= 100;
                            cliente.tiempoEntregaDatos = cliente.tiempoEsperadoEntregaDatos;
                            
                            cliente.tiempoEsperadoProcesoSolicitud = getNumeroRandom(100, 130); //de 1.0 a 1.3
                            cliente.tiempoEsperadoProcesoSolicitud /= 100;
                            cliente.tiempoProcesoSolicitud = cliente.tiempoEsperadoProcesoSolicitud;
                            
                            cliente.tiempoEsperadoServicio = cliente.tiempoEsperadoCola + cliente.tiempoEsperadoEntregaDatos + cliente.tiempoEsperadoProcesoSolicitud;
                        }
                        else if (cliente.idOperacion == 3)
                        {
                            //cambio moneda
                            cliente.tiempoEsperadoCola = getNumeroRandom(120, 150);  //de 1.2 a 1.5
                            cliente.tiempoEsperadoCola /= 100;
                            cliente.tiempoCola = cliente.tiempoEsperadoCola;

                            cliente.tiempoEsperadoEntregaDatos = getNumeroRandom(120, 150); // de 1.2 a 1.5
                            cliente.tiempoEsperadoEntregaDatos /= 100;
                            cliente.tiempoEntregaDatos = cliente.tiempoEsperadoEntregaDatos;

                            cliente.tiempoEsperadoProcesoSolicitud = getNumeroRandom(180, 200); //de  1.8 a 2.0
                            cliente.tiempoEsperadoProcesoSolicitud /= 100;
                            cliente.tiempoProcesoSolicitud = cliente.tiempoEsperadoProcesoSolicitud;
                            
                            cliente.tiempoEsperadoServicio = cliente.tiempoEsperadoCola + cliente.tiempoEsperadoEntregaDatos + cliente.tiempoEsperadoProcesoSolicitud;
                        }
                        #endregion
                    }
                    else if (cliente.idTemporada == 2)
                    {
                        //temporada 2 baja (cliente dura poco tiempo)
                        #region
                        if (cliente.idOperacion == 1)
                        {
                            //deposito
                            cliente.tiempoEsperadoCola = getNumeroRandom(100, 200); // de 1 a 2
                            cliente.tiempoEsperadoCola /= 100;
                            cliente.tiempoCola = cliente.tiempoEsperadoCola;

                            cliente.tiempoEsperadoEntregaDatos = getNumeroRandom(100, 150);  // 1 a 1.5
                            cliente.tiempoEsperadoEntregaDatos /= 100;
                            cliente.tiempoEntregaDatos = cliente.tiempoEsperadoEntregaDatos;

                            cliente.tiempoEsperadoProcesoSolicitud = getNumeroRandom(100, 140); // de 1.0 a 1.4
                            cliente.tiempoEsperadoProcesoSolicitud /= 100;
                            cliente.tiempoProcesoSolicitud = cliente.tiempoEsperadoProcesoSolicitud;

                            cliente.tiempoEsperadoServicio = cliente.tiempoEsperadoCola + cliente.tiempoEsperadoEntregaDatos + cliente.tiempoEsperadoProcesoSolicitud;
                        }
                        else if (cliente.idOperacion == 2)
                        {
                            //retiro
                            cliente.tiempoEsperadoCola = getNumeroRandom(100, 130);  //de 1.0 a 1.3
                            cliente.tiempoEsperadoCola /= 100;
                            cliente.tiempoCola = cliente.tiempoEsperadoCola;

                            cliente.tiempoEsperadoEntregaDatos = getNumeroRandom(100, 150); // de 1 a 1.5
                            cliente.tiempoEsperadoEntregaDatos /= 100;
                            cliente.tiempoEntregaDatos = cliente.tiempoEsperadoEntregaDatos;

                            cliente.tiempoEsperadoProcesoSolicitud = getNumeroRandom(100, 130); // de 1.0 a 1.3
                            cliente.tiempoEsperadoProcesoSolicitud /= 100;
                            cliente.tiempoProcesoSolicitud = cliente.tiempoEsperadoProcesoSolicitud;

                            cliente.tiempoEsperadoServicio = cliente.tiempoEsperadoCola + cliente.tiempoEsperadoEntregaDatos + cliente.tiempoEsperadoProcesoSolicitud;
                        }
                        else if (cliente.idOperacion == 3)
                        {
                            //cambio moneda
                            cliente.tiempoEsperadoCola = getNumeroRandom(100, 140);  // de 1 a 1.4
                            cliente.tiempoEsperadoCola /= 100;
                            cliente.tiempoCola = cliente.tiempoEsperadoCola;

                            cliente.tiempoEsperadoEntregaDatos = getNumeroRandom(130, 150); //de 1.3 a 1.5
                            cliente.tiempoEsperadoEntregaDatos /= 100;
                            cliente.tiempoEntregaDatos = cliente.tiempoEsperadoEntregaDatos;

                            cliente.tiempoEsperadoProcesoSolicitud = getNumeroRandom(140, 180); //de 1.4 a 1.8
                            cliente.tiempoEsperadoProcesoSolicitud /= 100;
                            cliente.tiempoProcesoSolicitud = cliente.tiempoEsperadoProcesoSolicitud;

                            cliente.tiempoEsperadoServicio = cliente.tiempoEsperadoCola + cliente.tiempoEsperadoEntregaDatos + cliente.tiempoEsperadoProcesoSolicitud;
                        }
                        #endregion
                    }
                    else if (cliente.idTemporada == 3)
                    {
                        //temporada 3 alta (mas dura el cliente)
                        #region
                        if (cliente.idOperacion == 1)
                        {
                            //deposito
                            cliente.tiempoEsperadoCola = getNumeroRandom(100, 450); // de 1.0 a 4.5
                            cliente.tiempoEsperadoCola /= 100;
                            cliente.tiempoCola = cliente.tiempoEsperadoCola;

                            cliente.tiempoEsperadoEntregaDatos = getNumeroRandom(100, 180); // 1 a 1.8
                            cliente.tiempoEsperadoEntregaDatos /= 100;
                            cliente.tiempoEntregaDatos = cliente.tiempoEsperadoEntregaDatos;

                            cliente.tiempoEsperadoProcesoSolicitud = getNumeroRandom(100, 280); // de 1.0 a 2.8
                            cliente.tiempoEsperadoProcesoSolicitud /= 100;
                            cliente.tiempoProcesoSolicitud = cliente.tiempoEsperadoProcesoSolicitud;

                            cliente.tiempoEsperadoServicio = cliente.tiempoEsperadoCola + cliente.tiempoEsperadoEntregaDatos + cliente.tiempoEsperadoProcesoSolicitud;
                        }
                        else if (cliente.idOperacion == 2)
                        {
                            //retiro
                            cliente.tiempoEsperadoCola = getNumeroRandom(200, 350); // de 2.0 a 3.5
                            cliente.tiempoEsperadoCola /= 100;
                            cliente.tiempoCola = cliente.tiempoEsperadoCola;

                            cliente.tiempoEsperadoEntregaDatos = getNumeroRandom(170, 220); // de 1.7 a 2.2
                            cliente.tiempoEsperadoEntregaDatos /= 100;
                            cliente.tiempoEntregaDatos = cliente.tiempoEsperadoEntregaDatos;

                            cliente.tiempoEsperadoProcesoSolicitud = getNumeroRandom(100, 170); //de 1.0 a 1.7
                            cliente.tiempoEsperadoProcesoSolicitud /= 100;
                            cliente.tiempoProcesoSolicitud = cliente.tiempoEsperadoProcesoSolicitud;

                            cliente.tiempoEsperadoServicio = cliente.tiempoEsperadoCola + cliente.tiempoEsperadoEntregaDatos + cliente.tiempoEsperadoProcesoSolicitud;
                        }
                        else if (cliente.idOperacion == 3)
                        {
                            //cambio moneda
                            cliente.tiempoEsperadoCola = getNumeroRandom(100, 250);  // de 1 a 2.5
                            cliente.tiempoEsperadoCola /= 100;
                            cliente.tiempoCola = cliente.tiempoEsperadoCola;

                            cliente.tiempoEsperadoEntregaDatos = getNumeroRandom(100, 150); // de 1 a 1.5
                            cliente.tiempoEsperadoEntregaDatos /= 100;
                            cliente.tiempoEntregaDatos = cliente.tiempoEsperadoEntregaDatos;

                            cliente.tiempoEsperadoProcesoSolicitud = getNumeroRandom(140, 200); // de  1.4 a 2.0
                            cliente.tiempoEsperadoProcesoSolicitud /= 100;
                            cliente.tiempoProcesoSolicitud = cliente.tiempoEsperadoProcesoSolicitud;

                            cliente.tiempoEsperadoServicio = cliente.tiempoEsperadoCola + cliente.tiempoEsperadoEntregaDatos + cliente.tiempoEsperadoProcesoSolicitud;
                        }
                      
                        #endregion
                    }
                    #endregion


                    //simulando problemas
                    #region

                    //instanciando la lista de problemas del cliene
                    cliente.listaProblema = new List<problema>();

                    //instanciando la lista de los problemas log para el reporte
                    listaProblemaLogs =new List<problemasLogs>();
                    problemasLogs=new problemasLogs();

                    getListasProblema(cliente);
                    if (cliente.idTemporada == 1)
                    {
                        //temporada 1
                        #region
                        if (cliente.idOperacion == 1)
                        {
                            //deposito
                            #region

                            //recorriendo las fases
                            foreach (var faceActual in listaFases)
                            {
                                #region
                                if (faceActual.id == 1)
                                {
                                    //cola espera
                                    #region
                                    #endregion
                                }
                                else if (faceActual.id == 2)
                                {
                                    //entrega datos
                                    #region


                                    //falta numero de cuenta 13%
                                    #region
                                    randomEntero = getNumeroRandom(1, 100);
                                    if (randomEntero <= 13 && cliente.abandono == false)
                                    {
                                        problema = new problema();
                                        problema.id = cliente.listaProblema.Count + 1;
                                        problema.nombre = "falta numero cuenta";
                                        cliente.listaProblema.Add(problema);

                                        problemasLogs = new problemasLogs();
                                        problemasLogs.problema_encontrado = true;
                                        problemasLogs.idcliente = cliente.id;
                                        problemasLogs.fase = faceActual.nombre;
                                        //tiempo que tenia el cliente cuando se presento el problema
                                        problemasLogs.tiempo_antes = cliente.tiempoEntregaDatos;

                                        //saber si el cliente espera o se va
                                        if (getNumeroRandom(1, 2) == 1)
                                        {
                                            //cliente lo intenta y revisa de nuevo
                                            problemasLogs.cantidad_intentos += 1;
                                            problemasLogs.desicion = "cliente revisa de nuevo";
                                            cliente.tiempoEsperadoEntregaDatos += ((getNumeroRandom(0, 200))/100);
                                        }
                                        else
                                        {
                                            //cliente se va
                                            problemasLogs.desicion = "cliente abandona";
                                            cliente.abandono = true;
                                            problemasLogs.tiempo_despues = cliente.tiempoEntregaDatos;
                                        }

                                        //tiempo que tiene el cliente ahora despues de que el cliente espera o se va
                                        problemasLogs.tiempo_despues = 0;
                                        
                                        listaProblemaLogs.Add(problemasLogs);
                                    }
                                    #endregion
                                    
                                    //numero cuenta incorrecto 25%
                                    #region
                                    randomEntero = getNumeroRandom(1, 100);
                                    if (randomEntero <= 25 && cliente.abandono == false)
                                    {
                                        problema = new problema();
                                        problema.id = cliente.listaProblema.Count + 1;
                                        problema.nombre = "numero cuenta incorrecto";
                                        cliente.listaProblema.Add(problema);

                                        problemasLogs = new problemasLogs();
                                        problemasLogs.problema_encontrado = true;
                                        problemasLogs.idcliente = cliente.id;
                                        problemasLogs.fase = faceActual.nombre;
                                        //tiempo que tenia el cliente cuando se presento el problema
                                        problemasLogs.tiempo_antes = cliente.tiempoEntregaDatos;

                                        //saber si el cliente espera o se va
                                        if (getNumeroRandom(1, 2) == 1)
                                        {
                                            //cliente lo intenta y revisa de nuevo
                                            problemasLogs.cantidad_intentos += 1;
                                            problemasLogs.desicion = "cliente revisa de nuevo";
                                            cliente.tiempoEsperadoEntregaDatos += ((getNumeroRandom(0, 200)) / 100);
                                        }
                                        else
                                        {
                                            //cliente se va
                                            problemasLogs.desicion = "cliente abandona";
                                            cliente.abandono = true;
                                            problemasLogs.tiempo_despues = cliente.tiempoEntregaDatos;
                                        }

                                        //tiempo que tiene el cliente ahora despues de que el cliente espera o se va
                                        problemasLogs.tiempo_despues = 0;

                                        listaProblemaLogs.Add(problemasLogs);
                                    }
                                    #endregion

                                    //falta dinero por parte del cliente 27%
                                    #region
                                    randomEntero = getNumeroRandom(1, 100);
                                    if (randomEntero <= 27 && cliente.abandono == false)
                                    {
                                        problema = new problema();
                                        problema.id = cliente.listaProblema.Count + 1;
                                        problema.nombre = "falta dinero cliente";
                                        cliente.listaProblema.Add(problema);

                                        problemasLogs = new problemasLogs();
                                        problemasLogs.problema_encontrado = true;
                                        problemasLogs.idcliente = cliente.id;
                                        problemasLogs.fase = faceActual.nombre;
                                        //tiempo que tenia el cliente cuando se presento el problema
                                        problemasLogs.tiempo_antes = cliente.tiempoEntregaDatos;

                                        //saber si el cliente espera o se va
                                        if (getNumeroRandom(1, 2) == 1)
                                        {
                                            //cliente lo intenta y revisa de nuevo
                                            problemasLogs.cantidad_intentos += 1;
                                            problemasLogs.desicion = "cliente revisa de nuevo";
                                            cliente.tiempoEsperadoEntregaDatos += ((getNumeroRandom(0, 200)) / 100);
                                        }
                                        else
                                        {
                                            //cliente se va
                                            problemasLogs.desicion = "cliente abandona";
                                            cliente.abandono = true;
                                            problemasLogs.tiempo_despues = cliente.tiempoEntregaDatos;
                                        }

                                        //tiempo que tiene el cliente ahora despues de que el cliente espera o se va
                                        problemasLogs.tiempo_despues = 0;

                                        listaProblemaLogs.Add(problemasLogs);
                                    }
                                    #endregion

                                    //dinero efectivo mal estado 22%
                                    #region
                                    if (cliente.tipoOperacion == "efectivo" && cliente.abandono==false)
                                    {
                                        #region
                                        randomEntero = getNumeroRandom(1, 100);
                                        if (randomEntero <= 22 && cliente.abandono == false)
                                        {

                                            problema = new problema();
                                            problema.id = cliente.listaProblema.Count + 1;
                                            problema.nombre = "falta dinero cliente";
                                            cliente.listaProblema.Add(problema);

                                            problemasLogs = new problemasLogs();
                                            problemasLogs.problema_encontrado = true;
                                            problemasLogs.idcliente = cliente.id;
                                            problemasLogs.fase = faceActual.nombre;
                                            //tiempo que tenia el cliente cuando se presento el problema
                                            problemasLogs.tiempo_antes = cliente.tiempoEntregaDatos;

                                            //saber si el cliente espera o se va
                                            if (getNumeroRandom(1, 2) == 1)
                                            {
                                                //cliente lo intenta y revisa de nuevo
                                                problemasLogs.cantidad_intentos += 1;
                                                problemasLogs.desicion = "el cliente revisa de nuevo";
                                                cliente.tiempoEsperadoEntregaDatos += ((getNumeroRandom(0, 300))/100);
                                            }
                                            else
                                            {
                                                //cliente se va
                                                problemasLogs.desicion = "cliente abandona";
                                                cliente.abandono = true;
                                                problemasLogs.tiempo_despues = cliente.tiempoEntregaDatos;
                                            }

                                            //tiempo que tiene el cliente ahora despues de que el cliente espera o se va
                                            problemasLogs.tiempo_despues = 0;

                                            listaProblemaLogs.Add(problemasLogs);
                                        }

                                        #endregion
                                    }
                                    #endregion

                                    //cheque mal endosado 17%
                                    #region
                                    if (cliente.tipoOperacion == "cheque" && cliente.abandono==false)
                                    {
                                        #region
                                        randomEntero = getNumeroRandom(1, 100);
                                        if (randomEntero <= 17 && cliente.abandono == false)
                                        {
                                            problema = new problema();
                                            problema.id = cliente.listaProblema.Count + 1;
                                            problema.nombre = "cheque mal endosado";
                                            cliente.listaProblema.Add(problema);

                                            problemasLogs = new problemasLogs();
                                            problemasLogs.problema_encontrado = true;
                                            problemasLogs.idcliente = cliente.id;
                                            problemasLogs.fase = faceActual.nombre;
                                            //tiempo que tenia el cliente cuando se presento el problema
                                            problemasLogs.tiempo_antes = cliente.tiempoEntregaDatos;

                                            //saber si el cliente espera o se va
                                            if (getNumeroRandom(1, 2) == 1)
                                            {
                                                //cliente lo intenta y revisa de nuevo
                                                problemasLogs.cantidad_intentos += 1;
                                                problemasLogs.desicion = "el cliente endosa el cheque";
                                                cliente.tiempoEsperadoEntregaDatos += ((getNumeroRandom(0, 300)) / 100);
                                            }
                                            else
                                            {
                                                //cliente se va
                                                problemasLogs.desicion = "cliente abandona";
                                                cliente.abandono = true;
                                                problemasLogs.tiempo_despues = cliente.tiempoEntregaDatos;
                                            }

                                            //tiempo que tiene el cliente ahora despues de que el cliente espera o se va
                                            problemasLogs.tiempo_despues = 0;

                                            listaProblemaLogs.Add(problemasLogs);
                                        }

                                        #endregion
                                    }
                                    #endregion

                                    //saldo cuenta cliente es insuficiente para transferencia 13%
                                    #region
                                    if (cliente.tipoOperacion == "transferencia" && cliente.abandono == false)
                                    {
                                        #region
                                        randomEntero = getNumeroRandom(1, 100);
                                        if (randomEntero <= 13 && cliente.abandono == false)
                                        {
                                            problema = new problema();
                                            problema.id = cliente.listaProblema.Count + 1;
                                            problema.nombre = "saldo cuenta insuficiente";
                                            cliente.listaProblema.Add(problema);

                                            problemasLogs = new problemasLogs();
                                            problemasLogs.problema_encontrado = true;
                                            problemasLogs.idcliente = cliente.id;
                                            problemasLogs.fase = faceActual.nombre;
                                            //tiempo que tenia el cliente cuando se presento el problema
                                            problemasLogs.tiempo_antes = cliente.tiempoEntregaDatos;

                                            //saber si el cliente espera o se va
                                            if (getNumeroRandom(1, 2) == 1)
                                            {
                                                //cliente lo intenta y revisa de nuevo
                                                problemasLogs.cantidad_intentos += 1;
                                                problemasLogs.desicion = "el cliente endosa el cheque";
                                                cliente.tiempoEsperadoEntregaDatos += ((getNumeroRandom(0, 300)) / 100);
                                            }
                                            else
                                            {
                                                //cliente se va
                                                problemasLogs.desicion = "cliente abandona";
                                                cliente.abandono = true;
                                                problemasLogs.tiempo_despues = cliente.tiempoEntregaDatos;
                                            }

                                            //tiempo que tiene el cliente ahora despues de que el cliente espera o se va
                                            problemasLogs.tiempo_despues = 0;

                                            listaProblemaLogs.Add(problemasLogs);
                                        }

                                        #endregion
                                    }
                                    #endregion



                                    #endregion
                                }
                                else if (faceActual.id == 3)
                                {
                                    //proceso de solicitud
                                    #region
                                    #endregion
                                }

                                
                                #endregion
                            }
                            
                            #endregion

                        }
                        else if (cliente.idOperacion == 2)
                        {
                            //retiro
                          

                        }
                        else if (cliente.idOperacion == 3)
                        {
                            //cambio moneda
                           

                        }
                        #endregion

                    }else if (cliente.idTemporada == 2)
                    {
                        //temporada 2
                        #region
                        #endregion

                    }
                    else if (cliente.idTemporada == 3)
                    {
                        //temporada 3
                        #region
                        #endregion

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
                    dataGridView1.Rows.Add(x.id,x.operacion+"-"+x.tipoOperacion,x.tanda,x.tiempoEsperadoServicio,x.montoTransaccion.ToString("N"),x.listaProblema.Count.ToString("N"),x.tiempoTotalServicio,x.abandono,x.idCajero);
                }


                //llenando los problemas log de todos los clientes
                //listaProblemaLogs = listaProblemaLogs.FindAll(x => x.problema_encontrado == true);
                foreach (var x in listaProblemaLogs)
                {
                    dataGridView2.Rows.Add(x.idcliente, x.operacion,x.fase,x.tiempo_antes,x.tiempo_despues,x.nombreProblema,x.desicion);
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

        public List<problema> getListasProblema(cliente cliente)
        {
            //para asignar los intervalos de los problemas dependiendo de la temporada del cliente

            listaProblemaDeposito = new List<problema>();
            listaProblemaRetiro = new List<problema>();
            listaProblemaCambio = new List<problema>();

            if (cliente.idTemporada == 1)
            {
                //problemas deposito
                #region
                //numero cuenta
                problema = new problema();
                problema.id = 1;
                problema.nombre = "falta numero cuenta";
                problema.intervalo_inicial = 0;
                problema.intervalo_final = 13;
                listaProblemaDeposito.Add(problema);
                //numero cuenta incorrecto
                problema = new problema();
                problema.id = 2;
                problema.nombre = "numero cuenta incorrecto";
                problema.intervalo_inicial = 0;
                problema.intervalo_final = 25;
                listaProblemaDeposito.Add(problema);
                //monto incompleto
                problema = new problema();
                problema.id = 3;
                problema.nombre = "monto incompleto";
                problema.intervalo_inicial = 0;
                problema.intervalo_final = 27;
                listaProblemaDeposito.Add(problema);
                //dinero en mal estado
                problema = new problema();
                problema.id = 4;
                problema.nombre = "dinero mal estado";
                problema.intervalo_inicial = 0;
                problema.intervalo_final = 22;
                listaProblemaDeposito.Add(problema);
                //cheque mal ensosado
                problema = new problema();
                problema.id = 5;
                problema.nombre = "cheque mal endosado";
                problema.intervalo_inicial = 0;
                problema.intervalo_final = 17;
                listaProblemaDeposito.Add(problema);
                //saldo cuenta cliente es isuficiente
                problema = new problema();
                problema.id = 6;
                problema.nombre = "saldo cuenta cliente insuficiente";
                problema.intervalo_inicial = 0;
                problema.intervalo_final = 13;
                listaProblemaDeposito.Add(problema);
                #endregion

                //problemas retiro
                #region
                //numero cuenta
                problema = new problema();
                problema.id = 1;
                problema.nombre = "falta numero cuenta";
                problema.intervalo_inicial = 0;
                problema.intervalo_final = 13;
                listaProblemaRetiro.Add(problema);
                //numero cuenta
                problema = new problema();
                problema.id = 1;
                problema.nombre = "falta numero cuenta";
                problema.intervalo_inicial = 0;
                problema.intervalo_final = 13;
                listaProblemaRetiro.Add(problema);
                //numero cuenta
                problema = new problema();
                problema.id = 1;
                problema.nombre = "falta numero cuenta";
                problema.intervalo_inicial = 0;
                problema.intervalo_final = 13;
                listaProblemaRetiro.Add(problema);
                //numero cuenta
                problema = new problema();
                problema.id = 1;
                problema.nombre = "falta numero cuenta";
                problema.intervalo_inicial = 0;
                problema.intervalo_final = 13;
                listaProblemaRetiro.Add(problema);
                #endregion

                //problemas cambio moneda
                #region
                //numero cuenta
                problema = new problema();
                problema.id = 1;
                problema.nombre = "falta numero cuenta";
                problema.intervalo_inicial = 0;
                problema.intervalo_final = 13;
                listaProblemaCambio.Add(problema);
                //numero cuenta
                problema = new problema();
                problema.id = 1;
                problema.nombre = "falta numero cuenta";
                problema.intervalo_inicial = 0;
                problema.intervalo_final = 13;
                listaProblemaCambio.Add(problema);
                //numero cuenta
                problema = new problema();
                problema.id = 1;
                problema.nombre = "falta numero cuenta";
                problema.intervalo_inicial = 0;
                problema.intervalo_final = 13;
                listaProblemaCambio.Add(problema);
                //numero cuenta
                problema = new problema();
                problema.id = 1;
                problema.nombre = "falta numero cuenta";
                problema.intervalo_inicial = 0;
                problema.intervalo_final = 13;
                listaProblemaCambio.Add(problema);
                #endregion

            }else if (cliente.idTemporada == 2)
            {
                //problemas deposito
                #region
                //numero cuenta
                problema = new problema();
                problema.id = 1;
                problema.nombre = "falta numero cuenta";
                problema.intervalo_inicial = 0;
                problema.intervalo_final = 13;
                listaProblemaDeposito.Add(problema);
                //numero cuenta incorrecto
                problema = new problema();
                problema.id = 2;
                problema.nombre = "numero cuenta incorrecto";
                problema.intervalo_inicial = 0;
                problema.intervalo_final = 25;
                listaProblemaDeposito.Add(problema);
                //monto incompleto
                problema = new problema();
                problema.id = 3;
                problema.nombre = "monto incompleto";
                problema.intervalo_inicial = 0;
                problema.intervalo_final = 27;
                listaProblemaDeposito.Add(problema);
                //dinero en mal estado
                problema = new problema();
                problema.id = 4;
                problema.nombre = "dinero mal estado";
                problema.intervalo_inicial = 0;
                problema.intervalo_final = 22;
                listaProblemaDeposito.Add(problema);
                //cheque mal ensosado
                problema = new problema();
                problema.id = 5;
                problema.nombre = "cheque mal endosado";
                problema.intervalo_inicial = 0;
                problema.intervalo_final = 17;
                listaProblemaDeposito.Add(problema);
                //saldo cuenta cliente es isuficiente
                problema = new problema();
                problema.id = 6;
                problema.nombre = "saldo cuenta cliente insuficiente";
                problema.intervalo_inicial = 0;
                problema.intervalo_final = 13;
                listaProblemaDeposito.Add(problema);
                #endregion

                //problemas retiro
                #region
                //numero cuenta
                problema = new problema();
                problema.id = 1;
                problema.nombre = "falta numero cuenta";
                problema.intervalo_inicial = 0;
                problema.intervalo_final = 13;
                listaProblemaRetiro.Add(problema);
                //numero cuenta
                problema = new problema();
                problema.id = 1;
                problema.nombre = "falta numero cuenta";
                problema.intervalo_inicial = 0;
                problema.intervalo_final = 13;
                listaProblemaRetiro.Add(problema);
                //numero cuenta
                problema = new problema();
                problema.id = 1;
                problema.nombre = "falta numero cuenta";
                problema.intervalo_inicial = 0;
                problema.intervalo_final = 13;
                listaProblemaRetiro.Add(problema);
                //numero cuenta
                problema = new problema();
                problema.id = 1;
                problema.nombre = "falta numero cuenta";
                problema.intervalo_inicial = 0;
                problema.intervalo_final = 13;
                listaProblemaRetiro.Add(problema);
                #endregion

                //problemas cambio moneda
                #region
                //numero cuenta
                problema = new problema();
                problema.id = 1;
                problema.nombre = "falta numero cuenta";
                problema.intervalo_inicial = 0;
                problema.intervalo_final = 13;
                listaProblemaCambio.Add(problema);
                //numero cuenta
                problema = new problema();
                problema.id = 1;
                problema.nombre = "falta numero cuenta";
                problema.intervalo_inicial = 0;
                problema.intervalo_final = 13;
                listaProblemaCambio.Add(problema);
                //numero cuenta
                problema = new problema();
                problema.id = 1;
                problema.nombre = "falta numero cuenta";
                problema.intervalo_inicial = 0;
                problema.intervalo_final = 13;
                listaProblemaCambio.Add(problema);
                //numero cuenta
                problema = new problema();
                problema.id = 1;
                problema.nombre = "falta numero cuenta";
                problema.intervalo_inicial = 0;
                problema.intervalo_final = 13;
                listaProblemaCambio.Add(problema);
                #endregion


            }else if (cliente.idTemporada == 3)
            {
                //problemas deposito
                #region
                //numero cuenta
                problema = new problema();
                problema.id = 1;
                problema.nombre = "falta numero cuenta";
                problema.intervalo_inicial = 0;
                problema.intervalo_final = 13;
                listaProblemaDeposito.Add(problema);
                //numero cuenta incorrecto
                problema = new problema();
                problema.id = 2;
                problema.nombre = "numero cuenta incorrecto";
                problema.intervalo_inicial = 0;
                problema.intervalo_final = 25;
                listaProblemaDeposito.Add(problema);
                //monto incompleto
                problema = new problema();
                problema.id = 3;
                problema.nombre = "monto incompleto";
                problema.intervalo_inicial = 0;
                problema.intervalo_final = 27;
                listaProblemaDeposito.Add(problema);
                //dinero en mal estado
                problema = new problema();
                problema.id = 4;
                problema.nombre = "dinero mal estado";
                problema.intervalo_inicial = 0;
                problema.intervalo_final = 22;
                listaProblemaDeposito.Add(problema);
                //cheque mal ensosado
                problema = new problema();
                problema.id = 5;
                problema.nombre = "cheque mal endosado";
                problema.intervalo_inicial = 0;
                problema.intervalo_final = 17;
                listaProblemaDeposito.Add(problema);
                //saldo cuenta cliente es isuficiente
                problema = new problema();
                problema.id = 6;
                problema.nombre = "saldo cuenta cliente insuficiente";
                problema.intervalo_inicial = 0;
                problema.intervalo_final = 13;
                listaProblemaDeposito.Add(problema);
                #endregion

                //problemas retiro
                #region
                //numero cuenta
                problema = new problema();
                problema.id = 1;
                problema.nombre = "falta numero cuenta";
                problema.intervalo_inicial = 0;
                problema.intervalo_final = 13;
                listaProblemaRetiro.Add(problema);
                //numero cuenta
                problema = new problema();
                problema.id = 1;
                problema.nombre = "falta numero cuenta";
                problema.intervalo_inicial = 0;
                problema.intervalo_final = 13;
                listaProblemaRetiro.Add(problema);
                //numero cuenta
                problema = new problema();
                problema.id = 1;
                problema.nombre = "falta numero cuenta";
                problema.intervalo_inicial = 0;
                problema.intervalo_final = 13;
                listaProblemaRetiro.Add(problema);
                //numero cuenta
                problema = new problema();
                problema.id = 1;
                problema.nombre = "falta numero cuenta";
                problema.intervalo_inicial = 0;
                problema.intervalo_final = 13;
                listaProblemaRetiro.Add(problema);
                #endregion

                //problemas cambio moneda
                #region
                //numero cuenta
                problema = new problema();
                problema.id = 1;
                problema.nombre = "falta numero cuenta";
                problema.intervalo_inicial = 0;
                problema.intervalo_final = 13;
                listaProblemaCambio.Add(problema);
                //numero cuenta
                problema = new problema();
                problema.id = 1;
                problema.nombre = "falta numero cuenta";
                problema.intervalo_inicial = 0;
                problema.intervalo_final = 13;
                listaProblemaCambio.Add(problema);
                //numero cuenta
                problema = new problema();
                problema.id = 1;
                problema.nombre = "falta numero cuenta";
                problema.intervalo_inicial = 0;
                problema.intervalo_final = 13;
                listaProblemaCambio.Add(problema);
                //numero cuenta
                problema = new problema();
                problema.id = 1;
                problema.nombre = "falta numero cuenta";
                problema.intervalo_inicial = 0;
                problema.intervalo_final = 13;
                listaProblemaCambio.Add(problema);
                #endregion

            }
           


            return listaProblemaRetiro;
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

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

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

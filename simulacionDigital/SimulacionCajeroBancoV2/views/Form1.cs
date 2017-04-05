using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SimulacionCajeroBancoV2.modelos;
using SimulacionCajeroBancoV2.objetos;

namespace SimulacionCajeroBancoV2
{
    public partial class Form1 : Form
    {


        //modelos
        modeloTemporada modeloTemporada=new modeloTemporada();
        modeloTanda modeloTanda=new modeloTanda();
        modeloCajero modeloCajero=new modeloCajero();

        //objetos
        private temporada temporada;
        private cliente cliente;
        private tanda tanda;
        private problema problema;
        private cajero cajero;

        //listas
        private List<tanda> listaTanda; 
        private List<cliente> listaCliente;
        private List<temporada> listaTemporada;
        private List<cajero> listaCajero; 

        //lista de problema
        private List<problema> listaProblemaDeposito;
        private List<problema> listaProblemaRetiro;
        private List<problema> listaProblemaCambio;


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

        public void getCajeros()
        {
            try
            {
                listaCajero = new List<cajero>();
                listaCajero = modeloCajero.getListaCajero();

                comboBoxTipoCaja.DataSource = listaCajero;
                comboBoxTipoCaja.DisplayMember = "nombre";
                comboBoxTipoCaja.ValueMember = "id";

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
                    cliente.idTemporada = temporadaSeleccionada.id;
                    
                    //obteniendo la tanda del cliente
                    //temporada matutina con probabilidad de 41% y 59% tanda vespertina
                    Thread.Sleep(30);
                    randomEntero = getNumeroRandom(0, 100);
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

                    //obteniendo la operacion
                    //deposito 43, retiro 37, cambio moneda 20
                    //random=new Random();
                    //randomEntero = 0;
                    //randomEntero = random.Next(0, 100) + 1;
                    randomEntero = getNumeroRandom(0, 100);
                    if (randomEntero >= 1 && randomEntero<=43)
                    {
                        //deposito
                        cliente.idOperacion = 1;
                        cliente.operacion = "deposito";
                        #region
                        //el deposito puede ser efectivo 60,cheque 30, transferencia 10
                        //random=new Random();
                        //randomEntero = 0;
                        //randomEntero = random.Next(0,100)+1;
                        randomEntero = getNumeroRandom(0, 100);
                        if (randomEntero >= 1 && randomEntero <= 60)
                        {
                            //deposito en efectivo
                            cliente.tipoDespotiso = "efectivo";
                            //como el deposito es en efectivo si es monto > 45,000 se ofrece una cuenta de ahorro que el 38% acepta
                            cliente.montoTransaccion = getNumeroRandom(1, 100000);
                            if (cliente.montoTransaccion > 45000)
                            {
                                //monto de transaccion > 45,000 se oferta cuenta de ahorro. 
                                randomEntero = getNumeroRandom(1, 100);
                                if (randomEntero >= 1 && randomEntero <= 38)
                                {
                                    //acepta cuenta de ahorro aumenta tiempo formula (-1/landa * LN(random()))
                                    cliente.aceptaCuentaAhorro = true;
                                    random=new Random();
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
                            cliente.tipoDespotiso = "cheque";
                        }else if (randomEntero >= 90 && randomEntero <= 100)
                        {
                            //deposito en transferencia
                            cliente.tipoDespotiso = "transferencia";
                        }

                        #endregion
                    }
                    else if (randomEntero >= 43 && randomEntero <= 80)
                    {
                        //retiro
                        cliente.idOperacion = 2;
                        cliente.operacion = "retiro";
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
                        if (randomEntero >= 1 && randomEntero <= 63)
                        {
                            //dolares a pesos
                            cliente.cambioDolaresApesos = true;
                            cliente.cambioPesosADolares = false;
                        }
                        else
                        {
                            //pesos a dolares
                            cliente.cambioPesosADolares = true;
                            cliente.cambioDolaresApesos = false;
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

        private void button1_Click(object sender, EventArgs e)
        {
            getAction();
        }

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

                foreach (var x in  listaCliente)
                {
                    dataGridView1.Rows.Add(x.id,x.operacion,x.tanda);
                }

                MessageBox.Show("Finalizó", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loadListaCliente.: " + ex.ToString(), "", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        public int getNumeroRandom(int inicio, int final)
        {
            //Thread.Sleep(1);
            random=new Random();
            randomEntero = 0;
            randomEntero = random.Next(inicio, final) + 1;
            return randomEntero;
        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using SimulacionCajeroBanco.clases;
using _7ADMFIC_1._0.VentanasComunes;

namespace SimulacionCajeroBanco
{
    public partial class ColaBanco : Form
    {


        //objetos
        private cajero cajero;
        private cliente cliente;
        private operaciones operacion;
        private problema problema;
        private tanda tanda;
        private temporada temporada;

        //variables
        Random random;//para randoms
        private int cantidadClientes = 0;
        private int cantidadCajeros = 0;
        private double numero = 0;


        //listas
        private List<cajero> listaCajero;
        private List<cliente> listaCliente;
        private List<temporada> listaTemporada;
        private List<tanda> listaTanda; 


        //listas problemas
        private List<problema> listaProblemaDeposito;
        private List<problema> listaProblemaRetiro;
        private List<problema> listaProblemaCambio;
        private List<problema> lisaProblemaCheque; 

        //variables para datos
        private double tiempoLLegadaAcumulativo = 0;
        private double tiempoServicioAcumulativo = 0;
        private double clienteDeposito = 0;
        private double clienteRetiro = 0;
        private double clienteCambioMoneda = 0;
        


        public ColaBanco()
        {
            InitializeComponent();
            loadVentana();
        }

        public void loadVentana()
        {
            try
            {
                this.dataGridView1.RowsDefaultCellStyle.BackColor = Color.Blue;
                this.dataGridView1.AlternatingRowsDefaultCellStyle.BackColor =Color.DarkBlue;

                loadTemporada();
                loadTanda();
                getClientesByTemporada();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loadVentana.:" + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            loadTemporada();
            loadTanda();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }



        //get problemas
        #region
        public void getProblemas()
        {
            try
            {

                //instancia listas problemas
                listaProblemaDeposito=new List<problema>();
                listaProblemaRetiro=new List<problema>();
                listaProblemaCambio=new List<problema>();
                lisaProblemaCheque=new List<problema>();
                //instancia problemas
               
                
                //cuando la temporada sea primavera
                if (temporada.nombre == "primavera")
                {
                    #region
                    //problemas deposito
                   
                    problema = new problema(); 
                    problema.nombre = "fallo sistema";
                    problema.probabilidad_ocurrencia_inicial = 0;
                    problema.probabilidad_ocurrencia_final = 30.40;
                    random = new Random();
                    numero = random.Next(1,15);
                    problema.tiempo_aumenta = numero;
                    listaProblemaDeposito.Add(problema);

                    problema = new problema();
                    problema.nombre = "fallo electricidad";
                    problema.probabilidad_ocurrencia_inicial = 0;
                    problema.probabilidad_ocurrencia_final = 25.30;
                    random = new Random();
                    numero = random.Next(1, 20);
                    problema.tiempo_aumenta = numero;
                    listaProblemaDeposito.Add(problema);

                    problema = new problema();
                    problema.nombre = "numero cuenta incorrecto";
                    problema.probabilidad_ocurrencia_inicial = 0;
                    problema.probabilidad_ocurrencia_final = 23.60;
                    random = new Random();
                    numero = random.Next(1, 3);
                    problema.tiempo_aumenta = numero;
                    listaProblemaDeposito.Add(problema);

                    problema = new problema();
                    problema.nombre = "dinero insuficiente";
                    problema.probabilidad_ocurrencia_inicial = 0;
                    problema.probabilidad_ocurrencia_final = 17.85;
                    random = new Random();
                    numero = random.Next(1, 15);
                    problema.tiempo_aumenta = numero;
                    listaProblemaDeposito.Add(problema);

                    problema = new problema();
                    problema.nombre = "falta cedula";
                    problema.probabilidad_ocurrencia_inicial = 0;
                    problema.probabilidad_ocurrencia_final = 35.90;
                    random = new Random();
                    numero = random.Next(1, 2);
                    problema.tiempo_aumenta = numero;
                    listaProblemaDeposito.Add(problema);


                    //problemas retiro
                    problema = new problema();
                    problema.nombre = "fallo sistema";
                    problema.probabilidad_ocurrencia_inicial = 0;
                    problema.probabilidad_ocurrencia_final = 5.12;
                    random = new Random();
                    numero = random.Next(1, 15);
                    problema.tiempo_aumenta = numero;
                    listaProblemaRetiro.Add(problema);

                    problema = new problema(); 
                    problema.nombre = "fallo electricidad";
                    problema.probabilidad_ocurrencia_inicial = 0;
                    problema.probabilidad_ocurrencia_final = 9.85;
                    random = new Random();
                    numero = random.Next(1, 20);
                    problema.tiempo_aumenta = numero;
                    listaProblemaRetiro.Add(problema);

                    problema = new problema(); 
                    problema.nombre = "numero cuenta incorrecto";
                    problema.probabilidad_ocurrencia_inicial = 0;
                    problema.probabilidad_ocurrencia_final = 14.69;
                    random = new Random();
                    numero = random.Next(1, 2);
                    problema.tiempo_aumenta = numero;
                    listaProblemaRetiro.Add(problema);

                    problema=new problema();
                    problema.nombre = "dinero insuficiente";
                    problema.probabilidad_ocurrencia_inicial = 0;
                    problema.probabilidad_ocurrencia_final = 11.80;
                    random = new Random();
                    numero = random.Next(1,3);
                    problema.tiempo_aumenta = numero;
                    listaProblemaRetiro.Add(problema);

                    problema = new problema();
                    problema.nombre = "falta cedula";
                    problema.probabilidad_ocurrencia_inicial = 0;
                    problema.probabilidad_ocurrencia_final = 17.34;
                    random = new Random();
                    numero = random.Next(1, 2);
                    problema.tiempo_aumenta = numero;
                    listaProblemaRetiro.Add(problema);


                    //problemas cambio moneda
                    problema = new problema();
                    problema.nombre = "fallo sistema";
                    problema.probabilidad_ocurrencia_inicial = 0;
                    problema.probabilidad_ocurrencia_final = 5.12;
                    random = new Random();
                    numero = random.Next(1, 15);
                    problema.tiempo_aumenta = numero;
                    listaProblemaCambio.Add(problema);


                    problema = new problema();
                    problema.nombre = "fallo electricidad";
                    problema.probabilidad_ocurrencia_inicial = 0;
                    problema.probabilidad_ocurrencia_final = 9.85;
                    random = new Random();
                    numero = random.Next(1, 20);
                    problema.tiempo_aumenta = numero;
                    listaProblemaCambio.Add(problema);

                    
                    problema = new problema();
                    problema.nombre = "dinero insuficiente";
                    problema.probabilidad_ocurrencia_inicial = 0;
                    problema.probabilidad_ocurrencia_final = 5.45;
                    random = new Random();
                    numero = random.Next(1, 5);
                    problema.tiempo_aumenta = numero;
                    listaProblemaCambio.Add(problema);

                    problema = new problema();
                    problema.nombre = "moneda no es aceptada";
                    problema.probabilidad_ocurrencia_inicial = 0;
                    problema.probabilidad_ocurrencia_final = 9.85;
                    random = new Random();
                    numero = random.Next(1, 2);
                    problema.tiempo_aumenta = numero;
                    listaProblemaCambio.Add(problema);


                    ////problemas cheque
                    //random = new Random();
                    //problema = new problema();
                    //problema.nombre = "fallo sistema";
                    //problema.probabilidad_ocurrencia_inicial = 0;
                    //problema.probabilidad_ocurrencia_final = 5.12;
                    //numero = random.Next(1, 15);
                    //lisaProblemaCheque.Add(problema);

                    //random = new Random();
                    //problema = new problema();
                    //problema.nombre = "fallo electricidad";
                    //problema.probabilidad_ocurrencia_inicial = 0;
                    //problema.probabilidad_ocurrencia_final = 9.85;
                    //numero = random.Next(1, 5);
                    //lisaProblemaCheque.Add(problema);

                    //random = new Random();
                    //problema = new problema();
                    //problema.nombre = "falta cedula";
                    //problema.probabilidad_ocurrencia_inicial = 0;
                    //problema.probabilidad_ocurrencia_final = 14.42;
                    //lisaProblemaCheque.Add(problema);

                    //random = new Random();
                    //problema = new problema();
                    //problema.nombre = "cheque mal endosado";
                    //problema.probabilidad_ocurrencia_inicial = 0;
                    //problema.probabilidad_ocurrencia_final = 22.12;
                    //lisaProblemaCheque.Add(problema);

                    //random = new Random();
                    //problema = new problema();
                    //problema.nombre = "cheque sin fondos";
                    //problema.probabilidad_ocurrencia_inicial = 0;
                    //problema.probabilidad_ocurrencia_final = 35.20;
                    //lisaProblemaCheque.Add(problema);
                    #endregion
                }
                //cuando la temporada sea invierno
                if (temporada.nombre == "invierno")
                {
                    #region
                    //problemas deposito
                    problema = new problema();
                    problema.nombre = "fallo sistema";
                    problema.probabilidad_ocurrencia_inicial = 0;
                    problema.probabilidad_ocurrencia_final = 12.60;
                    random = new Random();
                    numero = random.Next(1, 20);
                    problema.tiempo_aumenta = numero;
                    listaProblemaDeposito.Add(problema);


                    problema = new problema();
                    problema.nombre = "fallo electricidad";
                    problema.probabilidad_ocurrencia_inicial = 0;
                    problema.probabilidad_ocurrencia_final = 20.30;
                    random = new Random();
                    numero = random.Next(1, 30);
                    problema.tiempo_aumenta = numero;
                    listaProblemaDeposito.Add(problema);


                    problema = new problema();
                    problema.nombre = "numero cuenta incorrecto";
                    problema.probabilidad_ocurrencia_inicial = 0;
                    problema.probabilidad_ocurrencia_final = 17.43;
                    random = new Random();
                    numero = random.Next(1, 2);
                    problema.tiempo_aumenta = numero;
                    listaProblemaDeposito.Add(problema);


                    problema = new problema();
                    problema.nombre = "dinero insuficiente";
                    problema.probabilidad_ocurrencia_inicial = 0;
                    problema.probabilidad_ocurrencia_final = 15.30;
                    random = new Random();
                    numero = random.Next(1, 3);
                    problema.tiempo_aumenta = numero;
                    listaProblemaDeposito.Add(problema);


                    problema = new problema();
                    problema.nombre = "falta cedula";
                    problema.probabilidad_ocurrencia_inicial = 0;
                    problema.probabilidad_ocurrencia_final = 7.50;
                    random = new Random();
                    numero = random.Next(1, 2);
                    problema.tiempo_aumenta = numero;
                    listaProblemaDeposito.Add(problema);


                    //problemas retiro
                    problema = new problema();
                    problema.nombre = "fallo sistema";
                    problema.probabilidad_ocurrencia_inicial = 0;
                    problema.probabilidad_ocurrencia_final = 12.60;
                    random = new Random();
                    numero = random.Next(1, 20);
                    problema.tiempo_aumenta = numero;
                    listaProblemaRetiro.Add(problema);


                    problema = new problema();
                    problema.nombre = "fallo electricidad";
                    problema.probabilidad_ocurrencia_inicial = 0;
                    problema.probabilidad_ocurrencia_final = 20.30;
                    random = new Random();
                    numero = random.Next(1, 30);
                    problema.tiempo_aumenta = numero;
                    listaProblemaRetiro.Add(problema);


                    problema = new problema();
                    problema.nombre = "numero cuenta incorrecto";
                    problema.probabilidad_ocurrencia_inicial = 0;
                    problema.probabilidad_ocurrencia_final = 9.60;
                    random = new Random();
                    numero = random.Next(1, 3);
                    problema.tiempo_aumenta = numero;
                    listaProblemaRetiro.Add(problema);


                    problema = new problema();
                    problema.nombre = "dinero insuficiente";
                    problema.probabilidad_ocurrencia_inicial = 0;
                    problema.probabilidad_ocurrencia_final = 14.39;
                    random = new Random();
                    numero = random.Next(1, 2);
                    problema.tiempo_aumenta = numero;
                    listaProblemaRetiro.Add(problema);

                    problema = new problema();
                    problema.nombre = "falta cedula";
                    problema.probabilidad_ocurrencia_inicial = 0;
                    problema.probabilidad_ocurrencia_final = 22.43;
                    random = new Random();
                    numero = random.Next(1, 2);
                    problema.tiempo_aumenta = numero;
                    listaProblemaRetiro.Add(problema);


                    //problemas cambio moneda
                    problema = new problema();
                    problema.nombre = "fallo sistema";
                    problema.probabilidad_ocurrencia_inicial = 0;
                    problema.probabilidad_ocurrencia_final = 12.60;
                    random = new Random();
                    numero = random.Next(1, 20);
                    problema.tiempo_aumenta = numero;
                    listaProblemaCambio.Add(problema);


                    problema = new problema();
                    problema.nombre = "fallo electricidad";
                    problema.probabilidad_ocurrencia_inicial = 0;
                    problema.probabilidad_ocurrencia_final = 20.30;
                    random = new Random();
                    numero = random.Next(1, 30);
                    problema.tiempo_aumenta = numero; 
                    listaProblemaCambio.Add(problema);


                    problema = new problema();
                    problema.nombre = "dinero insuficiente";
                    problema.probabilidad_ocurrencia_inicial = 0;
                    random = new Random();
                    numero = random.Next(1, 6);
                    problema.tiempo_aumenta = numero; 
                    listaProblemaCambio.Add(problema);


                    problema = new problema();
                    problema.nombre = "moneda no es aceptada";
                    problema.probabilidad_ocurrencia_inicial = 0;
                    problema.probabilidad_ocurrencia_final = 34.20;
                    random = new Random();
                    numero = random.Next(1, 3);
                    problema.tiempo_aumenta = numero;
                    listaProblemaCambio.Add(problema);


                    //problemas cheque
                    //random = new Random();
                    //problema = new problema();
                    //problema.nombre = "fallo sistema";
                    //problema.probabilidad_ocurrencia_inicial = 0;
                    //problema.probabilidad_ocurrencia_final = 12.60;
                    //numero = random.Next(1, 15);
                    //lisaProblemaCheque.Add(problema);

                    //random = new Random();
                    //problema = new problema();
                    //problema.nombre = "fallo electricidad";
                    //problema.probabilidad_ocurrencia_inicial = 0;
                    //problema.probabilidad_ocurrencia_final = 20.30;
                    //numero = random.Next(1, 5);
                    //lisaProblemaCheque.Add(problema);

                    //random = new Random();
                    //problema = new problema();
                    //problema.nombre = "falta cedula";
                    //problema.probabilidad_ocurrencia_inicial = 0;
                    //problema.probabilidad_ocurrencia_final = 23.41;
                    //lisaProblemaCheque.Add(problema);

                    //random = new Random();
                    //problema = new problema();
                    //problema.nombre = "cheque mal endosado";
                    //problema.probabilidad_ocurrencia_inicial = 0;
                    //problema.probabilidad_ocurrencia_final = 19.42;
                    //lisaProblemaCheque.Add(problema);

                    //random = new Random();
                    //problema = new problema();
                    //problema.nombre = "cheque sin fondos";
                    //problema.probabilidad_ocurrencia_inicial = 0;
                    //problema.probabilidad_ocurrencia_final = 12.98;
                    //lisaProblemaCheque.Add(problema);
                    #endregion
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("Error getProblemas.:" + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion


        
        //validar getAction
        #region
        public void loadTemporada()
        {
            try
            {
                listaTemporada=new List<temporada>();
                
                temporada=new temporada();
                temporada.nombre = "primavera";
                listaTemporada.Add(temporada);


                temporada = new temporada();
                temporada.nombre = "invierno";
                listaTemporada.Add(temporada);

                temporadaAnoCombo.DataSource = listaTemporada;
                temporadaAnoCombo.DisplayMember = "nombre";
                temporadaAnoCombo.ValueMember = "nombre";

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loadTemporada.: " + ex.ToString(), "", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

            }
        }
        #endregion




        //validar getAction
        #region
        public void loadTanda()
        {
            try
            {

                listaTanda=new List<tanda>();

                tanda = new tanda();
                tanda.nombre = "matutina";
                listaTanda.Add(tanda);

                tanda = new tanda();
                tanda.nombre = "vespertina";
                listaTanda.Add(tanda);


                tandaCombo.DataSource = listaTanda;
                tandaCombo.DisplayMember = "nombre";
                tandaCombo.ValueMember = "nombre";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loadTanda.: " + ex.ToString(), "", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                
            }
        }
        #endregion



        //validar getAction
        #region
        public bool validarGetAction()
        {
            try
            {

                if (cantidadCajeroText.Text == "")
                {
                    MessageBox.Show("Falta la cantidad de cajeros", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cantidadCajeroText.Focus();
                    cantidadCajeroText.SelectAll();
                    return false;
                }
                if (cantidadClienteText.Text == "")
                {
                    MessageBox.Show("Falta la cantidad de clientes", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cantidadClienteText.Focus();
                    cantidadClienteText.SelectAll();
                    return false;
                }
                if (temporadaAnoCombo.Text == "")
                {
                    MessageBox.Show("Falta la temporada del anio", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    temporadaAnoCombo.Focus();
                    temporadaAnoCombo.SelectAll();
                    return false;
                }
                if (tandaCombo.Text == "")
                {
                    MessageBox.Show("Falta la tanda", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tandaCombo.Focus();
                    tandaCombo.SelectAll();
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error validarGetAction.: " + ex.ToString(), "", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }
        }
        #endregion




        //get action
        #region
        public void getAction()
        {
            try
            {
                if (!validarGetAction())
                {
                    return;
                }

                //procesar

                //cargar problemas
                getProblemas();
                
                //cantidad de cajeros
                cantidadCajeros = Convert.ToInt16(cantidadCajeroText.Text.Trim());
                
                
                //cantidad de clientes
                cantidadClientes = Convert.ToInt16(cantidadClienteText.Text.Trim());
                getClientes();

                //rrecorriendo los clientes
                listaCliente.ForEach(x =>
                {
                    random=new Random();
                    
                    
                    //verificando si ocurren problemas

                    //deposito
                    if (x.operacion_deseada == "deposito")
                    {
                        listaProblemaDeposito.ForEach(p =>
                        {
                            numero = random.NextDouble();
                            numero = Math.Round(numero,2);
                            //0.45*100=  numero=45
                            numero*=100;
                            if (numero >= p.probabilidad_ocurrencia_inicial && numero <= p.probabilidad_ocurrencia_final)
                            {
                                MessageBox.Show(numero.ToString() + "-" + p.probabilidad_ocurrencia_inicial + "-" + p.probabilidad_ocurrencia_final + "--");
                                MessageBox.Show("cliente-> "+x.codigo+"-"+cliente.operacion_deseada+"->presento problema: " + p.nombre);
                                MessageBox.Show("tiempo antes->" + cliente.tiempo_servicio_final + " tiempo ahora->" + ((cliente.tiempo_servicio_final + p.tiempo_aumenta)).ToString("N"));
                                //cliente se presento este problema
                                cliente.problemas.Add(p);
                                cliente.tiempo_servicio_final += p.tiempo_aumenta;
                            }
                        });
                    }

                    //retiro
                    if (x.operacion_deseada == "retiro")
                    {
                        listaProblemaRetiro.ForEach(p =>
                        {
                            numero = random.NextDouble();
                            numero = Math.Round(numero, 2);
                            numero *= 100;
                            if (numero >= p.probabilidad_ocurrencia_inicial && numero <= p.probabilidad_ocurrencia_final)
                            {
                                MessageBox.Show(numero.ToString() + "-" + p.probabilidad_ocurrencia_inicial + "-" + p.probabilidad_ocurrencia_final + "--");
                                MessageBox.Show("cliente-> " + x.codigo + "-" + cliente.operacion_deseada + "->presento problema: " + p.nombre);
                                MessageBox.Show("tiempo antes->" + cliente.tiempo_servicio_final + " tiempo ahora->" + ((cliente.tiempo_servicio_final + p.tiempo_aumenta)).ToString("N"));
                                //cliente se presento este problema
                                cliente.problemas.Add(p);
                                cliente.tiempo_servicio_final += p.tiempo_aumenta;
                            }
                        });
                    }

                    //cambio moneda
                    if (x.operacion_deseada == "cambio moneda")
                    {
                        listaProblemaCambio.ForEach(p =>
                        {
                            numero = random.NextDouble();
                            numero = Math.Round(numero, 2);
                            numero *= 100;
                            if (numero >= p.probabilidad_ocurrencia_inicial && numero <= p.probabilidad_ocurrencia_final)
                            {
                                MessageBox.Show(numero.ToString() + "-" + p.probabilidad_ocurrencia_inicial + "-" + p.probabilidad_ocurrencia_final + "--");
                                MessageBox.Show("cliente-> " + x.codigo + "-" + cliente.operacion_deseada + "->presento problema: " + p.nombre);
                                MessageBox.Show("tiempo antes->" + cliente.tiempo_servicio_final + " tiempo ahora->" + ((cliente.tiempo_servicio_final + p.tiempo_aumenta)).ToString("N"));
                                //cliente se presento este problema
                                cliente.problemas.Add(p);
                                cliente.tiempo_servicio_final += p.tiempo_aumenta;
                            }
                        });
                    }


                    
                    
                });






                loadClientes();


            }
            catch (Exception ex)
            {

                MessageBox.Show("Error getAction.:" + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion


        #region
        //get clientes
        public void getClientes()
        {
            try
            {
                random=new Random();
                listaCliente=new List<cliente>();
                //llenar clientes
                
                for (int f = 1; f <= cantidadClientes; f++)
                {
                    //llenando los datos de cada cliente
                    cliente=new cliente();
                    cliente.problemas=new List<problema>();




                    cliente.codigo = f;
                    cliente.temporada = temporada.nombre;
                    cliente.tanda = tanda.nombre;
                    //asignar operacion que realizara dependiendo de la temporada y la tanda
                    //(deposito,retiro,cambio moneda,cheque)
                    #region
                    if (temporada.nombre == "primavera")
                    {
                        //temporada primavera
                        if (tanda.nombre == "matutina")
                        {
                            //asigar operacion
                            double numero = random.NextDouble();
                            numero = Math.Round(numero, 2);
                            //MessageBox.Show(numero.ToString());
                            if (numero >= 0 && numero <= 0.44)
                            {
                                //deposito
                                operacion=new operaciones();
                                operacion.nombre = "deposito";
                                operacion.tiempo_promedio = 3.5;
                                cliente.operacion_deseada = operacion.nombre;
                                cliente.tiempo_servicio_esperado = operacion.tiempo_promedio;
                                cliente.tiempo_servicio_final = operacion.tiempo_promedio;
                            }
                            if (numero >= 0.45 && numero <= 0.79)
                            {
                                //retiro
                                operacion = new operaciones();
                                operacion.nombre = "retiro";
                                operacion.tiempo_promedio = 2.7;
                                cliente.operacion_deseada = operacion.nombre;
                                cliente.tiempo_servicio_esperado = operacion.tiempo_promedio;
                                cliente.tiempo_servicio_final = operacion.tiempo_promedio;
                            }
                            if (numero >= 0.79 && numero <= 1)
                            {
                                //cambio moneda
                                operacion = new operaciones();
                                operacion.nombre = "cambio moneda";
                                operacion.tiempo_promedio = 3.0;
                                cliente.operacion_deseada = operacion.nombre;
                                cliente.tiempo_servicio_esperado = operacion.tiempo_promedio;
                                cliente.tiempo_servicio_final = operacion.tiempo_promedio;
                            }
                        }
                        else if (tanda.nombre == "vespertina")
                        {
                            //asigar operacion
                            double numero = random.NextDouble();
                            numero = Math.Round(numero, 2);
                            //MessageBox.Show(numero.ToString());
                            if (numero >= 0 && numero <= 0.34)
                            {
                                //deposito
                                operacion = new operaciones();
                                operacion.nombre = "deposito";
                                operacion.tiempo_promedio = 3.2;
                                cliente.operacion_deseada = operacion.nombre;
                                cliente.tiempo_servicio_esperado = operacion.tiempo_promedio;
                                cliente.tiempo_servicio_final = operacion.tiempo_promedio;
                            }
                            if (numero >= 0.35 && numero <= 0.69)
                            {
                                //retiro
                                operacion = new operaciones();
                                operacion.nombre = "retiro";
                                operacion.tiempo_promedio = 2.9;
                                cliente.operacion_deseada = operacion.nombre;
                                cliente.tiempo_servicio_esperado = operacion.tiempo_promedio;
                                cliente.tiempo_servicio_final = operacion.tiempo_promedio;
                            }
                            if (numero >= 0.70 && numero <= 1)
                            {
                                //cambio moneda
                                operacion = new operaciones();
                                operacion.nombre = "cambio monesa";
                                operacion.tiempo_promedio = 3.1;
                                cliente.operacion_deseada = operacion.nombre;
                                cliente.tiempo_servicio_esperado = operacion.tiempo_promedio;
                                cliente.tiempo_servicio_final = operacion.tiempo_promedio;
                            }
                        }
                    }
                    if(temporada.nombre=="invierno")
                    {
                        //temporada invierno
                        if (tanda.nombre == "matutina")
                        {
                            //asigar operacion
                            double numero = random.NextDouble();
                            numero = Math.Round(numero, 2);
                            //MessageBox.Show(numero.ToString());
                            if (numero >= 0 && numero <= 0.26)
                            {
                                //deposito
                                operacion = new operaciones();
                                operacion.nombre = "deposito";
                                operacion.tiempo_promedio = 3.0;
                                cliente.operacion_deseada = operacion.nombre;
                                cliente.tiempo_servicio_esperado = operacion.tiempo_promedio;
                                cliente.tiempo_servicio_final = operacion.tiempo_promedio;
                            }
                            if (numero >= 0.25 && numero <= 0.63)
                            {
                                //retiro
                                operacion = new operaciones();
                                operacion.nombre = "retiro";
                                operacion.tiempo_promedio = 3.2;
                                cliente.operacion_deseada = operacion.nombre;
                                cliente.tiempo_servicio_esperado = operacion.tiempo_promedio;
                                cliente.tiempo_servicio_final = operacion.tiempo_promedio;
                            }
                            if (numero >= 0.64 && numero <= 1)
                            {
                                //cambio moneda
                                operacion = new operaciones();
                                operacion.nombre = "cambio moneda";
                                operacion.tiempo_promedio = 2.5;
                                cliente.operacion_deseada = operacion.nombre;
                                cliente.tiempo_servicio_esperado = operacion.tiempo_promedio;
                                cliente.tiempo_servicio_final = operacion.tiempo_promedio;
                            }
                        }
                        else if (tanda.nombre == "vespertina")
                        {
                            //asigar operacion
                            double numero = random.NextDouble();
                            numero = Math.Round(numero, 2);
                            //MessageBox.Show(numero.ToString());
                            if (numero >= 0 && numero <= 0.29)
                            {
                                //deposito
                                operacion = new operaciones();
                                operacion.nombre = "deposito";
                                operacion.tiempo_promedio = 2.9;
                                cliente.operacion_deseada = operacion.nombre;
                                cliente.tiempo_servicio_esperado = operacion.tiempo_promedio;
                                cliente.tiempo_servicio_final = operacion.tiempo_promedio;
                            }
                            if (numero >= 0.30 && numero <= 0.62)
                            {
                                //retiro
                                operacion = new operaciones();
                                operacion.nombre = "retiro";
                                operacion.tiempo_promedio = 3.5;
                                cliente.operacion_deseada = operacion.nombre;
                                cliente.tiempo_servicio_esperado = operacion.tiempo_promedio;
                                cliente.tiempo_servicio_final = operacion.tiempo_promedio;
                            }
                            if (numero >= 0.63 && numero <= 1)
                            {
                                //cambio moneda
                                operacion = new operaciones();
                                operacion.nombre = "cambio moneda";
                                operacion.tiempo_promedio = 2.7;
                                cliente.operacion_deseada = operacion.nombre;
                                cliente.tiempo_servicio_esperado = operacion.tiempo_promedio;
                                cliente.tiempo_servicio_final = operacion.tiempo_promedio;
                            }
                        }
                    }
                    #endregion

                    cliente.atendido = false;
                    cliente.atendiendo = false;
                    cliente.abandono = false;
                    cliente.tiempo_servicio_base = 0;

                    listaCliente.Add(cliente);
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("Error getClientes.:" + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion


        #region
        //get clientes by temporada
        public void getClientesByTemporada()
        {
            try
            {
                temporada=new temporada();
                random = new Random();
                //si fue primavera
                if (temporadaAnoCombo.Text == "primavera")
                {
                    //se establecen los rango para la primavera
                    temporada.nombre = "primavera";
                    temporada.cantidad_cliente_rango_inicial = 250;
                    temporada.cantidad_cliente_rango_final = 350;
                    cantidadClienteText.Text=(random.Next(temporada.cantidad_cliente_rango_inicial,temporada.cantidad_cliente_rango_final)).ToString();
                }
                else if (temporadaAnoCombo.Text == "invierno")
                {
                    //se establecen los rangos para el invierno
                    temporada.nombre = "invierno";
                    temporada.cantidad_cliente_rango_inicial = 500;
                    temporada.cantidad_cliente_rango_final = 950;
                    cantidadClienteText.Text = (random.Next(temporada.cantidad_cliente_rango_inicial, temporada.cantidad_cliente_rango_final)).ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getClientesByTemporada.:" + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        //load clientes para presentarlo en el grid
        #region
        public void loadClientes()
        {
            try
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    dataGridView1.Rows.Clear();
                }

               listaCliente.ForEach(x =>
               {
                   dataGridView1.Rows.Add(x.codigo, x.operacion_deseada,x.tanda,x.tiempo_servicio_esperado,x.problemas.Count.ToString(),x.tiempo_servicio_final);
               });
                  
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error loadClientes.:" + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion


        //imprimir datos especificos
        #region
        public void imprimir1()
        {
            try
            {
                if (listaCliente == null)
                {
                    MessageBox.Show("No se encontraron datos, primero debe simular", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                

                //datos generales
                String reporte = "SimulacionCajeroBanco.clases_reportes.reporte_cliente.rdlc";
                List<ReportDataSource> listaReportDataSource = new List<ReportDataSource>();



                //reporte estudiante
                //reporte_estudiante reporteEstudiante = new reporte_estudiante();
                //List<reporte_estudiante> ListaReporteEstudiante = new List<reporte_estudiante>();
                //int cont = 0;
                //listaEstudiante.ForEach(x =>
                //{
                //    reporteEstudiante = new reporte_estudiante();

                //    reporteEstudiante.estudiante = cont + 1;
                //    reporteEstudiante.indice = x.indice;
                //    reporteEstudiante.carrera = x.Nombrecarrera;
                //    reporteEstudiante.tipoCarrera = x.tipoCarrera;
                //    reporteEstudiante.beca = x.tieneBeca;
                //    reporteEstudiante.tiempoServicioBase = x.tiempoServicioBase;
                //    reporteEstudiante.tiempoServicioFinal = x.tiempoServicioFinal;
                //    reporteEstudiante.jugadas = x.jugadas;

                //    ListaReporteEstudiante.Add(reporteEstudiante);
                //    cont++;

                //});

                //ReportDataSource reporteF = new ReportDataSource("estudiante", ListaReporteEstudiante);
                //listaReportDataSource.Add(reporteF);

                List<ReportParameter> ListaReportParameter = new List<ReportParameter>();

                VisorReporteComun ventana = new VisorReporteComun(reporte, listaReportDataSource, ListaReportParameter, true, false, false);
                ventana.ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error imprimir1: " + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion


        //imprimir la corrida
        #region
        public void imprimir2()
        {
            try
            {
                //if (listaCliente == null)
                //{
                //    MessageBox.Show("No se encontraron datos, primero debe simular", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //}



                ////datos generales
                //String reporte = "SimulacionCajeroBanco.clases_reportes.reporte_cliente.rdlc";
                //List<ReportDataSource> listaReportDataSource = new List<ReportDataSource>();



                ////reporte estudiante
                ////reporte_estudiante reporteEstudiante = new reporte_estudiante();
                ////List<reporte_estudiante> ListaReporteEstudiante = new List<reporte_estudiante>();
                ////int cont = 0;
                ////listaEstudiante.ForEach(x =>
                ////{
                ////    reporteEstudiante = new reporte_estudiante();

                ////    reporteEstudiante.estudiante = cont + 1;
                ////    reporteEstudiante.indice = x.indice;
                ////    reporteEstudiante.carrera = x.Nombrecarrera;
                ////    reporteEstudiante.tipoCarrera = x.tipoCarrera;
                ////    reporteEstudiante.beca = x.tieneBeca;
                ////    reporteEstudiante.tiempoServicioBase = x.tiempoServicioBase;
                ////    reporteEstudiante.tiempoServicioFinal = x.tiempoServicioFinal;
                ////    reporteEstudiante.jugadas = x.jugadas;

                ////    ListaReporteEstudiante.Add(reporteEstudiante);
                ////    cont++;

                ////});

                ////ReportDataSource reporteF = new ReportDataSource("estudiante", ListaReporteEstudiante);
                ////listaReportDataSource.Add(reporteF);

                //List<ReportParameter> ListaReportParameter = new List<ReportParameter>();

                //VisorReporteComun ventana = new VisorReporteComun(reporte, listaReportDataSource, ListaReportParameter, true, false, false);
                //ventana.ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error imprimir2: " + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion


        //imprimir reportes graficos
        #region
        public void imprimir3()
        {
            try
            {
                //if (listaCliente == null)
                //{
                //    MessageBox.Show("No se encontraron datos, primero debe simular", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //}



                ////datos generales
                //String reporte = "SimulacionCajeroBanco.clases_reportes.reporte_cliente.rdlc";
                //List<ReportDataSource> listaReportDataSource = new List<ReportDataSource>();



                ////reporte estudiante
                ////reporte_estudiante reporteEstudiante = new reporte_estudiante();
                ////List<reporte_estudiante> ListaReporteEstudiante = new List<reporte_estudiante>();
                ////int cont = 0;
                ////listaEstudiante.ForEach(x =>
                ////{
                ////    reporteEstudiante = new reporte_estudiante();

                ////    reporteEstudiante.estudiante = cont + 1;
                ////    reporteEstudiante.indice = x.indice;
                ////    reporteEstudiante.carrera = x.Nombrecarrera;
                ////    reporteEstudiante.tipoCarrera = x.tipoCarrera;
                ////    reporteEstudiante.beca = x.tieneBeca;
                ////    reporteEstudiante.tiempoServicioBase = x.tiempoServicioBase;
                ////    reporteEstudiante.tiempoServicioFinal = x.tiempoServicioFinal;
                ////    reporteEstudiante.jugadas = x.jugadas;

                ////    ListaReporteEstudiante.Add(reporteEstudiante);
                ////    cont++;

                ////});

                ////ReportDataSource reporteF = new ReportDataSource("estudiante", ListaReporteEstudiante);
                ////listaReportDataSource.Add(reporteF);

                //List<ReportParameter> ListaReportParameter = new List<ReportParameter>();

                //VisorReporteComun ventana = new VisorReporteComun(reporte, listaReportDataSource, ListaReportParameter, true, false, false);
                //ventana.ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error imprimir3: " + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        

        private void button1_Click(object sender, EventArgs e)
        {
            getAction();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (listaCliente == null)
                {
                    MessageBox.Show("Debe haber simulado primero");
                    return;
                }


                //imprimir reportes
                imprimir1();
                imprimir2();
                imprimir3();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void temporadaAnoCombo_TextChanged(object sender, EventArgs e)
        {
            getClientesByTemporada();
        }

    }
}

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
using Microsoft.Reporting.WinForms;
using SimulacionCajeroBanco.clases;
using SimulacionCajeroBanco.clases_reportes;
using _7ADMFIC_1._0.VentanasComunes;

namespace SimulacionCajeroBanco
{
    public partial class ColaBanco : Form
    {


        //objetos
        private cajero cajero;  //objero cajero con todos los atributos, guardar un cajero
        private cliente cliente; //objero cliente con todos los atributos, guardar un cliente
        private operaciones operacion;//objero operacion con todos los atributos, guardar una operacion(deposito,retiro,etc)
        private problema problema;//objero problema con todos los atributos, guardar problema falla luz, falla sistema,etc
        private tanda tanda;//objero tanda con todos los atributos, guardar tanda matutina o vespertina
        private temporada temporada;//objero temporada con todos los atributos, guardar temporada de anio
        private dia dia;//objero dia con todos los atributos, guardar dia si es pago o normal
        private tipo_caja tipoCaja;//objero tipoCaja con todos los atributos, guardar el tipo de caja
        private clientesProblemasLog clientesProblemasLog;//objero clientesProblemasLog con todos los atributos
        private fases fases;//objeto para almacenar las fases de los procesos



        //objetos reportes
        private reporte_por_cajero reportePorCajero; //objeto para el reporte por cajero
        private reporte_grafico_cliente reporteGraficoCliente; //objeto para el reporte grafico de clientes
        private List<clientesProblemasLog> listaClienteProblemasLog;  //objeto para guardar los problemas de tdos los clientes
        


        //listas de reportes
        private List<reporte_por_cajero> listaReportePorCajero; //lista para almacenar los objetos reporte por cajeros 
        private List<reporte_grafico_cliente> listaReporteGraficoCliente; //lista para almacenar los objetos reporte grafico de cliente



        //variables
        Random random;//para randoms
        private int cantidadClientes = 0; //saber la cantidad de clientes
        private int cantidadCajeros = 0; //para saber la cantidad de cajeros de todas als operaciones
        private double numero = 0; //usado para almacenar randoms
        int contadorCajero = 0;    //usado para asignar codigo a los cajero
        private bool cajaEncontrada = false; //para saber siel cliente encontro la caja deseada
        private int cantidadCajerosDeposito = 0; //para saber la cantidad de cajeros con la operacion deposito
        private int cantidadCajerosRetiro = 0;//para saber la cantidad de cajeros con la operacion retiro
        private int cantidadCajerosCambioMoneda = 0; //para saber la cantidad de cajeros con la operacion cambio moneda
        int cajeroSeleccionado = 0;     //para saber el cajero que selecciono el cliente
        private int cantidadCajerosDisponibles = 0; //para saber la cantidad de cajeros que estan disponibles



        //listas
        private List<cajero> listaCajero;             //para guardar todos los cajeros
        private List<cliente> listaCliente;         //para guardar los clientes
        private List<temporada> listaTemporada;     //para guardar las temporadas del anio
        private List<tanda> listaTanda; //para guardar las tandas
        private List<dia> listaDias;   //para guardar los tipos de dias
        private List<tipo_caja> listaTipoCaja; //para guardar los tipos de caja
        private List<cajero> listaCajeroDeposito; //para guardar los cajeros que son de deposito
        private List<cajero> listaCajeroRetiro; //para guardar los cajeros que son de retiro
        private List<cajero> listaCajeroCambioMoneda;  //para guardar los cajero que son cambio moneda
        


        //lista de fases por operacion
        private List<fases> listaFasesDeposito;//lista que almacena las fases de depositos
        private List<fases> listaFasesRetiro;//lista que almacena las fases de retiros
        private List<fases> listaFasesCambioMoneda; //lista que almacena las fases de cambio monedas



        //listas problemas
        private List<problema> listaProblemaDeposito; //lista par guardar los problemas de depositos
        private List<problema> listaProblemaRetiro;//lista par guardar los problemas de retiro
        private List<problema> listaProblemaCambio;//lista par guardar los problemas de cambio de moneda
        private List<problema> lisaProblemaCheque; //lista par guardar los problemas de cheques



        //variables para datos
        private double tiempoLLegadaAcumulativo = 0; //variable para acumular el tiempo llegada
        private double tiempoServicioAcumulativo = 0;//variable para acumular el tiempo servicio
        private double clienteDeposito = 0;//variable para acumular los clientes de cambio de deposito
        private double clienteRetiro = 0;//variable para acumular los clientes de cambio de retiro
        private double clienteCambioMoneda = 0;//variable para acumular los clientes de cambio de moneda
        

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

                getFases();
                loadDias();
                loadTiposCaja();
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

        //get fases
        public void getFases()
        {
            try
            {
                //para depositos
                #region
                listaFasesDeposito =new List<fases>();
                //entrega dinero
                fases=new fases();
                fases.nombre = "entrega dinero";
                listaFasesDeposito.Add(fases);
                //entrega datos
                fases = new fases();
                fases.nombre = "entrega datos";
                listaFasesDeposito.Add(fases);
                #endregion


                //para retiros
                #region
                listaFasesRetiro = new List<fases>();
                //entrega datos
                fases = new fases();
                fases.nombre = "entrega datos";
                listaFasesRetiro.Add(fases);
                //verificar dinero
                fases = new fases();
                fases.nombre = "verificar dinero";
                listaFasesRetiro.Add(fases);
                #endregion



                //para cambio de moneda
                #region
                listaFasesCambioMoneda = new List<fases>();
                //entrega datos
                fases = new fases();
                fases.nombre = "entrega datos";
                listaFasesCambioMoneda.Add(fases);
                //verificar dinero
                fases = new fases();
                fases.nombre = "verificar dinero";
                listaFasesCambioMoneda.Add(fases);
                #endregion


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getFases.:" + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                    Thread.Sleep(5);
                    random = new Random();
                    numero = random.Next(1,15);
                    problema.tiempo_aumenta = numero;
                    listaProblemaDeposito.Add(problema);

                    problema = new problema();
                    problema.nombre = "fallo electricidad";
                    problema.probabilidad_ocurrencia_inicial = 0;
                    problema.probabilidad_ocurrencia_final = 25.30;
                    Thread.Sleep(5);
                    random = new Random();
                    numero = random.Next(1, 20);
                    problema.tiempo_aumenta = numero;
                    listaProblemaDeposito.Add(problema);

                    problema = new problema();
                    problema.nombre = "numero cuenta incorrecto";
                    problema.probabilidad_ocurrencia_inicial = 0;
                    problema.probabilidad_ocurrencia_final = 23.60;
                    Thread.Sleep(5);
                    random = new Random();
                    numero = random.Next(1, 3);
                    problema.tiempo_aumenta = numero;
                    listaProblemaDeposito.Add(problema);

                    problema = new problema();
                    problema.nombre = "dinero insuficiente";
                    problema.probabilidad_ocurrencia_inicial = 0;
                    problema.probabilidad_ocurrencia_final = 17.85;
                    Thread.Sleep(5);
                    random = new Random();
                    numero = random.Next(1, 4);
                    problema.tiempo_aumenta = numero;
                    listaProblemaDeposito.Add(problema);

                    problema = new problema();
                    problema.nombre = "falta cedula";
                    problema.probabilidad_ocurrencia_inicial = 0;
                    problema.probabilidad_ocurrencia_final = 35.90;
                    Thread.Sleep(5);
                    random = new Random();
                    numero = random.Next(1, 2);
                    problema.tiempo_aumenta = numero;
                    listaProblemaDeposito.Add(problema);


                    //problemas retiro
                    problema = new problema();
                    problema.nombre = "fallo sistema";
                    problema.probabilidad_ocurrencia_inicial = 0;
                    problema.probabilidad_ocurrencia_final = 5.12;
                    Thread.Sleep(5);
                    random = new Random();
                    numero = random.Next(1, 15);
                    problema.tiempo_aumenta = numero;
                    listaProblemaRetiro.Add(problema);

                    problema = new problema(); 
                    problema.nombre = "fallo electricidad";
                    problema.probabilidad_ocurrencia_inicial = 0;
                    problema.probabilidad_ocurrencia_final = 9.85;
                    Thread.Sleep(5);
                    random = new Random();
                    numero = random.Next(1, 20);
                    problema.tiempo_aumenta = numero;
                    listaProblemaRetiro.Add(problema);

                    problema = new problema(); 
                    problema.nombre = "numero cuenta incorrecto";
                    problema.probabilidad_ocurrencia_inicial = 0;
                    problema.probabilidad_ocurrencia_final = 14.69;
                    Thread.Sleep(5);
                    random = new Random();
                    numero = random.Next(1, 2);
                    problema.tiempo_aumenta = numero;
                    listaProblemaRetiro.Add(problema);

                    problema=new problema();
                    problema.nombre = "dinero insuficiente";
                    problema.probabilidad_ocurrencia_inicial = 0;
                    problema.probabilidad_ocurrencia_final = 11.80;
                    Thread.Sleep(5);
                    random = new Random();
                    numero = random.Next(1,3);
                    problema.tiempo_aumenta = numero;
                    listaProblemaRetiro.Add(problema);

                    problema = new problema();
                    problema.nombre = "falta cedula";
                    problema.probabilidad_ocurrencia_inicial = 0;
                    problema.probabilidad_ocurrencia_final = 17.34;
                    Thread.Sleep(5);
                    random = new Random();
                    numero = random.Next(1, 2);
                    problema.tiempo_aumenta = numero;
                    listaProblemaRetiro.Add(problema);


                    //problemas cambio moneda
                    problema = new problema();
                    problema.nombre = "fallo sistema";
                    problema.probabilidad_ocurrencia_inicial = 0;
                    problema.probabilidad_ocurrencia_final = 5.12;
                    Thread.Sleep(5);
                    random = new Random();
                    numero = random.Next(1, 15);
                    problema.tiempo_aumenta = numero;
                    listaProblemaCambio.Add(problema);


                    problema = new problema();
                    problema.nombre = "fallo electricidad";
                    problema.probabilidad_ocurrencia_inicial = 0;
                    problema.probabilidad_ocurrencia_final = 9.85;
                    Thread.Sleep(5);
                    random = new Random();
                    numero = random.Next(1, 20);
                    problema.tiempo_aumenta = numero;
                    listaProblemaCambio.Add(problema);

                    
                    problema = new problema();
                    problema.nombre = "dinero insuficiente";
                    problema.probabilidad_ocurrencia_inicial = 0;
                    problema.probabilidad_ocurrencia_final = 5.45;
                    Thread.Sleep(5);
                    random = new Random();
                    numero = random.Next(1, 5);
                    problema.tiempo_aumenta = numero;
                    listaProblemaCambio.Add(problema);

                    problema = new problema();
                    problema.nombre = "moneda no es aceptada";
                    problema.probabilidad_ocurrencia_inicial = 0;
                    problema.probabilidad_ocurrencia_final = 9.85;
                    Thread.Sleep(5);
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
                //if (temporada.nombre == "invierno")
                //{
                    #region
                //    //problemas deposito
                //    problema = new problema();
                //    problema.nombre = "fallo sistema";
                //    problema.probabilidad_ocurrencia_inicial = 0;
                //    problema.probabilidad_ocurrencia_final = 12.60;
                //    Thread.Sleep(5);
                //    random = new Random();
                //    numero = random.Next(1, 20);
                //    problema.tiempo_aumenta = numero;
                //    listaProblemaDeposito.Add(problema);


                //    problema = new problema();
                //    problema.nombre = "fallo electricidad";
                //    problema.probabilidad_ocurrencia_inicial = 0;
                //    problema.probabilidad_ocurrencia_final = 20.30;
                //    Thread.Sleep(5);
                //    random = new Random();
                //    numero = random.Next(1, 30);
                //    problema.tiempo_aumenta = numero;
                //    listaProblemaDeposito.Add(problema);


                //    problema = new problema();
                //    problema.nombre = "numero cuenta incorrecto";
                //    problema.probabilidad_ocurrencia_inicial = 0;
                //    problema.probabilidad_ocurrencia_final = 17.43;
                //    Thread.Sleep(5);
                //    random = new Random();
                //    numero = random.Next(1, 4);
                //    problema.tiempo_aumenta = numero;
                //    listaProblemaDeposito.Add(problema);


                //    problema = new problema();
                //    problema.nombre = "dinero insuficiente";
                //    problema.probabilidad_ocurrencia_inicial = 0;
                //    problema.probabilidad_ocurrencia_final = 15.30;
                //    Thread.Sleep(5);
                //    random = new Random();
                //    numero = random.Next(1, 3);
                //    problema.tiempo_aumenta = numero;
                //    listaProblemaDeposito.Add(problema);


                //    problema = new problema();
                //    problema.nombre = "falta cedula";
                //    problema.probabilidad_ocurrencia_inicial = 0;
                //    problema.probabilidad_ocurrencia_final = 7.50;
                //    Thread.Sleep(5);
                //    random = new Random();
                //    numero = random.Next(1, 3);
                //    problema.tiempo_aumenta = numero;
                //    listaProblemaDeposito.Add(problema);


                //    //problemas retiro
                //    problema = new problema();
                //    problema.nombre = "fallo sistema";
                //    problema.probabilidad_ocurrencia_inicial = 0;
                //    problema.probabilidad_ocurrencia_final = 12.60;
                //    Thread.Sleep(5);
                //    random = new Random();
                //    numero = random.Next(1, 20);
                //    problema.tiempo_aumenta = numero;
                //    listaProblemaRetiro.Add(problema);


                //    problema = new problema();
                //    problema.nombre = "fallo electricidad";
                //    problema.probabilidad_ocurrencia_inicial = 0;
                //    problema.probabilidad_ocurrencia_final = 20.30;
                //    Thread.Sleep(5);
                //    random = new Random();
                //    numero = random.Next(1, 30);
                //    problema.tiempo_aumenta = numero;
                //    listaProblemaRetiro.Add(problema);


                //    problema = new problema();
                //    problema.nombre = "numero cuenta incorrecto";
                //    problema.probabilidad_ocurrencia_inicial = 0;
                //    problema.probabilidad_ocurrencia_final = 9.60;
                //    Thread.Sleep(5);
                //    random = new Random();
                //    numero = random.Next(1, 3);
                //    problema.tiempo_aumenta = numero;
                //    listaProblemaRetiro.Add(problema);


                //    problema = new problema();
                //    problema.nombre = "dinero insuficiente";
                //    problema.probabilidad_ocurrencia_inicial = 0;
                //    problema.probabilidad_ocurrencia_final = 14.39;
                //    Thread.Sleep(5);
                //    random = new Random();
                //    numero = random.Next(1, 3);
                //    problema.tiempo_aumenta = numero;
                //    listaProblemaRetiro.Add(problema);

                //    problema = new problema();
                //    problema.nombre = "falta cedula";
                //    problema.probabilidad_ocurrencia_inicial = 0;
                //    problema.probabilidad_ocurrencia_final = 22.43;
                //    Thread.Sleep(5);
                //    random = new Random();
                //    numero = random.Next(1, 2);
                //    problema.tiempo_aumenta = numero;
                //    listaProblemaRetiro.Add(problema);


                //    //problemas cambio moneda
                //    problema = new problema();
                //    problema.nombre = "fallo sistema";
                //    problema.probabilidad_ocurrencia_inicial = 0;
                //    problema.probabilidad_ocurrencia_final = 12.60;
                //    Thread.Sleep(5);
                //    random = new Random();
                //    numero = random.Next(1, 20);
                //    problema.tiempo_aumenta = numero;
                //    listaProblemaCambio.Add(problema);


                //    problema = new problema();
                //    problema.nombre = "fallo electricidad";
                //    problema.probabilidad_ocurrencia_inicial = 0;
                //    problema.probabilidad_ocurrencia_final = 20.30;
                //    Thread.Sleep(5);
                //    random = new Random();
                //    numero = random.Next(1, 30);
                //    problema.tiempo_aumenta = numero; 
                //    listaProblemaCambio.Add(problema);


                //    problema = new problema();
                //    problema.nombre = "dinero insuficiente";
                //    problema.probabilidad_ocurrencia_inicial = 0;
                //    Thread.Sleep(5);
                //    random = new Random();
                //    numero = random.Next(1, 6);
                //    problema.tiempo_aumenta = numero; 
                //    listaProblemaCambio.Add(problema);


                //    problema = new problema();
                //    problema.nombre = "moneda no es aceptada";
                //    problema.probabilidad_ocurrencia_inicial = 0;
                //    problema.probabilidad_ocurrencia_final = 34.20;
                //    Thread.Sleep(5);
                //    random = new Random();
                //    numero = random.Next(1, 4);
                //    problema.tiempo_aumenta = numero;
                //    listaProblemaCambio.Add(problema);


                //    //problemas cheque
                //    //random = new Random();
                //    //problema = new problema();
                //    //problema.nombre = "fallo sistema";
                //    //problema.probabilidad_ocurrencia_inicial = 0;
                //    //problema.probabilidad_ocurrencia_final = 12.60;
                //    //numero = random.Next(1, 15);
                //    //lisaProblemaCheque.Add(problema);

                //    //random = new Random();
                //    //problema = new problema();
                //    //problema.nombre = "fallo electricidad";
                //    //problema.probabilidad_ocurrencia_inicial = 0;
                //    //problema.probabilidad_ocurrencia_final = 20.30;
                //    //numero = random.Next(1, 5);
                //    //lisaProblemaCheque.Add(problema);

                //    //random = new Random();
                //    //problema = new problema();
                //    //problema.nombre = "falta cedula";
                //    //problema.probabilidad_ocurrencia_inicial = 0;
                //    //problema.probabilidad_ocurrencia_final = 23.41;
                //    //lisaProblemaCheque.Add(problema);

                //    //random = new Random();
                //    //problema = new problema();
                //    //problema.nombre = "cheque mal endosado";
                //    //problema.probabilidad_ocurrencia_inicial = 0;
                //    //problema.probabilidad_ocurrencia_final = 19.42;
                //    //lisaProblemaCheque.Add(problema);

                //    //random = new Random();
                //    //problema = new problema();
                //    //problema.nombre = "cheque sin fondos";
                //    //problema.probabilidad_ocurrencia_inicial = 0;
                //    //problema.probabilidad_ocurrencia_final = 12.98;
                //    //lisaProblemaCheque.Add(problema);
                    #endregion
                //}

            }
            catch (Exception ex)
            {

                MessageBox.Show("Error getProblemas.:" + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion


        //load tandas
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
      

        //load dias
        public void loadDias()
        {       
            #region
            try
            {
                listaDias=new List<dia>();

                //nuevo dia
                dia=new dia();
                dia.nombre = "normal";
                dia.cantidad_cliente_rango_inicial = 0;
                dia.cantidad_cliente_rango_final = 0;
                listaDias.Add(dia);

                //nuevo dia
                dia = new dia();
                dia.nombre = "pago";
                dia.cantidad_cliente_rango_inicial = 0;
                dia.cantidad_cliente_rango_final = 0;
                listaDias.Add(dia);

                //agregando los dias al combo
                diasCombo.DisplayMember = "nombre";
                diasCombo.ValueMember = "nombre";
                diasCombo.DataSource = listaDias;


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loadTiposCaja.:" + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            #endregion
        }

        //load tipos de caja
        public void loadTiposCaja()
        {
            #region

            try
            {
                listaTipoCaja=new List<tipo_caja>();

                //nuevo tipo caja
                tipoCaja=new tipo_caja();
                tipoCaja.nombre = "deposito";
                tipoCaja.clientes_abandono = 0;
                tipoCaja.clientes_atendidos = 0;
                tipoCaja.total_clientes = 0;
                listaTipoCaja.Add(tipoCaja);

                //nuevo tipo caja
                tipoCaja = new tipo_caja();
                tipoCaja.nombre = "retiro";
                tipoCaja.clientes_abandono = 0;
                tipoCaja.clientes_atendidos = 0;
                tipoCaja.total_clientes = 0;
                listaTipoCaja.Add(tipoCaja);

                //nuevo tipo caja
                tipoCaja = new tipo_caja();
                tipoCaja.nombre = "cambio moneda";
                tipoCaja.clientes_abandono = 0;
                tipoCaja.clientes_atendidos = 0;
                tipoCaja.total_clientes = 0;
                listaTipoCaja.Add(tipoCaja);

                //agregando los tipos de caja al combo
                tipoCajaCombo.DisplayMember = "nombre";
                tipoCajaCombo.ValueMember = "nombre";
                tipoCajaCombo.DataSource = listaTipoCaja;
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loadTiposCaja.:" + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            #endregion
        }
        
        //load tanda
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
       
        public bool validarGetAction()
        {
            #region
            try
            {

                if (listaCajero == null)
                {
                    MessageBox.Show("Falta agregar los cajeros", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cantidadTipoCajaText.Focus();
                    cantidadTipoCajaText.SelectAll();
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
            #endregion
        }
       
        


        //get action
        
        public void getAction()
        {
            try
            {
                listaClienteProblemasLog=new List<clientesProblemasLog>();
                if (!validarGetAction())
                {
                    return;
                }

                //procesar

                //cargar fases
                getFases();

                //cargar problemas
                getProblemas();
                
                //cantidad de cajeros
                cantidadCajeros = Convert.ToInt16(listaCajero.ToList().Count);

                //cantidad de clientes
                cantidadClientes = Convert.ToInt16(cantidadClienteText.Text.Trim());

                //generar clientes
                getClientes();


                //instanciando la lista de problemas que puede tener un cliente
                if (listaClienteProblemasLog == null)
                {
                    listaClienteProblemasLog = new List<clientesProblemasLog>();
                }
                
                //rrecorriendo los clientes
                listaCliente.ForEach(clienteActual =>
                {
                    
                    Thread.Sleep(5);
                    //instancia de los problemas log del cliente
                    //clientesProblemasLog.codigocliente = clienteActual.codigo;
                    //clientesProblemasLog.problema_encontrado = false;
                    //clientesProblemasLog.tiempo_antes = clienteActual.tiempo_servicio_final;
                    //clientesProblemasLog.cantidad_intentos = 0;

                    //cliente esta siendo atendido
                    clienteActual.atendiendo = true;
                    clienteActual.abandono = false;
                    random = new Random();



                    //deposito
                    if (clienteActual.operacion_deseada == "deposito" && clienteActual.abandono == false)
                    {

                        //inicio asignar caja a cliente
                        #region
                        cajaEncontrada = false;
                        //saber cuantos cajeros de esta operacion hay
                        cantidadCajerosDisponibles = listaCajeroDeposito.ToList().Count;
                        //elegir que caja quiere el cliente
                        cajeroSeleccionado = 0;
                        Thread.Sleep(5);
                        cajeroSeleccionado = random.Next(0, cantidadCajerosDisponibles);
                        //MessageBox.Show(cajeroSeleccionado.ToString());
                        if (cajaEncontrada == false)
                        {
                             //caja encontrada
                             cajero.clientesCola = cajero.clientesCola + 1;
                             clienteActual.tipo_cajero = cajeroSeleccionado + "-" +"deposito";
                             cajaEncontrada = true;
                        }
                        else
                        {
                             //MessageBox.Show(" cliente-> " + clienteActual.codigo + " cajero seleccionado-> " +cajeroSeleccionado);
                        }
                        #endregion
                        //fin asignar caja a cliente

                       
                       
                        listaFasesDeposito.ForEach(faseActual =>
                        {
                            
                            listaProblemaDeposito.ForEach(problemaActual =>
                            {
                            #region
                            Thread.Sleep(5);
                            random = new Random();
                            numero = random.NextDouble();
                            numero = Math.Round(numero,2);
                            //0.45*100=  numero=45
                            numero*=100;
                            if (numero >= problemaActual.probabilidad_ocurrencia_inicial && numero <= problemaActual.probabilidad_ocurrencia_final)
                            {
                                //MessageBox.Show("tiempo antes->" + cliente.tiempo_servicio_final + " tiempo ahora->" + ((cliente.tiempo_servicio_final + p.tiempo_aumenta)).ToString("N"));
                                //cliente se presento este problema y toma desiciones por ende aumenta el tiempo
                                #region
                                //recorriendo las fases
                                if (problemaActual.nombre == "fallo sistema")
                                {

                                    //verificando si ocurren problemas en los procesos
                                    clientesProblemasLog = new clientesProblemasLog();
                                    clientesProblemasLog.codigocliente = clienteActual.codigo;
                                    clientesProblemasLog.tiempo_antes = clienteActual.tiempo_servicio_final;
                                    clientesProblemasLog.operacion = clienteActual.operacion_deseada;
                                    clientesProblemasLog.problema_encontrado = true;
                                    clientesProblemasLog.nombreProblema = problemaActual.nombre;
                                    clientesProblemasLog.fase = faseActual.nombre;
                                    //el cliente puede elegir si se queda o se va porque el fallo es grave
                                    Thread.Sleep(5);
                                    numero = random.Next(1, 3);
                                    clienteActual.problemas.Add(problemaActual);
                                    if (numero == 1)
                                    {
                                        //se fue el cliente
                                        //cliente problema log
                                        clientesProblemasLog.tiempo_despues = clienteActual.tiempo_servicio_final;
                                        clientesProblemasLog.respuesta = "el cliente se fue, no espero la solucion del problema";
                                        clienteActual.abandono = true;
                                        //clienteActual.tipo_cajero = cajero.codigo + "-" + cajero.operacion;

                                    }
                                    else
                                    {
                                        Thread.Sleep(5);
                                        //cliente problema log
                                        //el cliente se queda por ende aumenta el tiempo de servicio
                                        clienteActual.tiempo_servicio_final += problemaActual.tiempo_aumenta;
                                        clientesProblemasLog.tiempo_despues = clienteActual.tiempo_servicio_final;
                                        clientesProblemasLog.respuesta = "el cliente no se fue, espero a que el problema se solucione";
                                    }
                                    listaClienteProblemasLog.Add(clientesProblemasLog);
                                }
                                if (problemaActual.nombre == "fallo electricidad")
                                {
                                    //verificando si ocurren problemas en los procesos
                                    clientesProblemasLog = new clientesProblemasLog();
                                    clientesProblemasLog.codigocliente = clienteActual.codigo;
                                    clientesProblemasLog.tiempo_antes = clienteActual.tiempo_servicio_final;
                                    clientesProblemasLog.operacion = clienteActual.operacion_deseada;
                                    clientesProblemasLog.problema_encontrado = true;
                                    clientesProblemasLog.nombreProblema = problemaActual.nombre;
                                    clientesProblemasLog.fase = faseActual.nombre;
                                    //el cliente puede elegir si se queda o se va porque el fallo es grave
                                    Thread.Sleep(5);
                                    numero = random.Next(1, 3);
                                    clienteActual.problemas.Add(problemaActual);
                                    if (numero == 1)
                                    {
                                        //se fue el cliente
                                        //cliente problema log
                                        clientesProblemasLog.tiempo_despues = clienteActual.tiempo_servicio_final;
                                        clientesProblemasLog.respuesta = "el cliente se fue, no espero la solucion del problema";
                                        clienteActual.abandono = true;
                                        //clienteActual.tipo_cajero = cajero.codigo + "-" + cajero.operacion;
                                    }
                                    else
                                    {
                                        Thread.Sleep(5);
                                        //cliente problema log
                                        //el cliente se queda por ende aumenta el tiempo de servicio
                                        clienteActual.tiempo_servicio_final += problemaActual.tiempo_aumenta;
                                        clientesProblemasLog.tiempo_despues = clienteActual.tiempo_servicio_final;
                                        clientesProblemasLog.respuesta = "el cliente no se fue, espera que el problema se solucione";
                                    }
                                    listaClienteProblemasLog.Add(clientesProblemasLog);
                                }
                                if (problemaActual.nombre == "dinero insuficiente")
                                {
                                    //verificando si ocurren problemas en los procesos
                                    clientesProblemasLog = new clientesProblemasLog();
                                    clientesProblemasLog.codigocliente = clienteActual.codigo;
                                    clientesProblemasLog.tiempo_antes = clienteActual.tiempo_servicio_final;
                                    clientesProblemasLog.operacion = clienteActual.operacion_deseada;
                                    clientesProblemasLog.problema_encontrado = true;
                                    clientesProblemasLog.nombreProblema = problemaActual.nombre;
                                    clientesProblemasLog.fase = faseActual.nombre;
                                    //el cliente puede elegir si lo intenta una vez mas
                                    Thread.Sleep(5);
                                    numero = random.Next(1, 2);
                                    clienteActual.problemas.Add(problemaActual);
                                    if (numero == 1)
                                    {
                                        Thread.Sleep(5);
                                        clientesProblemasLog.tiempo_antes = clienteActual.tiempo_servicio_final;
                                        clientesProblemasLog.respuesta = "el cliente no se fue, lo intenta de nuevo";
                                        clientesProblemasLog.cantidad_intentos = 1;
                                        //lo intentara y aumenta tiempo
                                        clienteActual.intentos +=1;
                                        clienteActual.tiempo_servicio_final += problemaActual.tiempo_aumenta;
                                        clientesProblemasLog.tiempo_despues = clienteActual.tiempo_servicio_final;
                                    }
                                    else
                                    {
                                        Thread.Sleep(5);
                                        //cliente se fue
                                        clientesProblemasLog.tiempo_despues = clienteActual.tiempo_servicio_final;
                                        clientesProblemasLog.respuesta = "el cliente se fue, no espero la solucion del problema";
                                        clienteActual.abandono = true;
                                    }
                                    listaClienteProblemasLog.Add(clientesProblemasLog);
                                }
                                if (problemaActual.nombre == "numero cuenta incorrecto")
                                {
                                    //verificando si ocurren problemas en los procesos
                                    clientesProblemasLog = new clientesProblemasLog();
                                    clientesProblemasLog.codigocliente = clienteActual.codigo;
                                    clientesProblemasLog.tiempo_antes = clienteActual.tiempo_servicio_final;
                                    clientesProblemasLog.operacion = clienteActual.operacion_deseada;
                                    clientesProblemasLog.problema_encontrado = true;
                                    clientesProblemasLog.nombreProblema = problemaActual.nombre;
                                    clientesProblemasLog.fase = faseActual.nombre;
                                    //el cliente puede elegir si lo intenta una vez mas
                                    Thread.Sleep(5);
                                    numero = random.Next(1, 2);
                                    clienteActual.problemas.Add(problemaActual);
                                    if (numero == 1)
                                    {
                                        Thread.Sleep(5);
                                        clientesProblemasLog.tiempo_antes = clienteActual.tiempo_servicio_final;
                                        clientesProblemasLog.respuesta = "el cliente no se fue lo intenta denuevo";
                                        clientesProblemasLog.cantidad_intentos = 1;
                                        //lo intentara y aumenta tiempo
                                        clienteActual.intentos += 1;
                                        clienteActual.tiempo_servicio_final += problemaActual.tiempo_aumenta;
                                        clientesProblemasLog.tiempo_despues = clienteActual.tiempo_servicio_final;
                                        
                                    }
                                    else
                                    {
                                        Thread.Sleep(5);
                                        //cliente se fue
                                        clientesProblemasLog.tiempo_despues = clienteActual.tiempo_servicio_final;
                                        clientesProblemasLog.respuesta = "el cliente se fue, no espero la solucion del problema";
                                        clienteActual.abandono = true;
                                    }
                                    listaClienteProblemasLog.Add(clientesProblemasLog);
                                }
                                if (problemaActual.nombre == "falta cedula")
                                {
                                    //verificando si ocurren problemas en los procesos
                                    clientesProblemasLog = new clientesProblemasLog();
                                    clientesProblemasLog.codigocliente = clienteActual.codigo;
                                    clientesProblemasLog.tiempo_antes = clienteActual.tiempo_servicio_final;
                                    clientesProblemasLog.operacion = clienteActual.operacion_deseada;
                                    clientesProblemasLog.problema_encontrado = true;
                                    clientesProblemasLog.nombreProblema = problemaActual.nombre;
                                    clientesProblemasLog.fase = faseActual.nombre;
                                    //el cliente puede elegir si lo intenta una vez mas
                                    Thread.Sleep(5);
                                    numero = random.Next(1, 2);
                                    clienteActual.problemas.Add(problemaActual);
                                    if (numero == 1)
                                    {
                                        Thread.Sleep(5);
                                        clientesProblemasLog.tiempo_antes = clienteActual.tiempo_servicio_final;
                                        clientesProblemasLog.respuesta = "el cliente no se fue, lo intenta denuevo";
                                        clientesProblemasLog.cantidad_intentos = 1;
                                        //lo intentara y aumenta tiempo
                                        clienteActual.intentos += 1;
                                        clienteActual.tiempo_servicio_final += problemaActual.tiempo_aumenta;
                                        clientesProblemasLog.tiempo_despues = clienteActual.tiempo_servicio_final;
                                    }
                                    else
                                    {
                                        Thread.Sleep(5);
                                        //cliente se fue
                                        clientesProblemasLog.tiempo_despues = clienteActual.tiempo_servicio_final;
                                        clientesProblemasLog.respuesta = "el cliente se fue, no espero la solucion del problema";
                                        clienteActual.abandono = true;
                                    }
                                    listaClienteProblemasLog.Add(clientesProblemasLog);
                                }
                                #endregion
                            }
                            #endregion
                            });
                        });
                    }
                

                    ////retiro
                    if (clienteActual.operacion_deseada == "retiro" && clienteActual.abandono == false)
                    {

                        //inicio asignar caja a cliente
                        #region
                        //clientesProblemasLog.operacion = clienteActual.operacion_deseada;
                        cajaEncontrada = false;
                        //saber cuantos cajeros de esta operacion hay
                        cantidadCajerosDisponibles = listaCajeroRetiro.ToList().Count;
                        //elegir que caja quiere el cliente
                        cajeroSeleccionado = 0;
                        Thread.Sleep(5);
                        cajeroSeleccionado = random.Next(0, cantidadCajerosDisponibles);
                        //MessageBox.Show(cajeroSeleccionado.ToString());
                        if (cajaEncontrada == false)
                        {
                            //caja encontrada
                            cajero.clientesCola = cajero.clientesCola + 1;
                            clienteActual.tipo_cajero = cajeroSeleccionado + "-" + "retiro";
                            cajaEncontrada = true;
                        }
                        else
                        {
                            //MessageBox.Show(" cliente-> " + clienteActual.codigo + " cajero seleccionado-> " +cajeroSeleccionado);
                        }
                        #endregion
                        //fin asignar caja a cliente



                        //verificando si ocurren problemas en los procesos
                        listaFasesRetiro.ForEach(faseActual =>
                        {
                           
                            listaProblemaRetiro.ForEach(problemaActual =>
                            {
                                #region

                                Thread.Sleep(5);
                                random = new Random();
                                numero = random.NextDouble();
                                numero = Math.Round(numero, 2);
                                //0.45*100=  numero=45
                                numero *= 100;
                                if (numero >= problemaActual.probabilidad_ocurrencia_inicial &&
                                    numero <= problemaActual.probabilidad_ocurrencia_final)
                                {
                                    //MessageBox.Show("tiempo antes->" + cliente.tiempo_servicio_final + " tiempo ahora->" + ((cliente.tiempo_servicio_final + p.tiempo_aumenta)).ToString("N"));
                                    //cliente se presento este problema y toma desiciones por ende aumenta el tiempo

                                    #region

                                    if (problemaActual.nombre == "fallo sistema")
                                    {
                                        //verificando si ocurren problemas en los procesos
                                        clientesProblemasLog = new clientesProblemasLog();
                                        clientesProblemasLog.codigocliente = clienteActual.codigo;
                                        clientesProblemasLog.tiempo_antes = clienteActual.tiempo_servicio_final;
                                        clientesProblemasLog.operacion = clienteActual.operacion_deseada;
                                        clientesProblemasLog.problema_encontrado = true;
                                        clientesProblemasLog.nombreProblema = problemaActual.nombre;
                                        clientesProblemasLog.fase = faseActual.nombre;
                                        //el cliente puede elegir si se queda o se va porque el fallo es grave
                                        Thread.Sleep(5);
                                        numero = random.Next(1, 3);
                                        clienteActual.problemas.Add(problemaActual);
                                        if (numero == 1)
                                        {
                                            //se fue el cliente
                                            //cliente problema log
                                            clientesProblemasLog.tiempo_despues = clienteActual.tiempo_servicio_final;
                                            clientesProblemasLog.respuesta ="el cliente se fue, no espero la solucion del problema";
                                            clienteActual.abandono = true;
                                            //clienteActual.tipo_cajero = cajero.codigo + "-" + cajero.operacion;
                                        }
                                        else
                                        {
                                            Thread.Sleep(5);
                                            //cliente problema log
                                            //el cliente se queda por ende aumenta el tiempo de servicio
                                            clienteActual.tiempo_servicio_final += problemaActual.tiempo_aumenta;
                                            clientesProblemasLog.tiempo_despues = clienteActual.tiempo_servicio_final;
                                            clientesProblemasLog.respuesta =
                                                "el cliente no se fue, espero a que el problema se solucione";
                                        }
                                        listaClienteProblemasLog.Add(clientesProblemasLog);
                                    }
                                    if (problemaActual.nombre == "fallo electricidad")
                                    {
                                        //verificando si ocurren problemas en los procesos
                                        clientesProblemasLog = new clientesProblemasLog();
                                        clientesProblemasLog.codigocliente = clienteActual.codigo;
                                        clientesProblemasLog.tiempo_antes = clienteActual.tiempo_servicio_final;
                                        clientesProblemasLog.operacion = clienteActual.operacion_deseada;
                                        clientesProblemasLog.problema_encontrado = true;
                                        clientesProblemasLog.nombreProblema = problemaActual.nombre;
                                        clientesProblemasLog.fase = faseActual.nombre;
                                        //el cliente puede elegir si se queda o se va porque el fallo es grave
                                        Thread.Sleep(5);
                                        numero = random.Next(1, 3);
                                        clienteActual.problemas.Add(problemaActual);
                                        if (numero == 1)
                                        {
                                            //se fue el cliente
                                            //cliente problema log
                                            clientesProblemasLog.tiempo_despues = clienteActual.tiempo_servicio_final;
                                            clientesProblemasLog.respuesta =
                                                "el cliente se fue, no espero la solucion del problema";
                                            clienteActual.abandono = true;
                                            //clienteActual.tipo_cajero = cajero.codigo + "-" + cajero.operacion;
                                        }
                                        else
                                        {
                                            //cliente se queda por ende aumenta el tiempo de servicio
                                            clienteActual.tiempo_servicio_final += problemaActual.tiempo_aumenta;
                                            clientesProblemasLog.tiempo_despues = clienteActual.tiempo_servicio_final;
                                            clientesProblemasLog.respuesta =
                                                "el cliente no se fue, espera que el problema se solucione";
                                        }
                                        listaClienteProblemasLog.Add(clientesProblemasLog);
                                    }
                                    if (problemaActual.nombre == "dinero insuficiente")
                                    {
                                        //verificando si ocurren problemas en los procesos
                                        clientesProblemasLog = new clientesProblemasLog();
                                        clientesProblemasLog.codigocliente = clienteActual.codigo;
                                        clientesProblemasLog.tiempo_antes = clienteActual.tiempo_servicio_final;
                                        clientesProblemasLog.operacion = clienteActual.operacion_deseada;
                                        clientesProblemasLog.problema_encontrado = true;
                                        clientesProblemasLog.nombreProblema = problemaActual.nombre;
                                        clientesProblemasLog.fase = faseActual.nombre;
                                        //el cliente puede elegir si lo intenta una vez mas
                                        Thread.Sleep(5);
                                        numero = random.Next(1, 2);
                                        clienteActual.problemas.Add(problemaActual);
                                        if (numero == 1)
                                        {
                                            //cliente se queda por ende aumenta el tiempo de servicio
                                            clientesProblemasLog.cantidad_intentos = 1;
                                            clienteActual.tiempo_servicio_final += problemaActual.tiempo_aumenta;
                                            clientesProblemasLog.tiempo_despues = clienteActual.tiempo_servicio_final;
                                            clientesProblemasLog.respuesta =
                                                "el cliente no se fue, espera que el problema se solucione";
                                        }
                                        else
                                        {
                                            //cliente se fue
                                            clientesProblemasLog.tiempo_despues = clienteActual.tiempo_servicio_final;
                                            clientesProblemasLog.respuesta =
                                                "el cliente se fue, no espero la solucion del problema";
                                            clienteActual.abandono = true;
                                        }
                                        listaClienteProblemasLog.Add(clientesProblemasLog);
                                    }
                                    if (problemaActual.nombre == "numero cuenta incorrecto")
                                    {
                                        //verificando si ocurren problemas en los procesos
                                        clientesProblemasLog = new clientesProblemasLog();
                                        clientesProblemasLog.codigocliente = clienteActual.codigo;
                                        clientesProblemasLog.tiempo_antes = clienteActual.tiempo_servicio_final;
                                        clientesProblemasLog.operacion = clienteActual.operacion_deseada;
                                        clientesProblemasLog.problema_encontrado = true;
                                        clientesProblemasLog.nombreProblema = problemaActual.nombre;
                                        clientesProblemasLog.fase = faseActual.nombre;
                                        //el cliente puede elegir si lo intenta una vez mas
                                        Thread.Sleep(5);
                                        numero = random.Next(1, 2);
                                        clienteActual.problemas.Add(problemaActual);
                                        if (numero == 1)
                                        {
                                            //cliente se queda por ende aumenta el tiempo de servicio
                                            clientesProblemasLog.cantidad_intentos = 1;
                                            clienteActual.tiempo_servicio_final += problemaActual.tiempo_aumenta;
                                            clientesProblemasLog.tiempo_despues = clienteActual.tiempo_servicio_final;
                                            clientesProblemasLog.respuesta =
                                                "el cliente no se fue, espera que el problema se solucione";
                                        }
                                        else
                                        {
                                            //cliente se fue
                                            clientesProblemasLog.tiempo_despues = clienteActual.tiempo_servicio_final;
                                            clientesProblemasLog.respuesta =
                                                "el cliente se fue, no espero la solucion del problema";
                                            clienteActual.abandono = true;
                                        }
                                        listaClienteProblemasLog.Add(clientesProblemasLog);
                                    }
                                    if (problemaActual.nombre == "falta cedula")
                                    {
                                        //verificando si ocurren problemas en los procesos
                                        clientesProblemasLog = new clientesProblemasLog();
                                        clientesProblemasLog.codigocliente = clienteActual.codigo;
                                        clientesProblemasLog.tiempo_antes = clienteActual.tiempo_servicio_final;
                                        clientesProblemasLog.operacion = clienteActual.operacion_deseada;
                                        clientesProblemasLog.problema_encontrado = true;
                                        clientesProblemasLog.nombreProblema = problemaActual.nombre;
                                        clientesProblemasLog.fase = faseActual.nombre;
                                        //el cliente puede elegir si lo intenta una vez mas
                                        Thread.Sleep(5);
                                        numero = random.Next(1, 2);
                                        clienteActual.problemas.Add(problemaActual);
                                        if (numero == 1)
                                        {
                                            //cliente se queda por ende aumenta el tiempo de servicio
                                            clientesProblemasLog.cantidad_intentos = 1;
                                            clienteActual.tiempo_servicio_final += problemaActual.tiempo_aumenta;
                                            clientesProblemasLog.tiempo_despues = clienteActual.tiempo_servicio_final;
                                            clientesProblemasLog.respuesta ="el cliente no se fue, espera que el problema se solucione";
                                        }
                                        else
                                        {
                                            //cliente se fue
                                            clientesProblemasLog.tiempo_despues = clienteActual.tiempo_servicio_final;
                                            clientesProblemasLog.respuesta ="el cliente se fue, no espero la solucion del problema";
                                            clienteActual.abandono = true;
                                        }
                                        listaClienteProblemasLog.Add(clientesProblemasLog);
                                    }

                                    #endregion

                                }

                                #endregion

                                listaClienteProblemasLog.Add(clientesProblemasLog);
                            });
                        });
                    }



                    //cambio moneda
                    if (clienteActual.operacion_deseada == "cambio moneda" && clienteActual.abandono == false)
                    {

                        //inicio asignar caja a cliente
                        #region

                        cajaEncontrada = false;
                        //saber cuantos cajeros de esta operacion hay
                        cantidadCajerosDisponibles = listaCajeroCambioMoneda.ToList().Count;
                        //elegir que caja quiere el cliente
                        cajeroSeleccionado = 0;
                        Thread.Sleep(5);
                        cajeroSeleccionado = random.Next(0, cantidadCajerosDisponibles);
                        //MessageBox.Show(cajeroSeleccionado.ToString());

                        if (cajaEncontrada == false)
                        {
                            //caja encontrada
                            cajero.clientesCola = cajero.clientesCola + 1;
                            clienteActual.tipo_cajero = cajeroSeleccionado + "-" + "cambio moneda";
                            cajaEncontrada = true;
                        }
                        else
                        {
                            //MessageBox.Show(" cliente-> " + clienteActual.codigo + " cajero seleccionado-> " +cajeroSeleccionado);
                        }
                        #endregion
                        //fin asignar caja a cliente



                        //verificando si ocurren problemas en los procesos
                        //clientesProblemasLog.operacion = clienteActual.operacion_deseada;
                        listaFasesCambioMoneda.ForEach(faseActual =>
                        {
                            listaProblemaCambio.ForEach(problemaActual =>
                            {
                                #region

                                Thread.Sleep(5);
                                random = new Random();
                                numero = random.NextDouble();
                                numero = Math.Round(numero, 2);
                                //0.45*100=  numero=45
                                numero *= 100;
                                if (numero >= problemaActual.probabilidad_ocurrencia_inicial &&
                                    numero <= problemaActual.probabilidad_ocurrencia_final)
                                {
                                    //MessageBox.Show("tiempo antes->" + cliente.tiempo_servicio_final + " tiempo ahora->" + ((cliente.tiempo_servicio_final + p.tiempo_aumenta)).ToString("N"));
                                    //cliente se presento este problema y toma desiciones por ende aumenta el tiempo

                                    #region

                                    if (problemaActual.nombre == "fallo sistema")
                                    {
                                        //verificando si ocurren problemas en los procesos
                                        clientesProblemasLog = new clientesProblemasLog();
                                        clientesProblemasLog.codigocliente = clienteActual.codigo;
                                        clientesProblemasLog.tiempo_antes = clienteActual.tiempo_servicio_final;
                                        clientesProblemasLog.operacion = clienteActual.operacion_deseada;
                                        clientesProblemasLog.problema_encontrado = true;
                                        clientesProblemasLog.nombreProblema = problemaActual.nombre;
                                        clientesProblemasLog.fase = faseActual.nombre;
                                        //el cliente puede elegir si se queda o se va porque el fallo es grave
                                        Thread.Sleep(5);
                                        numero = random.Next(1, 3);
                                        clienteActual.problemas.Add(problemaActual);
                                        if (numero == 1)
                                        {
                                            //se fue el cliente
                                            //cliente problema log
                                            clientesProblemasLog.tiempo_despues = clienteActual.tiempo_servicio_final;
                                            clientesProblemasLog.respuesta ="el cliente se fue, no espero la solucion del problema";
                                            clienteActual.abandono = true;
                                            //clienteActual.tipo_cajero = cajero.codigo + "-" + cajero.operacion;
                                        }
                                        else
                                        {
                                            //el cliente se queda por ende aumenta el tiempo de servicio
                                            clienteActual.tiempo_servicio_final += problemaActual.tiempo_aumenta;
                                            clientesProblemasLog.tiempo_despues = clienteActual.tiempo_servicio_final;
                                            clientesProblemasLog.respuesta ="el cliente no se fue, espero a que el problema se solucione";
                                        }
                                        listaClienteProblemasLog.Add(clientesProblemasLog);
                                    }
                                    if (problemaActual.nombre == "fallo electricidad")
                                    {
                                        //verificando si ocurren problemas en los procesos
                                        clientesProblemasLog = new clientesProblemasLog();
                                        clientesProblemasLog.codigocliente = clienteActual.codigo;
                                        clientesProblemasLog.tiempo_antes = clienteActual.tiempo_servicio_final;
                                        clientesProblemasLog.operacion = clienteActual.operacion_deseada;
                                        clientesProblemasLog.problema_encontrado = true;
                                        clientesProblemasLog.nombreProblema = problemaActual.nombre;
                                        clientesProblemasLog.fase = faseActual.nombre;
                                        //el cliente puede elegir si se queda o se va porque el fallo es grave
                                        Thread.Sleep(5);
                                        numero = random.Next(1, 3);
                                        clienteActual.problemas.Add(problemaActual);
                                        if (numero == 1)
                                        {
                                            //se fue el cliente
                                            //cliente problema log
                                            clientesProblemasLog.tiempo_despues = clienteActual.tiempo_servicio_final;
                                            clientesProblemasLog.respuesta ="el cliente se fue, no espero la solucion del problema";
                                            clienteActual.abandono = true;
                                            //clienteActual.tipo_cajero = cajero.codigo + "-" + cajero.operacion;
                                        }
                                        else
                                        {
                                            //el cliente se queda por ende aumenta el tiempo de servicio
                                            clienteActual.tiempo_servicio_final += problemaActual.tiempo_aumenta;
                                            clientesProblemasLog.tiempo_despues = clienteActual.tiempo_servicio_final;
                                            clientesProblemasLog.respuesta ="el cliente no se fue, espera que el problema se solucione";
                                        }
                                        listaClienteProblemasLog.Add(clientesProblemasLog);
                                    }
                                    if (problemaActual.nombre == "dinero insuficiente")
                                    {
                                        //verificando si ocurren problemas en los procesos
                                        clientesProblemasLog = new clientesProblemasLog();
                                        clientesProblemasLog.codigocliente = clienteActual.codigo;
                                        clientesProblemasLog.tiempo_antes = clienteActual.tiempo_servicio_final;
                                        clientesProblemasLog.operacion = clienteActual.operacion_deseada;
                                        clientesProblemasLog.problema_encontrado = true;
                                        clientesProblemasLog.nombreProblema = problemaActual.nombre;
                                        clientesProblemasLog.fase = faseActual.nombre;
                                        //el cliente puede elegir si lo intenta una vez mas
                                        Thread.Sleep(5);
                                        numero = random.Next(1, 2);
                                        clienteActual.problemas.Add(problemaActual);
                                        if (numero == 1)
                                        {
                                            //lo intentara y aumenta tiempo
                                            clientesProblemasLog.cantidad_intentos = 1;
                                            clienteActual.intentos += 1;
                                            clienteActual.tiempo_servicio_final += problemaActual.tiempo_aumenta;
                                            clientesProblemasLog.respuesta ="el cliente no se fue, espero la solucion del problema";
                                        }
                                        else
                                        {
                                            //cliente se fue
                                            clientesProblemasLog.tiempo_despues = clienteActual.tiempo_servicio_final;
                                            clientesProblemasLog.respuesta ="el cliente se fue, no espero la solucion del problema";
                                            clienteActual.abandono = true;
                                        }
                                        listaClienteProblemasLog.Add(clientesProblemasLog);
                                    }
                                    if (problemaActual.nombre == "numero cuenta incorrecto")
                                    {
                                        //verificando si ocurren problemas en los procesos
                                        clientesProblemasLog = new clientesProblemasLog();
                                        clientesProblemasLog.codigocliente = clienteActual.codigo;
                                        clientesProblemasLog.tiempo_antes = clienteActual.tiempo_servicio_final;
                                        clientesProblemasLog.operacion = clienteActual.operacion_deseada;
                                        clientesProblemasLog.problema_encontrado = true;
                                        clientesProblemasLog.nombreProblema = problemaActual.nombre;
                                        clientesProblemasLog.fase = faseActual.nombre;
                                        //el cliente puede elegir si lo intenta una vez mas
                                        Thread.Sleep(5);
                                        numero = random.Next(1, 2);
                                        clienteActual.problemas.Add(problemaActual);
                                        if (numero == 1)
                                        {
                                            //lo intentara y aumenta tiempo
                                            clientesProblemasLog.cantidad_intentos = 1;
                                            clienteActual.intentos += 1;
                                            clienteActual.tiempo_servicio_final += problemaActual.tiempo_aumenta;
                                            clientesProblemasLog.respuesta ="el cliente no se fue, espero la solucion del problema";
                                        }
                                        else
                                        {
                                            //cliente se fue
                                            clientesProblemasLog.tiempo_despues = clienteActual.tiempo_servicio_final;
                                            clientesProblemasLog.respuesta ="el cliente se fue, no espero la solucion del problema";
                                            clienteActual.abandono = true;
                                        }
                                        listaClienteProblemasLog.Add(clientesProblemasLog);
                                    }
                                    if (problemaActual.nombre == "falta cedula")
                                    {
                                        //verificando si ocurren problemas en los procesos
                                        clientesProblemasLog = new clientesProblemasLog();
                                        clientesProblemasLog.codigocliente = clienteActual.codigo;
                                        clientesProblemasLog.tiempo_antes = clienteActual.tiempo_servicio_final;
                                        clientesProblemasLog.operacion = clienteActual.operacion_deseada;
                                        clientesProblemasLog.problema_encontrado = true;
                                        clientesProblemasLog.nombreProblema = problemaActual.nombre;
                                        clientesProblemasLog.fase = faseActual.nombre;
                                        //el cliente puede elegir si lo intenta una vez mas
                                        Thread.Sleep(5);
                                        numero = random.Next(1, 2);
                                        clienteActual.problemas.Add(problemaActual);
                                        if (numero == 1)
                                        {
                                            //lo intentara y aumenta tiempo
                                            clientesProblemasLog.cantidad_intentos = 1;
                                            clienteActual.intentos += 1;
                                            clienteActual.tiempo_servicio_final += problemaActual.tiempo_aumenta;
                                            clientesProblemasLog.respuesta ="el cliente no se fue, espero la solucion del problema";
                                        }
                                        else
                                        {
                                            //cliente se fue
                                            clientesProblemasLog.tiempo_despues = clienteActual.tiempo_servicio_final;
                                            clientesProblemasLog.respuesta ="el cliente se fue, no espero la solucion del problema";
                                            clienteActual.abandono = true;
                                        }
                                        listaClienteProblemasLog.Add(clientesProblemasLog);
                                    }
                                    #endregion
                                }
                                #endregion
                            });
                        });

                    }









                    //if (clienteActual.tiempo_servicio_esperado != clienteActual.tiempo_servicio_final)
                    //{
                    //    if (clienteActual.problemas.Count == 0)
                    //    {
                    //        MessageBox.Show("cliente-> " + clienteActual.codigo + "  tiempo esperado->" + clienteActual.tiempo_servicio_esperado + " tiempo final->" + clienteActual.tiempo_servicio_final);
                    //    }
                    //}

                    //if (x.abandono == true)
                    //{
                    //    MessageBox.Show("cliente abandono->"+x.codigo);
                        
                    //}
                    //if (x.problemas.Count > 0)
                    //{
                    //    MessageBox.Show("cliente->" + x.codigo + " problemas->" + x.problemas.Count);
                    //}
                    //if (x.tiempo_servicio_esperado != x.tiempo_servicio_final)
                    //{
                    //    MessageBox.Show("cliente-> "+x.codigo+"  tiempo esperado->" + x.tiempo_servicio_esperado + " tiempo final->" + x.tiempo_servicio_final);
                    //}
                    if (clienteActual.abandono == false)
                    {
                        clienteActual.operacion_completada = true;
                    }
                   
                });
                
                loadClientes();


            }
            catch (Exception ex)
            {

                MessageBox.Show("Error getAction.:" + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }






        //get clientes || distribucion clientes
        public void getClientes()
        {
            try
            {

                if (dataGridView2.Rows.Count > 0)
                {
                    dataGridView2.Rows.Clear();
                }
                random=new Random();
                listaCliente=new List<cliente>();
                //llenar clientes
                for (int f = 1; f <= cantidadClientes; f++)
                {
                    
                    //llenando los datos de cada cliente
                    cliente=new cliente();
                    //instanciando los problemas que pueda tener el cliente
                    cliente.problemas=new List<problema>();
                    cliente.codigo = f;
                    cliente.temporada = temporada.nombre;
                    cliente.tanda = tanda.nombre;
                    //asignar operacion que realizara dependiendo de la temporada y la tanda
                    //(deposito,retiro,cambio moneda)
                    //para que no se corra el valor porque va tan rapido que el valor se pierde
                    Thread.Sleep(5);
                    numero = random.NextDouble();
                    numero = Math.Round(numero, 2);
                    //MessageBox.Show("rand operacion->"+numero.ToString());
                    if (temporadaAnoCombo.Text == "primavera")
                    {
                        #region
                        //temporada primavera
                        if (tandaCombo.Text == "matutina")
                        {
                            if (diasCombo.Text == "normal")
                            {
                                //asigar operacion
                                #region
                                if (numero >= 0 && numero <= 0.44)
                                {
                                    //deposito
                                    #region
                                    operacion = new operaciones();
                                    operacion.nombre = "deposito";
                                    operacion.tiempo_promedio_rango_inicial = 2.0;
                                    operacion.tiempo_promedio_rango_final = 3.5;
                                    cliente.operacion_deseada = operacion.nombre;
                                    //obteniendo el tiempo promedio que dura la operacion y asigando a cliente

                                    numero = random.Next(2, 3);
                                    cliente.tiempo_servicio_esperado = numero;
                                    random = new Random();
                                    if (numero < 3)
                                    {
                                        Thread.Sleep(5);
                                        numero = random.Next(0, 9);
                                    }
                                    else
                                    {
                                        numero = (random.Next(0, 5));
                                    }
                                    cliente.tiempo_servicio_esperado =cliente.tiempo_servicio_esperado+ (numero/10);
                                    cliente.tiempo_servicio_final = cliente.tiempo_servicio_esperado;
                                    //MessageBox.Show("primavera-matutina-dia normal-deposito->" + cliente.tiempo_servicio_final);
                                    #endregion

                                }
                                if (numero >= 0.45 && numero <= 0.79)
                                {
                                    //retiro
                                    #region
                                    operacion = new operaciones();
                                    operacion.nombre = "retiro";
                                    operacion.tiempo_promedio_rango_inicial = 2.0;
                                    operacion.tiempo_promedio_rango_final = 3.3;
                                    cliente.operacion_deseada = operacion.nombre;
                                    //obteniendo el tiempo promedio que dura la operacion y asigando a cliente
                                    Thread.Sleep(5);
                                    numero = random.Next(2, 3);
                                    cliente.tiempo_servicio_esperado = numero;
                                    random = new Random();
                                    if (numero < 3)
                                    {cliente.tiempo_servicio_esperado =cliente.tiempo_servicio_esperado+ (numero/10);
                                        numero = (random.Next(0, 9)) / 10;
                                    }
                                    else
                                    {
                                        numero = (random.Next(0, 3)) / 10;
                                    }
                                    cliente.tiempo_servicio_esperado = cliente.tiempo_servicio_esperado + (numero / 10);
                                    cliente.tiempo_servicio_final = cliente.tiempo_servicio_esperado;
                                    //MessageBox.Show("primavera-matutina-dia normal-retiro->" + cliente.tiempo_servicio_final);
                                    #endregion
                                }
                                if (numero >= 0.80 && numero <= 1)
                                {
                                    //cambio moneda
                                    #region
                                    operacion = new operaciones();
                                    operacion.nombre = "cambio moneda";
                                    operacion.tiempo_promedio_rango_inicial = 2.0;
                                    operacion.tiempo_promedio_rango_final = 3.2;
                                    cliente.operacion_deseada = operacion.nombre;
                                    //obteniendo el tiempo promedio que dura la operacion y asigando a cliente
                                    Thread.Sleep(5);
                                    numero = random.Next(2, 3);
                                    cliente.tiempo_servicio_esperado = numero;
                                    random = new Random();
                                    if (numero < 3)
                                    {
                                        numero = (random.Next(0, 9)) / 10;
                                    }
                                    else
                                    {
                                        numero = (random.Next(0, 2)) / 10;
                                    }
                                    cliente.tiempo_servicio_esperado =cliente.tiempo_servicio_esperado+ (numero/10);
                                    cliente.tiempo_servicio_final = cliente.tiempo_servicio_esperado;
                                    //MessageBox.Show("primavera-matutina-dia normal-cambio moneda->" + cliente.tiempo_servicio_final);
                                    #endregion
                                }
                                #endregion
                            }
                            else if (diasCombo.Text == "pago")
                            {
                                //asigar operacion
                                #region
                                //double numero = random.NextDouble();
                                numero = Math.Round(numero, 2);
                                //MessageBox.Show(numero.ToString());
                                if (numero >= 0 && numero <= 0.35)
                                {
                                    //deposito
                                    #region
                                    operacion = new operaciones();
                                    operacion.nombre = "deposito";
                                    operacion.tiempo_promedio_rango_inicial = 3.0;
                                    operacion.tiempo_promedio_rango_final = 4.8;
                                    cliente.operacion_deseada = operacion.nombre;
                                    //obteniendo el tiempo promedio que dura la operacion y asigando a cliente
                                    Thread.Sleep(5);
                                    numero = random.Next(3, 4);
                                    cliente.tiempo_servicio_esperado = numero;
                                    random = new Random();
                                    if (numero < 4)
                                    {
                                        numero = (random.Next(0, 9)) / 10;
                                    }
                                    else
                                    {
                                        numero = (random.Next(0, 8)) / 10;
                                    }
                                    cliente.tiempo_servicio_esperado =cliente.tiempo_servicio_esperado+ (numero/10);
                                    cliente.tiempo_servicio_final = cliente.tiempo_servicio_esperado;
                                    //MessageBox.Show("primavera-matutina-dia pago-deposito->" + cliente.tiempo_servicio_final);
                                    #endregion

                                }
                                if (numero >= 0.36 && numero <= 0.81)
                                {
                                    #region
                                    //retiro
                                    operacion = new operaciones();
                                    operacion.nombre = "retiro";
                                    operacion.tiempo_promedio_rango_inicial = 2.0;
                                    operacion.tiempo_promedio_rango_final = 4.7;
                                    cliente.operacion_deseada = operacion.nombre;
                                    //obteniendo el tiempo promedio que dura la operacion y asigando a cliente
                                    Thread.Sleep(5);
                                    numero = random.Next(2, 4);
                                    cliente.tiempo_servicio_esperado = numero;
                                    random = new Random();
                                    if (numero < 4)
                                    {
                                        numero = (random.Next(0, 9)) / 10;
                                    }
                                    else
                                    {
                                        numero = (random.Next(0, 7)) / 10;
                                    }
                                    cliente.tiempo_servicio_esperado =cliente.tiempo_servicio_esperado+ (numero/10);
                                    cliente.tiempo_servicio_final = cliente.tiempo_servicio_esperado;
                                    //MessageBox.Show("primavera-matutina-dia pago-retiro->" + cliente.tiempo_servicio_final);
                                    #endregion
                                }
                                if (numero >= 0.82 && numero <= 1)
                                {
                                    #region
                                    //cambio moneda
                                    operacion = new operaciones();
                                    operacion.nombre = "cambio moneda";
                                    operacion.tiempo_promedio_rango_inicial = 2.0;
                                    operacion.tiempo_promedio_rango_final = 3.5;
                                    cliente.operacion_deseada = operacion.nombre;
                                    //obteniendo el tiempo promedio que dura la operacion y asigando a cliente
                                    numero = random.Next(2, 3);
                                    random = new Random();
                                    cliente.tiempo_servicio_esperado = numero;
                                    random = new Random();
                                    if (numero < 3)
                                    {
                                        numero = (random.Next(0, 9)) / 10;
                                    }
                                    else
                                    {
                                        numero = (random.Next(0, 5)) / 10;
                                    }
                                    cliente.tiempo_servicio_esperado =cliente.tiempo_servicio_esperado+ (numero/10);
                                    cliente.tiempo_servicio_final = cliente.tiempo_servicio_esperado;
                                    //MessageBox.Show("primavera-matutina-dia pago-cambio moneda->" + cliente.tiempo_servicio_final);
                                    #endregion
                                }
                                #endregion
                            }
                        }
                        else if (tanda.nombre == "vespertina")
                        {
                           
                            if (diasCombo.Text == "normal")
                            {
                                //asigar operacion
                                #region
                                //double numero = random.NextDouble();
                                numero = Math.Round(numero, 2);
                                //MessageBox.Show(numero.ToString());
                                if (numero >= 0 && numero <= 0.44)
                                {
                                    //deposito
                                    #region
                                    operacion = new operaciones();
                                    operacion.nombre = "deposito";
                                    operacion.tiempo_promedio_rango_inicial = 2.8;
                                    operacion.tiempo_promedio_rango_final = 3.5;
                                    cliente.operacion_deseada = operacion.nombre;
                                    //obteniendo el tiempo promedio que dura la operacion y asigando a cliente
                                    numero = random.Next(2, 3);
                                    cliente.tiempo_servicio_esperado = numero;
                                    random = new Random();
                                    if (numero < 3)
                                    {
                                        numero = (random.Next(0, 9)) / 10;
                                    }
                                    else
                                    {
                                        numero = (random.Next(0, 5)) / 10;
                                    }
                                    cliente.tiempo_servicio_esperado =cliente.tiempo_servicio_esperado+ (numero/10);
                                    cliente.tiempo_servicio_final = cliente.tiempo_servicio_esperado;
                                    //MessageBox.Show("primavera-vespertina-dia normal-deposito->" + cliente.tiempo_servicio_final);
                                    #endregion

                                }
                                if (numero >= 0.45 && numero <= 0.79)
                                {
                                    //retiro
                                    #region
                                    operacion = new operaciones();
                                    operacion.nombre = "retiro";
                                    operacion.tiempo_promedio_rango_inicial = 2.3;
                                    operacion.tiempo_promedio_rango_final = 3.1;
                                    cliente.operacion_deseada = operacion.nombre;
                                    //obteniendo el tiempo promedio que dura la operacion y asigando a cliente
                                    numero = random.Next(2, 3);
                                    cliente.tiempo_servicio_esperado = numero;
                                    random = new Random();
                                    if (numero < 3)
                                    {
                                        numero = (random.Next(0, 9)) / 10;
                                    }
                                    else
                                    {
                                        numero = (random.Next(0, 1)) / 10;
                                    }
                                    cliente.tiempo_servicio_esperado =cliente.tiempo_servicio_esperado+ (numero/10);
                                    cliente.tiempo_servicio_final = cliente.tiempo_servicio_esperado;
                                    //MessageBox.Show("primavera-vespertina-dia normal-retiro->" + cliente.tiempo_servicio_final);
                                    #endregion
                                }
                                if (numero >= 0.80 && numero <= 1)
                                {
                                    //cambio moneda
                                    #region
                                    operacion = new operaciones();
                                    operacion.nombre = "cambio moneda";
                                    operacion.tiempo_promedio_rango_inicial = 2.0;
                                    operacion.tiempo_promedio_rango_final = 3.1;
                                    cliente.operacion_deseada = operacion.nombre;
                                    //obteniendo el tiempo promedio que dura la operacion y asigando a cliente
                                    numero = random.Next(2, 3);
                                    cliente.tiempo_servicio_esperado = numero;
                                    random = new Random();
                                    if (numero < 3)
                                    {
                                        numero = (random.Next(0, 9)) / 10;
                                    }
                                    else
                                    {
                                        numero = (random.Next(0, 1)) / 10;
                                    }
                                    cliente.tiempo_servicio_esperado =cliente.tiempo_servicio_esperado+ (numero/10);
                                    cliente.tiempo_servicio_final = cliente.tiempo_servicio_esperado;
                                    //MessageBox.Show("primavera-vespertina-dia normal-cambio moneda->" + cliente.tiempo_servicio_final);
                                    #endregion
                                }
                                #endregion
                            }
                            else if (diasCombo.Text == "pago")
                            {
                                //asigar operacion
                                #region
                                //double numero = random.NextDouble();
                                numero = Math.Round(numero, 2);
                                //MessageBox.Show(numero.ToString());
                                if (numero >= 0 && numero <= 0.35)
                                {
                                    //deposito
                                    #region
                                    operacion = new operaciones();
                                    operacion.nombre = "deposito";
                                    operacion.tiempo_promedio_rango_inicial = 3.0;
                                    operacion.tiempo_promedio_rango_final = 5.6;
                                    cliente.operacion_deseada = operacion.nombre;
                                    //obteniendo el tiempo promedio que dura la operacion y asigando a cliente
                                    numero = random.Next(3, 5);
                                    cliente.tiempo_servicio_esperado = numero;
                                    random = new Random();
                                    if (numero < 5)
                                    {
                                        numero = (random.Next(0, 9)) / 10;
                                    }
                                    else
                                    {
                                        numero = (random.Next(0, 6)) / 10;
                                    }
                                    cliente.tiempo_servicio_esperado =cliente.tiempo_servicio_esperado+ (numero/10);
                                    cliente.tiempo_servicio_final = cliente.tiempo_servicio_esperado;
                                    //MessageBox.Show("primavera-vespertina-dia pago-deposito->" + cliente.tiempo_servicio_final);
                                    #endregion

                                }
                                if (numero >= 0.36 && numero <= 0.81)
                                {
                                    //retiro
                                    #region
                                    operacion = new operaciones();
                                    operacion.nombre = "retiro";
                                    operacion.tiempo_promedio_rango_inicial = 2.0;
                                    operacion.tiempo_promedio_rango_final = 4.8;
                                    cliente.operacion_deseada = operacion.nombre;
                                    //obteniendo el tiempo promedio que dura la operacion y asigando a cliente
                                    numero = random.Next(2, 4);
                                    cliente.tiempo_servicio_esperado = numero;
                                    random = new Random();
                                    if (numero < 4)
                                    {
                                        numero = (random.Next(0, 9)) / 10;
                                    }
                                    else
                                    {
                                        numero = (random.Next(0, 8)) / 10;
                                    }
                                    cliente.tiempo_servicio_esperado =cliente.tiempo_servicio_esperado+ (numero/10);
                                    cliente.tiempo_servicio_final = cliente.tiempo_servicio_esperado;
                                    //MessageBox.Show("primavera-vespertina-dia pago-retiro->" + numero);
                                    #endregion
                                }
                                if (numero >= 0.82 && numero <= 1)
                                {
                                    //cambio moneda
                                    #region
                                    operacion = new operaciones();
                                    operacion.nombre = "cambio moneda";
                                    operacion.tiempo_promedio_rango_inicial = 2.0;
                                    operacion.tiempo_promedio_rango_final = 4.7;
                                    cliente.operacion_deseada = operacion.nombre;
                                    //obteniendo el tiempo promedio que dura la operacion y asigando a cliente
                                    numero = random.Next(2, 4);
                                    cliente.tiempo_servicio_esperado = numero;
                                    random = new Random();
                                    if (numero < 4)
                                    {
                                        numero = (random.Next(0, 9)) / 10;
                                    }
                                    else
                                    {
                                        numero = (random.Next(0, 7)) / 10;
                                    }
                                    cliente.tiempo_servicio_esperado =cliente.tiempo_servicio_esperado+ (numero/10);
                                    cliente.tiempo_servicio_final = cliente.tiempo_servicio_esperado;
                                    //MessageBox.Show("primavera-vespertina-dia pago-cambio moneda->" + numero);
                                    #endregion
                                }
                                #endregion
                            }
                        }
                        #endregion
                    }
                    //para invierno
                    #region
                    /*
                    else if (temporada.nombre == "invierno")
                    {
                        #region
                        //temporada primavera
                        if (tanda.nombre == "matutina")
                        {
                            if (diasCombo.Text == "normal")
                            {
                                //asigar operacion
                                #region
                                //double numero = random.NextDouble();
                                numero = Math.Round(numero, 2);
                                //MessageBox.Show(numero.ToString());
                                if (numero >= 0 && numero <= 0.44)
                                {
                                    //deposito
                                    #region
                                    operacion = new operaciones();
                                    operacion.nombre = "deposito";
                                    operacion.tiempo_promedio_rango_inicial = 2.0;
                                    operacion.tiempo_promedio_rango_final = 3.0;
                                    cliente.operacion_deseada = operacion.nombre;
                                    //obteniendo el tiempo promedio que dura la operacion y asigando a cliente
                                    numero = random.Next(2, 3);
                                    cliente.tiempo_servicio_esperado = numero;
                                    if (numero < 3)
                                    {
                                        numero = (random.Next(0, 9)) / 10;
                                    }
                                    else
                                    {
                                        numero = (random.Next(0, 5)) / 10;
                                    }
                                    cliente.tiempo_servicio_esperado =cliente.tiempo_servicio_esperado+ (numero/10);
                                    cliente.tiempo_servicio_final = cliente.tiempo_servicio_esperado;
                                    //MessageBox.Show("invierno-matutina-dia normal-deposito->" + cliente.tiempo_servicio_final);
                                    #endregion

                                }
                                if (numero >= 0.45 && numero <= 0.79)
                                {
                                    //retiro
                                    #region
                                    operacion = new operaciones();
                                    operacion.nombre = "retiro";
                                    operacion.tiempo_promedio_rango_inicial = 2.0;
                                    operacion.tiempo_promedio_rango_final = 3.3;
                                    cliente.operacion_deseada = operacion.nombre;
                                    //obteniendo el tiempo promedio que dura la operacion y asigando a cliente
                                    numero = random.Next(2, 3);
                                    cliente.tiempo_servicio_esperado = numero;
                                    if (numero < 3)
                                    {
                                        numero = (random.Next(0, 9)) / 10;
                                    }
                                    else
                                    {
                                        numero = (random.Next(0, 3)) / 10;
                                    }
                                    cliente.tiempo_servicio_esperado =cliente.tiempo_servicio_esperado+ (numero/10);
                                    cliente.tiempo_servicio_final = cliente.tiempo_servicio_esperado;
                                    //MessageBox.Show("invierno-matutina-dia normal-retiro->" + cliente.tiempo_servicio_final);
                                    #endregion
                                }
                                if (numero >= 0.80 && numero <= 1)
                                {
                                    //cambio moneda
                                    #region
                                    operacion = new operaciones();
                                    operacion.nombre = "cambio moneda";
                                    operacion.tiempo_promedio_rango_inicial = 2.0;
                                    operacion.tiempo_promedio_rango_final = 3.5;
                                    cliente.operacion_deseada = operacion.nombre;
                                    //obteniendo el tiempo promedio que dura la operacion y asigando a cliente
                                    numero = random.Next(2, 3);
                                    cliente.tiempo_servicio_esperado = numero;
                                    if (numero < 3)
                                    {
                                        numero = (random.Next(0, 9)) / 10;
                                    }
                                    else
                                    {
                                        numero = (random.Next(0, 5)) / 10;
                                    }
                                    cliente.tiempo_servicio_esperado =cliente.tiempo_servicio_esperado+ (numero/10);
                                    cliente.tiempo_servicio_final = cliente.tiempo_servicio_esperado;
                                    //MessageBox.Show("invierno-matutina-dia normal-cambio moneda->" + cliente.tiempo_servicio_final);
                                    #endregion
                                }
                                #endregion
                            }
                            else if (diasCombo.Text == "pago")
                            {
                                //asigar operacion
                                #region
                                //double numero = random.NextDouble();
                                numero = Math.Round(numero, 2);
                                //MessageBox.Show(numero.ToString());
                                if (numero >= 0 && numero <= 0.35)
                                {
                                    //deposito
                                    #region
                                    operacion = new operaciones();
                                    operacion.nombre = "deposito";
                                    operacion.tiempo_promedio_rango_inicial = 2.0;
                                    operacion.tiempo_promedio_rango_final = 4.9;
                                    cliente.operacion_deseada = operacion.nombre;
                                    //obteniendo el tiempo promedio que dura la operacion y asigando a cliente
                                    numero = random.Next(2, 4);
                                    cliente.tiempo_servicio_esperado = numero;
                                    random = new Random();
                                    if (numero < 4)
                                    {
                                        numero = (random.Next(0, 9)) / 10;
                                    }
                                    else
                                    {
                                        numero = (random.Next(0, 9)) / 10;
                                    }
                                    cliente.tiempo_servicio_esperado =cliente.tiempo_servicio_esperado+ (numero/10);
                                    cliente.tiempo_servicio_final = cliente.tiempo_servicio_esperado;
                                   // MessageBox.Show("invierno-matutina-dia pago-deposito->" + cliente.tiempo_servicio_final);
                                    #endregion

                                }
                                if (numero >= 0.36 && numero <= 0.81)
                                {
                                    #region
                                    //retiro
                                    operacion = new operaciones();
                                    operacion.nombre = "retiro";
                                    operacion.tiempo_promedio_rango_inicial = 2.0;
                                    operacion.tiempo_promedio_rango_final = 5.0;
                                    cliente.operacion_deseada = operacion.nombre;
                                    //obteniendo el tiempo promedio que dura la operacion y asigando a cliente
                                    numero = random.Next(2, 4);
                                    cliente.tiempo_servicio_esperado = numero;
                                    if (numero < 5)
                                    {
                                        numero = (random.Next(0, 9)) / 10;
                                    }
                                    else
                                    {
                                        numero = (random.Next(0, 0)) / 10;
                                    }
                                    cliente.tiempo_servicio_esperado =cliente.tiempo_servicio_esperado+ (numero/10);
                                    cliente.tiempo_servicio_final = cliente.tiempo_servicio_esperado;
                                    //MessageBox.Show("invierno-matutina-dia pago-retiro->" + cliente.tiempo_servicio_final);
                                    #endregion
                                }
                                if (numero >= 0.82 && numero <= 1)
                                {
                                    #region
                                    //cambio moneda
                                    operacion = new operaciones();
                                    operacion.nombre = "cambio moneda";
                                    operacion.tiempo_promedio_rango_inicial = 3.0;
                                    operacion.tiempo_promedio_rango_final = 4.5;
                                    cliente.operacion_deseada = operacion.nombre;
                                    //obteniendo el tiempo promedio que dura la operacion y asigando a cliente
                                    numero = random.Next(3, 4);
                                    cliente.tiempo_servicio_esperado = numero;
                                    if (numero < 4)
                                    {
                                        numero = (random.Next(0, 9)) / 10;
                                    }
                                    else
                                    {
                                        numero = (random.Next(0, 5)) / 10;
                                    }
                                    cliente.tiempo_servicio_esperado =cliente.tiempo_servicio_esperado+ (numero/10);
                                    cliente.tiempo_servicio_final = cliente.tiempo_servicio_esperado;
                                    //MessageBox.Show("invierno-matutina-dia pago-cambio moneda->" + cliente.tiempo_servicio_final);
                                    #endregion
                                }
                                #endregion
                            }
                        }
                        else if (tanda.nombre == "vespertina")
                        {

                            if (diasCombo.Text == "normal")
                            {
                                //asigar operacion
                                #region
                                //double numero = random.NextDouble();
                                numero = Math.Round(numero, 2);
                                //MessageBox.Show(numero.ToString());
                                if (numero >= 0 && numero <= 0.44)
                                {
                                    //deposito
                                    #region
                                    operacion = new operaciones();
                                    operacion.nombre = "deposito";
                                    operacion.tiempo_promedio_rango_inicial = 2.0;
                                    operacion.tiempo_promedio_rango_final = 3.3;
                                    cliente.operacion_deseada = operacion.nombre;
                                    //obteniendo el tiempo promedio que dura la operacion y asigando a cliente
                                    numero = random.Next(2, 3);
                                    cliente.tiempo_servicio_esperado = numero;
                                    random = new Random();
                                    if (numero <3)
                                    {
                                        numero = (random.Next(0, 9))/10;
                                    }
                                    else
                                    {
                                        numero = (random.Next(0, 3)) / 10;
                                    }
                                    cliente.tiempo_servicio_esperado =cliente.tiempo_servicio_esperado+ (numero/10);
                                    cliente.tiempo_servicio_final = cliente.tiempo_servicio_esperado;
                                    //MessageBox.Show("invierno-vespertina-dia normal-deposito->" + cliente.tiempo_servicio_final);
                                    #endregion

                                }
                                if (numero >= 0.45 && numero <= 0.79)
                                {
                                    //retiro
                                    #region
                                    operacion = new operaciones();
                                    operacion.nombre = "retiro";
                                    operacion.tiempo_promedio_rango_inicial = 2.0;
                                    operacion.tiempo_promedio_rango_final = 3.4;
                                    cliente.operacion_deseada = operacion.nombre;
                                    //obteniendo el tiempo promedio que dura la operacion y asigando a cliente
                                    numero = random.Next(2, 3);
                                    cliente.tiempo_servicio_esperado = numero;
                                    if (numero == 2)
                                    {
                                        numero = (random.Next(0, 9)) / 10;
                                    }
                                    else
                                    {
                                        numero = (random.Next(0, 4)) / 10;
                                    }
                                    numero = (random.Next(0, 1)) / 10;
                                    cliente.tiempo_servicio_esperado =cliente.tiempo_servicio_esperado+ (numero/10);
                                    cliente.tiempo_servicio_final = cliente.tiempo_servicio_esperado;
                                    //MessageBox.Show("invierno-vespertina-dia normal-retiro->" + cliente.tiempo_servicio_final);
                                    #endregion
                                }
                                if (numero >= 0.80 && numero <= 1)
                                {
                                    //cambio moneda
                                    #region
                                    operacion = new operaciones();
                                    operacion.nombre = "cambio moneda";
                                    operacion.tiempo_promedio_rango_inicial = 2.0;
                                    operacion.tiempo_promedio_rango_final = 3.8;
                                    cliente.operacion_deseada = operacion.nombre;
                                    //obteniendo el tiempo promedio que dura la operacion y asigando a cliente
                                    numero = random.Next(2, 3);
                                    cliente.tiempo_servicio_esperado = numero;
                                    if (numero <3)
                                    {
                                        numero = (random.Next(0, 9)) / 10;
                                    }
                                    else
                                    {
                                        numero = (random.Next(0, 8)) / 10;
                                    }
                                    cliente.tiempo_servicio_esperado =cliente.tiempo_servicio_esperado+ (numero/10);
                                    cliente.tiempo_servicio_final = cliente.tiempo_servicio_esperado;
                                    //MessageBox.Show("invierno-vespertina-dia normal-cambio moneda->" + cliente.tiempo_servicio_final);
                                    #endregion
                                }
                                #endregion
                            }
                            else if (diasCombo.Text == "pago")
                            {
                                //asigar operacion
                                #region
                                //double numero = random.NextDouble();
                                numero = Math.Round(numero, 2);
                                //MessageBox.Show(numero.ToString());
                                if (numero >= 0 && numero <= 0.35)
                                {
                                    //deposito
                                    #region
                                    operacion = new operaciones();
                                    operacion.nombre = "deposito";
                                    operacion.tiempo_promedio_rango_inicial = 2.3;
                                    operacion.tiempo_promedio_rango_final = 4.5;
                                    cliente.operacion_deseada = operacion.nombre;
                                    //obteniendo el tiempo promedio que dura la operacion y asigando a cliente
                                    numero = random.Next(2, 4);
                                    cliente.tiempo_servicio_esperado = numero;
                                    random = new Random();
                                    if (numero <4)
                                    {
                                        numero = (random.Next(0, 9)) / 10;
                                    }
                                    else
                                    {
                                        numero = (random.Next(0, 5)) / 10;
                                    } 
                                    cliente.tiempo_servicio_esperado =cliente.tiempo_servicio_esperado+ (numero/10);
                                    cliente.tiempo_servicio_final = cliente.tiempo_servicio_esperado;
                                    //MessageBox.Show("invierno-vespertina-dia pago-deposito->" + cliente.tiempo_servicio_final);
                                    #endregion

                                }
                                if (numero >= 0.36 && numero <= 0.81)
                                {
                                    //retiro
                                    #region
                                    operacion = new operaciones();
                                    operacion.nombre = "retiro";
                                    operacion.tiempo_promedio_rango_inicial = 2.0;
                                    operacion.tiempo_promedio_rango_final = 4.9;
                                    cliente.operacion_deseada = operacion.nombre;
                                    //obteniendo el tiempo promedio que dura la operacion y asigando a cliente
                                    numero = random.Next(2, 4);
                                    cliente.tiempo_servicio_esperado = numero;
                                    if (numero < 4)
                                    {
                                        numero = (random.Next(1, 9)) / 10;
                                    }
                                    else
                                    {
                                        numero = (random.Next(0, 9)) / 10;
                                    }
                                    cliente.tiempo_servicio_esperado =cliente.tiempo_servicio_esperado+ (numero/10);
                                    cliente.tiempo_servicio_final = cliente.tiempo_servicio_esperado;
                                    //MessageBox.Show("invierno-vespertina-dia pago-retiro->" + numero);
                                    #endregion
                                }
                                if (numero >= 0.82 && numero <= 1)
                                {
                                    //cambio moneda
                                    #region
                                    operacion = new operaciones();
                                    operacion.nombre = "cambio moneda";
                                    operacion.tiempo_promedio_rango_inicial = 2.9;
                                    operacion.tiempo_promedio_rango_final = 3.7;
                                    cliente.operacion_deseada = operacion.nombre;
                                    //obteniendo el tiempo promedio que dura la operacion y asigando a cliente
                                    numero = random.Next(2, 3);
                                    cliente.tiempo_servicio_esperado = numero;
                                    if (numero < 3)
                                    {
                                        numero = (random.Next(0, 9)) / 10;
                                    }
                                    else
                                    {
                                        numero = (random.Next(0, 7)) / 10;
                                    }
                                    cliente.tiempo_servicio_esperado =cliente.tiempo_servicio_esperado+ (numero/10);
                                    cliente.tiempo_servicio_final = cliente.tiempo_servicio_esperado;
                                    //MessageBox.Show("invierno-vespertina-dia pago-cambio moneda->" + numero);
                                    #endregion
                                }
                                #endregion
                            }
                        }
                        #endregion
                    }
                    */
                    #endregion

                    cliente.atendido = false;
                    cliente.atendiendo = false;
                    cliente.abandono = false;
                    cliente.intentos = 0;
                    listaCliente.Add(cliente);
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("Error getClientes.:" + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        

        




        //get clientes by temporada para poder establecer numeros random de clientes en base a la temporada 
        #region
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
                
                //cargar corrida
                if (dataGridView1.Rows.Count > 0)
                {
                    dataGridView1.Rows.Clear();
                }
                //listaCliente = listaCliente.OrderByDescending(x=> x.problemas).ToList();
                listaCliente.ForEach(x =>
                {
                    dataGridView1.Rows.Add(x.codigo, x.operacion_deseada, x.tanda, x.tiempo_servicio_esperado, x.problemas.Count.ToString(), x.tiempo_servicio_final,x.abandono,x.tipo_cajero);
                });

                //cargar todos los problemas que se le presentaron al cliente
                if (dataGridView2.Rows.Count > 0)
                {
                    dataGridView2.Rows.Clear();
                }
                
                listaClienteProblemasLog = listaClienteProblemasLog.Where(x => x.problema_encontrado == true).ToList();
                listaClienteProblemasLog = listaClienteProblemasLog.Distinct().ToList();
                listaClienteProblemasLog.ForEach(x =>
                {
                    dataGridView2.Rows.Add(x.codigocliente,x.operacion,x.fase,x.tiempo_antes,x.tiempo_despues,x.nombreProblema,x.respuesta);
                });

                MessageBox.Show("Finalizo proceso", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
            catch (Exception ex)
            {

                MessageBox.Show("Error loadClientes.:" + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion


        //reporte por cajeros;
        #region
        public void imprimir1()
        {
            //try
            //{
            //    #region
            //    if (listaCliente == null)
            //    {
            //        MessageBox.Show("No se encontraron datos, primero debe simular", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    }

            //    //datos generales
            //    String reporte = "SimulacionCajeroBanco.reportes.reporte_por_cajero.rdlc";
            //    List<ReportDataSource> listaReportDataSource = new List<ReportDataSource>();

            //    //reporte estudiante
            //    reporte_por_cajero reporte_por_cajero = new reporte_por_cajero();
            //    List<reporte_por_cajero> ListaReporteEstudiante = new List<reporte_por_cajero>();
            //    int cont = 0;
            //    listaReportePorCajero=new List<reporte_por_cajero>();
            //    listaCajero.ForEach(cajeroActual =>
            //    {
            //        Thread.Sleep(5);
            //        //iniciando los valores que cambiaran desde que cambie el cajero
            //        reporte_por_cajero = new reporte_por_cajero();
            //        reporte_por_cajero.cantidad_clientes = 0;
            //        reporte_por_cajero.cantidad_clientes_abandonaron = 0;
            //        reporte_por_cajero.cliente_promedio_tiempo_esperado = 0;
            //        reporte_por_cajero.cliente_promedio_tiempo_final = 0;
            //        Thread.Sleep(5);
            //        reporte_por_cajero.cajero = cajeroActual.codigo;
            //        reporte_por_cajero.operacion = cajero.operacion;
                    
            //        listaCliente.ForEach(clienteActual =>
            //        {
            //            if (clienteActual.operacion_deseada == cajeroActual.operacion && clienteActual.tipo_cajero.Contains(cajeroActual.codigo.ToString()))
            //            {
            //                //MessageBox.Show("cliente->" + clienteActual.codigo + "-tiene cajero->" + cajeroActual.codigo + "-" + cajeroActual.operacion);
            //                //este cajero fue el que el cliente selecciono
            //                Thread.Sleep(5);
            //                reporte_por_cajero.cantidad_clientes += 1;
            //                if (clienteActual.abandono == true)
            //                {
            //                    reporte_por_cajero.cantidad_clientes_abandonaron += 1;
            //                }
            //                Thread.Sleep(5);
            //                reporte_por_cajero.cliente_promedio_tiempo_esperado +=clienteActual.tiempo_servicio_esperado;
            //                reporte_por_cajero.cliente_promedio_tiempo_final += clienteActual.tiempo_servicio_final;
            //            }
            //        });
            //        Thread.Sleep(5);
            //        listaReportePorCajero.Add(reporte_por_cajero);
            //    });

            //    ReportDataSource reporteF = new ReportDataSource("reporte_por_cajero", listaReportePorCajero);
            //    listaReportDataSource.Add(reporteF);

            //    List<ReportParameter> ListaReportParameter = new List<ReportParameter>();

            //    VisorReporteComun ventana = new VisorReporteComun(reporte, listaReportDataSource, ListaReportParameter, false);
            //    ventana.ShowDialog();

            //    #endregion
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Error imprimir1: " + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
        }
        #endregion


        //imprimir reporte grafico clientes
        #region
        public void imprimir2()
        {
            try
            {
                #region
                if (listaCliente == null)
                {
                    MessageBox.Show("No se encontraron datos, primero debe simular", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //datos generales
                String reporte = "SimulacionCajeroBanco.reportes.reporte_grafico_cliente.rdlc";
                List<ReportDataSource> listaReportDataSource = new List<ReportDataSource>();

                listaReporteGraficoCliente=new List<reporte_grafico_cliente>();
                //reporte estudiante
                int cont = 0;
                listaCliente.ForEach(clienteActual =>
                {
                    //asignando valores del cliente
                    Thread.Sleep(5);
                    reporteGraficoCliente = new reporte_grafico_cliente();
                    reporteGraficoCliente.temporada = clienteActual.temporada;
                    reporteGraficoCliente.abandono = clienteActual.abandono;
                    reporteGraficoCliente.atendido = clienteActual.atendiendo;
                    reporteGraficoCliente.cantidad_problemas = clienteActual.problemas.Count;
                    reporteGraficoCliente.codigo = clienteActual.codigo;
                    reporteGraficoCliente.atendiendo = clienteActual.atendiendo;
                    reporteGraficoCliente.intentos = clienteActual.intentos;
                    reporteGraficoCliente.operacion_completada = clienteActual.operacion_completada;
                    reporteGraficoCliente.operacion_deseada = clienteActual.operacion_deseada;
                    reporteGraficoCliente.tanda = clienteActual.tanda;
                    reporteGraficoCliente.tiempo_servicio_esperado = clienteActual.tiempo_servicio_esperado;
                    reporteGraficoCliente.tiempo_servicio_final = clienteActual.tiempo_servicio_final;
                    reporteGraficoCliente.tipo_cajero = clienteActual.tipo_cajero;
                    Thread.Sleep(5);
                    listaReporteGraficoCliente.Add(reporteGraficoCliente);
                });

                ReportDataSource reporteGrafico = new ReportDataSource("reporte_grafico_cliente", listaReporteGraficoCliente);
                listaReportDataSource.Add(reporteGrafico);

                ReportDataSource reporteProblemas = new ReportDataSource("reporte_problemas_log", listaClienteProblemasLog);
                listaReportDataSource.Add(reporteProblemas);


                List<ReportParameter> ListaReportParameter = new List<ReportParameter>();

                VisorReporteComun ventana = new VisorReporteComun(reporte, listaReportDataSource, ListaReportParameter, false);
                ventana.ShowDialog();

                #endregion
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
                //imprimir1();
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

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (cantidadTipoCajaText.Text == "")
                {
                    MessageBox.Show("Falta la cantidad de ese tipo de caja", "", MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    cantidadTipoCajaText.Focus();
                    cantidadTipoCajaText.SelectAll();
                }

                int cantidadTipoCajero = Convert.ToInt16(cantidadTipoCajaText.Text);
                if (listaCajero == null)
                {
                    listaCajero = new List<cajero>();
                    contadorCajero = 0;
                }
                if (listaCajeroDeposito == null)
                {
                    listaCajeroDeposito = new List<cajero>();
                }
                if (listaCajeroRetiro == null)
                {
                    listaCajeroRetiro = new List<cajero>();
                }
                if (listaCajeroCambioMoneda == null)
                {
                    listaCajeroCambioMoneda = new List<cajero>();
                }

                //creando un cajero nuevo y asignando el tipo de caja que manejara
                for (int f = 0; f < cantidadTipoCajero; f++)
                {
                    cajero = new cajero();
                    contadorCajero++;
                    cajero.codigo = contadorCajero;
                    cajero.operacion = tipoCajaCombo.Text;
                    cajero.clientesAtendidos = 0;
                    cajero.tiempoPromedioEnServcio = 0;
                    cajero.clientesCola = 0;
                    cajero.cantidad_cajeros_esta_operacion=0;
                    //agregando el cajero a la lista de cajero
                    listaCajero.Add(cajero);

                    //guardando el cajero en cada lista separada
                    if (cajero.operacion == "deposito")
                    {
                        listaCajeroDeposito.Add(cajero);
                    }
                    if (cajero.operacion == "retiro")
                    {
                        listaCajeroRetiro.Add(cajero);
                    }
                    if (cajero.operacion == "cambio moneda")
                    {
                        listaCajeroCambioMoneda.Add(cajero);
                    }
                    //MessageBox.Show("cod-> " + cajero.codigo.ToString() + " cli antendidos-> " + cajero.clientesAtendidos + " clientes cola-> " + cajero.clientesCola + " tiempo serv-> " + cajero.tiempoPromedioEnServcio + " tipo caja-> " + cajero.tipo_caja);
                    
                    //agregandolo para hacer el reporte por cajero
                    reportePorCajero=new reporte_por_cajero();
                    reportePorCajero.cajero = cajero.codigo;
                    reportePorCajero.operacion = cajero.operacion;
                    reportePorCajero.cantidad_clientes = 0;
                    reportePorCajero.cantidad_clientes_abandonaron = 0;
                    reportePorCajero.cliente_promedio_tiempo_final = 0;
                    reportePorCajero.cliente_promedio_tiempo_esperado = 0;
                    if (listaReportePorCajero == null)
                    {
                        listaReportePorCajero=new List<reporte_por_cajero>();
                    }
                    listaReportePorCajero.Add(reportePorCajero);
                }

                MessageBox.Show("Cajero agregado","", MessageBoxButtons.OK, MessageBoxIcon.Information);
                

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :" + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                listaCajero = new List<cajero>();
                contadorCajero = 0;
                MessageBox.Show("Eliminado .:", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :" + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tandaCombo_TextChanged(object sender, EventArgs e)
        {
            if (tanda == null)
            {
                tanda=new tanda();
            }
            tanda.nombre = tandaCombo.Text;
            
        }

    }
}

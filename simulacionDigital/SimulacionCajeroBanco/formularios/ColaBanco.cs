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


        //listas
        private List<cajero> listaCajero;
        private List<cliente> listaCliente;
        private List<temporada> listaTemporada;
        private List<tanda> listaTanda; 


        //listas problemas
        private List<problema> listaProblemaDeposito;
        private List<problema> listaProblemaRetiro;
        private List<problema> listaProblemaCambio;
       

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
                loadTemporada();
                loadTanda();
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

                //instancia problemas
                problema=new problema();



                //problemas deposito
                problema.nombre = "fallo sistema";
                listaProblemaDeposito.Add(problema);

                problema = new problema();
                problema.nombre = "fallo electricidad";
                listaProblemaDeposito.Add(problema);
                
                problema = new problema();
                problema.nombre = "numero cuenta incorrecto";
                listaProblemaDeposito.Add(problema);
                
                problema = new problema();
                problema.nombre = "dinero insuficiente";
                listaProblemaDeposito.Add(problema);
                
                problema = new problema();
                problema.nombre = "falta cedula";
                listaProblemaDeposito.Add(problema);


                //problemas retiro
                problema.nombre = "fallo sistema";
                listaProblemaRetiro.Add(problema);

                problema = new problema();
                problema.nombre = "fallo electricidad";
                listaProblemaRetiro.Add(problema);

                problema = new problema();
                problema.nombre = "numero cuenta incorrecto";
                listaProblemaRetiro.Add(problema);

                problema = new problema();
                problema.nombre = "dinero insuficiente";
                listaProblemaRetiro.Add(problema);

                problema = new problema();
                problema.nombre = "falta cedula";
                listaProblemaRetiro.Add(problema);


                //problemas cambio moneda
                problema.nombre = "fallo sistema";
                listaProblemaCambio.Add(problema);

                problema = new problema();
                problema.nombre = "fallo electricidad";
                listaProblemaCambio.Add(problema);

                //problema = new problema();
                //problema.nombre = "falta cedula";
                //listaProblemaCambio.Add(problema);

                problema = new problema();
                problema.nombre = "dinero insuficiente";
                listaProblemaCambio.Add(problema);

                problema = new problema();
                problema.nombre = "moneda no es aceptada";
                listaProblemaCambio.Add(problema);


               
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

                //cantidad de cajeros
                cantidadCajeros = Convert.ToInt16(cantidadClienteText.Text.Trim());
                //cantidad de clientes
                cantidadClientes = Convert.ToInt16(cantidadCajeroText.Text.Trim());

                loadTemporada();
                loadTanda();



            }
            catch (Exception ex)
            {

                MessageBox.Show("Error getAction.:" + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion




        //load clientes
        #region
        public void loadClientes()
        {
            try
            {

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

    }
}

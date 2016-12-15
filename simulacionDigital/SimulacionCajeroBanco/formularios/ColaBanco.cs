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


        //listas
        private List<cajero> listaCajero;
        private List<cliente> listaCliente;
       

        //variables para datos
        private double tiempoLLegadaAcumulativo = 0;
        private double tiempoServicioAcumulativo = 0;
        private double clienteDeposito = 0;
        private double clienteRetiro = 0;
        private double clienteCambioMoneda = 0;
        


        public ColaBanco()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        public void getProblemas()
        {
            try
            {

            }
            catch (Exception ex)
            {

                MessageBox.Show("Error getProblemas.:" + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void getAction()
        {
            try
            {

            }
            catch (Exception ex)
            {

                MessageBox.Show("Error getAction.:" + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

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
                MessageBox.Show("Error imprimir: " + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

    }
}

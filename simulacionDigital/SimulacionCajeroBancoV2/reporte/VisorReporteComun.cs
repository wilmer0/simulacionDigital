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

namespace SimulacionCajeroBancoV2.reporte
{
    public partial class VisorReporteComun : Form
    {

        public VisorReporteComun(String reporte, List<ReportDataSource> lista, List<ReportParameter> ListaReportParameter)
        {
            InitializeComponent();
            GetLoad(reporte, lista, ListaReportParameter);
        }
        private void GetLoad(String reporte, List<ReportDataSource> lista, List<ReportParameter> ListaReportParameter)
        {
            Reporte.LocalReport.ReportEmbeddedResource = reporte;
            lista.ForEach(x =>
            {
                Reporte.LocalReport.DataSources.Add(x);
            });
            if (ListaReportParameter != null)
            {
                Reporte.LocalReport.SetParameters(ListaReportParameter);
            }
        }
        private void VisorReporteComun_Load(object sender, EventArgs e)
        {
            Reporte.SetDisplayMode(DisplayMode.PrintLayout);
            this.Reporte.RefreshReport();
            this.Reporte.RefreshReport();
        }

        private void Reporte_Load(object sender, EventArgs e)
        {

        }
    }
}

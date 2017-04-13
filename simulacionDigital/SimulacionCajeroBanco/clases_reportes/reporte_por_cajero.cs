using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulacionCajeroBanco.clases_reportes
{
    public class reporte_por_cajero
    {


        //private string reporte;
        //private List<Microsoft.Reporting.WinForms.ReportDataSource> listaReportDataSource;

        //public reporte_por_cajero()
        //{
        //}

        //public reporte_por_cajero(string reporte, List<Microsoft.Reporting.WinForms.ReportDataSource> listaReportDataSource)
        //{
        //    // TODO: Complete member initialization
        //    this.reporte = reporte;
        //    this.listaReportDataSource = listaReportDataSource;
        //}




        public int cajero { get; set; }
        public string operacion { get; set; }
        public int cantidad_clientes { get; set; }
        public double cliente_promedio_tiempo_esperado { get; set; }
        public double cliente_promedio_tiempo_final { get; set; }
        public int cantidad_clientes_abandonaron { get; set; }
        



      

    }
}

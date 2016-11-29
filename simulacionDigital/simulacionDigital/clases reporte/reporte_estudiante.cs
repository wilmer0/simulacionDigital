using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using simulacionDigital.clases;

namespace simulacionDigital.clases_reporte
{
    public class reporte_estudiante
    {
       
        private string reporte;
        private List<Microsoft.Reporting.WinForms.ReportDataSource> listaReportDataSource;

        public reporte_estudiante()
        {
        }

        

        public reporte_estudiante(string reporte, List<Microsoft.Reporting.WinForms.ReportDataSource> listaReportDataSource)
        {
            // TODO: Complete member initialization
            this.reporte = reporte;
            this.listaReportDataSource = listaReportDataSource;
        }

        //datos del estudiante
        public int estudiante { get; set; }
        public double indice { get; set; }
        public string carrera { get; set; }
        public string tipoCarrera { get; set; }
        public bool beca { get; set; }
        public double tiempoServicioBase { get; set; }
        public double tiempoServicioFinal { get; set; }
        public double jugadas { get; set; }


    }
}

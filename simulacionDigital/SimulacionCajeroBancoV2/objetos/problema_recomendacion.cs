using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulacionCajeroBancoV2.objetos
{
    public class problema_recomendacion
    {
        public int id_problema { get; set; }
        public string recomendacion { get; set; }


        utilidades utilidades=new utilidades();
        public string getRecomendacionByNombreProblema(string problema)
        {
            string sql = "select recomendacion from problema_recomendacion where problema='"+problema+"';";
            DataSet ds = utilidades.ejecutarcomando_mysql(sql);
            return ds.Tables[0].Rows[0][0].ToString();
        }
    }
}

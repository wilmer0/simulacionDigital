using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SimulacionCajeroBancoV2.objetos;

namespace SimulacionCajeroBancoV2.views
{
    public partial class visor_problemas : Form
    {
        //variables
        private List<problemasLogs> lista;
        private Int64 idCliente;

        public visor_problemas(Int64 idCliente, List<problemasLogs> lista)
        {
            InitializeComponent();
            this.lista = lista;
            this.idCliente = idCliente;
            loadVentana();

        }

        public void loadVentana()
        {
            dataGridView2.Rows.Clear();
            lista = lista.FindAll(x => x.idcliente == this.idCliente);

            foreach (var x in lista)
            {
                dataGridView2.Rows.Add(x.idcliente, x.operacion, x.fase, x.tiempo_antes, x.tiempo_despues, x.nombreProblema, x.respuesta);
            }
        }

    }
}

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
        public visor_problemas(Int64 idCliente, List<problemasLogs> lista)
        {
            InitializeComponent();
        }
    }
}

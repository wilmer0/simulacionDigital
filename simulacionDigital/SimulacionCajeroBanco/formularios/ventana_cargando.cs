using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimulacionCajeroBanco.formularios
{
    public partial class ventana_cargando : Form
    {

        //variables

        Timer timer = new Timer();




        public ventana_cargando()
        {
            InitializeComponent();
            timer.Start();
            timer.Interval = 1000;
            timer.Tick += (sender, args) => DoWork();
        }

        public void DoWork()
        {
            if (timer.Interval == 4000)
            {
                timer.Stop();
                ColaBanco ventana = new ColaBanco();
                ventana.Show();
                this.Hide();
            }
            else
            {
                timer.Interval+=1000;
            }
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}

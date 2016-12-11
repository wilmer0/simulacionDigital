using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using simulacionDigital.problemas;

namespace simulacionDigital
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            ventana_becas_universitarias ventana=new ventana_becas_universitarias();
            ventana.Owner = this;
            ventana.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ingenieriaSoftware.Form1 ventana1 = new ingenieriaSoftware.Form1();
            ventana1.ShowDialog();
            ingenieriaSoftware.Form2 ventana2 = new ingenieriaSoftware.Form2();
            ventana2.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

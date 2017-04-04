using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SimulacionCajeroBancoV2.modelos;
using SimulacionCajeroBancoV2.objetos;

namespace SimulacionCajeroBancoV2
{
    public partial class Form1 : Form
    {


        //modelos
        modeloTemporada modeloTemporada=new modeloTemporada();

        //objetos
        private temporada temporada;
        private cliente cliente;
        private tanda tanda;
        private problema problema;

        //listas
        private List<cliente> listaCliente;
        private List<temporada> listaTemporada;
        private List<problema> listaProblemaDeposito;
        private List<problema> listaProblemaRetiro;
        private List<problema> listaProblemaCambio;


        //variables
        public string temporadaSeleccionada="";
        public string tandaSeleccionada="";


        public Form1()
        {
            InitializeComponent();
            loadVentana();
        }

        public void loadVentana()
        {
            try
            {
                getTemporadas();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loadVentana.:" + ex.ToString(), "", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        public void getTemporadas()
        {
            try
            {
                listaTemporada = new List<temporada>();
                listaTemporada = modeloTemporada.getListaTemporada();

                comboBoxTemporada.DataSource = listaTemporada;
                comboBoxTemporada.DisplayMember = "nombre";
                comboBoxTemporada.ValueMember = "id";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getTemporadas.: " + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }



    }
}

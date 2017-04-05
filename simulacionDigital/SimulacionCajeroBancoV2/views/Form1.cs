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
        modeloTanda modeloTanda=new modeloTanda();
        modeloCajero modeloCajero=new modeloCajero();

        //objetos
        private temporada temporada;
        private cliente cliente;
        private tanda tanda;
        private problema problema;
        private cajero cajero;

        //listas
        private List<tanda> listaTanda; 
        private List<cliente> listaCliente;
        private List<temporada> listaTemporada;
        private List<cajero> listaCajero; 

        //lista de problema
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
                listaCajero=new List<cajero>();
                getTemporadas();
                getTandas();
                getCajeros();

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

        public void getTandas()
        {
            try
            {
                listaTanda = new List<tanda>();
                listaTanda = modeloTanda.getListaTanda(Convert.ToInt16(comboBoxTemporada.SelectedValue.ToString()));

                comboBoxTanda.DataSource = listaTanda;
                comboBoxTanda.DisplayMember = "nombre";
                comboBoxTanda.ValueMember = "id";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getTandas.: " + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void getCajeros()
        {
            try
            {
                listaCajero = new List<cajero>();
                listaCajero = modeloCajero.getListaCajero();

                comboBoxTipoCaja.DataSource = listaCajero;
                comboBoxTipoCaja.DisplayMember = "nombre";
                comboBoxTipoCaja.ValueMember = "id";

                getCantidadCajeros();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getCajeros.: " + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
           getCajeros();
           MessageBox.Show("Se ha blanqueado la lista de cajeros","",MessageBoxButtons.OK,MessageBoxIcon.Warning);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                cajero= new cajero();
                cajero.id = (listaCajero.Count) + 1;
                cajero.nombre = "Cajero "+cajero.id;
                listaCajero.Add(cajero);
                loadListaCajeros();
                

            }
            catch (Exception ex)
            {

                MessageBox.Show("Error agregando el cajero.:" + ex.ToString(), "", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        public void getCantidadCajeros()
        {
            try
            {
                cantidadCajerosLabel.Text = "cajeros: " + listaCajero.Count;
            }
            catch (Exception)
            {
                
            }
        }

        public void loadListaCajeros()
        {
            comboBoxTipoCaja.DataSource = listaCajero.ToList();
            comboBoxTipoCaja.DisplayMember = "nombre";
            comboBoxTipoCaja.ValueMember = "id";
            comboBoxTipoCaja.SelectedIndex = 0;
            getCantidadCajeros();
        }

    }
}

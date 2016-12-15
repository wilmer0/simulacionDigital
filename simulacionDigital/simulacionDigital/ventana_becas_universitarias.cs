using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using simulacionDigital.clases;
using simulacionDigital.clases_reporte;
using _7ADMFIC_1._0.VentanasComunes;

namespace simulacionDigital.problemas
{
    public partial class ventana_becas_universitarias : Form
    {


        //objetos
        private area area;
        private carrera carrera;
        private estudiante estudiante;
        private evento_retraso evento_retraso;
        private premios premios;
        private panel panel;

        //variables
        string []penelMatriz=new string[16];
        Random random;//para randoms


        //listas
        private List<carrera> listaCarreras;
        private List<carrera> listaCarreraIngenieria; 
        private List<carrera> listaCarreraSalud;
        private List<carrera> listaCarreraOtras;
        private List<evento_retraso> listaEventosRestraso;
        private List<premios> listaPremios;
        private List<estudiante> listaEstudiante;
        private List<panel> listaPanel; 

        //variables para datos
        private double tiempoLLegadaEstudianteAcumulativo=0;
        private double tiempoServivioEstudianteAcumulativo=0;
        private double estudiantesIngenieria = 0;
        private double estudiantesSalud = 0;
        private double estudiantesOtros = 0;
        private double estudiantesTotal = 0;
        private double estudianteConTodosLosEventosFallas = 0;

        /* 
           Datos que pide el problema
           Tiempo promedio entre llegadas.
           Número promedio de estudiantes en cada línea de espera.
           Tiempo promedio que ocupa un estudiante en la línea de espera.
           Tiempo promedio de servicio.
           Probabilidad de atender un estudiante sin tener que esperar por servicio.
           Probabilidad de que un estudiante termine en menos de 2 mins., entre 3 y 4, entre 5 y 6, o más.
           Probabilidad de que existan N estudiantes (entre 1 y 15) en la cola.
           Probabilidad de que se presente cada uno de los eventos que retrasan al estudiante.
         */




        public ventana_becas_universitarias()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            getAction();
        }

        public void getPremios()
        {
            try
            {
                //Costo Libros 
                //Costo Alojamiento 
                //Bono
                //Extra 
                //Costo Pasaje

                listaPremios=new List<premios>(); //nueva instancia de la lista
                premios =new premios();
                premios.nombre = "costo libros";
                listaPremios.Add(premios);

                premios = new premios();
                premios.nombre = "costo alojamiento";
                listaPremios.Add(premios);

                premios = new premios();
                premios.nombre = "bono";
                listaPremios.Add(premios);

                premios = new premios();
                premios.nombre = "extra";
                listaPremios.Add(premios);

                premios = new premios();
                premios.nombre = "costo pasaje";
                listaPremios.Add(premios);

            }
            catch (Exception ex)
            {

                MessageBox.Show("Error getPremios.:" + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void getEventosRetraso()
        {
            try
            {
                if(listaEventosRestraso==null)
                    listaEventosRestraso=new List<evento_retraso>();
                
                
                evento_retraso=new evento_retraso();
                evento_retraso.nombre = "Falla electricidad";
                evento_retraso.probabilidad_ocurrencia = 25.80;
                evento_retraso.probailidad_incremento_tiempo_servicio = 18.50;
                listaEventosRestraso.Add(evento_retraso);

                evento_retraso = new evento_retraso();
                evento_retraso.nombre = "salud empleado";
                evento_retraso.probabilidad_ocurrencia = 8.15;
                evento_retraso.probailidad_incremento_tiempo_servicio = 10.60;
                listaEventosRestraso.Add(evento_retraso);

                evento_retraso = new evento_retraso();
                evento_retraso.nombre = "salud estudiante";
                evento_retraso.probabilidad_ocurrencia = 7.25;
                evento_retraso.probailidad_incremento_tiempo_servicio = 15.20;
                listaEventosRestraso.Add(evento_retraso);

                evento_retraso = new evento_retraso();
                evento_retraso.nombre = "Falla equipo computadora";
                evento_retraso.probabilidad_ocurrencia = 8.5;
                evento_retraso.probailidad_incremento_tiempo_servicio = 28.30;
                listaEventosRestraso.Add(evento_retraso);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getEventosRetraso.:" + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void getCarreras()
        {
            try
            {

                //se crean las carreras disponibles una lista por cada tipo 
                //de carrera y una lista que guarda todas las carreras

                listaCarreras = new List<carrera>();
                listaCarreraIngenieria=new List<carrera>();
                listaCarreraSalud = new List<carrera>();
                listaCarreraOtras = new List<carrera>();
               

                //ingenierias
                carrera = new carrera();
                carrera.nombreCarrera = "informatica";
                carrera.tipo_carrera = "ingenieria";
                carrera.probabilidad = 32;
                carrera.tiempo_llegada = 3.8;
                listaCarreraIngenieria.Add(carrera);
                listaCarreras.Add(carrera);

                carrera = new carrera();
                carrera.nombreCarrera = "electronica";
                carrera.tipo_carrera = "ingenieria";
                carrera.probabilidad = 32;
                carrera.tiempo_llegada = 3.8;
                listaCarreraIngenieria.Add(carrera);
                listaCarreras.Add(carrera);

                carrera = new carrera();
                carrera.nombreCarrera = "arquitectura";
                carrera.tipo_carrera = "ingenieria";
                carrera.probabilidad = 32;
                carrera.tiempo_llegada = 3.8;
                listaCarreraIngenieria.Add(carrera);
                listaCarreras.Add(carrera);
                
                //ciencia salud
                carrera = new carrera();
                carrera.nombreCarrera = "medicina";
                carrera.tipo_carrera = "salud";
                carrera.probabilidad = 32;
                carrera.tiempo_llegada = 3.8;
                listaCarreraSalud.Add(carrera);
                listaCarreras.Add(carrera);

                carrera = new carrera();
                carrera.nombreCarrera = "veterinaria";
                carrera.tipo_carrera = "salud";
                carrera.probabilidad = 32;
                carrera.tiempo_llegada = 3.8;
                listaCarreraSalud.Add(carrera);
                listaCarreras.Add(carrera);

                carrera = new carrera();
                carrera.nombreCarrera = "bioanalis";
                carrera.tipo_carrera = "salud";
                carrera.probabilidad = 32;
                carrera.tiempo_llegada = 3.8;
                listaCarreraSalud.Add(carrera);
                listaCarreras.Add(carrera);
                

                //otras
                carrera = new carrera();
                carrera.nombreCarrera = "administracion de empresas";
                carrera.tipo_carrera = "otros";
                carrera.probabilidad = 32;
                carrera.tiempo_llegada = 3.8;
                listaCarreraOtras.Add(carrera);
                listaCarreras.Add(carrera);

                carrera = new carrera();
                carrera.nombreCarrera = "contaduria";
                carrera.tipo_carrera = "otros";
                carrera.probabilidad = 32;
                carrera.tiempo_llegada = 3.8;
                listaCarreraOtras.Add(carrera);
                listaCarreras.Add(carrera);

                carrera = new carrera();
                carrera.nombreCarrera = "derecho";
                carrera.tipo_carrera = "otros";
                carrera.probabilidad = 32;
                carrera.tiempo_llegada = 3.8;
                listaCarreraOtras.Add(carrera);
                listaCarreras.Add(carrera);
                
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getCarreras.:" + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void getAction()
        {
            try
            {

                getCarreras();
                getEventosRetraso();
                getPremios();
                listaEstudiante=new List<estudiante>();
                for (int est = 0; est < 500; est++)
                {
                    random = new Random();//instancia del objeto random
                    estudiante = new estudiante(); //nuevo estudiante

                    //inicio obteniendo la carrera por estudiante
                    #region
                    estudiante.indiceCarrera = random.Next(1, 9);
                    estudiante.tieneBeca = false; //iniciando no tiene beca
                    estudiante.cantidadPremios = 0;//iniciando tiene 0 premios
                    estudiante.cantidadJugadasParaElegirCarra = 0;
                    estudiante.eligioCarrera = false;
                    estudiante.premios=new List<premios>();
                    estudiante.eventosRestraso=new List<evento_retraso>();
                    estudiante.cantidadEventosRestraso = 0;
                    estudiante.cambiosPremiosExtraPorNuevaoportunidad = 0;



                    carrera = new carrera();
                    int randomCarrera = 0;
                    randomCarrera = random.Next(1, 100);
                    if (randomCarrera >= 1 && randomCarrera <= 29)//asignando la carrera aleatoriamente
                    {
                        //otros
                        carrera = listaCarreraOtras[random.Next(1, 3)];
                    }
                    else if (randomCarrera >= 30 && randomCarrera <= 61)
                    {
                        //ing
                        carrera = listaCarreraIngenieria[random.Next(1, 3)];
                    }
                    else if (randomCarrera >= 62 && randomCarrera <= 100)
                    {
                        //med
                        carrera = listaCarreraSalud[random.Next(1, 3)];
                    }
                    estudiante.Nombrecarrera = carrera.nombreCarrera; //tomand el nmbre de la carrera que genero
                    estudiante.tipoCarrera = carrera.tipo_carrera; // asigando el tipo de carrera al estudiante
                    Thread.Sleep(50);//para que duerma x segundos lo hace tan rapido que tiene que pausar para no empalmar
                    #endregion
                    //fin obteniendo la carrera por estudiante

                    //inicio obetiendo tiempo de servicio del estudiante
                    #region
                    if (estudiante.tipoCarrera == "ingenieria")
                    {
                        //ing
                        estudiante.tiempoServicioMiu = -4.5;
                        //calcular el tiempo servicio
                        estudiante.tiempoServicioBase = -1 * ((-4.5) * Math.Log(Math.Round(random.NextDouble(), 4)));
                    }
                    if (estudiante.tipoCarrera == "salud")
                    {
                        //ing
                        estudiante.tiempoServicioMiu = -3.8;
                        //calcular el tiempo servicio
                        estudiante.tiempoServicioBase = -1 * ((-3.8) * Math.Log(Math.Round(random.NextDouble(), 4)));
                    }
                    if (estudiante.tipoCarrera == "otros")
                    {
                        //ing
                        estudiante.tiempoServicioMiu = -5.5;
                        //calcular el tiempo servicio
                        estudiante.tiempoServicioBase = -1 * ((-5.5) * Math.Log(Math.Round(random.NextDouble(), 4)));
                    }
                    estudiante.tiempoServicioBase = estudiante.tiempoServicioBase / 10;
                    estudiante.tiempoServicioBase = Math.Round((estudiante.tiempoServicioBase * -1), 2);
                    #endregion
                    //fin obteniendo   tiempo de servicio del estudiante


                    //incio obteniendo el indice    
                    #region
                    estudiante.indice = random.Next(1, 4); //se obtiene un indice para el estudiante
                    estudiante.indice = estudiante.indice + random.NextDouble() + 0.01;
                    estudiante.indice = Math.Round(estudiante.indice, 2); // indi-3.145214-->3.1
                    estudiante.indice_procesado = estudiante.indice;
                    if (estudiante.indice < 4)
                        estudiante.indice_procesado = Math.Round(estudiante.indice + 0.01, 1);

                    estudiante.jugadas = Math.Round(estudiante.indice_procesado); //indice 3.6 -->jugadas:4

                    //MessageBox.Show(estudiante.indice.ToString()+"-- procesa: "+estudiante.indice_procesado+"  judagas->"+estudiante.jugadas);
                    #endregion
                    //fin obtenendo el indice


                    //inicio obteniendo eventos retraso
                    #region

                    double rand = 0; //guarda el valor del entero del rand que se usara 59
                    double randDecimal = 0;//guarda el valor decmal del rand que se usara 0.50
                    rand = random.Next(1,99); //99
                    randDecimal = Math.Round(random.NextDouble(),2);

                    evento_retraso = new evento_retraso();
                    rand = rand + randDecimal; //-- >59.50
                    //MessageBox.Show("rand-->" + (rand-randDecimal).ToString()+"+"+randDecimal.ToString()+"=="+rand.ToString());
                    if (rand >= 0 && rand <= 7.24)
                    {
                        //aumenta un 18.50
                        estudiante.tiempoServicioFinal = (estudiante.tiempoServicioBase*18.50)+estudiante.tiempoServicioBase;
                        evento_retraso.nombre = "Falla electricidad";
                        estudiante.cantidadEventosRestraso = estudiante.cantidadEventosRestraso + 1;
                        estudiante.eventosRestraso.Add(evento_retraso);
                    }
                    else if (rand >= 7.25 && rand <= 8.14)
                    {
                        //aumenta un 15.20
                        estudiante.tiempoServicioFinal = (estudiante.tiempoServicioBase * 15.20) + estudiante.tiempoServicioBase;
                        evento_retraso.nombre = "salud estudiante";
                        estudiante.cantidadEventosRestraso = estudiante.cantidadEventosRestraso + 1;
                        estudiante.eventosRestraso.Add(evento_retraso);
                    }
                    else if (rand >= 8.15 && rand <= 8.84)
                    {
                        //aumenta un 10.60
                        estudiante.tiempoServicioFinal = (estudiante.tiempoServicioBase * 10.60) + estudiante.tiempoServicioBase;
                        evento_retraso.nombre = "salud empleado";
                        estudiante.cantidadEventosRestraso = estudiante.cantidadEventosRestraso + 1;
                        estudiante.eventosRestraso.Add(evento_retraso);
                    }
                    else if (rand >= 8.85 && rand <= 25.79)
                    {
                        //aumenta un 28.30
                        estudiante.tiempoServicioFinal = (estudiante.tiempoServicioBase * 28.30) + estudiante.tiempoServicioBase;
                        evento_retraso.nombre = "fallo computacional";
                        estudiante.cantidadEventosRestraso = estudiante.cantidadEventosRestraso + 1;
                        estudiante.eventosRestraso.Add(evento_retraso);
                    }
                    else if (rand >= 25.80 && rand <= 100)
                    {
                        //no hace nada
                        estudiante.tiempoServicioFinal = estudiante.tiempoServicioBase;
                    }
                    estudiante.tiempoServicioFinal = Math.Round(estudiante.tiempoServicioFinal, 2);
                    #endregion
                    //fin    obteniendo eventos retraso



                    //inicio reiniciar items del panel
                    #region

                    reiniciarPanelEstudiante(estudiante);// para crear panel nuevo con nuevos elementos en diferentes posiciones
                    
                    #endregion
                    //fin    reiniciar items del panel

                    listaEstudiante.Add(estudiante);
                    //loadEstudiantes();
                    //inicio jugar por estudiante
                    #region

                    double oportunidades = 0;
                    bool eligioSuCarrera = false; // variables para saber si llego a elegir su carrera en el panel
                    oportunidades = estudiante.jugadas;// se asigan la cantidad de jugadas a oportunidades
                    while (oportunidades > 0)
                    {
                        y:
                        int posicionPanelElegida = 0;
                        posicionPanelElegida = random.Next(1, 16);
                        //MessageBox.Show("posicion panel elegida->" + posicionPanelElegida);
                        //MessageBox.Show("estudiante->"+(est+1)+" posicion elegida-->"+posicionPanelElegida+" contenido-->"+listaPanel[posicionPanelElegida].contenido.ToString()+" jugadas restantes->"+oportunidades);

                        if (listaPanel[posicionPanelElegida].pulsado == true)
                        {
                            //MessageBox.Show("ya pulso esa posicion anteriormente, debe elegir otra");
                            goto y; //si la opcion que tomo ya esta pulsada debe elegir otra posicion
                        }
                        if (listaPanel[posicionPanelElegida].contenido == estudiante.Nombrecarrera)
                        {
                            //acaba de elegir su carrera
                            eligioSuCarrera = true;
                        }

                        if (eligioSuCarrera == true)
                        {
                            //como eleigio su carrera se gano la beca
                            estudiante.tieneBeca = true;
                            //MessageBox.Show("eligio su carrera-->"+ listaPanel[posicionPanelElegida].contenido);
                            break;
                        }

                        //validando si escogio un premio y anadirlo al estudiante
                        bool premio = false;
                        listaPremios.ForEach(p =>
                        {
                            if (p.nombre.Contains(listaPanel[posicionPanelElegida].contenido))
                            {
                                premio = true;
                                //MessageBox.Show("Premio-->" + p.nombre + " añadido al estudiante");
                                estudiante.premios.Add(p);//se anade el premio al estudiante

                                //si el premio es extra el estudiante decide si cambiarlo por otra jugada o seguir
                                if (p.nombre == "extra")
                                {
                                    //pregunta cambia extra por otra oportunidad
                                    int cambiar = random.Next(0,2);
                                    //MessageBox.Show("cambio oportunidad->" + cambiar);
                                    if (cambiar == 1 && estudiante.tieneBeca==false)//eligio cambiar por nueva oportunidad y no tiene beca aun
                                    {
                                        oportunidades++;
                                        //estudiante.premios.Remove(p); // se quita el premio extra
                                        estudiante.cambiosPremiosExtraPorNuevaoportunidad =estudiante.cambiosPremiosExtraPorNuevaoportunidad + 1;
                                    }
                                }
                            }
                        });


                        
                        //MessageBox.Show("estudiante-> "+est+"--> oportunidades restantes-->" + oportunidades);
                        estudiante.cantidadJugadasParaElegirCarra = estudiante.cantidadJugadasParaElegirCarra + 1;
                        oportunidades--;
                    }
                    //fin    jugar por estudiante
                    #endregion
                    //loadPanel();
                
                 //listaEstudiante.Add(estudiante);
                }

                //agregando todos los datos al grid
                int cont = 0;
                
                //cargar todos los estudiantes
                loadEstudiantes();
                
            MessageBox.Show("Fin", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            getDatos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getAction.:" + ex.ToString(),"",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        public void loadEstudiantes()
        {
            try
            {
                if (dataGridView1.Rows.Count > 0)
                    dataGridView1.Rows.Clear();

                int cont = 0;
                listaEstudiante.ForEach(x =>
                {
                    cont++;
                    dataGridView1.Rows.Add(cont, x.Nombrecarrera, x.indice, x.jugadas, x.tiempoServicioBase,x.tiempoServicioFinal,(bool)x.tieneBeca,(x.eventosRestraso.ToList().Count+"-"+x.cantidadEventosRestraso.ToString("N")),(x.cambiosPremiosExtraPorNuevaoportunidad+"="+(+x.jugadas+x.cambiosPremiosExtraPorNuevaoportunidad)));
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loadEstudiantes.:" + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void loadPanel()
        {
            try
            {
                if(dataGridView2.Rows.Count>0)
                    dataGridView2.Rows.Clear();


                listaPanel.ForEach(x =>
                {
                    //MessageBox.Show(x.posicion+"-"+ x.contenido);
                    dataGridView2.Rows.Add(x.posicion, x.contenido);
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loadPanel.:" + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
      

        public void reiniciarPanelEstudiante(estudiante estudiante1)
        {
            try
            {
                int cont = 0;
                random=new Random();
                listaPanel=new List<panel>();
                penelMatriz=new string[16];
                
              
                //iniciando la lista peneles marcando los indices de 0 a 16 y contenio vacio
                for(int f=0;f<16;f++)
                {
                    panel=new panel();
                    panel.posicion = f;
                    panel.contenido = "";
                    panel.pulsado = false;
                    panel.cantidadPulsada = 0;
                    panel.intervaloInicial = 0;
                    panel.intervaloFinal = 0;
                    listaPanel.Add(panel);
                }

              
                cont = 0;
                //asignando las 5 carreras iguales del estudiante aleatoriamente
                while (cont < 5)
                {
                    int posicion = random.Next(0, 16);
                    if (listaPanel[posicion].posicion!=null && listaPanel[posicion].contenido=="")
                    {
                       
                        listaPanel.ForEach(x =>
                        {
                            if (x.posicion == posicion)
                            {
                                x.contenido = estudiante1.Nombrecarrera;
                            }
                        });
                        cont++;
                    }
                }
                // fin asignando las 5 carreras iguales del estudiante aleatoriamente
                

                //asignando 7 carreras diferentes
                cont = 0;
                while (cont < 7)
                {
                    int posicion = random.Next(0, 16);
                    if (listaPanel[posicion].posicion != null && listaPanel[posicion].contenido == "")
                    {
                        x:
                        int posicionCarrera = random.Next(1, 9); // se obtiene una posicion carrera random
                        string carreraTemp = listaCarreras[posicionCarrera].nombreCarrera; // guarda la carrera elegida random
                        if (estudiante1.Nombrecarrera != carreraTemp)
                        {
                            listaPanel.ForEach(x =>
                            {
                                if (x.posicion == posicion)
                                {
                                    x.contenido = listaCarreras[posicionCarrera].nombreCarrera;
                                }
                            });
                        }
                        else
                        {
                            goto x;   
                        }
                        cont++;
                     }
                }
                //fin asignando 7 carreras diferentes


                //asignando 4 premios
                cont = 0;
                while (cont < 4)
                {
                    int posicion = random.Next(0, 16);
                    if (listaPanel[posicion].posicion != null && listaPanel[posicion].contenido == "")
                    {
                        int posicionPremios = random.Next(1, 5); // se obtiene una posicion premio random
                        string premioTemp = listaPremios[posicionPremios].nombre; // guarda el premio elegido random
                        listaPanel.ForEach(x =>
                        {
                            if (x.posicion == posicion)
                            {
                                x.contenido = premioTemp;
                            }
                        });
                        cont++;
                    }
                }
                //fin asignando 4 premios


                //pasando los datos de la lista panel a la matriz
               loadPanel();
               
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error reiniciarPanelEstudiante.:" + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ventana_becas_universitarias_Load(object sender, EventArgs e)
        {

        }


        


        public void imprimir()
        {
            try
            {
                if (listaEstudiante == null)
                {
                    MessageBox.Show("No se encontraron datos, primero debe simular", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                    //Reporte.reporteCompraFanega ventanaReporteCompraFanegas = new Reporte.reporteCompraFanega(usuario, compra.id);
                    //ventanaReporteCompraFanegas.ShowDialog();

                    //datos generales
                    String reporte = "simulacionDigital.reportes.reporte_estudiantes.rdlc";
                    List<ReportDataSource> listaReportDataSource = new List<ReportDataSource>();

                   

                    //reporte estudiante
                    reporte_estudiante reporteEstudiante = new reporte_estudiante();
                    List<reporte_estudiante> ListaReporteEstudiante = new List<reporte_estudiante>();
                    int cont = 0;
                    listaEstudiante.ForEach(x =>
                    {
                        reporteEstudiante=new reporte_estudiante();

                        reporteEstudiante.estudiante = cont + 1;
                        reporteEstudiante.indice = x.indice;
                        reporteEstudiante.carrera = x.Nombrecarrera;
                        reporteEstudiante.tipoCarrera = x.tipoCarrera;
                        reporteEstudiante.beca = x.tieneBeca;
                        reporteEstudiante.tiempoServicioBase = x.tiempoServicioBase;
                        reporteEstudiante.tiempoServicioFinal = x.tiempoServicioFinal;
                        reporteEstudiante.jugadas = x.jugadas;

                        ListaReporteEstudiante.Add(reporteEstudiante);
                        cont++;

                    });
                    
                    ReportDataSource reporteF = new ReportDataSource("estudiante", ListaReporteEstudiante);
                    listaReportDataSource.Add(reporteF);

                    List<ReportParameter> ListaReportParameter = new List<ReportParameter>();

                    VisorReporteComun ventana=new VisorReporteComun(reporte, listaReportDataSource, ListaReportParameter, true, false, false);
                    ventana.ShowDialog();
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error imprimir: " + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            imprimir();
        }


        public void getDatos()
        {
            try
            {
                //inicio Número promedio de estudiantes en cada línea de espera
                #region
                estudiantesIngenieria = 0;
                estudiantesSalud = 0;
                estudiantesOtros = 0;
                estudiantesTotal = 0;

                  listaEstudiante.ForEach(x =>
                  {
                      if (x.tipoCarrera == "ingenieria")
                      {
                          estudiantesIngenieria++;
                      }
                      else if (x.tipoCarrera == "salud")
                      {
                          estudiantesSalud++;
                      }
                      else if (x.tipoCarrera == "otros")
                      {
                          estudiantesOtros++;
                      }
                      estudiantesTotal++;
                  });
                MessageBox.Show("Ingenieria-->"+(estudiantesIngenieria/estudiantesTotal));
                MessageBox.Show("Salud-->" + (estudiantesSalud / estudiantesTotal));
                MessageBox.Show("Otros-->" + (estudiantesOtros / estudiantesTotal));
                #endregion
                //Fin promedio de estudiantes en cada línea de espera


                
                
                
                
                //inicio Tiempo promedio de servicio.
                #region
                tiempoServivioEstudianteAcumulativo = 0;
                listaEstudiante.ForEach(x =>
                {
                    //tiempoServivioEstudianteAcumulativo = 0;
                    tiempoServivioEstudianteAcumulativo += x.tiempoServicioFinal;
                });
                MessageBox.Show("Tiempo promedio de servicio-->" + tiempoServivioEstudianteAcumulativo/estudiantesTotal);
                #endregion
                //fin Tiempo promedio de servicio.




                 
                //inicio Probabilidad de que se presente cada uno de los eventos que retrasan al estudiante
                #region
                estudianteConTodosLosEventosFallas = 0;
                listaEstudiante.ForEach(x =>
                {
                    //estudianteConTodosLosEventosFallas = 0;
                    if (x.eventosRestraso.ToList().Count == 4)
                    {
                        estudianteConTodosLosEventosFallas++;
                    }
                });
                #endregion
                //fin Probabilidad de que se presente cada uno de los eventos que retrasan al estudiante
                MessageBox.Show("Probabilidad de que se presente cada uno de los eventos que retrasan al estudiante-->"+estudianteConTodosLosEventosFallas/estudiantesTotal);






                //
                #region
                listaEstudiante.ForEach(x =>
                {
                });
                #endregion
                //





            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getDatos.:" + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

using InterpreteSencillo.analizador;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace InterpreteSencillo
{
    public partial class interfazGrafica : Form
    {
        public interfazGrafica()
        {
            
            InitializeComponent();
            //this.cargarArchivoToolStripMenuItem.Click += new System.EventHandler(this.cargarArchivoToolStripMenuItem_Click);
        }

        public void button2_Click(object sender, EventArgs e)
        {
            textCodigo.Text = "";
        }

        public void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        public void button1_Click(object sender, EventArgs e)
        {
            string cadenaEntrada = textCodigo.Text;
            string salida = "";
            string consola = "";
            Sintactico sintac = new Sintactico();
            sintac.analizar(cadenaEntrada);
            salida = sintac.retornarSalida();
            consola = sintac.retornarProcesos();

            textConsola.Text = sintac.retornarProcesos();
            textSalida.Text = salida;
            
        }

        public void textConsola_TextChanged(object sender, EventArgs e)
        {

        }

        public void textSalida_TextChanged(object sender, EventArgs e)
        {

        }

        private void cargarArchivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = "";
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = "D:\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files  (*.*)|*.*";

            openFileDialog1.FileName = "Selecciones el archivo de entrada";
            openFileDialog1.Title = "Carga de Archivos";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.FileName = this.labelArchivo.Text;

            System.Diagnostics.Debug.WriteLine("Antes de abrir el dialogo");
            if (openFileDialog1.ShowDialog() == DialogResult.OK && openFileDialog1.ToString() != " ") {
                path = openFileDialog1.FileName;
                this.labelArchivo.Text = Path.GetFileName(openFileDialog1.FileName);
            }

            textCodigo.Text = "";
            System.IO.StreamReader sr = new System.IO.StreamReader(path, System.Text.Encoding.Default);
            string texto;
            texto = sr.ReadToEnd();
            sr.Close();
            textCodigo.Text = texto;


        }

        private void guardarArchivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog guardarArchivo = new SaveFileDialog();
            guardarArchivo.Filter = "Text Files (*.txt)|*.txt";
            guardarArchivo.Title = "Guardar Archivo de Texto";

            DialogResult resultado = guardarArchivo.ShowDialog();

            if (resultado == DialogResult.OK) {
                System.IO.File.WriteAllText(guardarArchivo.FileName, textCodigo.Text);
            }
        }
    }
}

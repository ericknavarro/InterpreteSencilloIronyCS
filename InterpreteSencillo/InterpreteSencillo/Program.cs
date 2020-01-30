using InterpreteSencillo.analizador;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InterpreteSencillo
{
    class Program
    {
        //[System.STAThreadAttribute]
        [System.STAThread]

        static void Main(string[] args)
        {
 
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new interfazGrafica());

            

            //string text = System.IO.File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\input", "entrada.txt"));
            //Sintactico sintac = new Sintactico();
            //sintac.analizar(text);
        }
    }
}

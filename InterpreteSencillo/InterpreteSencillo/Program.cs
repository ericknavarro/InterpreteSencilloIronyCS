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
        [System.STAThread]

        static void Main(string[] args)
        {
 
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new interfazGrafica());

        }
    }
}

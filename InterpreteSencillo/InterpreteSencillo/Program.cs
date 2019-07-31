using InterpreteSencillo.analizador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterpreteSencillo
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = System.IO.File.ReadAllText("entrada.txt");
            Sintactico sintac = new Sintactico();
            sintac.analizar(text);
        }
    }
}

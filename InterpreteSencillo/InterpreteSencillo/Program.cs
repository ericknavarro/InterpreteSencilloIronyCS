using InterpreteSencillo.analizador;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterpreteSencillo
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = System.IO.File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\input", "entrada.txt"));
            Sintactico sintac = new Sintactico();
            sintac.analizar(text);
        }
    }
}

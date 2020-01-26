using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace InterpreteSencillo.Graficas
{
    class ControlDot
    {
        public static String graph = "";
        public static void ConstruirArbol(ParseTreeNode raiz)
        {
            //Este metodo genera el archivo .dot en el cual se escribira todo el codigo para graficar el AST
            System.IO.StreamWriter f = new System.IO.StreamWriter(Directory.GetCurrentDirectory() + @"\AST.dot");
            f.Write("digraph arbol{ \n ");
            graph = "";
            Generar(raiz);
            f.Write(graph);
            f.Write("}");
            f.Close();
        }

        public static void Generar(ParseTreeNode raiz)
        {
            //Este metodo recore el AST y concatena la informacion en la variable graph para escribirla en el archivo .dot
            graph = graph + "nodo" + raiz.GetHashCode() + "\n [label=\"" + raiz.ToString().Replace("\"", "\\\"") + " \", fillcolor=\"lightskyblue\", style =\"filled\"]; \n";
            if (raiz.ChildNodes.Count > 0)
            {
                ParseTreeNode[] hijos = raiz.ChildNodes.ToArray();
                for (int i = 0; i < raiz.ChildNodes.Count; i++)
                {
                    Generar(hijos[i]);
                    graph = graph + "\"nodo" + raiz.GetHashCode() + "\"-> \"nodo" + hijos[i].GetHashCode() + "\" \n";
                }
            }
        }
        public static void GraficarArbol(string fileName, string path)
        {
            //Este metodo corre comandos en el cmd para generar la imagen del AST y abrirla inmediatamente
            try
            {
                //Genera el comando para crear la imagen a partir del .dot
                var command = string.Format("dot.exe -Tjpg {0} -o {1}", Path.Combine(path, fileName), Path.Combine(path, fileName.Replace(".dot", ".jpg")));
                var proc1 = new ProcessStartInfo();
                proc1.UseShellExecute = true;
                proc1.WorkingDirectory = @"C:\Windows\System32";
                proc1.FileName = @"C:\Windows\System32\cmd.exe";
                proc1.Verb = "runas"; proc1.Arguments = "/c " + command;
                proc1.WindowStyle = ProcessWindowStyle.Hidden;
                Process.Start(proc1);

                //Abre la imagen generada en la ruta especificada
                var command1 = string.Format(path + @"\AST.jpg");
                var proc2 = new ProcessStartInfo();
                proc2.UseShellExecute = true;
                proc2.WorkingDirectory = @"C:\Windows\System32";
                proc2.FileName = @"C:\Windows\System32\cmd.exe";
                proc2.Verb = "runas"; proc2.Arguments = "/c " + command1;
                proc2.WindowStyle = ProcessWindowStyle.Hidden;
                Process.Start(proc2);
            }
            catch (Exception x)
            {
                System.Diagnostics.Debug.WriteLine("Error al generar la imagen: " + x);
            }
        }
    }
}

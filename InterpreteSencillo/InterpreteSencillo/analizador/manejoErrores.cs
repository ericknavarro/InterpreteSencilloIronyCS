using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterpreteSencillo.analizador
{
    class manejoErrores
    {
        private ParseTree arbol;
        private ParseTreeNode raiz;
        public manejoErrores(ParseTree arbol, ParseTreeNode raiz)
        {
            this.arbol = arbol;
            this.raiz = raiz;
        }
        public bool hayErrores()
        {
            String errores = "<html>\n <body> <h2>Errores proyecto 2</h2> <table style=\"width:100%\" border=\"1\"> <tr><th>Descripcion del error</th><th>fila</th> <th>columna</th></tr> \n";
            if (raiz == null)
            {
                errores += "<tr>" +
                        "<td>" + "Error fatal, no se recupero el analizador" +
                        "</td>" +
                        "<td>" + "0" +
                        "</td>" +
                        "<td>" + "0" +
                        "</td>" +
                        "</tr>";
            }
            if (arbol.ParserMessages.Count > 0 || raiz == null)
            {
                for (int i = 0; i < arbol.ParserMessages.Count; i++)
                {
                    errores += "<tr>" +
                        "<td>" + arbol.ParserMessages[i].Message +
                        "</td>" +
                        "<td>" + arbol.ParserMessages[i].Location.Line +
                        "</td>" +
                        "<td>" + arbol.ParserMessages[i].Location.Column +
                        "</td>" +
                        "</tr>";
                }
                errores += "</table> </body> </html>";
                using (StreamWriter outputFile = new StreamWriter("reporteErrores.html"))
                {
                    outputFile.WriteLine(errores);
                }
                return true;
            }
            return false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterpreteSencillo.arbol;
using Irony.Ast;
using Irony.Parsing;

namespace InterpreteSencillo.analizador
{
    class Sintactico
    {
        string consola = "";
        string salida = "";
        string temp = "";

        public void analizar(String cadena)
        {
            Gramatica gramatica = new Gramatica();
            LanguageData lenguaje = new LanguageData(gramatica);
            Parser parser = new Parser(lenguaje);
            consola += "[PROC]: Creando AST..." + Environment.NewLine;
            ParseTree arbol = parser.Parse(cadena);
            consola += "[PROC]: Asignando la raiz al AST..." + Environment.NewLine;
            ParseTreeNode raiz = arbol.Root;
            consola += "[PROC]: Raiz de AST asignada correctamente..." + Environment.NewLine;

            if (raiz == null)
            {
                salida = "[ERROR]: elementos incorrectos en el código";
            }
            else {
                LinkedList<Instruccion> AST = instrucciones(raiz.ChildNodes.ElementAt(0));
                TablaDeSimbolos global = new TablaDeSimbolos();

                foreach (Instruccion ins in AST)
                {

                    consola += "[AST]: Ejecutando instrucción: " + ins.ToString() + Environment.NewLine;
                    salida += ins.ejecutar(global);
                }
            }
             

        }

        public LinkedList<Instruccion> instrucciones(ParseTreeNode actual)
        {
            if (actual.ChildNodes.Count == 2)
            {
                LinkedList<Instruccion> lista = instrucciones(actual.ChildNodes.ElementAt(0));
                lista.AddLast(instruccion(actual.ChildNodes.ElementAt(1)));
                return lista;

            }
            else
            {
                LinkedList<Instruccion> lista = new LinkedList<Instruccion>();
                lista.AddLast(instruccion(actual.ChildNodes.ElementAt(0)));
                return lista;
            }
        }

        public Instruccion instruccion(ParseTreeNode actual)
        {
            string tokenOperacion = actual.ChildNodes.ElementAt(0).ToString().Split(' ')[0].ToLower();
            switch (tokenOperacion) {
                case "imprimir":
                    consola += "[ANALISIS]: Instrucción IMPRIMIR reconocida..." + Environment.NewLine;
                    return new Imprimir(expresion_cadena(actual.ChildNodes.ElementAt(2)));
                case "mientras":
                    consola += "[ANALISIS]: Instrucción MIENTRAS reconocida..." + Environment.NewLine;
                    return new Mientras(expresion_logica(actual.ChildNodes.ElementAt(2)), instrucciones(actual.ChildNodes.ElementAt(5)));
                case "numero":
                    consola += "[ANALISIS]: Elemento NUMERO reconocido..." + Environment.NewLine;
                    string tokenValor = actual.ChildNodes.ElementAt(1).ToString().Split(' ')[0];
                    return new Declaracion(tokenValor, Simbolo.Tipo.NUMERO);
                case "if":
                    consola += "[ANALISIS]: Instrucción IF reconocida..." + Environment.NewLine;
                    if (actual.ChildNodes.Count == 7)
                    {
                        return new If(expresion_logica(actual.ChildNodes.ElementAt(2)), instrucciones(actual.ChildNodes.ElementAt(5)));
                    }
                    else {
                        return new If(expresion_logica(actual.ChildNodes.ElementAt(2)), instrucciones(actual.ChildNodes.ElementAt(5)), instrucciones(actual.ChildNodes.ElementAt(9)));
                    }
                default:
                    tokenValor = actual.ChildNodes.ElementAt(0).ToString().Split(' ')[0];
                    return new Asignacion(tokenValor, expresion_numerica(actual.ChildNodes.ElementAt(2)));       
            }
        }

        public Operacion expresion_cadena(ParseTreeNode actual) {
            if (actual.ChildNodes.Count == 3)
            {
                return new Operacion(expresion_cadena(actual.ChildNodes.ElementAt(0)), expresion_cadena(actual.ChildNodes.ElementAt(2)), Operacion.Tipo_operacion.CONCATENACION);
            }
            else {
                String tokenValor = actual.ChildNodes.ElementAt(0).ToString().Split(' ')[0];
                if (tokenValor.Equals("expresion_numerica"))
                {
                    return expresion_numerica(actual.ChildNodes.ElementAt(0));
                }
                else {
                    tokenValor = actual.ChildNodes.ElementAt(0).ToString();
                    return new Operacion(tokenValor.Remove(tokenValor.ToCharArray().Length - 8, 8), Operacion.Tipo_operacion.CADENA);
                }
            }
        }

        public Operacion expresion_logica(ParseTreeNode actual)
        {
            string tokenOperador = actual.ChildNodes.ElementAt(1).ToString().Split(' ')[0];
            if (tokenOperador.Equals("<"))
            {
                return new Operacion(expresion_numerica(actual.ChildNodes.ElementAt(0)), expresion_numerica(actual.ChildNodes.ElementAt(2)), Operacion.Tipo_operacion.MENOR_QUE);
            }
            else {
                return new Operacion(expresion_numerica(actual.ChildNodes.ElementAt(0)), expresion_numerica(actual.ChildNodes.ElementAt(2)), Operacion.Tipo_operacion.MAYOR_QUE);
            }
        }

        public Operacion expresion_numerica(ParseTreeNode actual)
        {
            if (actual.ChildNodes.Count == 3)
            {
                string tokenOperador = actual.ChildNodes.ElementAt(1).ToString().Split(' ')[0];
                switch (tokenOperador)
                {
                    case "+":
                        return new Operacion(expresion_numerica(actual.ChildNodes.ElementAt(0)), expresion_numerica(actual.ChildNodes.ElementAt(2)), Operacion.Tipo_operacion.SUMA);
                    case "-":
                        return new Operacion(expresion_numerica(actual.ChildNodes.ElementAt(0)), expresion_numerica(actual.ChildNodes.ElementAt(2)), Operacion.Tipo_operacion.RESTA);
                    case "*":
                        return new Operacion(expresion_numerica(actual.ChildNodes.ElementAt(0)), expresion_numerica(actual.ChildNodes.ElementAt(2)), Operacion.Tipo_operacion.MULTIPLICACION);
                    case "/":
                        return new Operacion(expresion_numerica(actual.ChildNodes.ElementAt(0)), expresion_numerica(actual.ChildNodes.ElementAt(2)), Operacion.Tipo_operacion.DIVISION);
                    default:
                        return expresion_numerica(actual.ChildNodes.ElementAt(1));
                }

            }
            else if (actual.ChildNodes.Count == 2)
            {
                return new Operacion(expresion_numerica(actual.ChildNodes.ElementAt(1)), Operacion.Tipo_operacion.NEGATIVO);
            }
            else
            {
                string tokenOperador = actual.ChildNodes.ElementAt(0).ToString().Split(' ')[1];
                string tokenValor = actual.ChildNodes.ElementAt(0).ToString().Split(' ')[0];
                if (tokenOperador.Equals("(ID)")) {
                    return new Operacion(tokenValor, Operacion.Tipo_operacion.IDENTIFICADOR);
                }
                return new Operacion(Double.Parse(actual.ChildNodes.ElementAt(0).ToString().Split(' ')[0]));
            }
        }

        public string retornarSalida() {
            return salida;
        }

        public string retornarProcesos()
        {
            return consola;
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterpreteSencillo.arbol;
using Irony.Ast;
using Irony.Parsing;
using InterpreteSencillo.Graficas;
using System.IO;

namespace InterpreteSencillo.analizador
{
    class Sintactico
    {

        public void analizar(String cadena)
        {
            Gramatica gramatica = new Gramatica();
            LanguageData lenguaje = new LanguageData(gramatica);
            Parser parser = new Parser(lenguaje);
            ParseTree arbol = parser.Parse(cadena);
            ParseTreeNode raiz = arbol.Root;

            LinkedList<Instruccion> AST = instrucciones(raiz.ChildNodes.ElementAt(0));

            TablaDeSimbolos global = new TablaDeSimbolos();

            foreach (Instruccion ins in AST) {
                ins.ejecutar(global);
            }

            //Para graficar el AST
            ControlDot.ConstruirArbol(raiz); //Manda a llamar el metodo que genera el .dot
            ControlDot.GraficarArbol("AST.dot", Directory.GetCurrentDirectory()); //Compila el .dot y genera la imagen en la ruta donde se ubica el proyecto
            System.Diagnostics.Debug.WriteLine("AST generado en la ruta:"); //Indica en que ruta se encuentran el .dot y la imagen
            System.Diagnostics.Debug.WriteLine(Directory.GetCurrentDirectory()); //Indica en que ruta se encuentran el .dot y la imagen

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
                    return new Imprimir(expresion_cadena(actual.ChildNodes.ElementAt(2)));
                case "mientras":
                    return new Mientras(expresion_logica(actual.ChildNodes.ElementAt(2)), instrucciones(actual.ChildNodes.ElementAt(5)));
                case "numero":
                    string tokenValor = actual.ChildNodes.ElementAt(1).ToString().Split(' ')[0];
                    return new Declaracion(tokenValor, Simbolo.Tipo.NUMERO);
                case "if":
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
    }
}

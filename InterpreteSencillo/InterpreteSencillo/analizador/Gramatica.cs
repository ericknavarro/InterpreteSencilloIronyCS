using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Ast;
using Irony.Parsing;

namespace InterpreteSencillo.analizador
{
    class Gramatica : Grammar
    {
        public Gramatica() : base(caseSensitive: false)
        {

            #region ER
            StringLiteral CADENA = new StringLiteral("cadena", "\"");
            var ENTERO = new NumberLiteral("entero");
            var DECIMAL = new RegexBasedTerminal("Decimal", "[0-9]+'.'[0-9]+");
            IdentifierTerminal IDENTIFICADOR = new IdentifierTerminal("ID");

            CommentTerminal comentarioLinea = new CommentTerminal("comentarioLinea", "//", "\n", "\r\n"); //si viene una nueva linea se termina de reconocer el comentario.
            CommentTerminal comentarioBloque = new CommentTerminal("comentarioBloque", "/*", "*/");
            #endregion

            #region Terminales
            var RIMPRIMIR = ToTerm("imprimir");
            var RNUMERO = ToTerm("numero");
            var RMIENTRAS = ToTerm("mientras");
            var RIF = ToTerm("if");
            var RELSE = ToTerm("else");
            var PTCOMA = ToTerm(";");
            var LLAVIZQ = ToTerm("{");
            var LLAVDER = ToTerm("}");
            var PARIZQ = ToTerm("(");
            var PARDER = ToTerm(")");
            var MAS = ToTerm("+");
            var MENOS = ToTerm("-");
            var POR = ToTerm("*");
            var DIVIDIDO = ToTerm("/");
            var CONCAT = ToTerm("&");
            var MENQUE = ToTerm("<");
            var MAYQUE = ToTerm(">");
            var IGUAL = ToTerm("=");
            var OO = ToTerm("||");
            var YY = ToTerm("&&");
            var AUMENTO = ToTerm("++");
            var DECREMENTO = ToTerm("--");

            RegisterOperators(1, CONCAT);
            RegisterOperators(2, MAS, MENOS);
            RegisterOperators(3, POR, DIVIDIDO);

            NonGrammarTerminals.Add(comentarioLinea);
            NonGrammarTerminals.Add(comentarioBloque);

            #endregion

            #region No Terminales
            NonTerminal ini = new NonTerminal("ini");
            NonTerminal instruccion = new NonTerminal("instruccion");
            NonTerminal instrucciones = new NonTerminal("instrucciones");
            NonTerminal expresion_numerica = new NonTerminal("expresion_numerica");
            NonTerminal expresion_cadena = new NonTerminal("expresion_cadena");
            NonTerminal expresion_logica = new NonTerminal("expresion_logica");
            #endregion

            #region Gramatica
            ini.Rule = instrucciones;
;

            instrucciones.Rule = instrucciones + instruccion
                        | instruccion;

            instruccion.Rule = RIMPRIMIR + PARIZQ + expresion_cadena + PARDER + PTCOMA
                        | RMIENTRAS + PARIZQ + expresion_logica + PARDER + LLAVIZQ + instrucciones + LLAVDER
                        | RNUMERO + IDENTIFICADOR + PTCOMA
                        | IDENTIFICADOR + IGUAL + expresion_numerica + PTCOMA
                        | IDENTIFICADOR + AUMENTO + PTCOMA
                        | IDENTIFICADOR + DECREMENTO + PTCOMA
                        | RIF + PARIZQ + expresion_logica + PARDER + LLAVIZQ + instrucciones + LLAVDER
                        | RIF + PARIZQ + expresion_logica + PARDER + LLAVIZQ + instrucciones + LLAVDER + RELSE + LLAVIZQ + instrucciones + LLAVDER;

            expresion_numerica.Rule = MENOS + expresion_numerica
                        | expresion_numerica + MAS + expresion_numerica
                        | expresion_numerica + MENOS + expresion_numerica
                        | expresion_numerica + POR + expresion_numerica
                        | expresion_numerica + DIVIDIDO + expresion_numerica
                        | PARIZQ + expresion_numerica + PARDER
                        | ENTERO
                        | DECIMAL
                        | IDENTIFICADOR;

            expresion_cadena.Rule = expresion_cadena + CONCAT + expresion_cadena
                        | CADENA
                        | expresion_numerica;

            expresion_logica.Rule = expresion_numerica + MAYQUE + expresion_numerica
                        | expresion_numerica + MENQUE + expresion_numerica
                        | expresion_logica + OO + expresion_logica
                        | expresion_logica + YY + expresion_logica;

            #endregion

            #region Preferencias
            this.Root = ini;
            #endregion

        }
    }
}

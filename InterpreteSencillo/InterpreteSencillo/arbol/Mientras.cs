using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterpreteSencillo.arbol
{
    class Mientras : Instruccion
    {

        /**
     * Condición de la sentencia mientras.
     */
        private Operacion condicion;
    /**
     * Lista de las instrucciones que deben ejecutarse si la condición se cumple.
     */
    private LinkedList<Instruccion> listaInstrucciones;
        /**
         * Constructor de la clase Mientras
         * @param a condición que debe evaluarse para ejecutar el ciclo
         * @param b instrucciones que deben ejecutarse si la condición se cumpliera
         */
        public Mientras(Operacion a, LinkedList<Instruccion> b)
        {
            this.condicion = a;
            this.listaInstrucciones = b;
        }
        /**
         * Método que ejecuta la instrucción mientras, es una sobreescritura del 
         * método ejecutar que se debe programar por la implementación de la interfaz
         * instrucción
         * @param ts tabla de símbolos del ámbito padre de la sentencia
         * @return Esta instrucción retorna nulo porque no produce ningun valor
         */

    public Object ejecutar(TablaDeSimbolos ts)
        {
            while ((Boolean)condicion.ejecutar(ts))
            {
                TablaDeSimbolos tablaLocal = new TablaDeSimbolos();
                foreach (Simbolo item in ts)
                {
                    tablaLocal.AddLast(item);
                }
                foreach (Instruccion ins in listaInstrucciones)
                {
                    ins.ejecutar(tablaLocal);
                }
            }
            return null;
        }
    }
}

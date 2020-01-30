using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterpreteSencillo.arbol
{
    class If : Instruccion
    {
        /**
     * Condición de la instrucción si..entonces.
     */
        private Operacion condicion;
        string textOperaciones = "";
    /**
     * Lista de instrucciones que serán ejecutadas si la condición se cumple.
     */
    private LinkedList<Instruccion> listaInstrucciones;
        /**
         * Lista de instrucciones que se ejecutarán si la condición no se cumple,
         * esta lista existirá solo si la instrucción posee la clausula ELSE, de lo
         * contrario la lista será null.
         */
        private LinkedList<Instruccion> listaInsElse;
        /**
         * Primer constructor de la clase, este se utiliza cuando la instrucción no 
         * tiene clausula ELSE.
         * @param a Condición del si..entonces
         * @param b Lista de instrucciones que deberían ejecutarse si la condición se cumple
         */
        public If(Operacion a, LinkedList<Instruccion> b)
        {
            condicion = a;
            listaInstrucciones = b;
        }
        /**
         * Segundo constructor de la clase, este se utiliza cuando la instrucción tiene
         * clausula ELSE.
         * @param a Condición del si..entonces
         * @param b Lista de instrucciones que deberían ejecutarse si la condición se cumple
         * @param c Lista de instrucciones que deberían ejecutarse si la condición no se cumple
         */
        public If(Operacion a, LinkedList<Instruccion> b, LinkedList<Instruccion> c)
        {
            condicion = a;
            listaInstrucciones = b;
            listaInsElse = c;
        }
        /**
         * Método que ejecuta la instrucción si..entonces, es una sobreescritura del 
         * método ejecutar que se debe programar por la implementación de la interfaz
         * instrucción
         * @param ts tabla de símbolos del ámbito padre de la sentencia.
         * @return Estra instrucción retorna nulo porque no produce ningún valor en 
         * su ejecución
         */
        
    public Object ejecutar(TablaDeSimbolos ts)
        {
            if ((Boolean)condicion.ejecutar(ts))
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
            else
            {
                if (listaInsElse != null)
                {
                    TablaDeSimbolos tablaLocal = new TablaDeSimbolos();
                    foreach (Simbolo item in ts)
                    {
                        tablaLocal.AddLast(item);
                    }
                    foreach (Instruccion ins in listaInsElse)
                    {
                    ins.ejecutar(tablaLocal);
                    }
                }
            }
            return null;
        }
    }
}

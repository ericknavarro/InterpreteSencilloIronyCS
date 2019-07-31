using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterpreteSencillo.arbol
{
    /**
    * Clase que ejecuta las acciones de una instrucción de asignación y que implementa
    * la interfaz de instrucción
    * @author Julio Arango
    */
    class Asignacion : Instruccion
    {
        /**
     * Identificador de la variable a la que se le asigna el valor.
     */
        private String id;
    /**
     * Valor que se le asigna a la variable.
     */
    private Operacion valor;
    /**
     * Constructor de la clase asignación
     * @param a identificador de la variable
     * @param b valor que se le va a asignar
     */
    public Asignacion(String a, Operacion b)
        {
            this.id = a;
            this.valor = b;
        }
        /**
         * Método que ejecuta la accion de asignar un valor, es una sobreescritura del 
         * método ejecutar que se debe programar por la implementación de la interfaz
         * instrucción
         * @param ts tabla de símbolos del ámbito padre de la sentencia asignación
         * @return En este caso retorna nulo porque no es una sentencia que genere un valor.
         */
        
    public Object ejecutar(TablaDeSimbolos ts)
        {
            ts.setValor(id, valor.ejecutar(ts));
            return null;
        }
    }
}

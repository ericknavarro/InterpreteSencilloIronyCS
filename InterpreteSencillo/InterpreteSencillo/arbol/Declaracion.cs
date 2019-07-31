using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterpreteSencillo.arbol
{
    class Declaracion : Instruccion
    {
        /**
     * Identificador de la variable que será declarada.
     */
        private String id;
    /**
     * Tipo de la variable que será declarada.
     */
    Simbolo.Tipo tipo;
        /**
         * Constructor de la clase
         * @param a Identificador de la variable que será declarada
         * @param t Tipo de la clase que será declarada
         */
        public Declaracion(String a, Simbolo.Tipo t)
        {
            id = a;
            tipo = t;
        }
        /**
         * Método que ejecuta la accion de declarar una variable, es una sobreescritura del 
         * método ejecutar que se debe programar por la implementación de la interfaz
         * instrucción
         * @param ts Tabla de símbolos del ámbito padre.
         * @return No retorna nada porque no es una sentencia que genere un valor.
         */
        
    public Object ejecutar(TablaDeSimbolos ts)
        {
            ts.AddLast(new Simbolo(id, tipo));
            return null;
        }
    }
}

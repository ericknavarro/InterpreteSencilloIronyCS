using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterpreteSencillo.arbol
{
    class Imprimir : Instruccion
    {
        /**
     * Contenido que será impreso al ejecutar la instrucción imprimir, este debe
     * ser una instrucción que genere un valor al ser ejecutada.
     */
        private Instruccion contenido;
    /**
     * Constructor de la clase imprimir
     * @param contenido contenido que será impreso al ejecutar la instrucción
     */
    public Imprimir(Instruccion contenido)
        {
            this.contenido = contenido;
        }
        /**
         * Método que ejecuta la accion de imprimir un valor, es una sobreescritura del 
         * método ejecutar que se debe programar por la implementación de la interfaz
         * instrucción
         * @param ts Tabla de símbolos del ámbito padre de la sentencia.
         * @return Esta instrucción retorna nulo porque no produce ningun valor al ser
         * ejecutada.
         */
        
    public Object ejecutar(TablaDeSimbolos ts)
        {
            String impresion = contenido.ejecutar(ts).ToString();
            Console.WriteLine(impresion);
            return null;
        }
    }
}

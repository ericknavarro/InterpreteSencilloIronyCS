using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterpreteSencillo.arbol
{
    public class TablaDeSimbolos : LinkedList<Simbolo>
    {
        /**
        * Constructor de la clase que lo único que hace es llamar al constructor de 
        * la clase padre, es decir, el constructor de LinkedList.
        */

        string errores = "";
        public TablaDeSimbolos() : base()
        {
            // llamada del constructor de la clase padre
        }
        /**
         * Método que busca una variable en la tabla de símbolos y devuelve su valor.
         * @param id Identificador de la variable que quiere buscarse
         * @return Valor de la variable que se buscaba, si no existe se devuelve nulo
         */
        public Object getValor(String id)
        {
            foreach (Simbolo s in this)
            {
                if (s.getId().Equals(id))
                {
                    return s.getValor();
                }
            }
            errores += "La variable " + id + " no existe en este ámbito." +Environment.NewLine;
            Console.WriteLine("La variable " + id + " no existe en este ámbito.");
            return "Desconocido";
        }
        /**
         * Método que asigna un valor a una variable específica, si no la encuentra 
         * no realiza la asignación y despliega un mensaje de error.
         * @param id Identificador de la variable que quiere buscarse
         * @param valor Valor que quiere asignársele a la variable buscada
         */
        public void setValor(String id, Object valor)
        {
            foreach (Simbolo s in this)
            {
                if (s.getId().Equals(id))
                {
                    s.setValor(valor);
                    return;
                }
            }

            errores+= "La variable " + id + " no existe en este ámbito, por lo "+ "que no puede asignársele un valor." + Environment.NewLine;
            Console.WriteLine("La variable " + id + " no existe en este ámbito, por lo "
                    + "que no puede asignársele un valor.");
        }

        public string getErroresTS()
        {
            return errores;
        }
    }
}

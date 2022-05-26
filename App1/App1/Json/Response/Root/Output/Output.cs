using System.Collections.Generic;

namespace App1
{
    public class Output
    {
        //estas son las listas que digo vvv Tenemos que acceder a la lista de generic (?
        //cada generic tiene un responsetype y text. el text es lo que necesitamos imprimir
        public List<Generic> generic { get; set; }
        public List<Intent> intents { get; set; }
        public List<Entity> entities { get; set; }
    }
}



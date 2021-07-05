using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSMapasTiendav2.Models.Peticiones
{
    public class StoresPeticion
    {

        public long id { get; set; }
        public string nombre { get; set; }
        public string telefono { get; set; }
        public string direccion { get; set; }
        public string latitud { get; set; }
        public string longitud { get; set; }
    }
}

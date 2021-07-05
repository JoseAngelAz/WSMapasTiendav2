using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSMapasTiendav2.Models.Peticiones
{
    public class UsuarioPeticion
    {

        public long id { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public int rol { get; set; }
    }
}

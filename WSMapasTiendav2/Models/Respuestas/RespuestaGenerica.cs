using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSMapasTiendav2.Models.Respuestas
{
    public class RespuestaGenerica
    {
        public int Exito { get; set; }
        public string Mensaje { get; set; }
        public object Data { get; set; }

        public RespuestaGenerica()
        {
            this.Exito = 0;
        }
    }
}

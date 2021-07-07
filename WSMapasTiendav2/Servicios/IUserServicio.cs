using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSMapasTiendav2.Models.Peticiones;
using WSMapasTiendav2.Models.Respuestas;

namespace WSMapasTiendav2.Servicios
{
    interface IUserServicio
    {
        RespuestaGenerica Auth(AuthPeticion apeticion);
    }
}

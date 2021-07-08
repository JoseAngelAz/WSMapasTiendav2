using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSMapasTiendav2.Models.Peticiones;
using WSMapasTiendav2.Models.Respuestas;

namespace WSMapasTiendav2.Servicios
{
    //Asegurar de hacer publico el metodo para que el controlador pueda accederlo en su constructor
   public interface IUserServicio
    { 
        UsuarioRespuesta Auth(AuthPeticion apeticion);
    }
}

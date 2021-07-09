/*using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSMapasTiendav2.Models.Peticiones;
using WSMapasTiendav2.Models.Respuestas;
using WSMapasTiendav2.Servicios;

namespace WSMapasTiendav2.Controllers
{
    //localhost:puerto/api/Auth
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IUserServicio _userServicio;

        public AuthController(IUserServicio userServicio)
        {
            _userServicio = userServicio;
        }

        //Respuesta Generica para el cliente
        RespuestaGenerica miRes = new RespuestaGenerica();

        //Autenticar Usuario
        //localhost:44378/api/Auth/login
        [HttpPost("login")]
        public IActionResult Autentificar([FromBody] AuthPeticion Apeticion)
        {
            var UserResponse = _userServicio.Auth(Apeticion);
            if (UserResponse == null)
            {
                miRes.Exito = 0;
                miRes.Mensaje = "Usuario o contraseña incorrecta!";
                return BadRequest(miRes);
            }
            miRes.Exito = 1;
            miRes.Mensaje = "Usuario de Loggin Encontrado";
            miRes.Data = UserResponse;
            return Ok(miRes);
        }

    }
}
*/

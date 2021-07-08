using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSMapasTiendav2.Models;
using WSMapasTiendav2.Models.Peticiones;
using WSMapasTiendav2.Models.Respuestas;
using WSMapasTiendav2.Servicios;

namespace WSMapasTiendav2.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private  IUserServicio _userServi;
    

         public UsuarioController(IUserServicio userService)
        {
            _userServi = userService;
        }
       
        //Respuesta Generica para el cliente
        RespuestaGenerica miRes = new RespuestaGenerica();

        //Autenticar Usuario
        [HttpPost("login")]
        public IActionResult Autentificar([FromBody] AuthPeticion Apeticion)
        {
            var UserResponse = _userServi.Auth(Apeticion);
            if (UserResponse == null)
            {
                miRes.Exito = 0;
                miRes.Mensaje = "Usuario o contraseña incorrecta!";
                return BadRequest(miRes);
            }
            miRes.Exito = 1;
            miRes.Data = UserResponse;
            return Ok(miRes);
        }

        

        // GET: api/<UsuarioController>
        [HttpGet]
        public IActionResult GetUsuario()
        {            
            try {
                //trasemos datos del contexto de la database
                using (MapasTiendaWSV2Context db = new MapasTiendaWSV2Context())
                {
                    var usuarios = db.Usuarios.OrderByDescending(u => u.Id).ToList();
                    //llenamos objs de respuesta para el cliente
                    miRes.Exito = 1;
                    miRes.Data = usuarios;
                    miRes.Mensaje = "Usuario Consultado Exitosamente";
                }
            }
            catch (Exception err) { miRes.Mensaje = err.Message; }
            return Ok(miRes);
        }

        // GET api/<UsuarioController>/5 Consulta el Usuario por su ID
        [HttpGet("{id}")]
        public IActionResult GetUsuarioById(long id)
        {            

            try {
                //trasemos datos del contexto de la database
                using (MapasTiendaWSV2Context db = new MapasTiendaWSV2Context())
                {
                    Usuario usuario = db.Usuarios.Find(id);
                    //llenamos objs de respuesta para el cliente
                    miRes.Exito = 1;
                    miRes.Data = usuario;
                    miRes.Mensaje = "Usuario Consultado Exitosamente";
                }
            }
            catch (Exception err) { miRes.Mensaje = err.Message; }
            return Ok(miRes);
        }

        // POST api/<UsuarioController> AGREGAR USUARIO
        [HttpPost]
        public IActionResult AddUsuario(UsuarioPeticion user)
        {            
            try {
                using (MapasTiendaWSV2Context db = new MapasTiendaWSV2Context())
                {
                    Usuario User = new Usuario();
                    User.Nombre = user.nombre;
                    User.Email = user.email;
                    User.Password = user.password;
                    User.Rol = user.rol;

                    //Insertamos en base de datos
                    db.Add(User);
                    db.SaveChanges();
                    //llenamos objs de respuesta para el cliente
                    miRes.Exito = 1;
                    miRes.Data = User;
                    miRes.Mensaje = "Usuario Insertado Exitosamente";
                }
            }
            catch (Exception err) { miRes.Mensaje = err.Message; }
            return Ok(miRes);
        }

        // PUT api/<UsuarioController>/5 Modificar USUARIO
        [HttpPut]
        public IActionResult EditUsuario(UsuarioPeticion user)
        {            
            try {
                using (MapasTiendaWSV2Context db = new MapasTiendaWSV2Context())
                {
                    Usuario User = db.Usuarios.Find(user.id);
                    User.Nombre = user.nombre;
                    User.Email = user.email;
                    User.Password = user.password;
                    User.Rol = user.rol;
                    db.Entry(User).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    db.SaveChanges();
                    //llenamos objs de respuesta para el cliente
                    miRes.Exito = 1;
                    miRes.Data = User;
                    miRes.Mensaje = "Usuario Modificado Exitosamente";
                }
            }
            catch (Exception err) { miRes.Mensaje = err.Message; }
            return Ok(miRes);
        }

        // DELETE api/<UsuarioController>/5 ELIMINAR USUARIO
        [HttpDelete("{id}")]
        public IActionResult DeleteUsuario(long id)
        {            
            try
            {
                using (MapasTiendaWSV2Context db = new MapasTiendaWSV2Context())
                {
                    Usuario User = db.Usuarios.Find(id);
                    db.Remove(User);
                    db.SaveChanges();
                    //llenamos objs de respuesta para el cliente
                    miRes.Exito = 1;
                    miRes.Data = User;
                    miRes.Mensaje = "Usuario Consultado Exitosamente";
                }
            }
            catch(Exception err) { miRes.Mensaje = err.Message; }
            return Ok(miRes);
        }
    }
}

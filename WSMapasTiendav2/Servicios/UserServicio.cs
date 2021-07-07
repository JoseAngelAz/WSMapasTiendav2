using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSMapasTiendav2.Models;
using WSMapasTiendav2.Models.Peticiones;
using WSMapasTiendav2.Models.Respuestas;
using WSMapasTiendav2.Tools;

namespace WSMapasTiendav2.Servicios
{
    public class UserServicio : IUserServicio
    {
        public UsuarioRespuesta Auth(AuthPeticion apeticion)
        {
            //UsuarioRespuesta
            UsuarioRespuesta userResp = new UsuarioRespuesta();

            //buscamos en la db
            using (var db = new MapasTiendaWSV2Context())
            {
                //encriptamos el password del usuario
                string Encriptedpassword = EncriptarPass.GetSHA256(apeticion.password);
                var usuario = db.Usuarios.Where(d => d.Email == apeticion.email &&
                                                d.Password == Encriptedpassword).FirstOrDefault();
                //si el usuario es nullo
                if (usuario == null) return null;
                
                //llenamos la respuesta de UsuarioRespuesta
                userResp.emailRespuesta = usuario.Email;
            }
            return userResp;
        }
    }
}

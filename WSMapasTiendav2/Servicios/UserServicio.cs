using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WSMapasTiendav2.Models;
using WSMapasTiendav2.Models.commons;
using WSMapasTiendav2.Models.Peticiones;
using WSMapasTiendav2.Models.Respuestas;
using WSMapasTiendav2.Tools;

namespace WSMapasTiendav2.Servicios
{
    public class UserServicio : IUserServicio
    {
        //config AppSettigs donde esta el secreto
        private readonly AppSettings _appSettings;

        public UserServicio(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

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
                //Generar el token
                userResp.TokenRespuesta = GetToken(usuario);
            }
            return userResp;
        }
        //metodo que duevuelve el token
        private string GetToken(Usuario usuario)
        {
            var manejadorToken = new JwtSecurityTokenHandler();
            var llave = Encoding.ASCII.GetBytes(_appSettings.Secreto);
            var TokenDescriptor = new SecurityTokenDescriptor {

               Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                        new Claim(ClaimTypes.Email, usuario.Email)
                    }
                ),
                //opciones de expiracion
                Expires = DateTime.UtcNow.AddDays(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(llave),SecurityAlgorithms.HmacSha256Signature)            
            };
            var token = manejadorToken.CreateToken(TokenDescriptor);
            return manejadorToken.WriteToken(token);
        }

    }
}

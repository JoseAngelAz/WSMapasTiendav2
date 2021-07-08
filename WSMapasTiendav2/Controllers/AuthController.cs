using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSMapasTiendav2.Servicios;

namespace WSMapasTiendav2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IUserServicio _userServicio;

        public AuthController(IUserServicio userServicio)
        {

        }
    }
}

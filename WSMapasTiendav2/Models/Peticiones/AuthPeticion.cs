﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WSMapasTiendav2.Models.Peticiones
{
    public class AuthPeticion
    {
        [Required]
        public string nombre { get; set; }
        public string password { get; set; }
    }
}
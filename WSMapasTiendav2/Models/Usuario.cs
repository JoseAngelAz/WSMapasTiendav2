using System;
using System.Collections.Generic;

#nullable disable

namespace WSMapasTiendav2.Models
{
    public partial class Usuario
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Rol { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace EDWARDSTEWEN_GILBUITRAGO_PROYECTOFINAL.Data
{
    public partial class RolUsuario
    {
        public int Id { get; set; }
        public int? RolId { get; set; }
        public int? UsuarioId { get; set; }

        public virtual Rol Rol { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}

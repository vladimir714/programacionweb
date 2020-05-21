using System;
using System.Collections.Generic;

namespace EDWARDSTEWEN_GILBUITRAGO_PROYECTOFINAL.Data
{
    public partial class Rol
    {
        public Rol()
        {
            PermisosRol = new HashSet<PermisosRol>();
            RolUsuario = new HashSet<RolUsuario>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<PermisosRol> PermisosRol { get; set; }
        public virtual ICollection<RolUsuario> RolUsuario { get; set; }
    }
}

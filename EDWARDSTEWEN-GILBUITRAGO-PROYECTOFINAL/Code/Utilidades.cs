using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tienda_Online.Code
{
    public class Utilidades
    {
        public static bool TienePermisos(string permisos, string codfun)
        {
            bool result = false;

            if (permisos.Split(',').Contains(codfun))
            {
                result = true;
            }
            return result;
        }
    }
}

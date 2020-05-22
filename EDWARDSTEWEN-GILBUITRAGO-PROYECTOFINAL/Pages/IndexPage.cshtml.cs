using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace Prj_GestionPDC_AMTH.Pages
{
    public class IndexPageModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        private bool v_pais;
        private bool v_depto;
        private bool v_ciudad;


        public IndexPageModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            V_pais = Prj_GestionPDC_AMTH.Code.Utilidades.TienePermisos(_session.GetString("Permissions"), "V_PAIS");
            V_depto = Prj_GestionPDC_AMTH.Code.Utilidades.TienePermisos(_session.GetString("Permissions"), "V_DEPTO");
            V_ciudad = Prj_GestionPDC_AMTH.Code.Utilidades.TienePermisos(_session.GetString("Permissions"), "V_CIUDAD");
        }


        [BindProperty]
        public bool V_pais { get => v_pais; set => v_pais = value; }
        public bool V_depto { get => v_depto; set => v_depto = value; }
        public bool V_ciudad { get => v_ciudad; set => v_ciudad = value; }

        public void OnGet()
        {

        }
    }
}

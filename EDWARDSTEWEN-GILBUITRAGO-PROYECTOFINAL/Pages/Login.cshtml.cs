using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Prj_GestionPDC_AMTH.Entities;

namespace Prj_GestionPDC_AMTH
{
    public class LoginModel : PageModel
    {

        Login login_pg;

        private readonly Prj_GestionPDC_AMTH.Data.Prj_GestionPDC_AMTHContext _context;
        [BindProperty]
        public Login Login_pg { get => login_pg; set => login_pg = value; }

        public LoginModel(IHttpContextAccessor httpContextAccessor,Prj_GestionPDC_AMTH.Data.Prj_GestionPDC_AMTHContext context)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        
        public IActionResult OnGet()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return this.RedirectToPage("/IndexPage");
            }
            return this.Page();
        }

        public async Task<IActionResult> OnPostLogIn()
        {
            if (ModelState.IsValid)
            {
                var loginInfo = _context.RolUsuario
                    .Include(S => S.Rol)
                    .Include(S => S.Usuario)
                    .Where(s => s.Usuario.Usuario1 == Login_pg.Username.Trim() && s.Usuario.Password == Login_pg.Password.Trim()).ToList();
                
                if (loginInfo != null && loginInfo.Count() > 0)
                {
                    var logindetails = loginInfo.First();
                    
                    int rolid = logindetails.Rol.Id;

                    var lstpermissions = _context.PermisosRol.Where(s => s.RolId == rolid).ToList<PermisosRol>();

                    string strpermissions = "";
                    foreach (var perm in lstpermissions)
                    {
                        if (perm.Active==1)
                        {
                            strpermissions += perm.CodigoFuncion+",";
                        }
                    }

                    await this.SignInUser(logindetails.Usuario.Usuario1, false);
                    HttpContext.Session.SetString("UserName", logindetails.Usuario.Usuario1);
                    HttpContext.Session.SetString("RolName", logindetails.Rol.Nombre);
                    HttpContext.Session.SetString("Permissions", strpermissions);
                    //_session.SetString("Permissions", strpermissions);
                    return this.RedirectToPage("/IndexPage");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid username or password.");
                }
            }
            
            return this.Page();
        }

        private async Task SignInUser(string username, bool isPersistent)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, username));
            var claimIdenties = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimPrincipal = new ClaimsPrincipal(claimIdenties);
            var authenticationManager = Request.HttpContext;
            await authenticationManager.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimPrincipal, new AuthenticationProperties() { IsPersistent = isPersistent });
            
        }
    }

    public class Login
    {
          
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }
          
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

    }
}
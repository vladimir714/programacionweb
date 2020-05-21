using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using EDWARDSTEWEN_GILBUITRAGO_PROYECTOFINAL.servicios;
using EDWARDSTEWEN_GILBUITRAGO_PROYECTOFINAL.modelo;


namespace EDWARDSTEWEN_GILBUITRAGO_PROYECTOFINAL.Pages
{
    public class IndexModel : PageModel
    {

        ServicioGretingController servicioGretingController = new ServicioGretingController();

        [BindProperty]
        public modelo.gretingController Greting  { get; set; }

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
            Console.WriteLine("Standard Numeric Format Specifiers");
        }

        private void btnPrueba_Click(object sender, EventArgs e)
        {
            dynamic respuesta = servicioGretingController.Getgretting();
            Greting = respuesta.data[1].id.ToString();
            Console.WriteLine("Standard Numeric Format Specifiers");
        }


        public void OnGet()
        {
            Console.WriteLine("Standard Numeric Format Specifiers");
            Console.WriteLine("Standard Numeric Format Specifiers");
        }

       

  
    }
}

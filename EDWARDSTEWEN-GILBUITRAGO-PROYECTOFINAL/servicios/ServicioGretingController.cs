using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Windows;

namespace EDWARDSTEWEN_GILBUITRAGO_PROYECTOFINAL.servicios
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicioGretingController  
    {


        public dynamic Post(string url, string json, string autorizacion = null)
        {
            try
            {
                var client = new RestClient(url);
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", json, ParameterType.RequestBody);

                if (autorizacion != null)
                {
                    request.AddHeader("Authorization", autorizacion);
                }

                IRestResponse response = client.Execute(request);

                dynamic datos = JsonConvert.DeserializeObject(response.Content);

                return datos;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public dynamic Getgretting()
        {
            string url = $"https://seminario-segundo-corte.herokuapp.com/greeting";
            HttpWebRequest myWebRequest = (HttpWebRequest)WebRequest.Create(url);
            myWebRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/83.0.4103.61 Safari/537.36";
            //myWebRequest.CookieContainer = myCookie;
            myWebRequest.Credentials = CredentialCache.DefaultCredentials;
            myWebRequest.Proxy = null;
            HttpWebResponse myHttpWebResponse = (HttpWebResponse)myWebRequest.GetResponse();
            Stream myStream = myHttpWebResponse.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myStream);
            //Leemos los datos
            string Datos = HttpUtility.HtmlDecode(myStreamReader.ReadToEnd());

            dynamic data = JsonConvert.DeserializeObject(Datos);

            return data;
        }


     






    }
}
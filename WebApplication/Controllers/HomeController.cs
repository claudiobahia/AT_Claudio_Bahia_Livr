using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using RestSharp;
using Newtonsoft.Json;
using System;
using Editora.Domain;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {

        string _linkApi = "http://localhost:5000/api/";

        public HomeController()
        {

        }

        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrWhiteSpace(this.HttpContext.Session.GetString("Token")))
                return Redirect("Livro/Index");

            return View();
        }

        [HttpPost]
        public IActionResult Index(Usuario loginViewModel)
        {
            try
            {
                var client = new RestClient();
                var requestToken = new RestRequest(_linkApi + "usuario/Token");

                requestToken.AddJsonBody(JsonConvert.SerializeObject(loginViewModel));

                var result = client.Post<TokenResult>(requestToken).Data;


                if (result == null)
                {
                    ModelState.AddModelError(string.Empty, "Login ou senha inválidos");
                    return View(loginViewModel);
                }

                this.HttpContext.Session.SetString("Token", result.Token);

                return Redirect("Livro/Index");
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Ocorreu um erro, por favor tente mais tarde.");
                return View(loginViewModel);
            }
        }

        public class TokenResult
        {
            public String Token { get; set; }
        }
    }
}

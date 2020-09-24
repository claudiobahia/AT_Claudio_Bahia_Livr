using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Editora.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;

namespace WebApplication.Controllers
{
    public class LivroController : Controller
    {
        string _linkApi = "https://localhost:5001/api/";

        // GET: LivroController
        public async Task<ActionResult> Index()
        {
            var client = new RestClient();
            var request = new RestRequest(_linkApi + "livro", DataFormat.Json);

            request.AddHeader("Authorization", $"Bearer {this.HttpContext.Session.GetString("Token")}");

            var response = client.Get<List<Livro>>(request);

            if (response.Data == null)
                response.Data = new List<Livro>();

            return View(response.Data);

        }

        // GET: LivroController/Details/5
        public ActionResult Details(int id)
        {
            var client = new RestClient();
            var request = new RestRequest(_linkApi + "livro/" + id, DataFormat.Json);
            request.AddHeader("Authorization", $"Bearer {this.HttpContext.Session.GetString("Token")}");


            var response = client.Get<Livro>(request);

            return View(response.Data);
        }

        // GET: LivroController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LivroController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Livro livro)
        {
            try
            {
                if (ModelState.IsValid == false)
                    return View(livro);

                var client = new RestClient();
                var request = new RestRequest(_linkApi + "livro", DataFormat.Json);
                request.AddHeader("Authorization", $"Bearer {this.HttpContext.Session.GetString("Token")}");

                request.AddJsonBody(livro);

                var response = client.Post<Livro>(request);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Ocorreu um erro, verifique se o autor id esta correto.");
                return View(livro);
            }
        }

        // GET: LivroController/Edit/5
        public ActionResult Edit(int id)
        {
            var client = new RestClient();
            var request = new RestRequest(_linkApi + "livro/" + id, DataFormat.Json);
            request.AddHeader("Authorization", $"Bearer {this.HttpContext.Session.GetString("Token")}");


            var response = client.Get<Livro>(request);

            return View(response.Data);
        }

        // POST: LivroController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Livro livro)
        {
            try
            {
                if (ModelState.IsValid == false)
                    return View(livro);

                var client = new RestClient();
                var request = new RestRequest(_linkApi + "livro/" + id, DataFormat.Json);
                request.AddHeader("Authorization", $"Bearer {this.HttpContext.Session.GetString("Token")}");

                request.AddJsonBody(livro);

                var response = client.Put<Livro>(request);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LivroController/Delete/5
        public ActionResult Delete(int id)
        {
            var client = new RestClient();
            var request = new RestRequest(_linkApi + "livro/" + id, DataFormat.Json);
            request.AddHeader("Authorization", $"Bearer {this.HttpContext.Session.GetString("Token")}");


            var response = client.Get<Livro>(request);

            return View(response.Data);
        }

        // POST: LivroController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Livro livro)
        {
            try
            {
                var client = new RestClient();
                var request = new RestRequest(_linkApi + "livro/" + id, DataFormat.Json);
                request.AddHeader("Authorization", $"Bearer {this.HttpContext.Session.GetString("Token")}");

                request.AddJsonBody(livro);

                var response = client.Delete<Livro>(request);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

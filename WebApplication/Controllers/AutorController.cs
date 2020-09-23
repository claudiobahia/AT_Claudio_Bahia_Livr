using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Editora.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace WebApplication.Controllers
{
    public class AutorController : Controller
    {
        string _linkApi = "http://localhost:5000/api/";

        // GET: AutorController
        public ActionResult Index()
        {
            var client = new RestClient();
            var request = new RestRequest(_linkApi + "autor", DataFormat.Json);

            //Não esta adicionando o Token no Header Bearer
            request.AddHeader("Authorization", $"Bearer {this.HttpContext.Session.GetString("Token")}");

            var response = client.Get<List<Autor>>(request);

            if (response.Data == null)
                response.Data = new List<Autor>();

            return View(response.Data);
        }

        // GET: AutorController/Details/5
        public ActionResult Details(int id)
        {
            var client = new RestClient();
            var request = new RestRequest(_linkApi + "autor/" + id, DataFormat.Json);

            var response = client.Get<Autor>(request);

            return View(response.Data);
        }

        // GET: AutorController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AutorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Autor autor)
        {
            try
            {
                if (ModelState.IsValid == false)
                    return View(autor);

                var client = new RestClient();
                var request = new RestRequest(_linkApi + "autor", DataFormat.Json);
                request.AddJsonBody(autor);

                var response = client.Post<Autor>(request);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Ocorreu um erro, por favor tente mais tarde.");
                return View(autor);
            }
        }

        // GET: AutorController/Edit/5
        public ActionResult Edit(int id)
        {
            var client = new RestClient();
            var request = new RestRequest(_linkApi + "autor/" + id, DataFormat.Json);

            var response = client.Get<Autor>(request);

            return View(response.Data);
        }

        // POST: AutorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Autor autor)
        {
            try
            {
                if (ModelState.IsValid == false)
                    return View(autor);

                var client = new RestClient();
                var request = new RestRequest(_linkApi + "autor/" + id, DataFormat.Json);
                request.AddJsonBody(autor);

                var response = client.Put<Autor>(request);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AutorController/Delete/5
        public ActionResult Delete(int id)
        {
            var client = new RestClient();
            var request = new RestRequest(_linkApi + "autor/" + id, DataFormat.Json);

            var response = client.Get<Autor>(request);

            return View(response.Data);
        }

        // POST: AutorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Autor autor)
        {
            try
            {
                var client = new RestClient();
                var request = new RestRequest(_linkApi + "autor/" + id, DataFormat.Json);
                request.AddJsonBody(autor);

                var response = client.Delete<Autor>(request);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

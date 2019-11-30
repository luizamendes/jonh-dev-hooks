using Newtonsoft.Json.Linq;
using SocialNetwork.Web.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SocialNetwork.Web.Controllers
{
    public class AccountController : Controller
    {
        //GET /Account/Register
        public ActionResult Register()
        {
            return View();
        }

        //GET /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:24260");

                    var response = await client.PostAsJsonAsync("api/Account/Register", model);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        return View("Error");
                    }
                }
            }
            return View();
        }

        // GET: Account
        public ActionResult Login()
        {

            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var data = new Dictionary<string, string>
                {
                    {"grant_type", "password" },
                    {"username", model.Username },
                    {"password", model.Password }
                };

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:24260");

                    using (var requestContent = new FormUrlEncodedContent(data))
                    {
                        var response = await client.PostAsync("/Token", requestContent);

                        if (response.IsSuccessStatusCode)
                        {
                            var responseContent = await response.Content.ReadAsStringAsync();
                            var tokenData = JObject.Parse(responseContent);
                            Session.Add("access_token", tokenData["access_token"]);
                            Session.Add("user_email", model.Username);

                            return RedirectToAction("Index", "Home");
                        }
                        return View("Error");
                    }
                }
            }
            return View();
        }

        // POST api/Account/Logout
        // FAZER ROTA DE LOGOUT
    }
}
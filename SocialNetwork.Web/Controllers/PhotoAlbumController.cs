using Newtonsoft.Json;
using SocialNetwork.Core.Models;
using SocialNetwork.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SocialNetwork.Web.Controllers
{
    public class PhotoAlbumController : Controller
    {
        // GET: PhotoAlbum/Index
        /* https://www.tutorialsteacher.com/webapi/consume-web-api-get-method-in-aspnet-mvc */
        public ActionResult Index()
        {
            IEnumerable<PhotoAlbumViewModel> photoAlbums = null;
            string access_token = Session["access_token"]?.ToString();

            if (!string.IsNullOrEmpty(access_token))
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:24260");
                    client.DefaultRequestHeaders.Accept.Clear();

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"(access_token");

                    var responseTask = client.GetAsync("/api/PhotoAlbums");
                    responseTask.Wait();

                    var result = responseTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<IList<PhotoAlbumViewModel>>();
                        readTask.Wait();
                        photoAlbums = readTask.Result;
                        
                        return View(photoAlbums);
                    }

                    return View("Error");
                }
            }

            return View();
        }

        // GET: PhotoAlbum/Details/5
        public ActionResult Details(int id)
        {
            PhotoAlbumViewModel album = null;
            string access_token = Session["access_token"]?.ToString();

            if (!string.IsNullOrEmpty(access_token))
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:24260");
                    client.DefaultRequestHeaders.Accept.Clear();

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"(access_token");

                    var responseTask = client.GetAsync("/api/PhotoAlbums/" + id);
                    responseTask.Wait();

                    var result = responseTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<PhotoAlbumViewModel>();
                        readTask.Wait();
                        album = readTask.Result;

                        return View(album);
                    }

                    return View("Error");
                }
            }

            return RedirectToAction("Login", "Account", null);
        }

        // GET: PhotoAlbum/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PhotoAlbum/Create
        [HttpPost]
        public async Task<ActionResult> Create(PhotoAlbumViewModel model)
        {
            string access_token = Session["access_token"]?.ToString();

            if (string.IsNullOrEmpty(access_token))
            {
                return RedirectToAction("Login", "Account", null);
            }

            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:24260/");
                    client.DefaultRequestHeaders.Accept.Clear();

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"(access_token");

                    var response = await client.PostAsJsonAsync("/api/PhotoAlbums", model);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index", "PhotoAlbum");
                    }
                    else
                    {
                        return View("Error");
                    }
                }
            }

            return View();
        }

        // GET: PhotoAlbum/Edit/5
        public ActionResult Edit(int id)
        {
            PhotoAlbumViewModel album = null;
            string access_token = Session["access_token"]?.ToString();

            if (!string.IsNullOrEmpty(access_token))
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:24260");
                    client.DefaultRequestHeaders.Accept.Clear();

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"(access_token");

                    var responseTask = client.GetAsync("/api/PhotoAlbums/" + id);
                    responseTask.Wait();

                    var result = responseTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<PhotoAlbumViewModel>();
                        readTask.Wait();
                        album = readTask.Result;

                        return View(album);
                    }

                    return View("Error");
                }
            }

            return RedirectToAction("Login", "Account", null);
        }

        // POST: PhotoAlbum/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, PhotoAlbumViewModel model)
        {
            string access_token = Session["access_token"]?.ToString();

            if (string.IsNullOrEmpty(access_token))
            {
                return RedirectToAction("Login", "Account", null);
            }

            PhotoAlbumViewModel album = new PhotoAlbumViewModel()
            {
                PhotoAlbumId = id,
                AlbumName = model.AlbumName,
                Description = model.Description
            };

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:24260");

                var putTask = client.PutAsJsonAsync<PhotoAlbumViewModel>($"api/PhotoAlbums/{id}", album);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                } else
                {
                    return View("Error");
                }
            }            
        }

        // GET: PhotoAlbum/Delete/5
        public ActionResult Delete(int id)
        {
            PhotoAlbumViewModel album = null;
            string access_token = Session["access_token"]?.ToString();

            if (!string.IsNullOrEmpty(access_token))
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:24260");
                    client.DefaultRequestHeaders.Accept.Clear();

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"(access_token");

                    var responseTask = client.GetAsync("/api/PhotoAlbums/" + id);
                    responseTask.Wait();

                    var result = responseTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<PhotoAlbumViewModel>();
                        readTask.Wait();
                        album = readTask.Result;

                        return View(album);
                    }

                    return View("Error");
                }
            }

            return RedirectToAction("Login", "Account", null);
        }

        // POST: PhotoAlbum/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, PhotoAlbumViewModel model)
        {
            string access_token = Session["access_token"]?.ToString();

            if (string.IsNullOrEmpty(access_token))
            {
                return RedirectToAction("Login", "Account", null);
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:24260/");

                var deleteTask = client.DeleteAsync("/api/PhotoAlbums/" + id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                } else
                {
                    return View("Error");
                }
            }
        }
    }
}

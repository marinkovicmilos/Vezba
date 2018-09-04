using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ASPNET_MVC_Samples.Models;
using Newtonsoft.Json;

namespace ASPNET_MVC_Samples.Controllers
{
    public class LoyaltiesController : Controller
    {
        private string _baseUrl = "http://localhost:52700/";

        // GET: Loyalties
        public async Task<ActionResult> Index()
        {
            List<Loyalty> listLoyalties = new List<Loyalty>();

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(_baseUrl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllLoyalties using HttpClient  
                HttpResponseMessage response = await client.GetAsync("api/Loyalties");

                //Checking the response is successful or not which is sent using HttpClient  
                if (response.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var dataResponse = response.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Loyalty list  
                    listLoyalties = JsonConvert.DeserializeObject<List<Loyalty>>(dataResponse);

                }
                //returning the Loyalty list to view  
                return View(listLoyalties);
            }

            //return View(new List<Loyalty>());
        }

        // GET: Loyalties/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Loyalties/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        // POST: Loyalties/Create
        [HttpPost]
        public JsonResult Create(Loyalty loyalty)
        {
            try
            {
                if (string.IsNullOrEmpty(loyalty.Name) || string.IsNullOrEmpty(loyalty.Surname))
                    return Json(new { error = true, errorMessage = "Name and Surname are required" }, JsonRequestBehavior.AllowGet);

                HttpClient httpClient = new HttpClient { BaseAddress = new Uri(_baseUrl) };
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = httpClient.PostAsJsonAsync("api/Loyalties", loyalty).Result;

                if (response.IsSuccessStatusCode)
                {
                    return Json(new { error = false }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { error = true, errorMessage = "Not created" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = true, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Loyalties/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Loyalties/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Loyalties/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Loyalties/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

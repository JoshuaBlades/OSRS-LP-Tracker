using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;

namespace OSRSBossTracker.Controllers
{
    public class StatsController : Controller
    {
        // GET: Stats
        public ActionResult Index()
        {
            return View();
        }

        // GET: Stats/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Stats/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Stats/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Stats/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Stats/Edit/5
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

        // GET: Stats/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Stats/Delete/5
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

        /// <summary>
        /// Gets user stats from OSRS API
        /// </summary>
        /// <param name="ign"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetStats(string ign)
        {
            // Create a new instance of the model
            Models.UsersModel User = new Models.UsersModel();

            // Create dynamic API URL
            string apiUrl = "https://secure.runescape.com/m=hiscore_oldschool/index_lite.ws?player=" + ign;

            // Create Web Request for the API
            WebRequest request = WebRequest.Create(apiUrl);

            // Sort any credentials
            request.Credentials = CredentialCache.DefaultCredentials;

            // Create WebResponse 
            WebResponse response;

            // Try search for username
            try
            {
                // Get the response from the server
                 response = request.GetResponse();
            }
            catch
            {
                User.Name = "User not found";
                return View("Index", User);
            }

            // Create string response from server
            string responseFromServer = "";

            // Read the data from the API
            using (Stream dataStream = response.GetResponseStream())
            {
                // Open the stream using a StreamReader for easy access.  
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.  
                responseFromServer = reader.ReadToEnd();
            }

            // Close the response.  
            response.Close();

            // Split stats data 
            string[] lines = responseFromServer.Split(new[] { ",", "\n"}, StringSplitOptions.RemoveEmptyEntries);

            // Set the model name
            User.Name = ign;

            // Set the model stats
            foreach (string s in lines)
            {
                User.Stats.Add(s);
            }

            // Return the data to the correct view
            return View("Index", User);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;

namespace TestNumbersAPI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // Step 1: Build and Make a request to API
            HttpWebRequest APIRequest = WebRequest.CreateHttp("https://numbersapi.p.mashape.com/1492/year?fragment=true&json=true");
            
            // used to add keys [NEVER PUT KEYS INSIDE OF CODE]
            APIRequest.Headers.Add("X-Mashape-Key", ConfigurationManager.AppSettings["X-Mashape-Key"]); 
            
            // used to assign UserAgent
            APIRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:47.0) Gecko/20100101 Firefox/47.0";

            // Step 2:
            HttpWebResponse APIResponse = (HttpWebResponse)APIRequest.GetResponse();
            
            // if we got a status code == 200
            if (APIResponse.StatusCode == HttpStatusCode.OK)
            {
                // get data and then parse
                StreamReader ResponseData = new StreamReader(APIResponse.GetResponseStream());

                //reads data from response
                string Trivia = ResponseData.ReadToEnd();

                // *To-Do: Parse data
                JObject JsonTrivia = JObject.Parse(Trivia);

                ViewBag.Trivia = JsonTrivia["text"];
                ViewBag.TriviaDate = JsonTrivia["date"];
            }

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
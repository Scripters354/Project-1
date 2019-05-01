using MvC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace MvC.Controllers
{
    public class AboutController : Controller
    {
        // GET: About
        public ActionResult Index()
        {
            IEnumerable<mvcAboutModel> aboutList;
            HttpResponseMessage response = GlobalVariables.WepApiClient.GetAsync("About_Malaria").Result;
            aboutList = response.Content.ReadAsAsync<IEnumerable<mvcAboutModel>>().Result; 
            return View(aboutList);
        }
        
       
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View( new mvcAboutModel());
            else
            {
                HttpResponseMessage response = GlobalVariables.WepApiClient.GetAsync("About_Malaria/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<mvcAboutModel>().Result);
            }
        }

        [HttpPost]
        public ActionResult AddOrEdit(mvcAboutModel abt) //passing an object of type mvcaboutmodel
        {
            if (abt.About_Malaria_ID == 0)
            {
                HttpResponseMessage response = GlobalVariables.WepApiClient.PostAsJsonAsync("About_Malaria", abt).Result;
                TempData["SuccessMessage"] = "Saved Successfully";
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WepApiClient.PutAsJsonAsync("About_Malaria/" + abt.About_Malaria_ID, abt).Result;
                TempData["SuccessMessage"] = "Updated Successfully";
            }
            return RedirectToAction("Index");
        }
        
        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariables.WepApiClient.DeleteAsync("About_Malaria/" + id.ToString()).Result;
            TempData["SuccessMessage"] = "Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}
using MvC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace MvC.Controllers
{
    [Authorize]
    public class mvcSymptomController : Controller
    {
        // GET: mvcSymptom
        public ActionResult Index()
        {
            IEnumerable<mvcSymptomModel> signList;
            HttpResponseMessage response = GlobalVariables.WepApiClient.GetAsync("Symptom").Result;
            signList = response.Content.ReadAsAsync<IEnumerable<mvcSymptomModel>>().Result;
            return View(signList);
        }

        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new mvcSymptomModel());
            else
            {
                HttpResponseMessage response = GlobalVariables.WepApiClient.GetAsync("Symptom/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<mvcSymptomModel>().Result);
            }
        }

        [HttpPost]
        public ActionResult AddOrEdit(mvcSymptomModel pst) //passing an object of type mvcModel
        {
            if (pst.Symptom_Sign_ID == 0)
            {
                HttpResponseMessage response = GlobalVariables.WepApiClient.PostAsJsonAsync("Symptom", pst).Result;
                TempData["SuccessMessage"] = "Saved Successfully";
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WepApiClient.PutAsJsonAsync("Symptom/" + pst.Symptom_Sign_ID, pst).Result;
                TempData["SuccessMessage"] = "Updated Successfully";
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariables.WepApiClient.DeleteAsync("Symptom/" + id.ToString()).Result;
            TempData["SuccessMessage"] = "Deleted Successfully";
            return RedirectToAction("Index");
        }

    }
}
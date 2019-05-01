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
    public class mvcTreatmentController : Controller
    {
        // GET: mvcTreatment
        public ActionResult Index()
        {
            IEnumerable<mvcTreatmentModel> treatList;
            HttpResponseMessage response = GlobalVariables.WepApiClient.GetAsync("Treatment").Result;
            treatList = response.Content.ReadAsAsync<IEnumerable<mvcTreatmentModel>>().Result;
            return View(treatList);
        }

        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new mvcTreatmentModel());
            else
            {
                HttpResponseMessage response = GlobalVariables.WepApiClient.GetAsync("Treatment/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<mvcTreatmentModel>().Result);
            }
        }

        [HttpPost]
        public ActionResult AddOrEdit(mvcTreatmentModel pst) //passing an object of type mvcModel
        {
            if (pst.Treatment_ID == 0)
            {
                HttpResponseMessage response = GlobalVariables.WepApiClient.PostAsJsonAsync("Treatment", pst).Result;
                TempData["SuccessMessage"] = "Saved Successfully";
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WepApiClient.PutAsJsonAsync("Treatment/" + pst.Treatment_ID, pst).Result;
                TempData["SuccessMessage"] = "Updated Successfully";
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariables.WepApiClient.DeleteAsync("Treatment/" + id.ToString()).Result;
            TempData["SuccessMessage"] = "Deleted Successfully";
            return RedirectToAction("Index");
        }

    }
}
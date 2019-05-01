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
    public class mvcCauseController : Controller
    {
        // GET: mvcCause
        public ActionResult Index()
        {
            IEnumerable<mvcCauseModel> causeList;
            HttpResponseMessage response = GlobalVariables.WepApiClient.GetAsync("Cause").Result;
            causeList = response.Content.ReadAsAsync<IEnumerable<mvcCauseModel>>().Result;
            return View(causeList);
            
        }

        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new mvcCauseModel());
            else
            {
                HttpResponseMessage response = GlobalVariables.WepApiClient.GetAsync("Cause/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<mvcCauseModel>().Result);
            }
        }

        [HttpPost]
        public ActionResult AddOrEdit(mvcCauseModel pst) //passing an object of type mvcModel
        {
            if (pst.Cause_ID == 0)
            {
                HttpResponseMessage response = GlobalVariables.WepApiClient.PostAsJsonAsync("Cause", pst).Result;
                TempData["SuccessMessage"] = "Saved Successfully";
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WepApiClient.PutAsJsonAsync("Cause/" + pst.Cause_ID, pst).Result;
                TempData["SuccessMessage"] = "Updated Successfully";
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariables.WepApiClient.DeleteAsync("Cause/" + id.ToString()).Result;
            TempData["SuccessMessage"] = "Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}
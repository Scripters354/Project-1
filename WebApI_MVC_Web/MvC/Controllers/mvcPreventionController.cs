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
    public class mvcPreventionController : Controller
    {
        // GET: mvcPrevention
        public ActionResult Index()
        {
            IEnumerable<mvcPreventionModel> preventList;
            HttpResponseMessage response = GlobalVariables.WepApiClient.GetAsync("Prevention").Result;
            preventList = response.Content.ReadAsAsync<IEnumerable<mvcPreventionModel>>().Result;
            return View(preventList);
        }
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new mvcPreventionModel());
            else
            {
                HttpResponseMessage response = GlobalVariables.WepApiClient.GetAsync("Prevention/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<mvcPreventionModel>().Result);
            }
        }

        [HttpPost]
        public ActionResult AddOrEdit(mvcPreventionModel pst) //passing an object of type mvcModel
        {
            if (pst.Prevention_ID == 0)
            {
                HttpResponseMessage response = GlobalVariables.WepApiClient.PostAsJsonAsync("Prevention", pst).Result;
                TempData["SuccessMessage"] = "Saved Successfully";
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WepApiClient.PutAsJsonAsync("Prevention/" + pst.Prevention_ID, pst).Result;
                TempData["SuccessMessage"] = "Updated Successfully";
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariables.WepApiClient.DeleteAsync("Prevention/" + id.ToString()).Result;
            TempData["SuccessMessage"] = "Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}
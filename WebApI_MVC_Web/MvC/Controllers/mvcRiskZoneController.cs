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
    public class mvcRiskZoneController : Controller
    {
        // GET: mvcRiskZone
        public ActionResult Index()
        {
            IEnumerable<mvcRiskZonesModel> zoneList;
            HttpResponseMessage response = GlobalVariables.WepApiClient.GetAsync("Risk_Zone").Result;
            zoneList = response.Content.ReadAsAsync<IEnumerable<mvcRiskZonesModel>>().Result;
            return View(zoneList);
        }

        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new mvcRiskZonesModel());
            else
            {
                HttpResponseMessage response = GlobalVariables.WepApiClient.GetAsync("Risk_Zone/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<mvcRiskZonesModel>().Result);
            }
        }

        [HttpPost]
        public ActionResult AddOrEdit(mvcRiskZonesModel pst) //passing an object of type mvcModel
        {
            if (pst.Malaria_Risk_Zone_ID == 0)
            {
                HttpResponseMessage response = GlobalVariables.WepApiClient.PostAsJsonAsync("Risk_Zone", pst).Result;
                TempData["SuccessMessage"] = "Saved Successfully";
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WepApiClient.PutAsJsonAsync("Risk_Zone/" + pst.Malaria_Risk_Zone_ID, pst).Result;
                TempData["SuccessMessage"] = "Updated Successfully";
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariables.WepApiClient.DeleteAsync("Risk_Zone/" + id.ToString()).Result;
            TempData["SuccessMessage"] = "Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}
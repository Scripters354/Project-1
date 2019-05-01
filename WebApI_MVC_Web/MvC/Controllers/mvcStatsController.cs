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
    public class mvcStatsController : Controller
    {
        // GET: mvcStats
        public ActionResult Index()
        {
            IEnumerable<mvcStaticModel> statList;
            HttpResponseMessage response = GlobalVariables.WepApiClient.GetAsync("Statistics").Result;
            statList = response.Content.ReadAsAsync<IEnumerable<mvcStaticModel>>().Result;
            return View(statList);
        }

        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new mvcStaticModel());
            else
            {
                HttpResponseMessage response = GlobalVariables.WepApiClient.GetAsync("Statistics/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<mvcStaticModel>().Result);
            }
        }

        [HttpPost]
        public ActionResult AddOrEdit(mvcStaticModel pst) //passing an object of type mvcModel
        {
            if (pst.Statistic_ID == 0)
            {
                HttpResponseMessage response = GlobalVariables.WepApiClient.PostAsJsonAsync("Statistics", pst).Result;
                TempData["SuccessMessage"] = "Saved Successfully";
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WepApiClient.PutAsJsonAsync("Statistics/" + pst.Statistic_ID, pst).Result;
                TempData["SuccessMessage"] = "Updated Successfully";
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariables.WepApiClient.DeleteAsync("Statistics/" + id.ToString()).Result;
            TempData["SuccessMessage"] = "Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}
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
    public class mvcPublicationController : Controller
    {
        // GET: mvcPublication
        public ActionResult Index()
        {
            IEnumerable<mvcPublicationModel> pubList;
            HttpResponseMessage response = GlobalVariables.WepApiClient.GetAsync("Publication").Result;
            pubList = response.Content.ReadAsAsync<IEnumerable<mvcPublicationModel>>().Result;
            return View(pubList);
        }

        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new mvcPublicationModel());
            else
            {
                HttpResponseMessage response = GlobalVariables.WepApiClient.GetAsync("Publication/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<mvcPublicationModel>().Result);
            }
        }

        [HttpPost]
        public ActionResult AddOrEdit(mvcPublicationModel pst) //passing an object of type mvcModel
        {
            if (pst.Publication_ID == 0)
            {
                HttpResponseMessage response = GlobalVariables.WepApiClient.PostAsJsonAsync("Publication", pst).Result;
                TempData["SuccessMessage"] = "Saved Successfully";
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WepApiClient.PutAsJsonAsync("Publication/" + pst.Publication_ID, pst).Result;
                TempData["SuccessMessage"] = "Updated Successfully";
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariables.WepApiClient.DeleteAsync("Publication/" + id.ToString()).Result;
            TempData["SuccessMessage"] = "Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}
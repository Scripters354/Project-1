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
    public class mvcFAQController : Controller
    {
        // GET: mvcFAQ
        public ActionResult Index()
        {
            IEnumerable<mvcFAQModel> faqList;
            HttpResponseMessage response = GlobalVariables.WepApiClient.GetAsync("FAQ").Result;
            faqList = response.Content.ReadAsAsync<IEnumerable<mvcFAQModel>>().Result;
            return View(faqList);
        }

        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new mvcFAQModel());
            else
            {
                HttpResponseMessage response = GlobalVariables.WepApiClient.GetAsync("FAQ/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<mvcFAQModel>().Result);
            }
        }

        [HttpPost]
        public ActionResult AddOrEdit(mvcFAQModel pst) //passing an object of type mvcModel
        {
            if (pst.FAQ_ID == 0)
            {
                HttpResponseMessage response = GlobalVariables.WepApiClient.PostAsJsonAsync("FAQ", pst).Result;
                TempData["SuccessMessage"] = "Saved Successfully";
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WepApiClient.PutAsJsonAsync("FAQ/" + pst.FAQ_ID, pst).Result;
                TempData["SuccessMessage"] = "Updated Successfully";
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariables.WepApiClient.DeleteAsync("FAQ/" + id.ToString()).Result;
            TempData["SuccessMessage"] = "Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}
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
    public class mvcHelpController : Controller
    {
        // GET: mvcHelp
            public ActionResult Index()
            {
                IEnumerable<mvcHelpModel> helpList;
                HttpResponseMessage response = GlobalVariables.WepApiClient.GetAsync("Help_Line").Result;
                helpList = response.Content.ReadAsAsync<IEnumerable<mvcHelpModel>>().Result;
                return View(helpList);
            }

        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new mvcHelpModel());
            else
            {
                HttpResponseMessage response = GlobalVariables.WepApiClient.GetAsync("Help_Line/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<mvcHelpModel>().Result);
            }
        }

        [HttpPost]
        public ActionResult AddOrEdit(mvcHelpModel pst) //passing an object of type mvcModel
        {
            if (pst.Help_Line_ID == 0)
            {
                HttpResponseMessage response = GlobalVariables.WepApiClient.PostAsJsonAsync("Help_Line", pst).Result;
                TempData["SuccessMessage"] = "Saved Successfully";
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WepApiClient.PutAsJsonAsync("Help_Line/" + pst.Help_Line_ID, pst).Result;
                TempData["SuccessMessage"] = "Updated Successfully";
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariables.WepApiClient.DeleteAsync("Help_Line/" + id.ToString()).Result;
            TempData["SuccessMessage"] = "Deleted Successfully";
            return RedirectToAction("Index");
        }



    }
}
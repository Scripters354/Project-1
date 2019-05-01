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
    public class PostUpdateController : Controller
    {
        // GET: PostUpdate
        public ActionResult Index()
        {
            IEnumerable<mvcPostUpdateModel> postList; 
            HttpResponseMessage response = GlobalVariables.WepApiClient.GetAsync("Post_Update").Result;  
            postList = response.Content.ReadAsAsync<IEnumerable<mvcPostUpdateModel>>().Result;
            return View(postList);
        }


        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new mvcPostUpdateModel());
            else
            {
                HttpResponseMessage response = GlobalVariables.WepApiClient.GetAsync("Post_Update/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<mvcPostUpdateModel>().Result);
            }
        }

        [HttpPost]
        public ActionResult AddOrEdit(mvcPostUpdateModel pst) //passing an object of type mvcModel
        {
            if (pst.Post_Update_ID == 0)
            {
                HttpResponseMessage response = GlobalVariables.WepApiClient.PostAsJsonAsync("Post_Update", pst).Result;
                TempData["SuccessMessage"] = "Saved Successfully";
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WepApiClient.PutAsJsonAsync("Post_Update/" + pst.Post_Update_ID, pst).Result;
                TempData["SuccessMessage"] = "Updated Successfully";
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariables.WepApiClient.DeleteAsync("Post_Update/" + id.ToString()).Result;
            TempData["SuccessMessage"] = "Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace MvC
{
    public static class GlobalVariables
    { 
        public static HttpClient WepApiClient = new HttpClient(); 

       static GlobalVariables()
        {
            WepApiClient.BaseAddress = new Uri("http://localhost:58619/api/");
            WepApiClient.DefaultRequestHeaders.Clear();
            WepApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

    }
}
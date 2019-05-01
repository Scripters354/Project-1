using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace MvC
{
    public  class AuthorizationVariable
    {
        HttpClientHandler handler = new HttpClientHandler();
        HttpClient client = new HttpClient();
         
         AuthorizationVariable()
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization","Jeff:password ");
            var result = client.GetAsync(new Uri("http://localhost:58619/api/UserAuth/")).Result;
        }
       
      
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using WebApI.Models;

namespace WebApI
{
    public class AuthorizeAccess
    {
        public User CheckCredential(string username, string password)
        {
        
            using (var ctx = new DBMobileAppEntities())
            {
                return ctx.Users.Where(un => un.User_Name == username && un.User_Password == password).FirstOrDefault();
            }
        }
    }

    public class AuthenticationHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                var tokens = request.Headers.GetValues("Authorization").FirstOrDefault();
                if (tokens != null)
                {
                    byte[] data = Convert.FromBase64String(tokens);
                    string decodedString = Encoding.UTF8.GetString(data);
                    string[] tokensValues = decodedString.Split(':');

                    User ObjUser = new AuthorizeAccess().CheckCredential(tokensValues[0], tokensValues[1]);
                    if (ObjUser != null)
                    {
                        IPrincipal principal = new GenericPrincipal(new GenericIdentity(ObjUser.User_Name), ObjUser.User_Name.Split(','));
                        Thread.CurrentPrincipal = principal;
                        HttpContext.Current.User = principal;
                    }
                    else
                    {
                        //The user is unauthorize and return 401 status  
                        var response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                        var tsc = new TaskCompletionSource<HttpResponseMessage>();
                        tsc.SetResult(response);
                        return tsc.Task;
                    }
                }
                else
                {
                    //Bad Request request because Authentication header is set but value is null  
                    var response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                    var tsc = new TaskCompletionSource<HttpResponseMessage>();
                    tsc.SetResult(response);
                    return tsc.Task;
                }
                return base.SendAsync(request, cancellationToken);
            }
            catch
            {
                //User did not set Authentication header  
                var response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                var tsc = new TaskCompletionSource<HttpResponseMessage>();
                tsc.SetResult(response);
                return tsc.Task;
            }
        }

    }
}  

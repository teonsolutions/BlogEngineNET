using BlogEngine.Application.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace BlogEngine.Application.Services
{
    public class SessionService: ISessionService
    {
        private readonly IHttpContextAccessor http;
        public SessionService(IHttpContextAccessor http)
        {
            this.http = http;
        }

        public SessionInfoResponse GetSession()
        {
            SessionInfoResponse session = new SessionInfoResponse();
            session.UserID = ClaimValue<int>("UserID");
            session.UserLogin = ClaimValue<string>("UserLogin");
            session.FullName = ClaimValue<string>("FullName");
            session.RolGuid = ClaimValue<string>("RolGuid");
            return session;
        }

        private T ClaimValue<T>(string claimKey)
        {
            string valor = (((ClaimsIdentity)this.http.HttpContext.User.Identity).FindFirst(claimKey) != null) ? ((ClaimsIdentity)this.http.HttpContext.User.Identity).FindFirst(claimKey).Value : "";
            return (valor != "") ? (T)Convert.ChangeType(valor, typeof(T)) : default(T);
        }
    }
}

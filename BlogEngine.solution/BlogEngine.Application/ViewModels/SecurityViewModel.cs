using System;
using System.Collections.Generic;
using System.Text;

namespace BlogEngine.Application.ViewModels
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public bool Success { get; set; }
        public Guid RolGuid { get; set; }
        public string UserLogin { get; set; }
        public string FullName { get; set; }
        public string RolName { get; set; }
    }

    public class TokenJwtRequest
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
    }

    public class UserLoginResponse
    {
        public int UserID { get; set; }
        public string UserLogin { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
    }

    public class AuthorizedResponse
    {
        public bool IsAuthorized { get; set; }
        public string Message { get; set; }
    }

    public class RolUserReponse
    {
        public int RolUserID { get; set; }
        public int RolID { get; set; }
        public int UserID { get; set; }
        public Guid RolGuid { get; set; }
        public string RolName { get; set; }
    }

    public class MenuResponse
    {
        public int MenuID { get; set; }
        public int? MenuBaseID { get; set; }
        public Guid Guid { get; set; }
        public string Url { get; set; }
    }

    public class PermissionResponse
    {
        public Guid MenuGuid { get; set; }
        public Guid RolGuid { get; set; }
        public string ActionCode { get; set; }
    }

    public class AuthorizationInfoResponse
    {
        public IEnumerable<MenuResponse> Menus { get; set; }
        public IEnumerable<PermissionResponse> Permissions { get; set; }
    }

    public class SessionInfoResponse
    {
        public int UserID { get; set; }
        public string UserLogin { get; set; }
        public string FullName { get; set; }
        public string RolGuid { get; set; }
    }
}

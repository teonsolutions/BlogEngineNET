using BlogEngine.Application.ViewModels;
using BlogEngine.Common;
using BlogEngine.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogEngine.Application.Queries
{
    public class SecurityQueries : ISecurityQueries
    {
        private readonly BlogEngineContext _context;

        public SecurityQueries(BlogEngineContext context)
        {
            _context = context;
        }

        public async Task<UserLoginResponse> GetUserInfoAsync(string userLogin)
        {
            var query = _context.Users.AsNoTracking();
            var reponse = query.Where(row => row.UserLogin.Equals(userLogin))
                               .Select(row => new UserLoginResponse
                               {
                                   UserID = row.Id,
                                   UserLogin = row.UserLogin,
                                   Password = row.Password,
                                   FullName = row.FullName,
                               }).FirstOrDefault();
            return await Task.FromResult(reponse);
        }

        public async Task<RolUserReponse> GetUserRolAsync(int userID)
        {
            var rolUsers = _context.RolUsers.AsNoTracking();
            var rols = _context.Rols.AsNoTracking();
            var result = (from ru in rolUsers
                          join r in rols on ru.RolID equals r.Id
                          where ru.UserID.Equals(userID)
                          select new RolUserReponse
                          {
                              RolGuid = r.Guid,
                              RolName = r.RolName
                          }).FirstOrDefault();

            return await Task.FromResult(result);
        }

        public async Task<AuthorizedResponse> GetAuthorizedAsync(Guid menuGuid, Guid rolGuid, string actionCode)
        {
            AuthorizedResponse response = new AuthorizedResponse();
            var permissions = _context.Permissions.AsNoTracking();
            var menus = _context.Menus.AsNoTracking();
            var rols = _context.Rols.AsNoTracking();
            var result = (from p in permissions
                          join m in menus on p.MenuID equals m.Id
                          join r in rols on p.RolID equals r.Id
                          where m.Guid.Equals(menuGuid) &&
                          r.Guid.Equals(rolGuid) &&
                          p.ActionCode.Equals(actionCode) &&
                          p.IsActive
                          select new { p }
                         ).FirstOrDefault();

            response.IsAuthorized = result != null;
            return await Task.FromResult(response);
        }

        public async Task<AuthorizationInfoResponse> GetAuthorizationInfoAsync(int UserID, Guid rolGuid)
        {
            AuthorizationInfoResponse response = new AuthorizationInfoResponse();
            var permissions = _context.Permissions.AsNoTracking();
            var menus = _context.Menus.AsNoTracking();
            var rols = _context.Rols.AsNoTracking();

            var permissionList = (from p in permissions
                                  join m in menus on p.MenuID equals m.Id
                                  join r in rols on p.RolID equals r.Id
                                  where r.Guid.Equals(rolGuid) && p.IsActive
                                  select new PermissionResponse
                                  {
                                      MenuGuid = m.Guid,
                                      RolGuid = r.Guid,
                                      ActionCode = p.ActionCode
                                  }
                         ).ToList();

            var menuList = menus.Select(x => new MenuResponse
            {
                MenuID = x.Id,
                MenuBaseID = x.MenuBaseID,
                Guid = x.Guid,
                Url = x.Url
            }).ToList();

            response.Menus = menuList;
            response.Permissions = permissionList;

            return await Task.FromResult(response);
        }
    }
}

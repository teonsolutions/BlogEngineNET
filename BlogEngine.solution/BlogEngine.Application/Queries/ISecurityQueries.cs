using BlogEngine.Application.ViewModels;
using BlogEngine.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlogEngine.Application.Queries
{
    public interface ISecurityQueries
    {
        Task<UserLoginResponse> GetUserInfoAsync(string userLogin);
        Task<RolUserReponse> GetUserRolAsync(int userID);
        Task<AuthorizedResponse> GetAuthorizedAsync(Guid menuGuid, Guid rolGuid, string actionCode);
        Task<AuthorizationInfoResponse> GetAuthorizationInfoAsync(int UserID, Guid rolGuid);
    }
}

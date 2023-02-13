using BlogEngine.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogEngine.Application.Services
{
    public interface ITokenService
    {
        string GenerateJwtLogin(int userID, string userLogin, string fullName, Guid rolGuid);
    }
}

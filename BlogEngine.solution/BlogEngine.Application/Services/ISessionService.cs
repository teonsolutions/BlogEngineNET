using BlogEngine.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogEngine.Application.Services
{
    public interface ISessionService
    {
        SessionInfoResponse GetSession();
    }
}

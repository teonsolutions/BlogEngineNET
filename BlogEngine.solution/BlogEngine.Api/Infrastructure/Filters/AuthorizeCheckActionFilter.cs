using BlogEngine.Application.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlogEngine.Api.Infrastructure.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class AuthorizeCheckActionFilter : TypeFilterAttribute
    {
        public AuthorizeCheckActionFilter(ActionCode accessType = ActionCode.NONE) : base(typeof(AuthorizeCheckActionImplFilter))
        {
            Arguments = new object[] { accessType };
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class AuthorizeCheckActionImplFilter : ActionFilterAttribute
    {
        public string _urlAuthorization;
        public IConfiguration _configuration;
        private readonly ActionCode _accessType;
        private readonly ISecurityQueries _securityQueries;
        public AuthorizeCheckActionImplFilter(ISecurityQueries securityQueries, IConfiguration configuration, ActionCode accessType = ActionCode.NONE)
        {
            _securityQueries = securityQueries;
            _configuration = configuration;
            _accessType = accessType;
        }
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            string token, rolGuid, menuGuid, actionCode;

            var httpRequest = actionContext.HttpContext.Request;
            var swagger = httpRequest.Headers["Referer"];
            if (swagger.Count > 0 && swagger[0].Contains("swagger"))
            {
                if (!TryRetrieveToken(actionContext.HttpContext.Request))
                {
                    var mensajeJson = new JObject();
                    mensajeJson["HasErrors"] = true;
                    mensajeJson["Message"] = "Unauthorized request";
                    actionContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    actionContext.Result = new JsonResult(new { HttpStatusCode.Unauthorized, mensajeJson });
                }
            }
            else if (TryRetrieveToken(actionContext.HttpContext.Request, out token, out rolGuid, out menuGuid, out actionCode))
            {
                if (_accessType != ActionCode.NONE)
                {
                    actionCode = CodigoAccion.GetAccessTypeString(this._accessType);
                }

                try
                {
                    var permission = _securityQueries.GetAuthorizedAsync(Guid.Parse(menuGuid), Guid.Parse(rolGuid), actionCode).Result;
                    if (permission.IsAuthorized)
                    {
                        actionContext.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(
                                      new[] {
                                    new Claim("UserLogin", ""),
                                    new Claim("Token", token),
                                    new Claim("MenuGuid", menuGuid),
                                    new Claim("RolGuid", rolGuid)
                                      }
                                  ));
                    }
                    else
                    {
                        var errors = new JObject();
                        errors["HasErrors"] = true;
                        errors["Message"] = "You do not have authorization to execute this action.";
                        actionContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
                        actionContext.Result = new JsonResult(new { HttpStatusCode.ServiceUnavailable, errors });
                    }
                }
                catch (Exception ex)
                {
                    var errors = new JObject();
                    errors["HasErrors"] = true;
                    errors["Message"] = $"An error occurred in the request: { ex.Message}";
                    actionContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    actionContext.Result = new JsonResult(new { HttpStatusCode.InternalServerError, errors });
                }
            }
            else
            {
                var errors = new JObject();
                errors["HasErrors"] = true;
                errors["Message"] = "Unauthorized information request. multiple parameters were not delivered";
                actionContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                actionContext.Result = new JsonResult(new { HttpStatusCode.BadRequest, errors });

            }

            base.OnActionExecuting(actionContext);
        }

        private bool TryRetrieveToken(HttpRequest request, out string token, out string rolGuid, out string menuGuid, out string actionCode)
        {
            try
            {
                token = string.Empty;
                rolGuid = string.Empty;
                menuGuid = string.Empty;
                actionCode = string.Empty;

                var authHeaders = request.Headers["Authorization"];
                if (authHeaders.Count == 0)
                {
                    return false;
                }
                var bearerToken = authHeaders[0];
                token = bearerToken.StartsWith("Bearer ", StringComparison.CurrentCultureIgnoreCase) ? bearerToken.Substring(7) : bearerToken;


                var rolGuidHeaders = request.Headers["RolGuid"];
                if (rolGuidHeaders.Count == 0)
                {
                    return false;
                }
                rolGuid = rolGuidHeaders[0];

                var menuGuidHeaders = request.Headers["MenuGuid"];
                if (menuGuidHeaders.Count == 0)
                {
                    return false;
                }
                menuGuid = menuGuidHeaders[0];

                var actionCodeHeaders = request.Headers["ActionCode"];
                if (actionCodeHeaders.Count == 0)
                {
                    return false;
                }
                actionCode = actionCodeHeaders[0];

                return true;
            }
            catch (Exception e)
            {
                throw new Exception($"An error occurred in the verification of the request parameters {e.Message}");
            }
        }

        private bool TryRetrieveToken(HttpRequest request)
        {
            bool validado = false;

            try
            {
                string token = string.Empty;

                var authHeaders = request.Headers["Authorization"];
                if (authHeaders.Count > 0)
                {
                    var bearerToken = authHeaders[0];
                    token = bearerToken.StartsWith("Bearer ", StringComparison.CurrentCultureIgnoreCase) ? bearerToken.Substring(7) : bearerToken;

                    // it needs to validate the token, in this case I assume the token is correct
                    validado = true;
                }

            }
            catch (Exception e)
            {
                throw new Exception($"An error occurred while checking the request parameters {e.Message}");
            }

            return validado;
        }
    }
    public class GenericMessage
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public int Type { get; set; }
    }

    public enum ActionCode
    {
        /// <summary>
        /// Access
        /// </summary>
        [StringValue("ACC")]
        ACCESS,
        /// <summary>
        /// List rows
        /// </summary>
        [StringValue("LST")]
        LIST,
        /// <summary>
        /// Add new row
        /// </summary>
        [StringValue("ADD")]
        ADD,
        /// <summary>
        /// Update row
        /// </summary>
        [StringValue("UPD")]
        UPDATE,
        /// <summary>
        /// Delete row
        /// </summary>
        [StringValue("DEL")]
        DELETE,
        /// <summary>
        /// Aprove Row
        /// </summary>
        [StringValue("APR")]
        APROVE,
        /// <summary>
        /// Reject Row
        /// </summary>
        [StringValue("REJ")]
        REJECT,
        /// <summary>
        /// View Row
        /// </summary>
        [StringValue("VIW")]
        VIEW,
        /// <summary>
        /// Submit Row
        /// </summary>
        [StringValue("SUB")]
        SUBMIT,
        [StringValue("")]
        NONE
    }

    internal class StringValueAttribute : Attribute
    {
        public string Valor { get; private set; }
        public StringValueAttribute(string v)
        {
            this.Valor = v;
        }
    }

    public class CodigoAccion
    {
        public static string GetAccessTypeString(ActionCode tIPO_ACCESO)
        {
            return tIPO_ACCESO.GetAttributeOfType<StringValueAttribute>().Valor;
        }
    }

    public static class EnumExtensions
    {
        public static T GetAttributeOfType<T>(this Enum enumVal) where T : System.Attribute
        {
            var type = enumVal.GetType();
            var memInfo = type.GetMember(enumVal.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
            return (attributes.Length > 0) ? (T)attributes[0] : null;
        }
    }
}

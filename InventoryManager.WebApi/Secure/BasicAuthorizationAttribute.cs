using Microsoft.AspNetCore.Authorization;
using System.Security.Principal;

namespace InventoryManager.WebApi.Secure
{
    public class BasicAuthorizationAttribute : AuthorizeAttribute
    {
        public BasicAuthorizationAttribute()
        {
            Policy = "BasicAuthentication";
        }
    }
}
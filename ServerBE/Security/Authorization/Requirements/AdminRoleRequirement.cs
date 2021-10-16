using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerBE.Security.Authorization.Requirements
{
    public class AdminRoleRequirement : IAuthorizationRequirement
    {
        public AdminRoleRequirement()
        {
               
        }
    }
}

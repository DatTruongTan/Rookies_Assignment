using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using ServerBE.Helpers;
using ServerBE.Security.Authorization.Requirements;
using Shared.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerBE.Security.Authorization.Handlers
{
    public class AdminRoleHandler : AuthorizationHandler<AdminRoleRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                    AdminRoleRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == JwtClaimTypes.Role &&
                                            c.Issuer == WebHostEnvironmentHelper.GetWebUrl()))
            {
                return Task.CompletedTask;
            }

            var adminClaim = context.User.FindFirst(c => c.Type == JwtClaimTypes.Role &&
                                                      c.Issuer == WebHostEnvironmentHelper.GetWebUrl() &&
                                                      c.Value == ConstSecurity.ADMINISTRATION_ROLE).Value;

            if (!string.IsNullOrEmpty(adminClaim))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}

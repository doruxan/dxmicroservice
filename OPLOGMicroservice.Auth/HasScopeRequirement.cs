using Microsoft.AspNetCore.Authorization;
using System;

namespace OPLOGMicroservice.Auth
{
    public enum BasePolicyBehavior
    {
#pragma warning disable SA1602 // Enumeration items must be documented
        OverrideBase,
        Or
#pragma warning restore SA1602 // Enumeration items must be documented
    }

    public class HasScopeRequirement : IAuthorizationRequirement
    {
        public HasScopeRequirement(string scope, string issuer, BasePolicyBehavior basePolicyBehavior = BasePolicyBehavior.OverrideBase)
        {
            Scope = scope ?? throw new ArgumentNullException(nameof(scope));
            Issuer = issuer ?? throw new ArgumentNullException(nameof(issuer));
            BasePolicyBehavior = basePolicyBehavior;
        }

        public string Issuer { get; }

        public string Scope { get; }

        public BasePolicyBehavior BasePolicyBehavior { get; }
    }
}

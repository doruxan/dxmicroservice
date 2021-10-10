namespace OPLOGMicroservice.Auth
{
#pragma warning disable CA1815 // Override equals and operator equals on value types
    public struct AuthorizationPermissions
#pragma warning restore CA1815 // Override equals and operator equals on value types
    {
        public const string SYSTEM_ADMIN = "SystemAdmin";
        public const string TENANT_ADMIN = "TenantAdmin";
        public const string TENANT_USER = "TenantUser";
        public const string WAREHOUSE_ADMIN = "WarehouseAdmin";
    }
}

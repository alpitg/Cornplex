namespace Cornplex.Domain.Enum
{
    using System;

    public enum Roles
    {
        SuperAdmin,
        Admin,
        User
    }

    public static class Constants
    {
        public static readonly string SuperAdmin = Guid.NewGuid().ToString();
        public static readonly string Admin = Guid.NewGuid().ToString();
        public static readonly string User = Guid.NewGuid().ToString();
    }
}

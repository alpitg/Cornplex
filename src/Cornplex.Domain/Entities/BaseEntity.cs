namespace Cornplex.Domain.Entities
{
    using System;

    public class BaseEntity
    {
        public DateTime UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
    }
}
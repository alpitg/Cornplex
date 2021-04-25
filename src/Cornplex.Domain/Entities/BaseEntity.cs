namespace Cornplex.Domain.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
    }
}
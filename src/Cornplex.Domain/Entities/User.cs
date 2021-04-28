namespace Cornplex.Domain.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class User : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }
    }
}

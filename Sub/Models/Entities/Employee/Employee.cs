
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sub.Models.Entities.Employee
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string Position { get; set; } = null!;
        public string? Obligation { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string? UpdatedAt { get; set; }

        // nav props

        [Required]
        public string UserId { get; set; } = null!;

        [ForeignKey("UserId")]
        public Sub.Models.Entities.User.User.User User { get; set; } = null!;

        [Required]
        public int CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        public Sub.Models.Entities.Company.Company Company { get; set; } = null!;

    }
}

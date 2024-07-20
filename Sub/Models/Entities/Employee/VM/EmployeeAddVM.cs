using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Sub.Models.Entities.Employee.VM
{
    public class EmployeeAddVM
    {
        public string Position { get; set; } = null!;
        public string? Obligation { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // nav props

        [Required]
        public string UserId { get; set; } = null!;

        [Required]
        public int CompanyId { get; set; }

    }
}

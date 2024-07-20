using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Sub.Models.Entities.Company.VM
{
    public class CompanyVM
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Имя компании не может привышать 50 символов")]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(15, ErrorMessage = "15 символов максимум")]
        [Phone]
        public string Phone { get; set; } = null!;


        public string Address { get; set; } = null!;
        public string Location { get; set; } = null!;
        public string Info { get; set; } = null!;
        public string Type { get; set; } = null!; // maybe later make enums for that
        public string NameLegal { get; set; } = null!;
        public string INN { get; set; } = null!;
        public string KPP { get; set; } = null!;
        public string OGRN { get; set; } = null!;
        public string OKPO { get; set; } = null!;
        public string BIK { get; set; } = null!;
        public string BankName { get; set; } = null!;
        public string BankAddress { get; set; } = null!;
        public string CorrAccount { get; set; } = null!;
        public List<Sub.Models.Entities.Employee.Employee> Employees { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string? UpdatedAt { get; set; }

        // nav props

        [Required]
        public string UserId { get; set; } = null!;

        [ForeignKey("UserId")]
        public Sub.Models.Entities.User.User.User User { get; set; } = null!;
    }
}

using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Sub.Models.Entities.Invitation
{
    public class Invitation
    {
        [Key]
        public int Id { get; set; } 
        [Required]
        public string InviterId { get; set; }
        [ForeignKey("InviterId")]
        public Sub.Models.Entities.User.User.User Inviter { get; set; }

        [Required]
        public string InviteeEmail { get; set; }

        [Required]
        public int CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        public Sub.Models.Entities.Company.Company Company { get; set; }

        public DateTime SendDate { get; set; } = DateTime.Now;
        public DateTime? ResponseDate { get; set; }

        [Required]
        public InviteEnums Status { get; set; } = InviteEnums.Pending;

    }
}

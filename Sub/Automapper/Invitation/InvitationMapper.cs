using AutoMapper;
using Sub.Models.Entities.Invitation;

namespace Sub.Automapper.Invitation
{
    public class InvitationMapper : Profile
    {
        public InvitationMapper()
        {
            CreateMap<Sub.Models.Entities.Invitation.Invitation, InvitationVM>();
        }
    }
}

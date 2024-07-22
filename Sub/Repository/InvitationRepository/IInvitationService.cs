using Sub.Models.Entities.Invitation;

namespace Sub.Repository.InvitationRepository
{
    public interface IInvitationService
    {
        Task<List<Invitation>> GetInvitationsForUserAsync(string userId);
        Task SendInvitationAsync(InvitationVM request, string inviterId);
        Task RespondToInvitationAsync(int invitationId, bool accept, string userEmail);
    }
}
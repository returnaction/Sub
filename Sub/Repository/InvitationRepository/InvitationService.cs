using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Sub.Models.Entities.Company;
using Sub.Models.Entities.Invitation;
using Sub.Models.Entities.User.User;
using Sub.Repository.BaseRepository;
using Sub.UnitOfWork;

namespace Sub.Repository.InvitationRepository
{
    public class InvitationService : IInvitationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Invitation> _repository;
        private readonly UserManager<User> _userManager;

        public InvitationService(IUnitOfWork unitOfWork, IMapper mapper, IGenericRepository<Invitation> repository, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repository = repository;
            _userManager = userManager;
        }

        public async Task SendInvitationAsync(InvitationVM request, string inviterId)
        {
            //var invitation = _mapper.Map<Invitation>(request);
            var invitation = new Invitation()
            {
                InviteeEmail = request.InviteeEmail,
                CompanyId = request.CompanyId,
                InviterId = inviterId
            };
            
            await _repository.AddAsync(invitation);
            await _unitOfWork.CommitAsync();

        }

        public async Task<List<Invitation>> GetInvitationsForUserAsync(string userEmail)
        {
            var invitationList =  await _repository.Include(i=>i.Company)
                                                   .Include(i=>i.Inviter)
                                                   .Where(i => i.InviteeEmail == userEmail && i.Status == InviteEnums.Pending)
                                                   .ToListAsync();
            return invitationList;
        }

        public async Task RespondToInvitationAsync(int invitationId, bool accept)
        {
            var invitation = await _repository.GetEnityByIdAsync(invitationId);
            invitation.Status = accept ? InviteEnums.Accepted : InviteEnums.Decline;
            invitation.ResponseDate = DateTime.Now;

            if (accept)
            {
                //
            }
            _repository.UpdateEntity(invitation);
            await _unitOfWork.CommitAsync();
        }
    }
}

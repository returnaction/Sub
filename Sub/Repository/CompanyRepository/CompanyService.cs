using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Sub.Models.Entities.Company;
using Sub.Models.Entities.Company.VM;
using Sub.Repository.BaseRepository;
using Sub.UnitOfWork;

namespace Sub.Repository.CompanyRepository
{
    public class CompanyService : ICompanyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Company> _repository;

        public CompanyService(IUnitOfWork unitOfWork, IMapper mapper, IGenericRepository<Company> repository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<List<CompanyVM>> GetListCompaniesAsync()
        {
            var companies = await _repository.GetAllAsync()
                                             .ProjectTo<CompanyVM>(_mapper.ConfigurationProvider)
                                             .ToListAsync();
            return companies;
        }

        public async Task<CompanyVM> GetCompanyByIdAsync(int id)
        {
            var company = await _repository.Where(x => x.Id == id)
                                           .ProjectTo<CompanyVM>(_mapper.ConfigurationProvider)
                                           .SingleOrDefaultAsync();
            return company;
        }

        public async Task AddCompanyAsync(CompanyAddVM request)
        {
            Company? company = _mapper.Map<Company>(request);

            await _repository.AddAsync(company);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateCompanyAsync(CompanyVM request)
        {
            var company = _mapper.Map<Company>(request);

            _repository.UpdateEntity(company);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteCompanyAsync(int id)
        {
            var company = await _repository.GetEnityByIdAsync(id);
            _repository.DeleteEntity(company);
            await _unitOfWork.CommitAsync();
        }
    }
}

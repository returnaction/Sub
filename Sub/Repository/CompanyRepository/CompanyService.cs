using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Sub.Models.Entities.Company;
using Sub.Models.Entities.Company.VM;
using Sub.Models.Entities.Employee.VM;
using Sub.Models.Entities.User.User;
using Sub.Repository.BaseRepository;
using Sub.UnitOfWork;
using System.ComponentModel.Design;

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

        public async Task<List<CompanyVM>> GetListCompaniesOfUser(string userId)
        {
            return await _repository.Where(c => c.UserId == userId)
                                    .ProjectTo<CompanyVM>(_mapper.ConfigurationProvider)
                                    .ToListAsync();
        }

        public async Task<CompanyVM> GetCompanyByIdAsync(int id)
        {
            var query = _repository.Include(c => c.Employees)
                            .ThenInclude(e => e.User);


            var company = await query.Where(c => c.Id == id)
                                     .SingleOrDefaultAsync();

            var companyVM = new CompanyVM
            {
                Id = company.Id,
                Name = company.Name,
                NameLegal = company.NameLegal,
                Address = company.Address,
                Phone = company.Phone,
                
                Employees = company.Employees.Select(e => new EmployeeVM
                {
                    Id = e.Id,
                    Position = e.Position,
                    Obligation = e.Obligation,
                    CreatedAt = e.CreatedAt,
                    UpdatedAt = e.UpdatedAt,
                    UserId = e.UserId,
                    User = new User
                    {
                        Id = e.User.Id,
                        FirstName = e.User.FirstName,
                        LastName = e.User.LastName,
                        Email = e.User.Email,
                    },
                    CompanyId = e.CompanyId,
                }).ToList()
            };

            return companyVM;
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

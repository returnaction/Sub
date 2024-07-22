using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Sub.Data;
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
        private readonly ApplicationDbContext _context;

        public CompanyService(IUnitOfWork unitOfWork, IMapper mapper, IGenericRepository<Company> repository, ApplicationDbContext context)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repository = repository;
            _context = context;
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
                Phone = company.Phone,
                Address = company.Address,
                Location = company.Location,
                Info = company.Info,
                Type = company.Type,
                NameLegal = company.NameLegal,
                INN = company.INN,
                KPP = company.KPP,
                OGRN = company.OGRN,
                OKPO = company.OKPO,
                BIK = company.BIK,
                BankName = company.BankName,
                BankAddress = company.BankAddress,
                CorrAccount = company.CorrAccount,
                UserId = company.UserId,
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
            var existingCompany = await _repository.Include(e => e.Employees)
                                                   .ThenInclude(u => u.User)
                                                   .SingleOrDefaultAsync(c => c.Id == request.Id);
            // add null check

            existingCompany.Name = request.Name;
            existingCompany.Phone = request.Phone;
            existingCompany.Address = request.Address;
            existingCompany.Location = request.Location;
            existingCompany.Info = request.Info;
            existingCompany.Type = request.Type;
            existingCompany.NameLegal = request.NameLegal;
            existingCompany.INN = request.INN;
            existingCompany.KPP = request.KPP;
            existingCompany.OGRN = request.OGRN;
            existingCompany.OKPO = request.OKPO;
            existingCompany.BIK = request.BIK;
            existingCompany.BankName = request.BankName;
            existingCompany.BankAddress = request.BankAddress;
            existingCompany.CorrAccount = request.CorrAccount;
            existingCompany.UserId = request.UserId;
            
           
            _repository.UpdateEntity(existingCompany);
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

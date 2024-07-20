using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Sub.Models.Entities.Employee;
using Sub.Models.Entities.Employee.VM;
using Sub.Repository.BaseRepository;
using Sub.UnitOfWork;

namespace Sub.Repository.EmployeeRepository
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Employee> _repository;

        public EmployeeService(IUnitOfWork unitOfWork, IMapper mapper, IGenericRepository<Employee> repository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<List<EmployeeVM>> GetListEmployees()
        {
            List<EmployeeVM>? employeeList = await _repository.GetAllAsync().ProjectTo<EmployeeVM>(_mapper.ConfigurationProvider).ToListAsync();
            return employeeList;
        }

        public async Task<EmployeeVM> GetEmployeeById(int id)
        {
            EmployeeVM? employee = await _repository.Where(x => x.Id == id).ProjectTo<EmployeeVM>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();
            return employee;
        }

        public async Task AddEmployeeAsync(EmployeeAddVM request)
        {
            Employee? employee = _mapper.Map<Employee>(request);

            await _repository.AddAsync(employee);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateEmployeeAsync(EmployeeVM request)
        {
            var employee = _mapper.Map<Employee>(request);

            _repository.UpdateEntity(employee);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            Employee? employee = await _repository.GetEnityByIdAsync(id);
            _repository.DeleteEntity(employee);
            await _unitOfWork.CommitAsync();
        }
    }
}

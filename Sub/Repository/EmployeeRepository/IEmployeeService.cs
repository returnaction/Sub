using Sub.Models.Entities.Employee.VM;

namespace Sub.Repository.EmployeeRepository
{
    public interface IEmployeeService
    {
        Task AddEmployeeAsync(EmployeeAddVM request);
        Task DeleteEmployeeAsync(int id);
        Task<EmployeeVM> GetEmployeeById(int id);
        Task<List<EmployeeVM>> GetListEmployees();
        Task UpdateEmployeeAsync(EmployeeVM request);
    }
}
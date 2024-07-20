using AutoMapper;
using Sub.Models.Entities.Employee;
using Sub.Models.Entities.Employee.VM;

namespace Sub.Automapper.Employee
{
    public class EmployeeMapper : Profile
    {
        public EmployeeMapper()
        {
            CreateMap<Sub.Models.Entities.Employee.Employee, EmployeeVM>().ReverseMap();
            CreateMap<Sub.Models.Entities.Employee.Employee, EmployeeAddVM>().ReverseMap();
        }
    }
}

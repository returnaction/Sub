using AutoMapper;
using Sub.Models.Entities.Company;
using Sub.Models.Entities.Company.VM;

namespace Sub.Automapper.Company
{
    public class CompanyMapper : Profile
    {
        public CompanyMapper()
        {
            CreateMap<Sub.Models.Entities.Company.Company, CompanyVM>().ReverseMap();
            CreateMap<Sub.Models.Entities.Company.Company, CompanyAddVM>().ReverseMap();
        }
    }
}

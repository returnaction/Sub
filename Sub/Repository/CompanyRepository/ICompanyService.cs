using Sub.Models.Entities.Company.VM;

namespace Sub.Repository.CompanyRepository
{
    public interface ICompanyService
    {
        Task AddCompanyAsync(CompanyAddVM request);
        Task DeleteCompanyAsync(int id);
        Task<CompanyVM> GetCompanyByIdAsync(int id);
        Task<List<CompanyVM>> GetListCompaniesAsync();
        Task UpdateCompanyAsync(CompanyVM request);
    }
}
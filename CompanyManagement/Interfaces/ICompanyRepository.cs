using CompanyManagement.Models;

namespace CompanyManagement.Interfaces
{
    public interface ICompanyRepository
    {
        ICollection<Company> GetCompanies();
        Company GetCompanyById(int id);
        Company GetCompanyByName(string name);
        bool CompanyExists(int compId);
        bool CreateCompany(Company company);
        bool UpdateCompany(Company company);
        bool DeleteCompany(Company company);
        bool Save();
    }

}

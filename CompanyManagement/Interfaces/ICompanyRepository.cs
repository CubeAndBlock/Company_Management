using CompanyManagement.Models;

namespace CompanyManagement.Interfaces
{
    public interface ICompanyRepository
    {
        ICollection<Company> GetCompanies();
        Company GetCompanyById(int id);
        Company GetCompanyByName(string name);
        bool CompanyExists(int compId);
    }

}

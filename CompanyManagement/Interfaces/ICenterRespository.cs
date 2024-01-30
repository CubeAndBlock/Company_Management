using CompanyManagement.Models;

namespace CompanyManagement.Interfaces
{
    public interface ICenterRespository
    {
        ICollection<Center> GetCenters();
        Center GetCenterById(int id);
        Center GetCenterByName(string name);
        ICollection<Company> GetCompanyByCenter(int centId);
        bool CenterExists(int centId);
        bool CreateCenter(Center center);
        bool UpdateCenter(Center center);
        bool DeleteCenter(Center center);
        bool Save();
    }
}

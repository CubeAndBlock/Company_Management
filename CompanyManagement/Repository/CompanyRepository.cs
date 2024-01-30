using CompanyManagement.Data;
using CompanyManagement.Interfaces;
using CompanyManagement.Models;

namespace CompanyManagement.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        public readonly DataContext _context;
        public CompanyRepository(DataContext context) { 
            _context = context;
        }

        public bool CompanyExists(int compId)
        {
            return _context.Companies.Any(p => p.Id == compId);
        }

        public bool CreateCompany(Company company)
        {
            //Change Tracker
            _context.Add(company);
            return Save();
        }

        public ICollection<Company> GetCompanies()
        {
            return _context.Companies.OrderBy(p => p.Id).ToList();
;        }

        public Company GetCompanyById(int id)
        {
            return _context.Companies.Where(p => p.Id == id).FirstOrDefault();        }

        public Company GetCompanyByName(string name)
        {
            return _context.Companies.Where(p => p.Name == name).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}

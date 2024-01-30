using CompanyManagement.Data;
using CompanyManagement.Interfaces;
using CompanyManagement.Models;

namespace CompanyManagement.Repository
{
    public class CenterRespository : ICenterRespository
    {
        private DataContext _context;
        public CenterRespository(DataContext context)
        {
            _context = context;
        }
        public bool CenterExists(int id)
        {
            return _context.Centers.Any(c => c.Id == id);
        }

        public ICollection<Company> GetCompanyByCenter(int centId)
        {
            return _context.Centers.Where(c => c.Id == centId).Select(c => c.Company).ToList();
        }

        public Center GetCenterById(int id)
        {
            return _context.Centers.Where(e => e.Id == id).FirstOrDefault()
;       }

        public Center GetCenterByName(string name)
        {
            return _context.Centers.Where(e => e.Name == name).FirstOrDefault();
        }

        public ICollection<Center> GetCenters()
        {
            return _context.Centers.ToList();
        }

        public bool CreateCenter(Center center)
        {
            _context.Add(center);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCenter(Center center)
        {
            _context.Update(center);
            return Save();
        }

        public bool DeleteCenter(Center center)
        {
            _context.Remove(center);
            return Save();
        }
    }
}

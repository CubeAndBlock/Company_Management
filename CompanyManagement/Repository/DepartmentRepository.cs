using CompanyManagement.Data;
using CompanyManagement.Interfaces;
using CompanyManagement.Models;
using System.Xml.Linq;

namespace CompanyManagement.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private DataContext _context;
        public DepartmentRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateDepartment(Department department)
        {
            _context.Add(department);
            return Save();
        }

        public bool DepartmentExists(int departmentId)
        {
            return _context.Departments.Any(d => d.Id == departmentId);
        }
        public ICollection<Center> GetCenterByDepartment(int departmentId)
        {
            return _context.Departments.Where(d => d.Id == departmentId).Select(d => d.Center).ToList();
        }

        public Department GetDepartmentById(int id)
        {
            return _context.Departments.Where(d => d.Id == id).FirstOrDefault();
        }

        public Department GetDepartmentByName(string name)
        {
            return _context.Departments.Where(d => d.Name == name).FirstOrDefault();
        }

        public ICollection<Department> GetDepartments()
        {
            return _context.Departments.ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true: false;
        }
    }
}

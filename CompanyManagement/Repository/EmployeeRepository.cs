using CompanyManagement.Data;
using CompanyManagement.Interfaces;
using CompanyManagement.Models;

namespace CompanyManagement.Repository
{
    
    public class EmployeeRepository : IEmployeeRepository
    {
        private DataContext _context;
        public EmployeeRepository(DataContext context)
        {
            _context = context;
        }

        public bool EmployeeExists(int employeeId)
        {
            return _context.Employees.Any(e => e.Id == employeeId);
        }

        public ICollection<Department> GetDepartmentsByEmplyee(int employeeId)
        {
            return _context.Employees.Where(e => e.Id == employeeId).Select(e => e.Department).ToList();
        }

        public Employee GetEmployeeById(int id)
        {
            return _context.Employees.Where(e => e.Id == id).FirstOrDefault();
        }

        public Employee GetEmployeeByName(string name)
        {
            return _context.Employees.Where(e => e.Name == name).FirstOrDefault();
        }

        public ICollection<Employee> GetEmployees()
        {
            return _context.Employees.ToList();
        }
    }
}

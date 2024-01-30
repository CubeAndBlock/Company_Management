using CompanyManagement.Models;

namespace CompanyManagement.Interfaces
{
    public interface IEmployeeRepository
    {
        Employee GetEmployeeById(int id);
        Employee GetEmployeeByName(string name);
        ICollection<Employee> GetEmployees();
        ICollection<Department> GetDepartmentsByEmplyee(int employeeId);
        bool EmployeeExists(int employeeId);
        bool CreateEmployee( Employee employee);
        bool Save();
    }
}

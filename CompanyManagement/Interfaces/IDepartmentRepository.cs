using CompanyManagement.Models;

namespace CompanyManagement.Interfaces
{
    public interface IDepartmentRepository
    {
        Department GetDepartmentById(int id);
        Department GetDepartmentByName(string name);
        ICollection<Department> GetDepartments();
        ICollection<Center> GetCenterByDepartment(int departmentId);
        bool DepartmentExists(int departmentId);
        bool CreateDepartment(Department department);
        bool UpdateDepartment(Department department);
        bool DeleteDepartment(Department department);
        bool Save();
    }
}

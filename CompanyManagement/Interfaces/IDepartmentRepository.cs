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
    }
}

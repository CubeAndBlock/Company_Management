using System.Security;

namespace CompanyManagement.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Center Center { get; set; }
        public ICollection<Employee> Employees { get; set; }
        public ICollection<Project> Projects { get; set; }
        
    }
}

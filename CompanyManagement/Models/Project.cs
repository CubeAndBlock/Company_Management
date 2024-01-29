namespace CompanyManagement.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public Department Department { get; set; }
        public ICollection<EmployeeProject> EmployeeProjects { get; set; }
    }
}

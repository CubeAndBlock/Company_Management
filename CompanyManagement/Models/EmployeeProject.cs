namespace CompanyManagement.Models
{
    public class EmployeeProject
    {
        public int ProjectId { get; set; }
        public int EmployeeId { get; set; } 
        public Employee Employee { get; set; }
        public Project Project { get; set; }
    }
}

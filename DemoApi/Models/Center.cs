namespace CompanyManagement.Models
{
    public class Center
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Company Company { get; set; }
        public ICollection<Department> Departments { get; set; }
    }
}

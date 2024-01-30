using CompanyManagement.Data;
using CompanyManagement.Interfaces;
using CompanyManagement.Models;

namespace CompanyManagement.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        private DataContext _context;
        public ProjectRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateProject(Project project)
        {
            _context.Add(project);
            return Save();
        }

        public ICollection<Department> GetDepartmentByProject(int projectId)
        {
            return _context.Projects.Where(p => p.Id == projectId).Select(p => p.Department).ToList();
        }

        public Project GetProjectById(int id)
        {
            return _context.Projects.Where(p => p.Id == id).FirstOrDefault();
        }

        public Project GetProjectByName(string name)
        {
            return _context.Projects.Where(p => p.Name == name).FirstOrDefault();
        }

        public ICollection<Project> GetProjects()
        {
            return _context.Projects.ToList();
        }

        public bool ProjectExists(int projectId)
        {
           return _context.Projects.Any(p => p.Id == projectId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}

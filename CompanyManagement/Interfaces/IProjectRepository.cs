﻿using CompanyManagement.Models;

namespace CompanyManagement.Interfaces
{
    public interface IProjectRepository
    {
        Project GetProjectById(int id);
        Project GetProjectByName(string name);
        ICollection<Project> GetProjects(); 
        ICollection<Department> GetDepartmentByProject(int projectId);
        bool ProjectExists(int projectId);
    }
}

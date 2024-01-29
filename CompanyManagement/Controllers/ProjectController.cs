using AutoMapper;
using CompanyManagement.Dto;
using CompanyManagement.Interfaces;
using CompanyManagement.Models;
using CompanyManagement.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CompanyManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IProjectRepository _projectRepository;
        public ProjectController(IProjectRepository projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Project>))]
        public IActionResult GetProjects()
        {
            var projects = _mapper.Map<List<DepartmentDto>>(_projectRepository.GetProjects());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(projects);
        }
        [HttpGet("{projectId}")]
        [ProducesResponseType(200, Type = typeof(Project))]
        [ProducesResponseType(400)]
        public IActionResult GetDepartment(int projectId)
        {
            if (!_projectRepository.ProjectExists(projectId))
                return NotFound();
            var project = _mapper.Map<Project>(_projectRepository.GetProjectById(projectId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(project);
        }

        [HttpGet("Department/{projectId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Project>))]
        [ProducesResponseType(400)]
        public IActionResult GetDepartmentByProjectId(int projectId)
        {
            var projects = _mapper.Map<List<DepartmentDto>>(_projectRepository.GetDepartmentByProject(projectId));
            if (!ModelState.IsValid)
                return BadRequest();
            return Ok(projects);
        }
    }
}

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
        private readonly IDepartmentRepository _departmentRepository;
        public ProjectController(IProjectRepository projectRepository, IMapper mapper, IDepartmentRepository departmentRepository)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
            _departmentRepository = departmentRepository;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Project>))]
        public IActionResult GetProjects()
        {
            var projects = _mapper.Map<List<ProjectDto>>(_projectRepository.GetProjects());
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
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateProject([FromQuery] int departmentId, [FromBody] ProjectDto projectCreate)
        {
            if (projectCreate == null)
                return BadRequest(ModelState);

            var project = _projectRepository.GetProjects()
                .Where(c => c.Name.Trim().ToUpper() == projectCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (project != null)
            {
                ModelState.AddModelError("", "Project already exists");
                return StatusCode(442, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var projectMap = _mapper.Map<Project>(projectCreate);

            projectMap.Department = _departmentRepository.GetDepartmentById(departmentId);

            if (!_projectRepository.CreateProject(projectMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }
        [HttpPut("{projectId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateProject(int projectId, [FromBody] ProjectDto updatedProject)
        {
            if (updatedProject == null)
                return BadRequest(ModelState);

            if (projectId != updatedProject.Id)
                return BadRequest(ModelState);

            if (!_projectRepository.ProjectExists(projectId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var projectMap = _mapper.Map<Project>(updatedProject);

            if (!_projectRepository.UpdateProject(projectMap))
            {
                ModelState.AddModelError("", "Something went wrong updating project");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
        [HttpDelete("{projectId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteProject(int projectId)
        {
            if (!_projectRepository.ProjectExists(projectId))
            {
                return NotFound();
            }

            var projectToDelete = _projectRepository.GetProjectById(projectId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_projectRepository.DeleteProject(projectToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting project");
            }

            return NoContent();
        }
    }
}

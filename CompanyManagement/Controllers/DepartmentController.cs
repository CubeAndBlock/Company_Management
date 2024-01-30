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
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly ICenterRespository _centerRespository;
        private readonly IMapper _mapper;
        public DepartmentController(IDepartmentRepository departmentRepository, IMapper mapper, ICenterRespository centerRespository)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
            _centerRespository = centerRespository;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Department>))]
        public IActionResult GetDepartments()
        {
            var departments = _mapper.Map<List<DepartmentDto>>(_departmentRepository.GetDepartments());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(departments);
        }
        [HttpGet("{departmentId}")]
        [ProducesResponseType(200, Type = typeof(Department))]
        [ProducesResponseType(400)]
        public IActionResult GetDepartment(int departmentId)
        {
            if (!_departmentRepository.DepartmentExists(departmentId))
                return NotFound();
            var department = _mapper.Map<Department>(_departmentRepository.GetDepartmentById(departmentId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(department);
        }

        [HttpGet("Center/{departmentId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Department>))]
        [ProducesResponseType(400)]
        public IActionResult GetCenterByDepartmentId(int departmentId)
        {
            var departments = _mapper.Map<List<CenterDto>>(_departmentRepository.GetCenterByDepartment(departmentId));
            if (!ModelState.IsValid)
                return BadRequest();
            return Ok(departments);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateDepartment([FromQuery] int centerId, [FromBody] DepartmentDto departmentCreate)
        {
            if (departmentCreate == null)
                return BadRequest(ModelState);

            var department = _departmentRepository.GetDepartments()
                .Where(c => c.Name.Trim().ToUpper() == departmentCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (department != null)
            {
                ModelState.AddModelError("", "Department already exists");
                return StatusCode(442, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var departmentMap = _mapper.Map<Department>(departmentCreate);

            departmentMap.Center = _centerRespository.GetCenterById(centerId);

            if (!_departmentRepository.CreateDepartment(departmentMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }
        [HttpPut("{departmentId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateDeparatment(int departmentId, [FromBody] DepartmentDto updatedDepartment)
        {
            if (updatedDepartment == null)
                return BadRequest(ModelState);

            if (departmentId != updatedDepartment.Id)
                return BadRequest(ModelState);

            if (!_departmentRepository.DepartmentExists(departmentId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var departmentMap = _mapper.Map<Department>(updatedDepartment);

            if (!_departmentRepository.UpdateDepartment(departmentMap))
            {
                ModelState.AddModelError("", "Something went wrong updating department");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
        [HttpDelete("{departmentId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteDepartment(int departmentId)
        {
            if (!_departmentRepository.DepartmentExists(departmentId))
            {
                return NotFound();
            }

            var departmentToDelete = _departmentRepository.GetDepartmentById(departmentId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_departmentRepository.DeleteDepartment(departmentToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting department");
            }

            return NoContent();
        }
    }
}

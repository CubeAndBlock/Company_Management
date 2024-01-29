using AutoMapper;
using CompanyManagement.Dto;
using CompanyManagement.Interfaces;
using CompanyManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace CompanyManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;
        public DepartmentController(IDepartmentRepository departmentRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
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
    }
}

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
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly IDepartmentRepository _departmentRepository;
        public EmployeeController(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _departmentRepository = departmentRepository;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Employee>))]
        public IActionResult GetEmployees()
        {
            var employees = _mapper.Map<List<DepartmentDto>>(_employeeRepository.GetEmployees());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(employees);
        }
        [HttpGet("{employeeId}")]
        [ProducesResponseType(200, Type = typeof(Employee))]
        [ProducesResponseType(400)]
        public IActionResult GetDepartment(int employeeId)
        {
            if (!_employeeRepository.EmployeeExists(employeeId))
                return NotFound();
            var employee = _mapper.Map<Employee>(_employeeRepository.GetEmployeeById(employeeId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(employee);
        }

        [HttpGet("Department/{employeeId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Employee>))]
        [ProducesResponseType(400)]
        public IActionResult GetDepartmentByEmployeeId(int employeeId)
        {
            var employees = _mapper.Map<List<DepartmentDto>>(_employeeRepository.GetDepartmentsByEmplyee(employeeId));
            if (!ModelState.IsValid)
                return BadRequest();
            return Ok(employees);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateEmployee([FromQuery] int departmentId, [FromBody] EmployeeDto employeeCreate)
        {
            if (employeeCreate == null)
                return BadRequest(ModelState);

            var employee = _employeeRepository.GetEmployees()
                .Where(c => c.Name.Trim().ToUpper() == employeeCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (employee != null)
            {
                ModelState.AddModelError("", "Employee already exists");
                return StatusCode(442, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employeeMap = _mapper.Map<Employee>(employeeCreate);

            employeeMap.Department = _departmentRepository.GetDepartmentById(departmentId);

            if (!_employeeRepository.CreateEmployee(employeeMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using CompanyManagement.Models;
using CompanyManagement.Interfaces;
using CompanyManagement.Repository;
using CompanyManagement.Data;
using AutoMapper;
using CompanyManagement.Dto;
namespace CompanyManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : Controller
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;
        public CompanyController(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Company>))]
        public IActionResult GetCompanies()
        {
            var companies = _mapper.Map<List<CompanyDto>>(_companyRepository.GetCompanies());
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(companies);
        }
        [HttpGet("{compId}")]
        [ProducesResponseType(200, Type = typeof(Company))]
        [ProducesResponseType(400)]
        public IActionResult GetCompany(int compId)
        {
            if (!_companyRepository.CompanyExists(compId))
                return NotFound();
            var company = _mapper.Map<Company>(_companyRepository.GetCompanyById(compId));

            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(company);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCompany([FromBody] CompanyDto companyCreate)
        {
            if(companyCreate == null)
                return BadRequest(ModelState);

            var company = _companyRepository.GetCompanies()
                .Where(c => c.Name.Trim().ToUpper() == companyCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if(company != null)
            {
                ModelState.AddModelError("", "Company already exists");
            }

            if(!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            var companyMap = _mapper.Map<Company>(companyCreate);

            if (!_companyRepository.CreateCompany(companyMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }
        [HttpPut("{companyId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCompany(int companyId, [FromBody]CompanyDto updatedCompany)
        {
            if(updatedCompany == null)
                return BadRequest(ModelState);

            if(companyId != updatedCompany.Id)
                return BadRequest(ModelState);

            if (!_companyRepository.CompanyExists(companyId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var companyMap = _mapper.Map<Company>(updatedCompany);

            if (!_companyRepository.UpdateCompany(companyMap))
            {
                ModelState.AddModelError("", "Something went wrong updating company");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
        [HttpDelete("{companyId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCompany(int companyId)
        {
            if (!_companyRepository.CompanyExists(companyId)){
                return NotFound();
            }

            var companyToDelete = _companyRepository.GetCompanyById(companyId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_companyRepository.DeleteCompany(companyToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting company");
            }

            return NoContent();
        }

    }
}
 
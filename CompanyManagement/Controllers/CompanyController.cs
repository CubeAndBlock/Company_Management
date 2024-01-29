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
    }
}
 
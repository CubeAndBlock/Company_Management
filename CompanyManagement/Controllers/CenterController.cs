using AutoMapper;
using CompanyManagement.Dto;
using CompanyManagement.Interfaces;
using CompanyManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace CompanyManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CenterController : Controller
    {
        private readonly ICenterRespository _centerRepository;
        private readonly IMapper _mapper;
        public CenterController(ICenterRespository centerRepository, IMapper mapper)
        {
            _centerRepository = centerRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Center>))]
        public IActionResult GetCenters()
        {
            var centers = _mapper.Map<List<CenterDto>>(_centerRepository.GetCenters());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(centers);
        }
        [HttpGet("{centId}")]
        [ProducesResponseType(200, Type = typeof(Center))]
        [ProducesResponseType(400)]
        public IActionResult GetCenter(int centId)
        {
            if (!_centerRepository.CenterExists(centId))
                return NotFound();
            var center = _mapper.Map<Center>(_centerRepository.GetCenterById(centId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(center);
        }

        [HttpGet("company/{centId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Center>))]
        [ProducesResponseType(400)]
        public IActionResult GetCompanyByCenterId(int centId)
        {
            var companies = _mapper.Map<List<CompanyDto>>(_centerRepository.GetCompanyByCenter(centId));
            if (!ModelState.IsValid)
                return BadRequest();
            return Ok(companies);
        }
    }
}

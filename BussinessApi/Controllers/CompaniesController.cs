using BussinessApi.Context;
using BussinessApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BussinessApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public CompaniesController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {            
            try
            {
                return Ok(
                new
                {
                    success = true,
                    data = await _appDbContext.Companies.ToListAsync(),
                    message = "Chamada executada com sucesso"
                }
                );
            }
            catch(Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message
                });
            }
            
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompanyById(int id)
        {
            try
            {
                var data = await _appDbContext.Companies.FindAsync(id);

                if(data == null)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Não foi encontrado empresa com esse id"
                    });
                }

                return Ok(new
                {
                    success = true,
                    data                   
                });
            }
            catch(Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        [HttpPost]
        public IActionResult CreateCompany([FromBody] Company company)
        {
           try
            {
                _appDbContext.Companies.Add(company);
                _appDbContext.SaveChanges();

                return CreatedAtAction(null, new
                {
                    success = true,
                    message = "Empresa criada com sucesso",
                    company
                });
            }
            catch(Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }
    }
}

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
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
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

                if (data == null)
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
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
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
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCompany([FromBody] Company company, int id)
        {
            try
            {
                var validationCompany = _appDbContext.Companies.SingleOrDefault(x => x.Id == id);

                if (validationCompany == null)
                    return BadRequest(new
                    {
                        success = false,
                        message = "Não foi encontrado empresa com esse id"
                    });

                _appDbContext.Entry(validationCompany).CurrentValues.SetValues(company);
                _appDbContext.SaveChanges();

                return Ok(new
                {
                    success = true,
                    data = company
                });
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCompany(int id)
        {
            try
            {
                var validationCompany = _appDbContext.Companies.SingleOrDefault(x => x.Id == id);

                if (validationCompany == null)
                    return BadRequest(new
                    {
                        success = false,
                        message = "Não foi encontrado empresa com esse id"
                    });

                _appDbContext.Companies.Remove(validationCompany);
                _appDbContext.SaveChanges();

                return Ok(new
                {
                    success = true,
                    message = "Empresa excluída com sucesso"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }
    }
}


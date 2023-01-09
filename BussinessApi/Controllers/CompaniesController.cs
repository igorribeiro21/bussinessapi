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
                var data = await _appDbContext.Companies.Include(c => c.Employees).ToListAsync();

                data.ForEach(company => {
                    company.Employees.ForEach(e =>
                    {
                        e.JobTitles = _appDbContext.JobTitles.FirstOrDefault(x => x.Id == e.JobTitlesId);
                    });
                });

                return Ok(
                new
                {
                    success = true,
                    data,
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
                var data = await _appDbContext.Companies.Include(c => c.Employees).FirstOrDefaultAsync(c => c.Id == id);

                if (data == null)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Não foi encontrado empresa com esse id"
                    });
                }

                data.Employees.ForEach(e =>
                {
                    e.JobTitles = _appDbContext.JobTitles.FirstOrDefault(x => x.Id == e.JobTitlesId);
                });
                

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
        public IActionResult CreateCompany([FromBody] CompanyCreateUpdate company)
        {
            try
            {
                Company companyCreate = new Company()
                {                    
                    Name = company.Name,
                    Street = company.Street,
                    City = company.City,
                    Complement = company.Complement,
                    Neighborhood = company.Neighborhood,
                    Number = company.Number,
                    State = company.State,
                    Telephone = company.Telephone,
                    Zipcode = company.Zipcode
                };

                _appDbContext.Companies.Add(companyCreate);
                _appDbContext.SaveChanges();

                return CreatedAtAction(null, new
                {
                    success = true,
                    message = "Empresa criada com sucesso",
                    company = companyCreate
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
        public IActionResult UpdateCompany([FromBody] CompanyCreateUpdate company, int id)
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

                Company companyUpdate = new Company()
                {
                    Id = id,
                    Name = company.Name != null ? company.Name : validationCompany.Name,
                    Street = company.Street != null ? company.Street : validationCompany.Street,
                    City = company.City != null ? company.City : validationCompany.City,
                    Complement = company.Complement != null ? company.Complement : validationCompany.Complement,
                    Neighborhood = company.Neighborhood != null ? company.Neighborhood : validationCompany.Neighborhood,
                    Number = company.Number != 0 ? company.Number : validationCompany.Number,
                    State = company.State != null ? company.State : validationCompany.State,
                    Telephone  = company.Telephone != null ? company.Telephone : validationCompany.Telephone,
                    Zipcode = company.Zipcode != 0 ? company.Zipcode : validationCompany.Zipcode
                };                

                _appDbContext.Entry(validationCompany).CurrentValues.SetValues(companyUpdate);
                _appDbContext.SaveChanges();

                return Ok(new
                {
                    success = true,
                    data = companyUpdate
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


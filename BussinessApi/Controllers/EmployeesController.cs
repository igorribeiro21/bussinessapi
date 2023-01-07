using BussinessApi.Context;
using BussinessApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BussinessApi.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public EmployeesController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            try
            {                

                return Ok(
                new
                {
                    success = true,
                    data = await _appDbContext.Employees.Include(e => e.Company).Include(e => e.JobTitles).ToListAsync(),
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
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            try
            {
                var data = await _appDbContext.Employees.Include(e => e.Company).Include(e => e.JobTitles).FirstOrDefaultAsync();

                if (data == null)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Não foi encontrado funcionário com esse id"
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
        public IActionResult CreateEmployees([FromBody] EmployeesCreate employees)
        {
            try
            {
                var jobTitle = _appDbContext.JobTitles.FirstOrDefault(x => x.Id == employees.JobTitlesId);

                if(jobTitle == null)
                {
                    return BadRequest(new
                    {
                        status = false,
                        message = "Não foi encontrado cargo com o id passado"
                    });
                }

                var company = _appDbContext.Companies.FirstOrDefault(x => x.Id == employees.CompanyId);

                if(company == null)
                {
                    return BadRequest(new
                    {
                        status = false,
                        message = "Não foi encontrado empresa com o id passado"
                    });
                }

                Employees employeeCreate = new Employees(){
                    Name = employees.Name,
                    Company = company,
                    JobTitles = jobTitle,
                    Salary = employees.Salary
                };

                _appDbContext.Employees.Add(employeeCreate);
                _appDbContext.SaveChanges();

                return CreatedAtAction(null, new
                {
                    success = true,
                    message = "Funcionário criado com sucesso",
                    employees
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
        public IActionResult UpdateEmployees([FromBody] EmployeesUpdate employees, int id)
        {
            try
            {
                var validationEmployee = _appDbContext.Employees.Include(e => e.Company).Include(e => e.JobTitles).SingleOrDefault(x => x.Id == id);

                if (validationEmployee == null)
                    return BadRequest(new
                    {
                        success = false,
                        message = "Não foi encontrado funcionário com esse id"
                    });

                if(employees.Salary != 0)
                    validationEmployee.Salary = employees.Salary;
                if (employees.Name != null)
                    validationEmployee.Name = employees.Name;


                _appDbContext.Employees.Update(validationEmployee);
                _appDbContext.SaveChanges();

                return Ok(new
                {
                    success = true,
                    data = validationEmployee
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
        public IActionResult DeleteEmployee(int id)
        {
            try
            {
                var validationEmployee = _appDbContext.Employees.SingleOrDefault(x => x.Id == id);

                if (validationEmployee == null)
                    return BadRequest(new
                    {
                        success = false,
                        message = "Não foi encontrado funcionário com esse id"
                    });

                _appDbContext.Employees.Remove(validationEmployee);
                _appDbContext.SaveChanges();

                return Ok(new
                {
                    success = true,
                    message = "Funcionário excluída com sucesso"
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


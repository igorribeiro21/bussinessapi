using BussinessApi.Context;
using BussinessApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BussinessApi.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class JobTitlesController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public JobTitlesController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetJobTitles()
        {
            try
            {
                return Ok(
                new
                {
                    success = true,
                    data = await _appDbContext.JobTitles.ToListAsync(),
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
        public async Task<IActionResult> GetJobTitleById(int id)
        {
            try
            {
                var data = await _appDbContext.JobTitles.FindAsync(id);

                if (data == null)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Não foi encontrado cargo com esse id"
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
        public IActionResult CreateCompany([FromBody] JobTitles job)
        {
            try
            {
                _appDbContext.JobTitles.Add(job);
                _appDbContext.SaveChanges();

                return CreatedAtAction(null, new
                {
                    success = true,
                    message = "Cargo criada com sucesso",
                    job
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
        public IActionResult UpdateJobTitle([FromBody] JobTitles job, int id)
        {
            try
            {
                job.Id = id;
                var validationJob = _appDbContext.JobTitles.SingleOrDefault(x => x.Id == id);

                if (validationJob == null)
                    return BadRequest(new
                    {
                        success = false,
                        message = "Não foi encontrado cargo com esse id"
                    });

                _appDbContext.Entry(validationJob).CurrentValues.SetValues(job);
                _appDbContext.SaveChanges();

                return Ok(new
                {
                    success = true,
                    data = job
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
        public IActionResult DeleteJobTitle(int id)
        {
            try
            {
                var validationJob = _appDbContext.JobTitles.SingleOrDefault(x => x.Id == id);

                if (validationJob == null)
                    return BadRequest(new
                    {
                        success = false,
                        message = "Não foi encontrado cargo com esse id"
                    });

                _appDbContext.JobTitles.Remove(validationJob);
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


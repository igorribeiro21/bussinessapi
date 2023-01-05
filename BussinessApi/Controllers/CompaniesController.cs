using BussinessApi.Context;
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
            return Ok(
                new
                {
                    success = true,
                    data = await _appDbContext.Companies.ToListAsync()
                }
                );
        }
    }
}

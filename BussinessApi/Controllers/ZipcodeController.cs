using Microsoft.AspNetCore.Mvc;
using BussinessApi.Models;
using Newtonsoft.Json;

namespace BussinessApi.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class ZipcodeController : ControllerBase
    {
        private HttpClient _client;

        public ZipcodeController()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://viacep.com.br/ws/");
        }

        [HttpGet("{zipcode}")]
        public async Task<IActionResult> GetZipcode(string zipcode)
        {
            try
            {
                var result = await _client.GetAsync($"{zipcode}/json");

                if (result.IsSuccessStatusCode)
                {
                    var responseBody = await result.Content.ReadAsStringAsync();
                    ResponseZipcodeModel response = JsonConvert.DeserializeObject<ResponseZipcodeModel>(responseBody);

                    if (response != null)
                    {
                        return Ok(response);
                    }
                }
                return BadRequest(new
                {
                    message = "Não foi encontrado informações com o cep informado"
                });
            }catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    message = ex.Message
                });
            }            
        }
    }
}

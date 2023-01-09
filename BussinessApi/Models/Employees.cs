using System.ComponentModel.DataAnnotations.Schema;

namespace BussinessApi.Models
{
    public class Employees
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int JobTitlesId { get; set; }
        public JobTitles? JobTitles { get; set; }
        public Company? Company { get; set; }        
        
        public decimal Salary { get; set; }
 
    }
}

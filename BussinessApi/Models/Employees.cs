using System.ComponentModel.DataAnnotations.Schema;

namespace BussinessApi.Models
{
    public class Employees
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        //public int JobTitlesId { get; set; }
        //[ForeignKey("JobTitlesId")]
        public JobTitles? JobTitles { get; set; }
        //public int CompanyId { get; set; }
        //[ForeignKey("CompanyId")]
        public Company? Company { get; set; }        
        
        public decimal Salary { get; set; }

        public void Update(string name, decimal salary)
        {
            Name = name;
            Salary = salary;
        }
    }
}

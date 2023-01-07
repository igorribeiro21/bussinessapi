namespace BussinessApi.Models
{
    public class EmployeesCreate
    {        
        public string? Name { get; set; }
        public int JobTitlesId { get; set; }       
        public int CompanyId { get; set; }        
        public decimal Salary { get; set; }
    }
}

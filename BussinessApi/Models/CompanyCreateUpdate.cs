namespace BussinessApi.Models
{
    public class CompanyCreateUpdate
    {        
        public string? Name { get; set; }
        public int Zipcode { get; set; }
        public string? Street { get; set; }
        public int Number { get; set; }
        public string? Complement { get; set; }
        public string? Neighborhood { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Telephone { get; set; }        
    }
}

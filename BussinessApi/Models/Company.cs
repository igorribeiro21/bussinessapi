namespace BussinessApi.Models
{
    public class Company
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public int Zipcode { get; set; }
        public string? Street { get; set; }
        public int Number { get; set; }
        public string? Complement { get; set; }
        public string? Neighborhood { get; set; }
        public string? City { get; set; }
        public string? State{ get; set; }
        public string? Telephone { get; set; }
        public List<Employees>? Employees { get; set; }

    }
}

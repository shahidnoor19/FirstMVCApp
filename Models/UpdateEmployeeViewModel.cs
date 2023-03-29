namespace FirstMVCapp.Models
{
    public class UpdateEmployeeViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Salary { get; set; }
        public DateTime DOB { get; set; }
        public string Department { get; set; }
    }
}

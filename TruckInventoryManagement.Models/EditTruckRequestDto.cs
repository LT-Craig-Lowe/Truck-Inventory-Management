namespace TruckInventoryManagement.Models
{
    public  class EditTruckRequestDto
    {
        public string ChassisNumber { get; set; }
        public string ModelFamily { get; set; }
        public string ModelNumber { get; set; }
        public string Customer { get; set; }
    }
}

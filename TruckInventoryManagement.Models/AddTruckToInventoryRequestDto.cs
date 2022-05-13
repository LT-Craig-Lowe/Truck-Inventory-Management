using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruckInventoryManagement.Models
{
    public class AddTruckToInventoryRequestDto
    {
        public string ChassisNumber { get; set; }
        public string ModelFamily { get; set; }
        public string ModelNumber { get; set; }
        public string Customer { get; set; }
    }
}

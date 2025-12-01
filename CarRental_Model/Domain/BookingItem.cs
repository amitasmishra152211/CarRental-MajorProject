using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental_Model.Domain
{
    public class BookingItem
    {
        public int Id { get; set; }
        public string VehicleName { get; set; }
        public decimal RentPerDay { get; set; }
        public string ImageUrl { get; set; }
    }
}

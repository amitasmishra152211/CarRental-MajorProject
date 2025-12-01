using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental_Model.Domain
{
    public class VehicleType
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Vehicle type name is required")]
        [StringLength(50)]
        public string TypeName { get; set; }

        [Url(ErrorMessage = "Please enter a valid URL")]
        [StringLength(250)]
        public string? VehicleTypeImg { get; set; } // New Field

        [ValidateNever]
        public ICollection<Vehicle> Vehicles { get; set; }
    }
}

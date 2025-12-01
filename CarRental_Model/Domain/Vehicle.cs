using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRental_Model.Domain
{
    public class Vehicle
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Vehicle name is required")]
        [StringLength(100, ErrorMessage = "Vehicle name cannot exceed 100 characters")]
        public string VehicleName { get; set; }

        [Required]
        public int VehicleTypeId { get; set; }
        [ForeignKey("VehicleTypeId")]
        [ValidateNever]
        public VehicleType VehicleType { get; set; }

        [Range(1, 20, ErrorMessage = "Passenger count must be between 1 and 20")]
        public int Passenger { get; set; }

        [Range(0, 10, ErrorMessage = "Luggage capacity must be between 0 and 10")]
        public int Luggage { get; set; }

        [StringLength(50, ErrorMessage = "Engine type cannot exceed 50 characters")]
        public string EngineType { get; set; }

        [Range(1, 6, ErrorMessage = "Doors must be between 1 and 6")]
        public int Doors { get; set; }

        [StringLength(500, ErrorMessage = "Refueling info cannot exceed 500 characters")]
        public string Refueling { get; set; }

        [StringLength(500, ErrorMessage = "Car wash info cannot exceed 500 characters")]
        public string CarWash { get; set; }

        [StringLength(500, ErrorMessage = "No smoking info cannot exceed 50 characters")]
        public string NoSmoking { get; set; }

        [StringLength(500, ErrorMessage = "Included info cannot exceed 500 characters")]
        public string Included { get; set; }

        [StringLength(500, ErrorMessage = "Not included info cannot exceed 500 characters")]
        public string NotIncluded { get; set; }

        [Required(ErrorMessage = "Rent per day is required")]
        [Range(0, 100000, ErrorMessage = "Rent per day must be positive")]
        public decimal RentPerDay { get; set; }

        [Url(ErrorMessage = "Please enter a valid URL")]
        [StringLength(250, ErrorMessage = "Image URL cannot exceed 250 characters")]
        public string? ImageUrl { get; set; }  

        // Relationship
        [Required]
        public int BrandId { get; set; }

        [ForeignKey("BrandId")]
        [ValidateNever]
        public Brand Brand { get; set; }

        [NotMapped]
        public int AvailableQuantity { get; set; } = 1;
    }
}

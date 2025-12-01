using CarRental_DataAccess.Data;
using CarRental_DataAccess.Repository.IRepoSer;
using CarRental_Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental_DataAccess.Repository.RepoSer
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly CarRentalDbContext _db;
        public IRoleSer Roles { get; set; }
        public IUserSer Users { get; set; }
        public IBrandSer Brands { get; set; }
        public IVehicleSer Vehicles { get; set; }
        public IVehicleTypeSer VehicleType { get; set; }
        public IBookingSer Booking { get; set; }
        public IContactMessageSer ContactMessage { get; set; }

        public UnitOfWork(CarRentalDbContext db)
        {
            _db = db;
            Roles = new RoleSer(_db);
            Users = new UserSer(_db);
            Brands = new BrandSer(_db);
            Vehicles = new VehicleSer(_db);
            VehicleType = new VehicleTypeSer(_db);
            Booking = new BookingSer(_db);
            ContactMessage = new ContactMessageSer(_db);
            
        }

        public void Save()
        {
           _db.SaveChanges();
        }
    }
}

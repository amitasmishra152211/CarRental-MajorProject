using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental_DataAccess.Repository.IRepoSer
{
    public interface IUnitOfWork
    {
        public IRoleSer Roles { get; }
        public IUserSer Users { get; }
        public IBrandSer Brands { get; }
        public IVehicleSer Vehicles { get; }   
        public IVehicleTypeSer VehicleType { get; }
        public IBookingSer Booking { get; }
        public IContactMessageSer ContactMessage { get; }
        void Save();
    }
}

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
    public class RoleSer : Repository<Role>, IRoleSer
    {
        private readonly CarRentalDbContext _db;
        public RoleSer(CarRentalDbContext db) : base(db) 
        {
           _db = db; 
        }
    }
}

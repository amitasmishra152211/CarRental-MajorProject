using CarRental_DataAccess.Data;
using CarRental_DataAccess.Repository.IRepoSer;
using CarRental_Model.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental_DataAccess.Repository.RepoSer
{
    public class UserSer : Repository<User> , IUserSer
    {
        private readonly CarRentalDbContext _db;
        public UserSer(CarRentalDbContext db) : base(db) 
        {
            _db = db;
        }

        public User? GetByUsernameWithRole(string username)
        {
            return _db.Users.Include(u => u.Role)
                            .FirstOrDefault(u => u.Username == username);
        }
    }
}

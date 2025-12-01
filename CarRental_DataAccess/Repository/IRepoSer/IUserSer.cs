using CarRental_Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental_DataAccess.Repository.IRepoSer
{
    public interface IUserSer : IRepository<User>
    {
        User? GetByUsernameWithRole(string username);
    }
}

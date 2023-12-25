using AspProjectZust.Business.Abstract;
using AspProjectZust.DataAccess.Abstract;
using AspProjectZust.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspProjectZust.Business.Concrete
{
    public class UserService : IUserService
    {
        private IUserDal 
        public Task Add(CustomIdentityDbContext user)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<CustomIdentityDbContext>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<CustomIdentityDbContext> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task Update(CustomIdentityDbContext user)
        {
            throw new NotImplementedException();
        }
    }
}

using MODEL;
using MODEL.Entity;
using REPOSITORY.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPOSITORY.Repositories.Repository;

internal class UserRepository : GenericRepository<User>, IUserRepository
{

    public UserRepository(DataContext context) : base(context)
    {
    }
}

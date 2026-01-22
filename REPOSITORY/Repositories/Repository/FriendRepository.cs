using MODEL;
using MODEL.Entity;
using REPOSITORY.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPOSITORY.Repositories.Repository
{
    internal class FriendRepository : GenericRepository<Friend>, IFriendRepository
    {
        public FriendRepository(DataContext context) : base(context)
        {
        }
    }
 
}

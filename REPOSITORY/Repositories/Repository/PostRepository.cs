using MODEL;
using MODEL.Entity;
using REPOSITORY.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPOSITORY.Repositories.Repository;

internal class PostRepository : GenericRepository<Post>, IPostRepository
{

    public PostRepository(DataContext context) : base(context)
    {
    }
}

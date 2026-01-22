using MODEL.DTOs;
using REPOSITORY.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPOSITORY.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    IUserRepository Users { get; }
    IPostRepository Posts { get; }
    ICommentRepository Comments { get; }
    ILikeRepository Likes { get; }
    IMessageRepository Messages { get; }
    IFriendRepository Friends { get; }

    AppSettings AppSettings { get; set; }
    Task<int> SaveChangesAsync();

}

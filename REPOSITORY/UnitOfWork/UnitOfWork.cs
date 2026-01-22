using Microsoft.Extensions.Options;
using MODEL;
using MODEL.DTOs;
using REPOSITORY.Repositories.IRepository;
using REPOSITORY.Repositories.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPOSITORY.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly DataContext _dataContext;

    public UnitOfWork(DataContext dataContext, IOptions<AppSettings> appsettings)
    {
        _dataContext = dataContext;
        Users = new UserRepository(_dataContext);
        Posts = new PostRepository(_dataContext);
        Comments = new CommentRepository(_dataContext);
        Likes = new LikeRepository(_dataContext);
        Messages = new MessageRepository(_dataContext);
        Friends = new FriendRepository(_dataContext);
        AppSettings = appsettings.Value;
        
    }


    public IUserRepository Users { get; set; }

    public IPostRepository Posts { get; set; }

    public ICommentRepository Comments { get; set; }

    public ILikeRepository Likes { get; set; }

    public IMessageRepository Messages { get; set; }

    public IFriendRepository Friends { get; set; }

    public AppSettings AppSettings { get; set; }

    public void Dispose()
    {
        _dataContext.Dispose();
    }


    public async Task<int> SaveChangesAsync ()
    {
        return await _dataContext.SaveChangesAsync();
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace eShop.DataAccess
{
    public class UserUnitOfWork : UnitOfWork, IUserUnitOfWork
    {
        public IUserDbRepository UserDbRepository { get; private set; }

        public UserUnitOfWork(IUserDbRepository UserDbRepository,EshopDbContext context) : base(context)
        {
            this.UserDbRepository = UserDbRepository;
        }
    }

    public interface IUserUnitOfWork : IUnitOfWork
    {
        IUserDbRepository UserDbRepository { get; }
    }
}

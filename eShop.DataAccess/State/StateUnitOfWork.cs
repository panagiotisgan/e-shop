using System;
using System.Collections.Generic;
using System.Text;

namespace eShop.DataAccess
{
    public class StateUnitOfWork : UnitOfWork, IStateUnitOfWork
    {
        public IStateDbRepository StateDbRepository { get; private set; }
        public StateUnitOfWork(EshopDbContext context, IStateDbRepository stateDbRepository) : base(context)
        {
            StateDbRepository = stateDbRepository;
        }
    }

    public interface IStateUnitOfWork : IUnitOfWork
    {
        public IStateDbRepository StateDbRepository { get; }
    }
}

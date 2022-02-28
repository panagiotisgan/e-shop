using eShop.DataAccess.IRepositories;
using eShop.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShop.DataAccess
{
    public class OrderDetailsUnitOfWork : UnitOfWork, IOrderDetailsUnitOfWork
    {
        public IOrderDetailsDbRepository OrderDetailsDbRepository { get; private set; }
        public OrderDetailsUnitOfWork(EshopDbContext context, IOrderDetailsDbRepository orderDetailsDbRepository) : base(context)
        {
            OrderDetailsDbRepository = orderDetailsDbRepository;
        }
    }

    public interface IOrderDetailsUnitOfWork : IUnitOfWork
    {
        IOrderDetailsDbRepository OrderDetailsDbRepository { get; }
    }
}

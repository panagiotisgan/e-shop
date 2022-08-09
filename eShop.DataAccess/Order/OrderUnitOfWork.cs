
using eShop.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShop.DataAccess
{
    public class OrderUnitOfWork : UnitOfWork, IOrderUnitOfWork
    {
        public IOrderDbRepository OrderDdRepository {get; private set; }

        public OrderUnitOfWork(EshopDbContext context, IOrderDbRepository orderDbRepository) : base(context)
        {
            OrderDdRepository = orderDbRepository;
        }
    }

    public interface IOrderUnitOfWork : IUnitOfWork
    {
        IOrderDbRepository OrderDdRepository { get; }
    }
}

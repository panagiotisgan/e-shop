using System;
using System.Collections.Generic;
using System.Text;

namespace eShop.DataAccess
{
    public class CredentialUnitOfWork : UnitOfWork, ICredentialUnitOfWork
    {
        public ICredentialDbRepository CredentialDbRepository { get; private set; }
        public CredentialUnitOfWork(ICredentialDbRepository credentialDbRepository,EshopDbContext context) : base(context)
        {
            CredentialDbRepository = credentialDbRepository;
        }
    }

    public interface ICredentialUnitOfWork : IUnitOfWork
    {
        ICredentialDbRepository CredentialDbRepository { get;}
    }
}

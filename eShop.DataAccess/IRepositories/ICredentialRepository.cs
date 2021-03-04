using eShop.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShop.DataAccess.IRepositories
{
    public interface ICredentialRepository:IGetByNameRepository<Credential,EshopDbContext>
    {
    }
}

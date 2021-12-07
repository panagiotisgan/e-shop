using eShop.DataAccess.IRepositories;
using eShop.Model;

namespace eShop.DataAccess
{
    public class CategoryRepository : GenericRepository<Category,EshopDbContext>,ICategoryDbRepository
    {
        public CategoryRepository(EshopDbContext dbContext):base(dbContext)
        {
        }
    }

    public interface ICategoryDbRepository : IDbRepository<Category>, ICategoryRepository
    {

    }
}

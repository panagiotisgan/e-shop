using eShop.DataAccess.IRepositories;
using eShop.Model;

namespace eShop.DataAccess.Repositories
{
    public class CategoryRepository : GenericRepository<Category,EshopDbContext>,ICategoryRepository
    {
        public CategoryRepository(EshopDbContext dbContext):base(dbContext)
        {
        }
    }
}

using Tech.Assessment.Repository.DBContext;
using Tech.Assessment.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tech.Assessment.Repository.Repository
{
    public interface IOrderRepository : IBaseRepository<Order>
    {

    }
    public class OrderRepository: BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationDBContext defaultDbContext)
              : base(defaultDbContext)
        {
        }
    }
}

using Tech.Assessment.Repository.DBContext;
using Tech.Assessment.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Tech.Assessment.Model;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Tech.Assessment.Repository.Repository
{
    public interface IPackageCalculationRepository : IBaseRepository<PackageCalculationDetail>
    {
      
    }
    public  class PackageCalculationRepository : BaseRepository<PackageCalculationDetail>, IPackageCalculationRepository
    {
        private readonly IMapper _mapper;
        public PackageCalculationRepository(ApplicationDBContext defaultDbContext,IMapper mapper)
              : base(defaultDbContext)
        {
            _mapper = mapper;
        }



    }
}

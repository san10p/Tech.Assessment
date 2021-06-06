using AutoMapper;
using Tech.Assessment.API.AutoMapping;

namespace Tech.Assessment.UnitTest
{
    public class BaseServiceTests
    {
        public IMapper _mapper;
        public BaseServiceTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                //Create all maps here
                cfg.AddProfile(new AutoMappingProfile());
            });

            _mapper = config.CreateMapper();            
          
        }
    }
}
using AutoMapper;
using Tech.Assessment.Model;
using Tech.Assessment.API.DTO.Response.Order;
using Tech.Assessment.API.DTO.Request.Order;
using Tech.Assessment.Repository.Entity;
using Tech.Assessment.API.DTO.Response.Customer;
using Tech.Assessment.API.DTO.Response;

namespace Tech.Assessment.API.AutoMapping
{
    public class AutoMappingProfile:Profile
    {
        public AutoMappingProfile()
        {
            #region Between DTO and Model
            this.CreateMap<OrderDTO, OrderDetailModel>().ReverseMap();
            this.CreateMap<OrderDetailDTO, OrderDetailModel>()              
                 .ForMember(x => x.CreatedDate, x => x.MapFrom(m => m.CreatedAt))
                .ReverseMap();
            this.CreateMap<DTO.Request.Order.OrderItemDTO, OrderItemModel>().ReverseMap();
            this.CreateMap<DTO.Response.Order.ItemDetailDTO, OrderItemModel>().ReverseMap();
            this.CreateMap<CustomerModel, CustomerDTO>().ReverseMap();
            this.CreateMap<AddressModel, AddressDTO>().ReverseMap();
            
            #endregion

            #region Between Model and Entity

            this.CreateMap<Order, OrderDetailModel>().ReverseMap();

            this.CreateMap<Customer, AddressModel>();
            this.CreateMap<Customer, CustomerModel>()
                .ForMember(x => x.Address, x => x.MapFrom(m => m));

            this.CreateMap<OrderItem, OrderItemModel>().ReverseMap();
            #endregion
        }
    }
}

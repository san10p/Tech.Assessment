using Tech.Assessment.Model;
using Tech.Assessment.Model.Enum;
using Tech.Assessment.Repository.Repository;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;
using Tech.Assessment.Repository.Entity;
using AutoMapper;
using System.Collections.Generic;

namespace Tech.Assessment.Service
{
    public interface IOrderService
    {
        Task<ResponseObject<OrderDetailModel>> Get(string orderId);
        Task<ResponseObject<double>> Save(OrderDetailModel model);

    }
    public class OrderService : IOrderService
    {
        private readonly IPackageCalculationRepository _packageRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderService(IPackageCalculationRepository packageRepository, IOrderRepository orderRepository, IMapper mapper)
        {
            _packageRepository = packageRepository;
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<ResponseObject<OrderDetailModel>> Get(string orderId)
        {
            var response = new ResponseObject<OrderDetailModel>();
            var orderEntity = await _orderRepository.GetAsync(x => x.OrderID == orderId, s => s.Items, p => p.Customer);

            if (orderEntity == null)
            {
                response.Errors.Add(new ErrorResponseModel() { ErrorCode = ErrorCodeEnum.NotFound });
                return response;
            }

            var order = _mapper.Map<OrderDetailModel>(orderEntity);

            order.RequiredBinWidth = await GetRequiredBinWidth(order.Items);

            response.Result = order;
            return response;
        }

        public async Task<ResponseObject<double>> Save(OrderDetailModel model)
        {
            var response = new ResponseObject<double>() { Errors = new List<ErrorResponseModel>() };

            var errors = await ValidateOrderAsync(model);

            if (errors.Count() > 0)
            {
                response.Errors.AddRange(errors);
                return response;
            }

            var entity = _mapper.Map<Order>(model);
            var newEntity = await _orderRepository.CreateAsync(entity);

            var newOrder = _mapper.Map<OrderDetailModel>(newEntity);

            response.Result = await GetRequiredBinWidth(newOrder.Items); ;
            return response;
        }

        private async Task<IEnumerable<ErrorResponseModel>> ValidateOrderAsync(OrderDetailModel model)
        {
            var errors = new List<ErrorResponseModel>();
            if (model == null)
            {
                errors.Add(new ErrorResponseModel() { ErrorCode = ErrorCodeEnum.RequiredField });
                return errors;
            }

            if (model.Items == null || model.Items.Count == 0)
            {
                errors.Add(new ErrorResponseModel() { ErrorCode = ErrorCodeEnum.RequiredField, PropertyName = nameof(model.Items), ErrorMessage = "No Items" });
                return errors;
            }

            var order = await _orderRepository.GetAsync(x => x.OrderID == model.OrderID);
            if (order != null)
            {
                errors.Add(new ErrorResponseModel() { ErrorCode = ErrorCodeEnum.AlreadyExists, PropertyName = nameof(model.OrderID), ErrorMessage = "OrderId should be unique" });
                return errors;
            }
            return errors;
        }

        private async Task<double> GetRequiredBinWidth(List<OrderItemModel> items)
        {
            double binWidth = 0;
            var packageCalculations = await _packageRepository.GetListAsync();

            var itemLst = items.GroupBy(x => x.ProductType);
            foreach (var item in itemLst)
            {
                var quantity = item.Sum(x => x.Quantity);
                var pkgCal = packageCalculations.Where(x => x.ProductType == (int)item.Key).FirstOrDefault();
                if (pkgCal != null)
                {
                    binWidth += Math.Ceiling((double)quantity / pkgCal.StackCapacity) * pkgCal.MinWidth;
                }

            }

            return binWidth;
        }
    }
}

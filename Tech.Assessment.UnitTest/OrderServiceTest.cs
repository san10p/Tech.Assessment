using AutoMapper;
using NSubstitute;
using Tech.Assessment.Model.Enum;
using Tech.Assessment.Repository.Entity;
using Tech.Assessment.Repository.Repository;
using Tech.Assessment.Service;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;
using System.Collections.Generic;
using Tech.Assessment.Model;
using System.Collections;

namespace Tech.Assessment.UnitTest
{
    public class OrderServiceTest : BaseServiceTests
    {

        private readonly IPackageCalculationRepository _packageRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderService _orderService;

        public OrderServiceTest()
        {

            _packageRepository = Substitute.For<IPackageCalculationRepository>();
            _orderRepository = Substitute.For<IOrderRepository>();
            _orderService = new OrderService(_packageRepository, _orderRepository, _mapper);
        }

        [Theory]
        [ClassData(typeof(ItemsWithExpectedBinWidth))]
        public void GetOrder_ReturnsOrderDetail_WithMinimumBinWidth(List<OrderItem> items, double expectedBinWidth)
        {
            var order = GetMockedOrder(items);
            _orderRepository.GetAsync(Arg.Any<Expression<Func<Order, bool>>>(), Arg.Any<Expression<Func<Order, object>>[]>())
                .Returns(order);
            _packageRepository.GetListAsync()
                .Returns(GetPackageCalcMock());


            var response = _orderService.Get("ORD-01").Result;


            Assert.NotNull(response.Result);
            Assert.Equal(expectedBinWidth, response.Result.RequiredBinWidth);
        }


        [Fact]
        public void GetOrder_ReturnsNotFoundError()
        {            
            _orderRepository.GetAsync(Arg.Any<Expression<Func<Order, bool>>>(), Arg.Any<Expression<Func<Order, object>>[]>())
                .Returns((Order)null);

            _packageRepository.GetListAsync()
                .Returns(GetPackageCalcMock());


            var response = _orderService.Get("ORD-02").Result;


            Assert.Null(response.Result);
            Assert.NotNull(response.Errors);
            Assert.Equal(ErrorCodeEnum.NotFound, response.Errors.FirstOrDefault().ErrorCode);
        }

        [Theory]
        [ClassData(typeof(ItemsWithExpectedBinWidth))]
        public void SaveOrder_ReturnsMinimumBinWidth(List<OrderItem> items, double expectedBinWidth)
        {
            var orderModel = GetMockedOrderModel(items);
            var savedOrder = GetMockedOrder(items);

            _orderRepository.GetAsync(Arg.Any<Expression<Func<Order, bool>>>())
                .Returns((Order)null);

            _orderRepository.CreateAsync(Arg.Any<Order>())
                .Returns(savedOrder);

            _packageRepository.GetListAsync()
                .Returns(GetPackageCalcMock());


            var response = _orderService.Save(orderModel).Result;


            Assert.True(response.Errors.Count==0);
            Assert.Equal(expectedBinWidth, response.Result);
        }

        [Fact]
        public void SaveOrder_ReturnsNoItemError()
        {
            var orderModel = GetMockedOrderModelWithNoItems();

            var response = _orderService.Save(orderModel).Result;

            Assert.True(response.Errors.Count > 0);
            Assert.Equal(ErrorCodeEnum.RequiredField, response.Errors.FirstOrDefault().ErrorCode);
        }



        [Fact]
        public void SaveOrder_ReturnsError_OrderIdAlreadyExists()
        {
            var orderModel = GetMockedOrderModel(GetOrderItemsMock());

            var orderEntity = _mapper.Map<Order>(orderModel);

            _orderRepository.GetAsync(Arg.Any<Expression<Func<Order, bool>>>())
             .Returns(orderEntity);

            var response = _orderService.Save(orderModel).Result;


            Assert.True(response.Errors.Count > 0);
            Assert.Equal(ErrorCodeEnum.AlreadyExists, response.Errors.FirstOrDefault().ErrorCode);
        }

        #region Mocked Data
        private IEnumerable<PackageCalculationDetail> GetPackageCalcMock()
        {
            var lst = new List<PackageCalculationDetail>();
            lst.Add(new PackageCalculationDetail
            {
                Id = 1,
                ProductType = (int)ProductType.PhotoBook,
                ProductSymbol = "0",
                MinWidth = 19,
                WidthUnit = "mm",
                StackCapacity = 1,
                CreatedBy = 100,
                CreatedOn = DateTime.UtcNow,
                IsActive = true,
                IsDeleted = false,
            });
            lst.Add(new PackageCalculationDetail
            {
                Id = 2,
                ProductType = (int)ProductType.Calendar,
                ProductSymbol = "|",
                MinWidth = 10,
                WidthUnit = "mm",
                StackCapacity = 1,
                CreatedBy = 100,
                CreatedOn = DateTime.UtcNow,
                IsActive = true,
                IsDeleted = false,
            });
            lst.Add(new PackageCalculationDetail
            {
                Id = 3,
                ProductType = (int)ProductType.Canvas,
                ProductSymbol = "*",
                MinWidth = 16,
                WidthUnit = "mm",
                StackCapacity = 1,
                CreatedBy = 100,
                CreatedOn = DateTime.UtcNow,
                IsActive = true,
                IsDeleted = false,
            });
            lst.Add(new PackageCalculationDetail
            {
                Id = 4,
                ProductType = (int)ProductType.Cards,
                ProductSymbol = "$",
                MinWidth = 4.7,
                WidthUnit = "mm",
                StackCapacity = 1,
                CreatedBy = 100,
                CreatedOn = DateTime.UtcNow,
                IsActive = true,
                IsDeleted = false,
            });
            lst.Add(new PackageCalculationDetail
            {
                Id = 5,
                ProductType = (int)ProductType.Mug,
                ProductSymbol = ".",
                MinWidth = 94,
                WidthUnit = "mm",
                StackCapacity = 4,
                CreatedBy = 100,
                CreatedOn = DateTime.UtcNow,
                IsActive = true,
                IsDeleted = false,
            });

            return lst;
        }

        private Order GetMockedOrder(List<OrderItem> items)
        {
            var order = new Order()
            {

                Id = 1,
                OrderID = "ORD-01",
                Items = items,
                CustomerId = 1,
                Customer = new Customer() { Name = "cus01", Id = 1 }

            };

            return order;
        }

        private OrderDetailModel GetMockedOrderModel(List<OrderItem> items)
        {
            var orderItems = _mapper.Map<List<OrderItemModel>>(items);

            var order = new OrderDetailModel()
            {
                Items = orderItems,
                OrderID = "ORD-01",
                CustomerId = 1
            };

            return order;
        }

        private OrderDetailModel GetMockedOrderModelWithNoItems()
        {
            var order = new OrderDetailModel() { Items = null, OrderID = "ORD-01", CustomerId = 1 };

            return order;
        }

        private List<OrderItem> GetOrderItemsMock()
        {
            return new List<OrderItem>()
                {
                    new OrderItem() { OrderId = 1, ProductType = (int)ProductType.Canvas, Quantity = 2 },
                    new OrderItem() { OrderId = 1, ProductType = (int)ProductType.Cards, Quantity = 3 }
                };
        }
        #endregion

        #region TestInput
        public class ItemsWithExpectedBinWidth : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[]
                { new List<OrderItem>()
                {
                    new OrderItem() { OrderId=1, ProductType=(int)ProductType.Canvas, Quantity=2 },
                    new OrderItem() {   OrderId=1,ProductType = (int)ProductType.Cards, Quantity = 3 }
                }
                , 46.1
                };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        #endregion
    }
}

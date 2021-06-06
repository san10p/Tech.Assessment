using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tech.Assessment.API.Controllers;
using Tech.Assessment.API.DTO.Response.Order;
using Tech.Assessment.Model;
using Tech.Assessment.Service;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Tech.Assessment.API.DTO.Request.Order;

[Route("api/[controller]")]
public class OrdersController : BaseController
{
    private readonly IOrderService _orderService;
    public OrdersController(IOrderService  orderService, IMapper mapper) : base(mapper)
    {
        _orderService = orderService;
      
    }

    [HttpGet("{orderId}")]
    [ProducesResponseType(typeof(OrderDetailDTO), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(List<ErrorResponseModel>), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetOrderDetail(string orderId)
    {
        var model = await _orderService.Get(orderId);

        var response = GetResponseDTOModel<OrderDetailDTO, OrderDetailModel>(model);

        return ReturnResponse(response);
    }

    /*ASSUMPTION: Customer is already loggedIn or Customer Info is being saved already by separate API. Hence Just passing customerId*/
    [HttpPost]
    [ProducesResponseType(typeof(double), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(List<ErrorResponseModel>), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> SaveOrderDetail([FromBody]OrderDTO order)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var model = _mapper.Map<OrderDetailModel>(order);

        var response = await _orderService.Save(model);
   
        return ReturnResponse(response);
    }
}
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Tech.Assessment.Model;
using Tech.Assessment.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tech.Assessment.API.Controllers
{
    public class BaseController : ControllerBase
    {
      
        protected readonly IMapper _mapper;
      
        public BaseController(IMapper mapper)
        {
            _mapper = mapper;
          
        }

        public ResponseObject<TEntity> GetResponseDTOModel<TEntity, T>(ResponseObject<T> sourceResponse)
        {
            var targetResponse = new ResponseObject<TEntity>();
            targetResponse.Errors = sourceResponse.Errors;
            if (sourceResponse.Result != null)
            {
                targetResponse.Result = _mapper.Map<TEntity>(sourceResponse.Result);
            }

            return targetResponse;
        }

        public IActionResult ReturnResponse<T>(ResponseObject<T> response)
        {
            if (response.Errors!=null && response.Errors.Count>0)
            {
                if (response.Errors.Any(x => x.ErrorCode == ErrorCodeEnum.NotFound))
                    return NotFound(response.Errors.Where(x => x.ErrorCode == ErrorCodeEnum.NotFound).FirstOrDefault());
               else
                return BadRequest(response.Errors);
            }
           
            return Ok(response.Result);
        }
    }
}

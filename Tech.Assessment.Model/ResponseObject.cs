using Tech.Assessment.Model.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tech.Assessment.Model
{
   public class ResponseObject<T>
    {
        public ResponseObject() {

            Errors = new List<ErrorResponseModel>();
        
        }
        public T Result { get; set; }
        public List<ErrorResponseModel> Errors { get; set; }
    }
    public class ErrorResponseModel
    {
        public ErrorCodeEnum ErrorCode { get; set; }
        public string PropertyName { get; set; }
        public string ErrorMessage { get; set; }
    }
}

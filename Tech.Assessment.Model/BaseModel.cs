using System;
using System.Collections.Generic;
using System.Text;

namespace Tech.Assessment.Model
{
    public class BaseModel<TKey>
    {
        public TKey Id { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
    }
}

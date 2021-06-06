using System;
using System.Collections.Generic;
using System.Text;

namespace Tech.Assessment.Repository.Entity
{
    public class PackageCalculationDetail : BaseEntity<int>
    {      
        public int ProductType { get; set; }
        public double MinWidth { get; set; }
        public string WidthUnit { get; set; }
        public int StackCapacity { get; set; }
        public string ProductSymbol { get; set; }
       
    }
}

using System;
using System.Collections.Generic;
using Angular.Data.Modals.Base;


namespace Angular.Data.Modals
{
    public partial class OrderDetailsExtended : Entity
    {
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public short Quantity { get; set; }
        public float Discount { get; set; }
        public Nullable<decimal> ExtendedPrice { get; set; }
    }
}

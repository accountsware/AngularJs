using System;
using System.Collections.Generic;
using Angular.Core.Modals.Base;


namespace Angular.Core.Modals
{
    public partial class Shipper : Entity
    {
        public Shipper()
        {
            this.Orders = new List<Order>();
        }

        public int ShipperID { get; set; }
        public string CompanyName { get; set; }
        public string Phone { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}

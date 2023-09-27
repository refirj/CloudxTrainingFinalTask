using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;

namespace Microsoft.eShopWeb.ApplicationCore.Entities;
public class Deliveryitem
{
    public string id { get; set; }

    public string BuyerID { get; set; }
    public Address ShipToAddress { get; set; }

    public Decimal finalprice { get; set; }

    public List<OrderItem> items { get; set; }
}

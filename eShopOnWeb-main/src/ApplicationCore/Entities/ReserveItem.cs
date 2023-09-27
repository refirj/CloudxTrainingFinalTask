using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;

namespace Microsoft.eShopWeb.ApplicationCore.Entities;

public class ReserveOrderDetails
{
    public string Id { get; set; }

    public string? BuyerID { get; set; }


    public  List<OrderItem> orderItems = new List<OrderItem>();

}


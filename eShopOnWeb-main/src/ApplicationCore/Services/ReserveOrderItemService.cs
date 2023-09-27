using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Newtonsoft.Json;
using Azure.Messaging.ServiceBus;
using System.IO;

namespace Microsoft.eShopWeb.ApplicationCore.Services;
public class ReserveOrderItemService : IReserveOrderItem
{
    private readonly ServiceBusSender _sBSender;
    public ReserveOrderItemService(ServiceBusSender SBSender)
    {
        _sBSender = SBSender;       
    }

    public async Task ReserveOrderItemAsync(Order order)
    {
        var selecteditem = new ReserveOrderDetails()
        {
            Id = Guid.NewGuid().ToString(),
            BuyerID = order.BuyerId,
            orderItems = order.OrderItems.ToList<OrderItem>()
        };
        var jsonitem = JsonConvert.SerializeObject(selecteditem);
        await _sBSender.SendMessageAsync(new ServiceBusMessage(jsonitem));
        
      
    }
}

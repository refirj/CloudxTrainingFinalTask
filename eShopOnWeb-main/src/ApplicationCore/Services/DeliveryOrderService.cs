using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Microsoft.eShopWeb.ApplicationCore.Services;

public class DeliveryOrderService : IDeliveryOrderService
{
    private readonly HttpClient _namedClient;
    private readonly static string clientName = "ProcessDelivery";
    

    public DeliveryOrderService(IHttpClientFactory httpFactory)
    {        
        
        this._namedClient = httpFactory.CreateClient(clientName);   
    }

    public async Task ProcessDeliveryOrderAsync(Order order)

    {
        var selecteditem = new Deliveryitem()
        {
            id = Guid.NewGuid().ToString(),
            BuyerID = order.BuyerId,
            ShipToAddress=order.ShipToAddress,
            finalprice=order.Total(),
            items=order.OrderItems.ToList<OrderItem>()
            };


        var jsonitem = JsonConvert.SerializeObject(selecteditem);
        var payload=new StringContent(jsonitem,Encoding.UTF8, "application/json");
        await _namedClient.PostAsync(_namedClient.BaseAddress,
                                           payload);
                                              
      
    }
   

}


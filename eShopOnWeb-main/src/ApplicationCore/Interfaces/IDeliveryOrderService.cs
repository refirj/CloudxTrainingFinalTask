using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;

namespace Microsoft.eShopWeb.ApplicationCore.Interfaces;

public interface IDeliveryOrderService
{
    Task ProcessDeliveryOrderAsync(Order order);
}

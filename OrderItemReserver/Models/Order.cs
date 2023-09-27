using Azure.Core.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ReserveOrderFinal.Models
{
    public class OrderDetails
    {
        public OrderDetails() { }   
        [JsonPropertyName("Id")]
        public Guid Id { get; set; }

        [JsonPropertyName("BuyerID")]
        public string? BuyerID { get; set; }

        [JsonPropertyName("orderItems")]
        public readonly IEnumerable<OrderItem> orderItems = new List<OrderItem>();

    }
}

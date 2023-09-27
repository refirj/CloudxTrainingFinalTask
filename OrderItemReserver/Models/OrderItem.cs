using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ReserveOrderFinal.Models
{
    public class OrderItem
    {
        
        public int Id { get; set; }
        
        public orderspec Order=new orderspec();
       
        [JsonPropertyName("UnitPrice")]
        public string UnitPrice { get;  set; }
       
        [JsonPropertyName("Units")]
        public int Units { get;  set; }
    }
    public class orderspec
    {
        [JsonProperty(PropertyName = "PictureUri")]
        public string PictureUri { get; set; }
        [JsonProperty(PropertyName = "CatalogItemId")]
        public string CatalogItemId { get; set; }
        [JsonProperty(PropertyName = "ProductName")]
        public string ProductName { get; set; }
    }
}

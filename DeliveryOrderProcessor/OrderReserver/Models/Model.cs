using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderReserver.Models
{
    public class Reserveitem
    {
        public string id { get; set; }
        public string BuyerID { get; set; }
        public Address ShipToAddress { get; set; }

        public Decimal finalprice { get; set; }

        public List<OrderItem> items { get; set; }
    }
    public class Address // ValueObject
    {
        public string Street { get; private set; }

        public string City { get; private set; }

        public string State { get; private set; }

        public string Country { get; private set; }

        public string ZipCode { get; private set; }

#pragma warning disable CS8618 // Required by Entity Framework
        private Address() { }

        public Address(string street, string city, string state, string country, string zipcode)
        {
            Street = street;
            City = city;
            State = state;
            Country = country;
            ZipCode = zipcode;
        }
    }


    public class OrderItem : BaseEntity
    {
        public CatalogItemOrdered ItemOrdered { get; private set; }
        public decimal UnitPrice { get; private set; }
        public int Units { get; private set; }

#pragma warning disable CS8618 // Required by Entity Framework
        private OrderItem() { }

        public OrderItem(CatalogItemOrdered itemOrdered, decimal unitPrice, int units)
        {
            ItemOrdered = itemOrdered;
            UnitPrice = unitPrice;
            Units = units;
        }
    }
    public abstract class BaseEntity
    {
        public virtual int Id { get; protected set; }
    }

    public class CatalogItemOrdered // ValueObject
    {
        public CatalogItemOrdered(int catalogItemId, string productName, string pictureUri)
        {


            CatalogItemId = catalogItemId;
            ProductName = productName;
            PictureUri = pictureUri;
        }

#pragma warning disable CS8618 // Required by Entity Framework
        private CatalogItemOrdered() { }

        public int CatalogItemId { get; private set; }
        public string ProductName { get; private set; }
        public string PictureUri { get; private set; }
    }
}

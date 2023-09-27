using Google.Protobuf.Reflection;
using Grpc.Net.Client.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ReserveOrderFinal.Interface;
using ReserveOrderFinal.Models;
using ReserveOrderFinal.Models.ConfigModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReserveOrderFinal.Services
{
    public class EmailService : IEmailService
    {
        private readonly HttpClient _namedClient;
        private readonly EmailServiceConfig _emailServiceConfig;
        private readonly EmailTemplate emailTemplate;
        public EmailService(IOptions<EmailServiceConfig> options,
                             IHttpClientFactory httpClientFactory,
                             EmailTemplate template)
        {
            _emailServiceConfig = options.Value;
            emailTemplate = template;
            _namedClient= httpClientFactory.CreateClient("SendEmail");

        }
        public async Task<int> SendEmailAsync(OrderDetails payload)
        {
            GenerateEmailTemplate(payload);
            await PostEmailAsync(emailTemplate);
            return 1;
         
        }
        private void GenerateEmailTemplate(OrderDetails payload)
        {
            emailTemplate.toAddress = _emailServiceConfig.EmailToAddress;
            //emailTemplate.fromAddress = _emailServiceConfig.EmailFromAddress;
            emailTemplate.subject = _emailServiceConfig.EmailSubject+""+payload.Id;

            //string emailbody = "A new order with ID- <b>" + payload.Id + "</b> has been Created by user <b>" + payload.BuyerID + "</b><br>";
            //emailbody += "<br>Please find the order details below<br><br>";
            //emailbody += "<table style=\"width:100%\"border=\"3\" ><tr><th>Product ID</th><th>Product Name</th><th>Units</th></tr><tbody>";
        
            //foreach (OrderItem item in payload.orderItems)
            //{
            //    var tr = "<tr>";
            //    tr += "<td>" + item.CatalogItemId.ToString() + "</td><td>" + item.ProductName + "</td><td>" + item.Units.ToString() + "</td></tr>";
            //    emailbody += tr;
            //}
            //emailbody += "</tbody></table>";
            emailTemplate.body  = JsonConvert.SerializeObject(payload); 
        }
        private async Task PostEmailAsync(EmailTemplate template)
        {
            var jsonitem = JsonConvert.SerializeObject(template);
            var payload = new StringContent(jsonitem, Encoding.UTF8, "application/json");
           await _namedClient.PostAsync(_namedClient.BaseAddress,
                                               payload);
           
        }

    }
}

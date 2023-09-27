using System;
using System.Text;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ReserveOrderFinal.Interface;
using ReserveOrderFinal.Models;
using static System.Net.Mime.MediaTypeNames;

namespace ReserveOrderFinal
{
    public class Function1
    {
        private readonly ILogger<Function1> _logger;
      //  private IBlobService _blobService;

        public Function1(ILogger<Function1> logger
            //,IBlobService blobService
            )
        {
            _logger = logger;
            //_blobService = blobService;
        }

        [Function(nameof(Function1))]
        public async Task Run([ServiceBusTrigger("itemreservesb", Connection = "SbConnString")] ServiceBusReceivedMessage message)
        {
            //_logger.LogInformation("Message ID: {id}", message.MessageId);
            //_logger.LogInformation("Message Body: {body}", message.Body);
           
        
            //_logger.LogInformation("Message Content-Type: {contentType}", message.ContentType);
            var a=message.Body.ToObjectFromJson<OrderDetails>();
            string requestbody;
            using (StreamReader reader = new StreamReader(message.Body.ToStream(), Encoding.UTF8, true, 1024, true))
            {
                requestbody = reader.ReadToEnd();
            }

            var payload = JsonConvert.DeserializeObject(message.Body.ToString());

        //    OrderDetails ord = payload;
            await Task.CompletedTask;
         // await  _blobService.ReserveOrderAsync(payload,message.Body);
        }
    }
}

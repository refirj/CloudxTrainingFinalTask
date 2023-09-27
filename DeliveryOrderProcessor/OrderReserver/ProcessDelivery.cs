using System;
using System.Net;
using System.Text;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OrderReserver.Models;

namespace OrderReserver
{
    public class ProcessDelivery
    {
        private readonly ILogger _logger;
        private readonly CosmosClient _cosmosclient;

        public ProcessDelivery(ILoggerFactory loggerFactory, CosmosClient cosmos)
        {
            _logger = loggerFactory.CreateLogger<ProcessDelivery>();
            _cosmosclient = cosmos;
        }

        [Function("ProcessDelivery")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req )
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            string requestbody = "";
            using (StreamReader reader
                  = new StreamReader(req.Body, Encoding.UTF8, true, 1024, true))
            {
                requestbody = reader.ReadToEnd();
            }
            Database database = await _cosmosclient.CreateDatabaseIfNotExistsAsync("ReserveOrder");
            Container container = await database.CreateContainerIfNotExistsAsync("OrderDetails", "/id");
            var jreader = JsonConvert.DeserializeObject<Reserveitem>(requestbody);

            var createdItem = await container.CreateItemAsync<Reserveitem>
                (jreader,new PartitionKey(jreader.id)); 
            
            var response = req.CreateResponse(HttpStatusCode.OK);
         
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            response.WriteString("Order Processed");

            return response;
        }

       

    }
}

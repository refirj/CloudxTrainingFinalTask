using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Polly;
using Polly.Contrib.WaitAndRetry;
using Polly.Fallback;
using ReserveOrderFinal.Interface;
using ReserveOrderFinal.Models;
using ReserveOrderFinal.Models.ConfigModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ReserveOrderFinal.Services
{
    internal class BlobService : IBlobService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly BlobContainerClient _containerClient;
        private BlobClient? _blobClient;
        private readonly int NUMBER_OF_RETRIES;
        private readonly BlobserviceConfig _config;
        private readonly IEmailService _emailService;
        private readonly ILogger<IBlobService> _logger;
        private OrderDetails _orderDetails;


        public BlobService(BlobServiceClient blobServiceClient,
                            IOptions<BlobserviceConfig> options,
                            ILogger<IBlobService> logger,
                             IEmailService emailService)
        {
            _blobServiceClient = blobServiceClient;
            _config = options.Value;
            _containerClient = blobServiceClient.GetBlobContainerClient(_config.ContainerName);
            NUMBER_OF_RETRIES = _config.RetryCount;
            _logger = logger;
            _emailService = emailService;

        }
        public async Task ReserveOrderAsync(OrderDetails payload, BinaryData message)
        {
           // GetBlobClient(payload);
            await UploadfilesAsync(payload, message);

        }

        public Task DeleteAsync()
        {
            throw new NotImplementedException();
        }

        public Task DownloadAsync()
        {
            throw new NotImplementedException();
        }
        private void GetBlobClient(OrderDetails Payload)
        {
            
            var orderId = Payload.Id;
           // _blobClient = _containerClient.GetBlobClient($"Order/{orderId}");

        }
        private async Task UploadfilesAsync(OrderDetails payload, BinaryData message)
        {
            _orderDetails = payload;

            //IAsyncPolicy<Response<BlobContainerInfo>> fallbackpolicy = Policy
            //                      .Handle<Azure.RequestFailedException>()
            //                      .FallbackAsync((rft,co,ct) =>
                                  
                                     
                                  

            var retrypolicy = Policy
                 .Handle<Azure.RequestFailedException>()
                 .WaitAndRetryAsync(Backoff.DecorrelatedJitterBackoffV2(TimeSpan.FromSeconds(1), NUMBER_OF_RETRIES),
                                   (result, timespan, count,context) => _logger.LogInformation($"Retrying count is ({count})...."));

            try
            {
                await retrypolicy
                 //.WrapAsync()
                 .ExecuteAsync(() => _containerClient.UploadBlobAsync($"Order/{payload.BuyerID}/{payload.Id}", message.ToStream()));
            }
            catch (Exception ex)
            {
                _logger.LogInformation("executing fallback -- Sending email");
               await _emailService.SendEmailAsync(payload);
            }


           // _logger.LogInformation($"reult of upload- {result.Value}");
            // await fallbackpolicy
            //.WrapAsync(retrypolicy
            //.(() => _containerClient.UploadBlobAsync($"Order/{payload.BuyerID}/{payload.Id}", message.ToStream()));Policy.Wrap(fallback, waitAndRetry, breaker).Execute(action);

            // await Policy.WrapAsync(fallbackpolicy, retrypolicy)
            //  .ExecuteAsync(() => _containerClient.UploadBlobAsync($"Order/{payload.BuyerID}/{payload.Id}", message.ToStream()));
            //  await  _containerClient.UploadBlobAsync($"Order/{payload.BuyerID}/{payload.Id}", message.ToStream());
        }
        private Task OnFallbackAsync(DelegateResult<Azure.RequestFailedException> response, Context context)
        {
            
            _logger.LogInformation("Fallback is called");
            return Task.CompletedTask;
        }

        private   Task FallbackAction(DelegateResult<RequestFailedException> responseToFailedRequest, Context context, CancellationToken cancellationToken)
        {

            Console.WriteLine("Fallback action is executing");



            _emailService.SendEmailAsync(_orderDetails);
            //    RequestFailedException azfailed = new RequestFailedException(rft.);
            BlobContainerInfo info = (BlobContainerInfo)responseToFailedRequest.Result.Data;
            var rse =responseToFailedRequest.Result.GetRawResponse();
            Response < BlobContainerInfo > response = Response.FromValue(info, rse);
            return Task.FromResult(response);
        }






    }
}

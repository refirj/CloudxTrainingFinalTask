using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReserveOrderFinal.Models.ConfigModels
{
  
        public class BlobserviceConfig
        {
            public int RetryCount { get; set; }
            public string? ContainerName { get; set; }
            

        }
    
}

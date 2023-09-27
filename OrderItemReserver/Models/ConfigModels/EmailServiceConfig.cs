using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReserveOrderFinal.Models.ConfigModels
{
    public class EmailServiceConfig
    {
        public string? EmailclientName { get; set; }
        public string? EmailFromAddress { get; set; }
        public string? EmailToAddress { get; set; }
        public string? EmailSubject { get; set; }
    }
}

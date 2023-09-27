using ReserveOrderFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReserveOrderFinal.Interface
{
    public interface IBlobService
    {
        public Task ReserveOrderAsync(OrderDetails payload,BinaryData binaryData);
        public Task DeleteAsync();
        public Task DownloadAsync();



    }
}

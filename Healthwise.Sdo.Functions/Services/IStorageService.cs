using Azure.Messaging.EventHubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Healthwise.Sdo.Functions.Services
{
    public interface IStorageService
    {
        public Task AddEventAsync(EventData eventdata);
    }
}

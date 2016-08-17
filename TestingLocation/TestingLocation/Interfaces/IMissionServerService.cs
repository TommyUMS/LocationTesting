using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Plugin.Geolocator.Abstractions;

namespace TestingLocation.Interfaces
{
    public interface IMissionServerService
    {
        Task<HttpResponseMessage> SendLocation(Position position, string msisdn);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Geolocator.Abstractions;

namespace TestingLocation.Interfaces
{
    public interface ILocationDataService
    {
        Task<Position> TestLoc();
        Task<bool> StartPeriodicLocationUpdate();
        Task<bool> StopPeriodicLocationUpdate();
        bool IsLocatorListening();
    }
}

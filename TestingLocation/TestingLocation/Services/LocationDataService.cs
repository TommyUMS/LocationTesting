using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using TestingLocation.Interfaces;
using Xamarin.Forms;


namespace TestingLocation.Services
{
    class LocationDataService : ILocationDataService
    {
        public async Task<Position> TestLoc()
        {
            try
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;
                if (locator.IsGeolocationEnabled)
                {
                    var position = await locator.GetPositionAsync(timeoutMilliseconds: 20000);

                    Debug.WriteLine("Position Status: {0}", position.Timestamp);
                    Debug.WriteLine("Position Latitude: {0}", position.Latitude);
                    Debug.WriteLine("Position Longitude: {0}", position.Longitude);
                    return position;
                }
                else
                {
                    Debug.WriteLine("IsGeoLocationEnabled false");
                    return null;
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to get location, may need to increase timeout: " + ex);
            }
            return null;
        }

        public async Task<bool> StartPeriodicLocationUpdate()
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;
            locator.AllowsBackgroundUpdates = true;
            locator.PausesLocationUpdatesAutomatically = false;

            locator.PositionChanged += (sender, e) =>
            {
                Debug.WriteLine("Position changed");
                MessagingCenter.Send(this, "PositionChanged", e.Position);
            };

            /*var settings = new ListenerSettings();
            settings.ActivityType = ActivityType.Other;
            settings.AllowBackgroundUpdates = true;
            settings.ListenForSignificantChanges = true;
            settings.PauseLocationUpdatesAutomatically = false;*/

            if (!locator.IsListening)
                return await locator.StartListeningAsync(36000000, 500);

            return false;
        }

        public async Task<bool> StopPeriodicLocationUpdate()
        {
            var locator = CrossGeolocator.Current;
            return await locator.StopListeningAsync();
        }

        public bool IsLocatorListening()
        {
            var locator = CrossGeolocator.Current;
            return locator.IsListening;
        }
    }
}

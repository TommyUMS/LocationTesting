using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Xamarin.Forms;
using PropertyChanged;
using TestingLocation.Interfaces;
using TestingLocation.Services;
using TestingLocation.Helpers;
using Xamarin.Forms.Maps;
using Position = Plugin.Geolocator.Abstractions.Position;
using TK.CustomMap;
using TK.CustomMap.Overlays;

namespace TestingLocation.ViewModels
{

    [ImplementPropertyChanged]
    public class LocationViewModel: FreshMvvm.FreshBasePageModel
    {
        private ILocationDataService _locationDataService;
        private IUserDialogs _userDialogs;
        private IMissionServerService _missionServerService;

        public IObservable<TKCustomMapPin> AedPins { get; set; }

        public IObservable<TKRoute> TKRoutes { get; set; }

        public string Filtext { get; set; }

        public String Timestamp { get; set; }

        public String TimestampStarted { get; set; }

        public Position GeoPosition { get; set; }

        public int NumberOfUpdates { get; set; }

        public bool Listening { get; set; }

        public MapType MapType { get; set; }
        
        public Xamarin.Forms.Maps.Position MapCenter { get; set; }

        

        public LocationViewModel(ILocationDataService locationDataService, IUserDialogs userDialogs, IMissionServerService missionServerService)
        {
            _locationDataService = locationDataService;
            _userDialogs = userDialogs;
            _missionServerService = missionServerService;
        }

        public override void Init(object initData)
        {
            base.Init(initData);
            
            Timestamp = "Not updated yet!";
            NumberOfUpdates = 0;
            MapType = MapType.Street;            
            MapCenter = new Xamarin.Forms.Maps.Position(60.3382023852435, 5.3437454901496);

            try
            {
                MessagingCenter.Subscribe<LocationDataService, Position>(
                    this, "PositionChanged", async (sender, args) =>
                    {
                        GeoPosition = args as Position;
                        if (GeoPosition != null)
                        {
                            Timestamp = GeoPosition.Timestamp.ToLocalTime().ToString("HH:mm:ss");
                            NumberOfUpdates++;
                           
                            Debug.WriteLine("LocationLog.txt" +
                                    Timestamp + " new position");
                            await _missionServerService.SendLocation(GeoPosition, "4797109265");
                            Filtext = DependencyService.Get<ILog>().LoadText("LocationLog.txt");
                            MapCenter = ConverterPosition.FromGeoPosToXamPos(GeoPosition);
                        }
                    });

                Device.StartTimer(TimeSpan.FromSeconds(5), CheckIfDeviceIsListening);
               


            }
            catch (Exception ex)
            {
                var test = ex.Message;
                Debug.WriteLine("Exeption indexview init");
            }

        }

        private bool CheckIfDeviceIsListening()
        {
            Listening = _locationDataService.IsLocatorListening();
            Debug.WriteLine(Listening);
            return true;
        }


        public Command StartPeriodic
        {
            get
            {
                return new Command(async () =>
                {
                    NumberOfUpdates = 0;
                    TimestampStarted = DateTime.Now.ToLocalTime().ToString("HH:mm:ss");
                    _userDialogs.ShowLoading("Starting periodic");
                    await Task.Delay(TimeSpan.FromSeconds(1));
                    await _locationDataService.StartPeriodicLocationUpdate();
                    _userDialogs.HideLoading();
                 });
            }
        }

        public Command StopPeriodic
        {
            get
            {
                return new Command(async () =>
                {
                   
                    _userDialogs.ShowLoading("Stopping periodic");
                    await Task.Delay(TimeSpan.FromSeconds(1));
                    await _locationDataService.StopPeriodicLocationUpdate();
                    _userDialogs.HideLoading();
                    MapCenter = new Xamarin.Forms.Maps.Position(60.3382023852435, 6.3437454901496);
                });
            }
        }
    }
}

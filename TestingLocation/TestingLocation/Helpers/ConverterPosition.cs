using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;

namespace TestingLocation.Helpers
{
    class ConverterPosition
    {
        public static Position FromGeoPosToXamPos(Plugin.Geolocator.Abstractions.Position pos)
        {
            if (pos != null)
                return new Position(pos.Latitude, pos.Longitude);
            else
                return new Position(0.0,0.0);




        }
    }
}

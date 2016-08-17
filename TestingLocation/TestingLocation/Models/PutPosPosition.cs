using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TestingLocation.Models
{
    public class PutPosPosition
    {
        [JsonProperty(PropertyName = "center")]
        public Center Center
        {
            get;
            set;
        }
        [JsonProperty(PropertyName = "radius")]
        public double Radius
        {
            get;
            set;
        }
    }
}

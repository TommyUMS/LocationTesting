using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TestingLocation.Models
{
    public class Center
    {
        [JsonProperty(PropertyName = "x")]
        public double X
        {
            get;
            set;
        }
        [JsonProperty(PropertyName = "y")]
        public double Y
        {
            get;
            set;
        }
    }
}

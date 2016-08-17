using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TestingLocation.Models
{
    public class PositionDTO
    {
        [JsonProperty(PropertyName = "position")]
        public PutPosPosition Position
        {
            get;
            set;
        }

    }

}

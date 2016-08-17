using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Plugin.Geolocator.Abstractions;
using TestingLocation.Helpers;
using TestingLocation.Interfaces;
using TestingLocation.Models;
using TestingLocation.Resources;

namespace TestingLocation.Services
{
    public class MissionServerService : IMissionServerService
    {
        HttpClient client;


        public MissionServerService()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
        }
        public async Task<HttpResponseMessage> SendLocation(Position position, string msisdn)
        {
            var requestUrl = AppSettingsResources.RestUrl + "pos/" + msisdn;
            var positionDTO = JsonConvert.SerializeObject(new PositionDTO
            {
                Position = new PutPosPosition
                {
                    Center = new Center { X = position.Longitude, Y = position.Latitude },
                    Radius = position.Accuracy
                }
            });
            try
            {
                HttpContent content = new StringContent(positionDTO, Encoding.UTF8, "application/json");
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = WebHelper.CreateBasicHeader(
                        AppSettingsResources.RestUsername, AppSettingsResources.RestPassword);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    return await client.PutAsync(requestUrl, content);
                }
            }
            catch
            {
                //Insights.Track("Error posting position", "Error", except.Message);
                return new HttpResponseMessage(HttpStatusCode.NotAcceptable);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using CarTracker.GooglePlaceRequester.Models;
using Newtonsoft.Json;

namespace CarTracker.GooglePlaceRequester
{
    public class GooglePlaceRequester
    {
        protected HttpClient Client { get; set; }
        
        protected string ApiKey { get; set; }

        public GooglePlaceRequester()
        {
            Client = new HttpClient();
        }

        public IEnumerable<PlaceSearchModel> GetPlacesNearby(double latitude, double longitude, int range)
        {
            string url = string.Format("https://maps.googleapis.com/maps/api/place/nearbysearch/json?location={0},{1}&radius={2}&key={3}",
                latitude, longitude, range, ApiKey);
            var results = GetRequest<PlaceSearchResult>(url);
            return results.Results;
        }

        protected T ParseJsonFromResponse<T>(HttpResponseMessage response)
        {
            var jsonResponse = response.Content.ReadAsStringAsync().Result;
            return (T) JsonConvert.DeserializeObject(jsonResponse, typeof(T));
        }

        protected T GetRequest<T>(string url)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
            {
                using (var response = Client.SendAsync(request).Result)
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return ParseJsonFromResponse<T>(response);
                    }
                    if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        return default(T);
                    }
                    throw new HttpRequestException("Failed to make request. " + response.ReasonPhrase);
                }
            }
        }

    }
}

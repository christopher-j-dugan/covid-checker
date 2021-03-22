namespace CovidMonitor
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    
    public class CVS : IStore
    {
        private readonly string _zipCode = "19401";
        public string Name => "CVS";

        public List<string> CheckAllLocations()
        {
            var storesWithAppts = new List<string>();

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "*/*");
            var url = "https://www.cvs.com/Services/ICEAGPV1/immunization/1.0.0/getIMZStores";
            var body =
                "{\"requestMetaData\":{\"appName\":\"CVS_WEB\",\"lineOfBusiness\":\"RETAIL\",\"channelName\":\"WEB\",\"deviceType\":\"DESKTOP\",\"deviceToken\":\"7777\",\"apiKey\":\"a2ff75c6-2da7-4299-929d-d670d827ab4a\",\"source\":\"ICE_WEB\",\"securityType\":\"apiKey\",\"responseFormat\":\"JSON\",\"type\":\"cn-dep\"},\"requestPayloadData\":{\"selectedImmunization\":[\"CVD\"],\"distanceInMiles\":35,\"imzData\":[{\"imzType\":\"CVD\",\"ndc\":[\"59267100002\",\"59267100003\",\"59676058015\",\"80777027399\"],\"allocationType\":\"1\"}],\"searchCriteria\":{\"addressLine\":\"" +
                _zipCode + "\"}}}";


            var rawResponse = client.PostAsync(url, new StringContent(body)).GetAwaiter().GetResult().Content
                .ReadAsStringAsync().GetAwaiter()
                .GetResult();
            var response = JsonSerializer.Deserialize<Response>(rawResponse);

            var hasAppts = response?.ResponsePayloadData != null;
            if (hasAppts)
                storesWithAppts.AddRange(
                    response?.ResponsePayloadData?.Locations.Select(location => location.AddressCityDescriptionText));

            return storesWithAppts;
        }

        private class Response
        {
            [JsonPropertyName("responsePayloadData")]
            public ResponsePayloadData ResponsePayloadData { get; set; }
        }

        private class ResponsePayloadData
        {
            [JsonPropertyName("locations")] public List<Location> Locations { get; set; }
        }

        private class Location
        {
            [JsonPropertyName("addressCityDescriptionText")]
            public string AddressCityDescriptionText { get; set; }
        }
    }
}
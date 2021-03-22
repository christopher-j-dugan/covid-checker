namespace CovidMonitor
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    public class RiteAid : IStore
    {
        /// <summary>
        ///     These are the store numbers that can be found on the RiteAid website with useful descriptions.
        ///     Likely edit these per your location.
        /// </summary>
        private readonly Dictionary<int, string> _locations = new()
        {
            {2017, "Norristown - Johnson Highway"},
            {1733, "Norristown - Jeffersonville"},
            {11160, "Conshy"},
            {11158, "KOP"},
            {995, "Wayne - E Lancaster"},
            {11119, "Wayne - W Lancaster"},
            {11101, "Bryn Mawr"},
            {1144, "Chesterbrook"}
            //{2364, "Defiance"}
        };

        public string Name => "RiteAid";

        public List<string> CheckAllLocations()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "*/*");

            var storesWithAppts = new List<string>();

            foreach (var storeId in _locations.Keys)
            {
                var url = $"https://www.riteaid.com/services/ext/v2/vaccine/checkSlots?storeNumber={storeId}";
                var rawResponse = client.GetAsync(url).GetAwaiter().GetResult().Content.ReadAsStringAsync().GetAwaiter()
                    .GetResult();
                var response = JsonSerializer.Deserialize<Response>(rawResponse);
                //var response = client.GetFromJsonAsync<Response>(url).GetAwaiter().GetResult();
                var hasAppts = response?.Data?.Slots != null &&
                               (response.Data.Slots.One || response.Data.Slots.Two);
                if (hasAppts) storesWithAppts.Add(_locations[storeId]);
            }

            return storesWithAppts;
        }

        private class Response
        {
            [JsonPropertyName("Data")] public Data Data { get; set; }
        }

        private class Data
        {
            [JsonPropertyName("slots")] public Slots Slots { get; set; }
        }

        private class Slots
        {
            [JsonPropertyName("1")] public bool One { get; set; }

            [JsonPropertyName("2")] public bool Two { get; set; }
        }
    }
}
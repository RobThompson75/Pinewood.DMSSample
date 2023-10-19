using System.Net.Http;

namespace Pinewood.DMSSample.Business
{
    public class PartAvailabilityClient : Interfaces.IPartAvailabilityClient, IDisposable
    {
        private HttpClient _httpClient;

        // This would benefit from injecting a IHTTPClient Factory as this is the recommended implementation of this
        public PartAvailabilityClient()
        {
            _httpClient = new HttpClient();
        }

        public void Dispose()
        {
            _httpClient.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<int> GetAvailability(string stockCode)
        {
            HttpResponseMessage responseMessage = await _httpClient.GetAsync($"https://www.api.pinewood.com/parts/availability/{stockCode}");

            if (responseMessage.IsSuccessStatusCode)
            {
                string responseString = await responseMessage.Content.ReadAsStringAsync();
                return int.Parse(responseString);
            }
            else
            {
                throw new Exception($"Could not get part availability for {stockCode}");
            }
        }
    }
}

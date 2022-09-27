using Newtonsoft.Json;
using VisitorManagement2022.DTO;

namespace VisitorManagement2022.Service
{
    public class API : IAPI
    {
        public async Task<Root> WeatherAPI()
        {
            HttpClient client = new HttpClient();
            string responseBody = null;
            string apiKey = "9b0cde9275755c3f4f43e05422f318e0";
            string url = "https://api.openweathermap.org/data/2.5/weather?q=Christchurch&units=metric&appid=" + apiKey;

            responseBody = await client.GetStringAsync(url);
            Root root = JsonConvert.DeserializeObject<Root>(responseBody);

            return root;
        }
    }
}

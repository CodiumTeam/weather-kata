using System.Text.Json.Nodes;

namespace Weather;

public class Forecast
{
    public string predict(string city, DateTime? dateTime, bool wind)
    {
        // When date is not provided we look for the current prediction
        if (!dateTime.HasValue)
        {
            dateTime = DateTime.Now;
        }

        var format = dateTime.Value.ToString("yyyy-MM-dd");

        // If there are predictions
        if (dateTime.Value.CompareTo(DateTime.Today.AddDays(6)) < 0)
        {
            // Find the latitude and longitude to get the prediction
            var url = "https://positionstack.com/geo_api.php?query=" + city;
            JsonNode response;
            using (var httpClient = new HttpClient())
            {
                var json = httpClient.GetStringAsync(url).Result;

                response = JsonNode.Parse(json)!;
            }

            var latitude = response!["data"]![0]!["latitude"]!.ToString();
            var longitude = response!["data"]![0]!["longitude"]!.ToString();

            // Find the predictions for the location
            url = "https://api.open-meteo.com/v1/forecast?latitude=" + latitude + "&longitude=" + longitude +
                  "&daily=weathercode,windspeed_10m_max&current_weather=true&timezone=Europe%2FBerlin";
            using (var httpClient = new HttpClient())
            {
                var json = httpClient.GetStringAsync(url).Result;

                response = JsonNode.Parse(json)!;
            }

            for (var i = 0; i < 7; i++)
            {
                // When the date is the expected
                if (format.Equals(response!["daily"]!["time"]![i]!.ToString()))
                {
                    // If we have to return the wind information
                    if (wind)
                    {
                        return response!["daily"]!["windspeed_10m_max"]![i]!.ToString();
                    }
                    else
                    {
                        var code = response!["daily"]!["weathercode"]![i]!.GetValue<int>();
                        return CodeToText(code);
                    }
                }
            }
        }
        else
        {
            return "";
        }

        return "";
    }

    private string CodeToText(int weatherCode)
    {
        var codes = new Dictionary<int, string>()
        {
            { 0, "Clear sky" },
            { 1, "Mainly clear, partly cloudy, and overcast" },
            { 2, "Mainly clear, partly cloudy, and overcast" },
            { 3, "Mainly clear, partly cloudy, and overcast" },
            { 45, "Fog and depositing rime fog" },
            { 48, "Fog and depositing rime fog" },
            { 51, "Drizzle: Light, moderate, and dense intensity" },
            { 53, "Drizzle: Light, moderate, and dense intensity" },
            { 55, "Drizzle: Light, moderate, and dense intensity" },
            { 56, "Freezing Drizzle: Light and dense intensity" },
            { 57, "Freezing Drizzle: Light and dense intensity" },
            { 61, "Rain: Slight, moderate and heavy intensity" },
            { 63, "Rain: Slight, moderate and heavy intensity" },
            { 65, "Rain: Slight, moderate and heavy intensity" },
            { 66, "Freezing Rain: Light and heavy intensity" },
            { 67, "Freezing Rain: Light and heavy intensity" },
            { 71, "Snow fall: Slight, moderate, and heavy intensity" },
            { 73, "Snow fall: Slight, moderate, and heavy intensity" },
            { 75, "Snow fall: Slight, moderate, and heavy intensity" },
            { 77, "Snow grains" },
            { 80, "Rain showers: Slight, moderate, and violent" },
            { 81, "Rain showers: Slight, moderate, and violent" },
            { 82, "Rain showers: Slight, moderate, and violent" },
            { 85, "Snow showers slight and heavy" },
            { 86, "Snow showers slight and heavy" },
            { 95, "Thunderstorm: Slight or moderate" },
            { 96, "Thunderstorm with slight and heavy hail" },
            { 99, "Thunderstorm with slight and heavy hail" },
        };
        return codes[weatherCode];
    }
}
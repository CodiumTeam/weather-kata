using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;

namespace Team.Codium.LegacyTraining.WeatherKata
{
    public class Forecast
    {
        public string predict(string city, DateTime? dateTime, bool wind)
        {
            // When date is not provided we look for the current prediction
            if (!dateTime.HasValue)
            {
                dateTime = DateTime.Now;
            }
            string format = dateTime.Value.ToString("yyyy-MM-dd");

            // If there are predictions
            if (dateTime.Value.CompareTo(DateTime.Today.AddDays(6)) < 0)
            {
                // Find the id of the city on metawheather
                var url = "https://www.metaweather.com/api/location/search/?query=" + city;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                string woeid;
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    var html = reader.ReadToEnd();

                    var xxx = JsonConvert.DeserializeObject<dynamic>(html);
                    woeid = xxx[0].woeid;
                }

                // Find the predictions for the city
                dynamic results;
                request = (HttpWebRequest)WebRequest.Create("https://www.metaweather.com/api/location/" + woeid);
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    var html = reader.ReadToEnd();

                    results = JsonConvert.DeserializeObject<dynamic>(html).consolidated_weather;
                }

                foreach (dynamic result in results)
                {
                    // When the date is the expected
                    if (format.Equals((string)result.applicable_date))
                    {
                        // If we have to return the wind information
                        if (wind)
                        {
                            return result.wind_speed;
                        }
                        else
                        {
                            return result.weather_state_name;
                        }
                    }
                }
            } else
            {
                return "";
            }
            return "";
        }
    }
}

using System;

namespace Team.Codium.LegacyTraining.WeatherKata
{
    interface ForecastService
    {
        string PredictWeather(string cityName, DateTime when);

        string predictWind(string cityName, DateTime when);
    }
}

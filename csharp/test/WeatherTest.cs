namespace WeatherKata;

using Weather;

public class WeatherTest
{
    // https://positionstack.com/geo_api.php?query=Barcelona
    // https://api.open-meteo.com/v1/forecast?latitude=52.52&longitude=13.41&daily=weathercode,windspeed_10m_max&timezone=Europe%2FBerlin
    
    [Fact]
    public void find_the_weather_of_today()
    {
        var forecast = new Forecast();


        Assert.Equal("xxx", "xxx");
    }    
}
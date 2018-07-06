using System;
using Xunit;
using Xunit.Abstractions;

namespace Team.Codium.LegacyTraining.WeatherKata.Test
{
    public class WeatherKataTest
    {
        private readonly ITestOutputHelper output;

        public WeatherKataTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void Find_The_Weather_Of_Today()
        {
            Forecast forecast = new Forecast();

            String prediction = forecast.predict("Madrid", null, false);

            output.WriteLine("Today: " + prediction);
        }

        [Fact]
        public void Find_The_Weather_Of_Any_Day()
        {
            Forecast forecast = new Forecast();

            String prediction = forecast.predict("Madrid", DateTime.Today.AddDays(1), false);

            output.WriteLine("Tomorrow: " + prediction);
        }


        [Fact]
        public void Find_The_Wind_Of_Any_day()
        {
            Forecast forecast = new Forecast();

            String prediction = forecast.predict("Madrid", null, true);

            output.WriteLine("Wind: " + prediction);
        }

        [Fact]
        public void There_Is_No_Prediction_For_More_Than_5_Days()
        {
            Forecast forecast = new Forecast();

            String prediction = forecast.predict("Madrid", DateTime.Now.AddDays(6), true);

            Assert.Equal("", prediction);
        }
    }
}

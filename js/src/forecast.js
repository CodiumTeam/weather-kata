import request from 'request';
import { promisify } from 'util';
const rp = promisify(request);

class Forecast {
    async predict(city, datetime, wind) {
        // When date is not provided we look for the current prediction
        if (!datetime) {
            datetime = new Date();
        }

        // If there is a prediction for datetime
        let datetime2 = new Date();
        datetime2.setDate(datetime2.getDate() + 6);
        if (datetime < datetime2) {
            // Find the latitude and longitude to get the prediction
            const response = JSON.parse((await rp("https://positionstack.com/geo_api.php?query="+ city)).body);
            const latitude = response['data'][0]['latitude'];
            const longitude = response['data'][0]['longitude'];

            // Find the predictions for the location
            const url = `https://api.open-meteo.com/v1/forecast?latitude=${latitude}&longitude=${longitude}&daily=weathercode,windspeed_10m_max&current_weather=true&timezone=Europe%2FBerlin`;
            const results = JSON.parse((await rp(url)).body);
            for (let i = 0; i < 7; i++) {
                // When the date is the expected
                if (results["daily"]['time'][i] === datetime.toISOString().slice(0, 10)) {
                    if (wind) {
                        return results['daily']['windspeed_10m_max'][i];
                    } else {
                        const weatherCode = results['daily']['weathercode'][i];

                        return this.codeToText(weatherCode);
                    }
                }
            }
        } else {
            return '';
        }
    }

    codeToText (weatherCode) {
        const text = {
            0: 'Clear sky',
            1: 'Mainly clear, partly cloudy, and overcast',
            2: 'Mainly clear, partly cloudy, and overcast',
            3: 'Mainly clear, partly cloudy, and overcast',
            45: 'Fog and depositing rime fog',
            48: 'Fog and depositing rime fog',
            51: 'Drizzle: Light, moderate, and dense intensity',
            53: 'Drizzle: Light, moderate, and dense intensity',
            55: 'Drizzle: Light, moderate, and dense intensity',
            56: 'Freezing Drizzle: Light and dense intensity',
            57: 'Freezing Drizzle: Light and dense intensity',
            61: 'Rain: Slight, moderate and heavy intensity',
            63: 'Rain: Slight, moderate and heavy intensity',
            65: 'Rain: Slight, moderate and heavy intensity',
            66: 'Freezing Rain: Light and heavy intensity',
            67: 'Freezing Rain: Light and heavy intensity',
            71: 'Snow fall: Slight, moderate, and heavy intensity',
            73: 'Snow fall: Slight, moderate, and heavy intensity',
            75: 'Snow fall: Slight, moderate, and heavy intensity',
            77: 'Snow grains',
            80: 'Rain showers: Slight, moderate, and violent',
            81: 'Rain showers: Slight, moderate, and violent',
            82: 'Rain showers: Slight, moderate, and violent',
            85: 'Snow showers slight and heavy',
            86: 'Snow showers slight and heavy',
            95: 'Thunderstorm: Slight or moderate',
            96: 'Thunderstorm with slight and heavy hail',
            99: 'Thunderstorm with slight and heavy hail',
        }[weatherCode];

        return text;
    }
}

export default Forecast;

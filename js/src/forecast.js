const request = require('request');

class Forecast {

    predict(city, datetime, wind) {

        return new Promise((resolve, reject) => {

            // When date is not provided we look for the current prediction
            if (!datetime) {
                datetime = new Date();
            }

            // If there is a prediction for datetime
            let datetime2 = new Date();
            datetime2.setDate(datetime2.getDate() + 6);
            if (datetime < datetime2) {

                // Find the id of the city on metawheather
                request("https://www.metaweather.com/api/location/search/?query=" + city, {json: true}, (err, res, body) => {
                    const woeid = body[0].woeid;

                    // Find the predictions for the city
                    request("https://www.metaweather.com/api/location/" + woeid, {json: true}, (err, res, body) => {
                        const results = body.consolidated_weather;
                        results.forEach(function (result) {

                            // When the date is the expected
                            if (result.applicable_date == datetime.toISOString().slice(0, 10)) {

                                // If we have to return the wind information
                                if (wind) {
                                    return resolve(result.wind_speed);
                                } else {
                                    return resolve(result.weather_state_name);
                                }
                            }
                        });
                    });
                });
            } else {
                resolve("");
            }
        });
    }
}

export default Forecast;

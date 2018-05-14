<?php

namespace Codium\CleanCode;

use GuzzleHttp\Client;

class Forecast
{
    public function predict(string &$city, \DateTime $datetime = null, bool $wind = false): string
    {
        // When date is not provided we look for the current prediction
        if (!$datetime) {
            $datetime = new \DateTime();
        }

        // If there are predictions
        if ($datetime < new \DateTime("+6 days 00:00:00")) {


            // Create a Guzzle Http Client
            $client = new Client();

            // Find the id of the city on metawheather
            $woeid = json_decode($client->get("https://www.metaweather.com/api/location/search/?query=$city")->getBody()->getContents(),
                true)[0]['woeid'];
            $city = $woeid;

            // Find the predictions for the city
            $results = json_decode($client->get("https://www.metaweather.com/api/location/$woeid")->getBody()->getContents(),
                true)['consolidated_weather'];
            foreach ($results as $result) {

                // When the date is the expected
                if ($result["applicable_date"] == $datetime->format('Y-m-d')) {
                    // If we have to return the wind information
                    if ($wind) {
                        return $result['wind_speed'];
                    } else {
                        return $result['weather_state_name'];
                    }
                }
            }
        } else {
            return "";
        }
    }
}
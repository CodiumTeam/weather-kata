<?php

namespace Codium\CleanCode;

use DateTime;
use GuzzleHttp\Client;

class Forecast
{
  public function predict(string $city, DateTime $datetime = null, bool $wind = false): string|float
  {
    // When date is not provided we look for the current prediction
    if (!$datetime) {
      $datetime = new DateTime();
    }

    // If there are predictions
    if ($datetime < new DateTime("+7 days 00:00:00")) {
      // Create a Guzzle Http Client
      $client = new Client();

      // Find the latitude and longitude to get the prediction
      $url = "https://positionstack.com/geo_api.php?query=$city";
      $response = json_decode($client->get($url)->getBody()->getContents(), true);
      $latitude = $response['data'][0]['latitude'];
      $longitude = $response['data'][0]['longitude'];

      // Find the predictions for the location
      $url = "https://api.open-meteo.com/v1/forecast?latitude=$latitude&longitude=$longitude&daily=weathercode,windspeed_10m_max&current_weather=true&timezone=Europe%2FBerlin";
      $response = json_decode($client->get($url)->getBody()->getContents(), true);

      for ($i = 0; $i < 7; $i++) {
        if ($response["daily"]['time'][$i] == $datetime->format('Y-m-d')) {
          if ($wind) {
            return $response['daily']['windspeed_10m_max'][$i];
          } else {
            $weatherCode = $response['daily']['weathercode'][$i];

            return $this->codeToText($weatherCode);
          }
        }
      }
    }

    return "";
  }

  private function codeToText(mixed $weatherCode)
  {
    return [
      0 => 'Clear sky',
      1 => 'Mainly clear, partly cloudy, and overcast',
      2 => 'Mainly clear, partly cloudy, and overcast',
      3 => 'Mainly clear, partly cloudy, and overcast',
      45 => 'Fog and depositing rime fog',
      48 => 'Fog and depositing rime fog',
      51 => 'Drizzle: Light, moderate, and dense intensity',
      53 => 'Drizzle: Light, moderate, and dense intensity',
      55 => 'Drizzle: Light, moderate, and dense intensity',
      56 => 'Freezing Drizzle: Light and dense intensity',
      57 => 'Freezing Drizzle: Light and dense intensity',
      61 => 'Rain: Slight, moderate and heavy intensity',
      63 => 'Rain: Slight, moderate and heavy intensity',
      65 => 'Rain: Slight, moderate and heavy intensity',
      66 => 'Freezing Rain: Light and heavy intensity',
      67 => 'Freezing Rain: Light and heavy intensity',
      71 => 'Snow fall: Slight, moderate, and heavy intensity',
      73 => 'Snow fall: Slight, moderate, and heavy intensity',
      75 => 'Snow fall: Slight, moderate, and heavy intensity',
      77 => 'Snow grains',
      80 => 'Rain showers: Slight, moderate, and violent',
      81 => 'Rain showers: Slight, moderate, and violent',
      82 => 'Rain showers: Slight, moderate, and violent',
      85 => 'Snow showers slight and heavy',
      86 => 'Snow showers slight and heavy',
      95 => 'Thunderstorm: Slight or moderate',
      96 => 'Thunderstorm with slight and heavy hail',
      99 => 'Thunderstorm with slight and heavy hail',
    ][$weatherCode];
  }
}

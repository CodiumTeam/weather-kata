<?php

namespace Tests\Codium\CleanCode;

use Codium\CleanCode\Forecast;
use PHPUnit\Framework\TestCase;

class WeatherTest extends TestCase
{
  // https://positionstack.com/geo_api.php?query=Barcelona
  // https://api.open-meteo.com/v1/forecast?latitude=52.52&longitude=13.41&daily=weathercode,windspeed_10m_max&timezone=Europe%2FBerlin

      /** @test */
    public function find_the_weather_of_today()
    {
        $forecast = new Forecast();
        $this->assertEquals("xxx", 'xxx');
    }
}
# Weather kata
We cannot control the weather but we can predict it.

This kata has a code that request the weather prediction from Metaweather.

## Goal
- Test coupled code.
- Remove the external dependency when testing in order to make the tests repeatable and fast

## Run the kata
On Linux and Mac

    make

in Windows using docker

    docker run --rm -v ${PWD}:/code codiumteam/legacy-training-php:weather make tests

## Authors
Luis Rovirosa [@luisrovirosa](https://www.twitter.com/luisrovirosa)

Jordi Anguela [@jordianguela](https://www.twitter.com/jordianguela)

[Original repository](https://github.com/CodiumTeam/weather-kata)

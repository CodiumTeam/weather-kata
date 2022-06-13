# Weather kata
We cannot control the weather but we can predict it.

This kata has a code that request the weather prediction from Metaweather.

## Goal
- Test coupled code.
- Remove the external dependency when testing in order to make the tests repeatable and fast

## How to run and see the result
## Docker

### on Linux and Mac
Run the tests

    make

Run the code coverage

    make docker-coverage

### on Windows
Open the Makefile and copy and paste the lines below each command.

## Locally

### on Linux and Mac
Run the tests

    make tests

Run the code coverage

    make coverage

### on Windows
Run the tests

    python -m unittest tests/weather_test.py

Run the code coverage

    pytest --cov=weather tests
	coverage html


## Authors
Luis Rovirosa [@luisrovirosa](https://www.twitter.com/luisrovirosa)

Jordi Anguela [@jordianguela](https://www.twitter.com/jordianguela)
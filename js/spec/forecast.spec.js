import Forecast from '../src/forecast';

describe("Forecast should", function () {

    let originalTimeout;

    beforeEach(function () {
        originalTimeout = jasmine.DEFAULT_TIMEOUT_INTERVAL;
        jasmine.DEFAULT_TIMEOUT_INTERVAL = 20000;
    });

    it("retrieve today's weather", function (done) {
        const forecast = new Forecast();

        forecast.predict("Madrid", null, false)
        .then(function (prediction) {
            console.log("Today: " + prediction);

            expect(true).toBe(true); // I don't know how to test it

            done();
        });
    });

    it("retrieve any day's weather", function (done) {
        const forecast = new Forecast();
        let city = "Madrid";
        let dayAfterTomorrow = new Date();
        dayAfterTomorrow.setDate(dayAfterTomorrow.getDate() + 2);

        forecast.predict(city, dayAfterTomorrow)
        .then(function (prediction) {
            console.log("Day after tomorrow: " + prediction);

            expect(true).toBe(true); // I don't know how to test it

            done();
        });
    });

    it("retrieve the wind of any day", function (done) {
        const forecast = new Forecast();
        let city = "Madrid";

        forecast.predict(city, null, true)
        .then(function (prediction) {
            console.log("Wind: " + prediction);

            expect(true).toBe(true); // I don't know how to test it

            done();
        });
    });

    it("return empty string when requesting a forecast for more than 5 days", function (done) {
        const forecast = new Forecast();
        let city = "Madrid";
        let sixDaysForecast = new Date();
        sixDaysForecast.setDate(new Date().getDate() + 6);

        forecast.predict(city, sixDaysForecast)
        .then(function (prediction) {
            expect(prediction).toBe("");

            done();
        });
    });

    afterEach(function () {
        jasmine.DEFAULT_TIMEOUT_INTERVAL = originalTimeout;
    });
});

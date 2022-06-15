package team.codium.legacytraining.weather;

import com.google.api.client.http.GenericUrl;
import com.google.api.client.http.HttpRequest;
import com.google.api.client.http.HttpRequestFactory;
import com.google.api.client.http.javanet.NetHttpTransport;
import org.json.JSONArray;
import org.json.JSONObject;

import java.io.IOException;
import java.time.LocalDate;
import java.time.format.DateTimeFormatter;
import java.util.HashMap;
import java.util.Map;

public class Forecast {
    public String predict(String city, LocalDate datetime, boolean wind) throws IOException {
        // When date is not provided we look for the current prediction
        if (datetime == null) {
            datetime = LocalDate.now();
        }
        String format = datetime.format(DateTimeFormatter.ofPattern("yyyy-MM-dd"));

        // If there are predictions
        if (datetime.isBefore(LocalDate.now().plusDays(7))) {

            // Find the latitude and longitude to get the prediction
            HttpRequestFactory requestFactory
                    = new NetHttpTransport().createRequestFactory();
            HttpRequest request = requestFactory.buildGetRequest(
                    new GenericUrl("https://positionstack.com/geo_api.php?query=" + city));
            String rawResponse = request.execute().parseAsString();
            JSONObject jsonObject = new JSONObject(rawResponse);
            float latitude = jsonObject.getJSONArray("data").getJSONObject(0).getFloat("latitude");
            float longitude = jsonObject.getJSONArray("data").getJSONObject(0).getFloat("longitude");

            // Find the predictions for the location
            requestFactory = new NetHttpTransport().createRequestFactory();
            request = requestFactory.buildGetRequest(
                    new GenericUrl("https://api.open-meteo.com/v1/forecast?latitude=" + latitude + "&longitude=" + longitude + "&daily=weathercode,windspeed_10m_max&current_weather=true&timezone=Europe%2FBerlin"));
            rawResponse = request.execute().parseAsString();
            JSONObject results = new JSONObject(rawResponse).getJSONObject("daily");

            JSONArray times = results.getJSONArray("time");
            for (int i = 0; i < times.length(); i++) {
//            // When the date is the expected
                if (format.equals(times.get(i))) {
//                // If we have to return the wind information
                    if (wind) {
                        return String.valueOf(results.getJSONArray("windspeed_10m_max").getFloat(i));
                    } else {
                        int weatherCode = results.getJSONArray("weathercode").getInt(i);
                        return codeToText(weatherCode);
                    }
                }
            }
        } else {
            return "";
        }
        return "";
    }

    private String codeToText(int weatherCode) {
        Map<Integer, String> translation = new HashMap<Integer, String>() {
            {
                put(0, "lear sky");
                put(1, "Mainly clear, partly cloudy, and overcast");
                put(2, "Mainly clear, partly cloudy, and overcast");
                put(3, "Mainly clear, partly cloudy, and overcast");
                put(45, "Fog and depositing rime fog");
                put(48, "Fog and depositing rime fog");
                put(51, "Drizzle: Light, moderate, and dense intensity");
                put(53, "Drizzle: Light, moderate, and dense intensity");
                put(55, "Drizzle: Light, moderate, and dense intensity");
                put(56, "Freezing Drizzle: Light and dense intensity");
                put(57, "Freezing Drizzle: Light and dense intensity");
                put(61, "Rain: Slight, moderate and heavy intensity");
                put(63, "Rain: Slight, moderate and heavy intensity");
                put(65, "Rain: Slight, moderate and heavy intensity");
                put(66, "Freezing Rain: Light and heavy intensity");
                put(67, "Freezing Rain: Light and heavy intensity");
                put(71, "Snow fall: Slight, moderate, and heavy intensity");
                put(73, "Snow fall: Slight, moderate, and heavy intensity");
                put(75, "Snow fall: Slight, moderate, and heavy intensity");
                put(77, "Snow grains");
                put(80, "Rain showers: Slight, moderate, and violent");
                put(81, "Rain showers: Slight, moderate, and violent");
                put(82, "Rain showers: Slight, moderate, and violent");
                put(85, "Snow showers slight and heavy");
                put(86, "Snow showers slight and heavy");
                put(95, "Thunderstorm: Slight or moderate");
                put(96, "Thunderstorm with slight and heavy hail");
                put(99, "Thunderstorm with slight and heavy hail");
            }
        };
        return translation.get(weatherCode);
    }
}

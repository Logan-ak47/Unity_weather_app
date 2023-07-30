using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class testWeatherAPI
{
    public string baseUrl = "https://api.open-meteo.com/v1/forecast?";
  
    [UnityTest]
    public IEnumerator testWeatherAPIWithEnumeratorPasses()
    {
        float testLong = 15.0f;
        float testLat = 16.0f;
        var gameObject = new GameObject();
        var weatherObj = gameObject.AddComponent<GetWeather>();

        weatherObj.CreateUrl(testLat.ToString(), testLong.ToString());
        yield return new WaitForSeconds(5f);

        Assert.AreEqual(weatherObj.baseUrl + "latitude=" + testLat + "&longitude=" + testLong + "&current_weather=true" + "&timezone=IST", baseUrl + "latitude=" + testLat + "&longitude=" + testLong + "&current_weather=true" + "&timezone=IST");
    }
}

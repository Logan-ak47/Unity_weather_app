using UnityEngine.Networking;
using UnityEngine;
using System.Collections;
using System;
using SimpleJSON;

public class GetWeather : MonoBehaviour
{

    [SerializeField]
    public  string baseUrl = "https://api.open-meteo.com/v1/forecast?";
   
    public  void CreateUrl(string lat, string lon)
    {
        string finalUrl= baseUrl+ "latitude=" + lat+ "&longitude=" +lon+ "&current_weather=true"+"&timezone=IST";
       
        GetWeatherFromUrl(finalUrl);
    }


    public void GetWeatherFromUrl(string url)
    {
        StartCoroutine(GetRequest(url));
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            Debug.Log("Url is " + uri);
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    ConvertDataToJson(webRequest.downloadHandler.text);

                    break;
            }
        }
    }


    public void ConvertDataToJson(string downloadedData)
    {
        JSONNode dataRecieved = JSON.Parse(downloadedData);
        Debug.Log(dataRecieved.ToString());
        ButtonClick.weatherInformation =  dataRecieved["current_weather"]["temperature"].ToString();
        ToastBar.ShowMessage("Current Temprature is "+dataRecieved["current_weather"]["temperature"].ToString(), ToastBar.Position.bottom, ToastBar.Time.twoSecond);
    }

}

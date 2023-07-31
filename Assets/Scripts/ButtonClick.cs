using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour
{
    [SerializeField]
    private Text playerLocationText;
    
    public static string weatherInformation;
    public GetWeather getWeather;


    private void Awake()
    {
        getWeather = gameObject.GetComponent<GetWeather>();
    }
    public void OnButtonClick()
    {

        var latitude = Input.location.lastData.latitude;
        var longitude = Input.location.lastData.longitude;
        Debug.Log("lat and long is "+latitude+","+longitude);
        playerLocationText.text = "Your Location is " + latitude.ToString() + " lat & " + longitude.ToString() + " long";
        getWeather.CreateUrl(latitude.ToString(), longitude.ToString());
    }


}

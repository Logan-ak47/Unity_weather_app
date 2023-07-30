
using System.Collections;
using System.Text;
using UnityEngine;

using UnityEngine.UI;

public class LocationService : MonoBehaviour
{

    public Text playerLocationText;
    public GetWeather getWeather;

    private void Update()
    {
        Debug.Log(UnityEngine.Input.location.status);
    }

    private void Start()
    {


        StringBuilder locationString = new StringBuilder();
        locationString.Append("A");
        locationString.Append("B");

        playerLocationText.text = locationString.ToString();
        /*#if UNITY_ANDROID
                if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
                {
                    Permission.RequestUserPermission(Permission.FineLocation);
                }
        #elif UNITY_IOS
                          PlayerSettings.iOS.locationUsageDescription = "Details to use location";
        #endif
                StartCoroutine(StartLocationService());*/

        StartCoroutine(LocationCoroutine());
    }
    private IEnumerator StartLocationService()
    {

        StringBuilder cRoutineStarting = new StringBuilder();
        if (!Input.location.isEnabledByUser)
        {
            playerLocationText.text = "User has not enabled location";
            Debug.Log("User has not enabled location");
            yield break;
        }
        Input.location.Start();
        while (Input.location.status == LocationServiceStatus.Initializing)
        {
            playerLocationText.text = "Initalizing";
            yield return new WaitForSeconds(1);
        }
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            playerLocationText.text = "Unable to determine device location";
            Debug.Log("Unable to determine device location");
            yield break;
        }
        Debug.Log("Latitude : " + Input.location.lastData.latitude);
        Debug.Log("Longitude : " + Input.location.lastData.longitude);
        Debug.Log("Altitude : " + Input.location.lastData.altitude);

        StringBuilder locationString = new StringBuilder();
        locationString.Append(Input.location.lastData.latitude.ToString());
        locationString.Append(Input.location.lastData.longitude.ToString());

        playerLocationText.text = locationString.ToString();
    }

    IEnumerator LocationCoroutine()
    {
       
        // Uncomment if you want to test with Unity Remote
#if UNITY_EDITOR
        yield return new WaitWhile(() => !UnityEditor.EditorApplication.isRemoteConnected);
        yield return new WaitForSecondsRealtime(5f);
#endif
#if UNITY_EDITOR
        // No permission handling needed in Editor
#elif UNITY_ANDROID
        if (!UnityEngine.Android.Permission.HasUserAuthorizedPermission(UnityEngine.Android.Permission.CoarseLocation)) {
            UnityEngine.Android.Permission.RequestUserPermission(UnityEngine.Android.Permission.CoarseLocation);
        }

        // First, check if user has location service enabled
       /* if (!UnityEngine.Input.location.isEnabledByUser) {
            // TODO Failure
            Debug.LogFormat("Android and Location not enabled");
            yield break;
        }*/

#elif UNITY_IOS
        if (!UnityEngine.Input.location.isEnabledByUser) {
            // TODO Failure
            Debug.LogFormat("IOS and Location not enabled");
              playerLocationText.text = "No enabled location";
            yield break;
        }
#endif
        // Start service before querying location
        UnityEngine.Input.location.Start(500f, 500f);

        // Wait until service initializes
        int maxWait = 20;
        while (UnityEngine.Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSecondsRealtime(1);
            maxWait--;
        }

        // Editor has a bug which doesn't set the service status to Initializing. So extra wait in Editor.
#if UNITY_EDITOR
        int editorMaxWait = 5;
        while (UnityEngine.Input.location.status == LocationServiceStatus.Stopped && editorMaxWait > 0)
        {
            yield return new WaitForSecondsRealtime(1);
            editorMaxWait--;
        }
#endif

        // Service didn't initialize in 15 seconds
        if (maxWait < 1)
        {

            // TODO Failure
            Debug.LogFormat("Timed out");
            playerLocationText.text = "Trying again Timed Out ";
            yield break;
        }

        // Connection has failed
        if (UnityEngine.Input.location.status != LocationServiceStatus.Running)
        {
            // TODO Failure
            Debug.LogFormat("Unable to determine device location. Failed with status {0}", UnityEngine.Input.location.status);
            yield break;
        }
        else
        {
            Debug.LogFormat("Location service live. status {0}", UnityEngine.Input.location.status);
            // Access granted and location value could be retrieved
            Debug.LogFormat("Location: "
                + UnityEngine.Input.location.lastData.latitude + " "
                + UnityEngine.Input.location.lastData.longitude + " "
                + UnityEngine.Input.location.lastData.altitude + " "
                + UnityEngine.Input.location.lastData.horizontalAccuracy + " "
                + UnityEngine.Input.location.lastData.timestamp);

            var latitude = UnityEngine.Input.location.lastData.latitude;
            var longitude = UnityEngine.Input.location.lastData.longitude;
            Debug.Log("Location is________" + latitude.ToString() + "______" + longitude.ToString());

            playerLocationText.text = latitude.ToString() + "______" + longitude.ToString();
            getWeather.CreateUrl(latitude.ToString(), longitude.ToString());
           // locationInformationRecieved?.Invoke(latitude.ToString(),longitude.ToString());

            // TODO success do something with location
        }

        // Stop service if there is no need to query location updates continuously
       // UnityEngine.Input.location.Stop();
    }

}

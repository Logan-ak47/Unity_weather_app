using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClick : MonoBehaviour
{
    [SerializeField]
    public static string weatherInformation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnButtonClick()
    {
        Debug.Log("Button Clicked"+ weatherInformation);
        Debug.Log("UnityEngine.Input.location.lastData.latitude+ UnityEngine.Input.location.lastData.longitude" + UnityEngine.Input.location.lastData.latitude + UnityEngine.Input.location.lastData.longitude);
                    

        ToastBar.ShowMessage("Current Temprature is  "+weatherInformation, ToastBar.Position.bottom, ToastBar.Time.threeSecond);
    }
}

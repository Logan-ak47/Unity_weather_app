
# Unity Weather App

Unity project for getting User's Location and showing him his current temprature. 



Following are the ways to use the project

  1.From Git

    -This project is created using Unity 2021.3.3f1. 
    -You can directly download this project from git and just open it in the editor and start using it. 

  2.Using Weather Package
    
    -download the WeatherPackage from https://drive.google.com/file/d/1TFhSNBv1L_61jefVn7MCOCVzVHaedr-5/view?usp=sharing
    -Import it in the project directly. 

Setting up the Project

    -Make sure the Project is on Android/iOS platform for location services. 
    -To properly run the project make use of Unity Remote feature as it will allow you to access the location.



## Documentation


Scripts Information 
    
    Following are the scripts used in the project and their information.


ToastBar.cs
    
    -A simple script which can be used to call a method and create a toast bar anywhere on the scene. 

SimpleJson.cs

    -An imported Script which makes accessing JSON object easier

LocationService.cs

    -Script which enables player's location service if it's not enabled and get's the player's latest location in terms of latitude and longitude. 

GetWeather.cs

    -This will be used to hit the open meteo API, with player's current location and get the data with the player's current temprature among other details 

ButtonClick.cs

    -This script allows us to hit the API once again if needed and show the current temprature in degree celcius.


testWeatherAPI.cs

    -A simple test script used to check if the API's address is correct or not. 

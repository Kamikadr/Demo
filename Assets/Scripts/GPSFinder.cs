using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class GPSFinder : MonoBehaviour
{
    public bool isSearching = false;
    public Text text;
    public double latitude { get; private set; }
    public double longitude { get; private set; }
    private void Start()
    {
#if UNITY_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
        }
#elif UNITY_IOS
        PlayerSettings.iOS.locationUsageDescription = "Details to use location";
#endif



    }
    public async Task<string> StartLocationService()
    {
        isSearching = true;
        if (!Input.location.isEnabledByUser)
        {
            text.text = ("User has not enabled location");
            isSearching = false;
            return await Task.FromResult("0,0"); ;
        }
        Input.location.Start();
        while (Input.location.status == LocationServiceStatus.Initializing)
        {
            text.text += " wait";
            //await Task.Yield();
        }
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            text.text = ("Unable to determine device location");
            isSearching = false;
            return await Task.FromResult("0,0"); ;
        }
        latitude = Input.location.lastData.latitude;
        longitude = Input.location.lastData.longitude;
        text.text += "Latitude : " + latitude;
        text.text += "Longitude : " + longitude;
        isSearching = false;
        Input.location.Stop();
        return await Task.FromResult(latitude.ToString() + ',' + longitude.ToString());
    }
}
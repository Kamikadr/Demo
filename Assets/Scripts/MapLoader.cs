using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MapLoader : MonoBehaviour
{
    [SerializeField] bool LoadOnStart;
    private int standartZoom;
    private int standartSize;
    public RawImage map;
    const string URL = "https://maps.googleapis.com/maps/api/staticmap";
    const string KEY = "AIzaSyBdNjqeuzy4Onzhs806qI09ZjV8HT3dRVs";
    private string parametersOfMap;
    void Start()
    {

        if (LoadOnStart)
        {
            LoadMap();
        }
        
    }

    
    private void LoadMap()
    {
        var URI = CreateURI(URL);
        StartCoroutine(RequestMap(URI));
    }
    //?center=40.714728,-73.998672&zoom=12&size=400x400&key=AIzaSyBdNjqeuzy4Onzhs806qI09ZjV8HT3dRVs
    private string CreateURI(string URL)
    {
        Dictionary<string, string> parameters = new Dictionary<string, string>();
        parametersOfMap = "?";
        parameters.Add("center", FindPosition());
        parameters.Add("zoom", "15");
        parameters.Add("scale", "2");
        parameters.Add("size", "960x540");
        parameters.Add("key", KEY);
        foreach (KeyValuePair<string, string> pair in parameters) parametersOfMap += "&" + pair.Key + "=" + pair.Value;
        return URL + parametersOfMap;
    }
    private string FindPosition()
    {
        if (!Input.location.isEnabledByUser) { }
        Input.location.Start();
        while (Input.location.status == LocationServiceStatus.Initializing) ;
        var position = Input.location.lastData.latitude.ToString() + ',' + Input.location.lastData.longitude.ToString();
        return position;
    }
    IEnumerator RequestMap(string URI)
    {
        UnityWebRequest u = UnityWebRequestTexture.GetTexture(URI);
        yield return u.SendWebRequest();
        if (u.result != UnityWebRequest.Result.Success) Debug.Log(u.error);
        else
        {
            map.texture = ((DownloadHandlerTexture) u.downloadHandler).texture;
        }
    }
}


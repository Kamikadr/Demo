using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MapLoader : MonoBehaviour
{
    [SerializeField] bool LoadOnStart = true;
    public RawImage map;
    public URIBuilder builder;
    public Text text;
    void Start()
    {
        if (LoadOnStart)
        {
            LoadMap();
        } 
    }

    
    public void LoadMap()
    {
        text.text += "start load";
        var URI =  builder.CreateURI().GetAwaiter().GetResult();
        StartCoroutine(RequestMap(URI));
    }
    //?center=40.714728,-73.998672&zoom=12&size=400x400&key=AIzaSyBdNjqeuzy4Onzhs806qI09ZjV8HT3dRVs

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


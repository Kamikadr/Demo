using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public  class URIBuilder: MonoBehaviour
{
    private const string URL = "https://maps.googleapis.com/maps/api/staticmap";
    private const string KEY = "AIzaSyBdNjqeuzy4Onzhs806qI09ZjV8HT3dRVs";

    public GPSFinder GPS;
    public Text text;

    public async Task<string> CreateURI()
    {
        text.text += " startGPS";
        var t = GPS.StartLocationService().GetAwaiter().GetResult();



        var parametersOfMap = "?";
        Dictionary<string, string> parameters = new Dictionary<string, string>();
        parameters.Add("center", t);
        parameters.Add("zoom", "15");
        parameters.Add("scale", "2");
        parameters.Add("size", "720x405");
        parameters.Add("key", KEY);
        foreach (KeyValuePair<string, string> pair in parameters) parametersOfMap += "&" + pair.Key + "=" + pair.Value;
        //return URL + parametersOfMap;
        return await Task.FromResult(URL + parametersOfMap);


    }

    /*private IEnumerator CreateURIp()
    {
        yield return StartCoroutine(GPS.StartLocationService());
        var parametersOfMap = "?";
        Dictionary<string, string> parameters = new Dictionary<string, string>();
        parameters.Add("center", );
        parameters.Add("zoom", "15");
        parameters.Add("scale", "2");
        parameters.Add("size", "720x405");
        parameters.Add("key", KEY);
        foreach (KeyValuePair<string, string> pair in parameters) parametersOfMap += "&" + pair.Key + "=" + pair.Value;
        return URL + parametersOfMap;

    }*/
}

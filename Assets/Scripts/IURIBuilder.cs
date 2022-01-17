using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IURIBuilder
{
    IURIBuilder AddSize(string size);
    IURIBuilder AddScale(int scale);
    IURIBuilder AddZoom(int zoom);
    IURIBuilder AddKey(string key);
    IURIBuilder AddCenter(string center);

}

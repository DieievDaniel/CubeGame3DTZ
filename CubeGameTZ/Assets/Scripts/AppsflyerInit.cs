using UnityEngine;
using AppsFlyerSDK;

public class AppsflyerInit : MonoBehaviour
{
    private const string AppId = "App_ID";
    private const string DevKey = "ytPuQc6oHMvGHLh83FVpdd";

    void Start()
    {
        AppsFlyer.initSDK(DevKey, AppId);
        AppsFlyer.startSDK();
    }
}

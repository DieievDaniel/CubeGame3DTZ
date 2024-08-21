using OneSignalSDK;
using UnityEngine;

public class OneSignalInit : MonoBehaviour
{
    void Start()
    {
        OneSignal.Default.Initialize("635c5f12-bb1c-4e8a-92a5-65636c604328");
    }
}

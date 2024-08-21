using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class GeoCheck : MonoBehaviour
{
    private const string geoUrl = "https://ipinfo.io/country";

    void Start()
    {
        StartCoroutine(CheckGeo());
    }

    IEnumerator CheckGeo()
    {
        UnityWebRequest request = UnityWebRequest.Get(geoUrl);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string country = request.downloadHandler.text.Trim();
            if (country != "UA")
            {
                Application.OpenURL("https://uk.wikipedia.org/");
            }
        }
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class TestLocationService : MonoBehaviour
{
    [SerializeField] Text locationOutput;
    public static bool isTesting = false;
#if UNITY_EDITOR
    public static float longitude = 14.5945f;
    public static float latitude = 121.1152f;
#endif
#if !UNITY_EDITOR
    public static float longitude = 0f;
    public static float latitude = 0f;
#endif
    void Start()
    {
        StartCoroutine(LocationCoroutine(2.0f));
    }

    private void Update()
    {
        /*if (locationOutput.text == "Start: LocationCoroutine")
        {
            StartCoroutine(LocationCoroutine(2.0f));
        }*/
    }

    private IEnumerator LocationCoroutine(float waitTime)
    {
        locationOutput.text = "Start: LocationCoroutine";
        // Uncomment if you want to test with Unity Remote
        /*#if UNITY_EDITOR
                yield return new WaitWhile(() => !UnityEditor.EditorApplication.isRemoteConnected);
                yield return new WaitForSecondsRealtime(5f);
        #endif*/
#if UNITY_EDITOR
        // No permission handling needed in Editor
#elif UNITY_ANDROID
        if (!UnityEngine.Android.Permission.HasUserAuthorizedPermission(UnityEngine.Android.Permission.CoarseLocation)) {
            UnityEngine.Android.Permission.RequestUserPermission(UnityEngine.Android.Permission.CoarseLocation);
        }

        // First, check if user has location service enabled
        if (!UnityEngine.Input.location.isEnabledByUser) {
            // TODO Failure
            Debug.LogFormat("Android and Location not enabled");
            locationOutput.text = "Start: Android and Location not enabled";
            yield break;
        }

#elif UNITY_IOS
        if (!UnityEngine.Input.location.isEnabledByUser) {
            // TODO Failure
            Debug.LogFormat("IOS and Location not enabled");
            locationOutput.text = "Start: IOS and Location not enabled";
            yield break;
        }
#endif
        // Start service before querying location
        UnityEngine.Input.location.Start(500f, 500f);

        // Wait until service initializes
        int maxWait = 15;
        while (UnityEngine.Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSecondsRealtime(1);
            maxWait--;
        }

        // Editor has a bug which doesn't set the service status to Initializing. So extra wait in Editor.
#if UNITY_EDITOR
        int editorMaxWait = 15;
        while (UnityEngine.Input.location.status == LocationServiceStatus.Stopped && editorMaxWait > 0)
        {
            yield return new WaitForSecondsRealtime(1);
            editorMaxWait--;
        }
#endif

        // Service didn't initialize in 15 seconds
        if (maxWait < 1)
        {
            // TODO Failure
            Debug.LogFormat("Timed out");
            locationOutput.text = "Timed out";
            yield break;
        }

        // Connection has failed
        if (UnityEngine.Input.location.status != LocationServiceStatus.Running)
        {
            // TODO Failure
            Debug.LogFormat("Unable to determine device location. Failed with status {0}", UnityEngine.Input.location.status);
            locationOutput.text = "Unable to determine device location. Failed with status "+ UnityEngine.Input.location.status;
            StartCoroutine(LocationCoroutine(2.0f));
            yield break;
        }
        else
        {
            Debug.LogFormat("Location service live. status {0}", UnityEngine.Input.location.status);

            locationOutput.text = "Location service live. status " + UnityEngine.Input.location.status;
            // Access granted and location value could be retrieved
            Debug.LogFormat("Location: "
                + UnityEngine.Input.location.lastData.latitude + " "
                + UnityEngine.Input.location.lastData.longitude + " "
                + UnityEngine.Input.location.lastData.altitude + " "
                + UnityEngine.Input.location.lastData.horizontalAccuracy + " "
                + UnityEngine.Input.location.lastData.timestamp);

            var _latitude = UnityEngine.Input.location.lastData.latitude;
            var _longitude = UnityEngine.Input.location.lastData.longitude;

            locationOutput.text = "Start: _latitude: "+ _latitude + " Start: _latitude: " + _longitude;
            longitude = _longitude;
            latitude = _latitude;
            // TODO success do something with location
        }

        // Stop service if there is no need to query location updates continuously
        UnityEngine.Input.location.Stop();
    }
}
using System.Collections;
using System.IO;
using UnityEngine;

public class NativeShareScript : MonoBehaviour
{
    public string[] extraStringArray;

    public GameObject shareDisplay;

    private bool isProcessing = false;
    private bool isFocus = false;

    public void ShareContent(){
        if(!isProcessing){
            shareDisplay.SetActive(true);
            StartCoroutine(ShareScreenshot());
        }
    }
    IEnumerator ShareScreenshot(){

        string appendedString = extraStringArray[Random.Range(0, extraStringArray.Length)] + 
            "\n My high score is " + PlayerPrefs.GetInt("AvoidHigh") + " on the game" +
                  "\n \"AVOID\" by Sebastian Park. Download for Android at: https://play.google.com/store/apps/details?id=com.SparkSoft.AVOID.";

        isProcessing = true;

        yield return new WaitForEndOfFrame();

        ScreenCapture.CaptureScreenshot("avoidScreenshot.png", 2);

        string filePath = Path.Combine(Application.persistentDataPath, "avoidScreenshot.png");

        yield return new WaitForSecondsRealtime(.3f);

        if(!Application.isEditor)
        {
            AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
            AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
            intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
            AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
            AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", "file://" + filePath);
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), appendedString);
            intentObject.Call<AndroidJavaObject>("setType", "image/jpeg");
            AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject chooser = intentClass.CallStatic<AndroidJavaObject>("createChooser",
                                                                                  intentObject, "Share you score");
            currentActivity.Call("startActivity", chooser);

            yield return new WaitForSecondsRealtime(1);

            yield return new WaitUntil(() => isFocus);

            shareDisplay.SetActive(false);
            isProcessing = false;

            
        }
    }

    private void OnApplicationFocus(bool focus)
    {
        isFocus = focus;
    }
}

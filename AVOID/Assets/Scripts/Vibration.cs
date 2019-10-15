using UnityEngine;
using System.Collections;

//From https://gist.github.com/aVolpe/707c8cf46b1bb8dfb363
//and https://gist.github.com/munkbusiness/9e0a7d41bb9c0eb229fd8f2313941564

public static class Vibration

{
    public static AndroidJavaClass vibrationEffectClass;
    public static int defaultAmplitude;

#if UNITY_ANDROID && !UNITY_EDITOR
    public static AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
    public static AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
    public static AndroidJavaObject vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
    public static AndroidJavaClass androidVersion = new AndroidJavaClass("android.os.Build$VERSION");
    public static int apiLevel = androidVersion.GetStatic<int>("SDK_INT");

#else
    public static AndroidJavaClass unityPlayer;
    public static AndroidJavaObject currentActivity;
    public static AndroidJavaObject vibrator;
    public static int apiLevel;
#endif

    public static void Vibrate()
    {

        if (isAndroid())
            vibrator.Call("vibrate");
        else
            Handheld.Vibrate();
    }

    public static void unalteredVibrate(){
        Handheld.Vibrate();
    }

    public static void Vibrate(long milliseconds)
    {
        if (apiLevel >= 26)
        {
            vibrationEffectClass = new AndroidJavaClass("android.os.VibrationEffect");
            defaultAmplitude = vibrationEffectClass.GetStatic<int>("DEFAULT_AMPLITUDE");
        }
        if (apiLevel >= 26)
        {
            CreateVibrationEffect("createOneShot", new object[] { milliseconds, defaultAmplitude });
        }
        else
        {
            if (isAndroid())
                vibrator.Call("vibrate", milliseconds);
            else
                Handheld.Vibrate();
        }
    }

    public static void Vibrate(long[] pattern, int repeat)
    {
        if (isAndroid())
            vibrator.Call("vibrate", pattern, repeat);
        else
            Handheld.Vibrate();
    }

    public static bool HasVibrator()
    {
        return isAndroid();
    }

    public static void Cancel()
    {
        if (isAndroid())
            vibrator.Call("cancel");
    }

    private static void CreateVibrationEffect(string function, params object[] args) {

        AndroidJavaObject vibrationEffect = vibrationEffectClass.CallStatic<AndroidJavaObject>(function, args);
        vibrator.Call("vibrate", vibrationEffect);
    }


    private static bool isAndroid()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
    return true;
#else
        return false;
#endif
    }
}
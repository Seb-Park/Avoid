using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Advertisements;

//using admob;

public class GameManager : MonoBehaviour
{

    //App id is: ca-app-pub-5433175531947512~6520101733
    
    public bool isStarted, gameOver, hasUntouched;
    public GameObject player;
    public ObstacleSpawner os;
    public GameObject[] deathEffect;
    public GameObject startText, restartText;
    public GameObject firstBlock;
    public float score, startTime;
    public Text scoreText, highScoreText, gemsText;
    public GameObject scoreBG;
    public GameObject mainMenuButton;
    public GameObject musicSource;
    public GameObject shareButton, continueButton;
    public GameObject adFailed;
    public int revivesLeft;
    public PlayerController playerController;
    public string videoID, rewardedVideoID;
    public GameObject flash;
    //Admob ad;


    // Use this for initialization
    void Start()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f;
        isStarted = false;
        Advertisement.Initialize("3057181");
        //initAdmob();
        revivesLeft = 1;
        playerController = player.GetComponent<PlayerController>();
    }

    void changeTime()
    {

            if (score <= 250)
            {
                Time.timeScale = (score / 100) + 1;
                Time.fixedDeltaTime = Time.timeScale / 50;
            }
            else
            {
                Time.timeScale = (250 / 100) + 1;
                Time.fixedDeltaTime = Time.timeScale / 50;
            }
    }

    // Update is called once per frame
    void Update()
    {
        changeTime();
        if (isStarted)
        {
            score = Time.time - startTime;
            scoreText.text = Mathf.Floor(score).ToString("f0");
            gemsText.text = playerController.gemsCollected.ToString();
        }
        if ((Input.touchCount > 0 && !gameOver && !isStarted && Input.GetTouch(0).position.y > 350)||(Input.GetMouseButton(0) && !gameOver && !isStarted))
        {
            //Debug.Log(Input.GetTouch(0).position.y);
            startTime = Time.time - score; //makes it so that if the player restarts then the score is appended
            if (revivesLeft != 0)//null proofs so that it's not looking for the block when you try to revive if the block is gone
            {
                firstBlock.gameObject.SetActive(true);
            }
            else{
                foreach (GameObject go in GameObject.FindGameObjectsWithTag("block"))
                {
                    if (!go.name.Substring(0, 4).Equals("Wall") && !go.name.Substring(0, 4).Equals("Star"))
                    {
                        //Debug.Log("Destroying " + go.name);
                        Destroy(go);
                    }
                }
                flash.SetActive(false);
            }
            isStarted = true;
            player.SetActive(true);
            os.gameObject.SetActive(true);
            startText.SetActive(false);
            mainMenuButton.gameObject.SetActive(false);
            if (PlayerPrefs.GetInt("isMusic") < 1)
            {
                musicSource.SetActive(true);
            }
        }
        if (isStarted && Input.touchCount < 1&&!Input.GetMouseButton(0))
        {
            hasUntouched = true;
            endGame();
        }
        if (Input.touchCount < 1 && gameOver)
        {
            hasUntouched = true;
        }
        //if (Input.touchCount > 0 && gameOver&&hasUntouched&&Input.GetTouch(0).position.y>350)
        //{
        //    Restart();
        //}
    }

    public void Restart()
    {
        Debug.Log("restarting I guess!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        //Debug.Log("loading menu...");
        SceneManager.LoadSceneAsync(0);
    }

    public void endGame()
    {
        if (PlayerPrefs.GetInt("isVibrate") < 1)
        {
            //Handheld.Vibrate();
            Vibration.Vibrate(100);
        }
        flash.SetActive(true);
        //Vibrate(200);
        scoreBG.SetActive(true);
        if ((int)score > PlayerPrefs.GetInt("AvoidHigh"))
        {
            PlayerPrefs.SetInt("AvoidHigh", (int)score);
        }
        mainMenuButton.gameObject.SetActive(true);
        highScoreText.gameObject.SetActive(true);
        highScoreText.text = PlayerPrefs.GetInt("AvoidHigh").ToString();
        isStarted = false;
        gameOver = true;
        playerController.clearFrozen();
        Instantiate(deathEffect[PlayerPrefs.GetInt("skin")], player.transform.position, Quaternion.identity);
        player.transform.position = new Vector3(0, 0, 0);
        player.SetActive(false);
        os.gameObject.SetActive(false);
        restartText.gameObject.SetActive(true);
        shareButton.SetActive(true);
        if (revivesLeft > 0 && score >= 50)
        {
            continueButton.SetActive(true);
        }
        musicSource.SetActive(false);
        int currentGems = PlayerPrefs.GetInt("gems");
        PlayerPrefs.SetInt("gems", currentGems + playerController.gemsCollected);

    }

    public void requestRevive()
    {
        ShowOptions so = new ShowOptions();
        so.resultCallback = testRewardedAdResult;

        Advertisement.Show("rewardedVideo", so);
    }

    public void testRewardedAdResult(ShowResult sr)
    {
        //loadAdMobAd();
        if (sr == ShowResult.Finished)
        {
            Revive();
        }
        else
        {
            //show failed ad thingy
            adFailed.SetActive(true);
            //and then try to do it again
            Advertisement.Initialize("3057181");
        }
    }

    public void Revive()
    {
        scoreBG.SetActive(false);
        mainMenuButton.gameObject.SetActive(false);
        highScoreText.gameObject.SetActive(false);
        isStarted = false;
        gameOver = false;
        player.SetActive(false);
        os.gameObject.SetActive(true);
        startText.SetActive(true);
        restartText.gameObject.SetActive(false);
        shareButton.SetActive(false);
        continueButton.SetActive(false);
        musicSource.SetActive(false);
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("block"))
        {
            if (!go.name.Substring(0, 4).Equals("Wall") && !go.name.Substring(0, 4).Equals("Star"))
            {
                //Debug.Log("Destroying " + go.name);
                Destroy(go);
            }
        }
        revivesLeft -= 1;
        int currentGems = PlayerPrefs.GetInt("gems");
        PlayerPrefs.SetInt("gems", currentGems - player.GetComponent<PlayerController>().gemsCollected);
    }

    public void Vibrate(long duration)
    {
        if (Application.platform != RuntimePlatform.Android) return;
        AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaClass vibratorClass = new AndroidJavaClass("android.os.Vibrator");
        AndroidJavaObject vibratorService = currentActivity.Call<AndroidJavaObject>("getSystemService", currentActivity.GetStatic<string>("VIBRATOR_SERVICE"));
        //AndroidJavaObject vibratorService = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");


        vibratorService.Call("vibrate", duration);
        unity.Dispose();
        currentActivity.Dispose();
        vibratorClass.Dispose();
        vibratorService.Dispose();
    }


    //private void loadAdMobAd()
    //{
    //    if (ad.isInterstitialReady())
    //    {
    //        ad.showInterstitial();
    //    }
    //    else
    //    {
    //        ad.loadInterstitial(videoID);
    //    }

    //}

//    void initAdmob()
//    {
//#if UNITY_IOS
//                // appID="ca-app-pub-3940256099942544~1458002511";
//                 bannerID="ca-app-pub-3940256099942544/2934735716";
//                 interstitialID="ca-app-pub-3940256099942544/4411468910";
//                 videoID="ca-app-pub-3940256099942544/1712485313";
//                 nativeBannerID = "ca-app-pub-3940256099942544/3986624511";
//#elif UNITY_ANDROID
//        //appID="ca-app-pub-3940256099942544~3347511713";
//        //bannerID = "ca-app-pub-3940256099942544/6300978111";
//        //interstitialID = "ca-app-pub-3940256099942544/1033173712";
//        //nativeBannerID = "ca-app-pub-3940256099942544/2247696110";
//#endif
    //    AdProperties adProperties = new AdProperties();
    //    /*
    //            adProperties.isTesting(true);
    //            adProperties.isAppMuted(true);
    //            adProperties.isUnderAgeOfConsent(false);
    //            adProperties.appVolume(100);
    //            adProperties.maxAdContentRating(AdProperties.maxAdContentRating_G);
    //    string[] keywords = { "key1", "key2", "key3" };
    //            adProperties.keyworks(keywords);
    //    */
    //    ad = Admob.Instance();
    //    //ad.bannerEventHandler += onBannerEvent;
    //    ad.interstitialEventHandler += onInterstitialEvent;
    //    //ad.rewardedVideoEventHandler += onRewardedVideoEvent;
    //    //ad.nativeBannerEventHandler += onNativeBannerEvent;
    //    ad.initSDK(adProperties);//reqired,adProperties can been null
    //}

    //void onInterstitialEvent(string eventName, string msg)
    //{
    //    Debug.Log("handler onAdmobEvent---" + eventName + "   " + msg);
    //    if (eventName == AdmobEvent.onAdLoaded)
    //    {
    //        Admob.Instance().showInterstitial();
    //    }
    //}
}

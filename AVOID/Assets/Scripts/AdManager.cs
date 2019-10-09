using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Advertisement.Initialize("3057181");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showAd(){
        if (Advertisement.IsReady("video "))
        {
            Advertisement.Show("video", new ShowOptions(){resultCallback = HandleAdResult});
        }
    }

    private void HandleAdResult(ShowResult result){
        switch(result){
            case ShowResult.Finished:
                Debug.Log("finished");
                break;
            case ShowResult.Skipped:
                Debug.Log("skipped");
                break;
            case ShowResult.Failed:
                Debug.Log("failed");
                break;
        }
    }



}

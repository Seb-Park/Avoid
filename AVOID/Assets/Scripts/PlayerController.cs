using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Vector3 touchPosition;
    public Vector3 pixelTouchPosition;
    public Camera camera;
    public GameManager gm;
    public GameObject gemEffect, blueGemEffect;
    public GameObject[] skins;
    public int gemsCollected;
    public bool frozen;
    public ObstacleSpawner obstacleSpawner;
    public GameObject frozenScreenEffect;
    public ParticleSystem snow;

    // Use this for initialization
    void Start () {
        Instantiate(skins[PlayerPrefs.GetInt("skin")],GetComponent<Transform>());
	}
	
	// Update is called once per frame
	void Update () {
        if (gm.isStarted)
        {
            //brackeysMove();
            debugMove();
        }
    }

    public void brackeysMove(){
        if(Input.touchCount>0){
            Touch touch = Input.GetTouch(0);
            touchPosition = camera.ScreenToWorldPoint(touch.position);
            pixelTouchPosition = touch.position;
            touchPosition.z = 0f;
            transform.position = Vector2.Lerp(transform.position, touchPosition, 0.5f); ;
        }
    }

    public void debugMove(){
        if (Input.GetMouseButton(0))
        {
            touchPosition = camera.ScreenToWorldPoint(Input.mousePosition);
            touchPosition.z = 0f;
            transform.position = Vector2.Lerp(transform.position, touchPosition, 0.5f); ;
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("found collision");
        if(collision.gameObject.CompareTag("block")){
            //Debug.Log("collision is a block");
            gm.endGame();
        }

    }

    public IEnumerator freezeTime(float dur){

        snow.Play();
        frozenScreenEffect.SetActive(true);
        frozenScreenEffect.GetComponent<FrozenTimeCanvas>().startTime = Time.time;
        obstacleSpawner.gameObject.SetActive(false);
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("block");
        foreach (GameObject obstacle in obstacles){
            if (obstacle != null)
            {
                if (obstacle.gameObject.GetComponent<Obstacle>() != null)
                {
                    obstacle.gameObject.GetComponent<Obstacle>().frozen = true;
                }
                //if (obstacle.gameObject.GetComponent<Animator>() != null)
                //{
                //    obstacle.gameObject.GetComponent<Animator>().enabled = false;
                //}
                //if (obstacle.gameObject.GetComponent<Rotate>() != null)
                //{
                //    obstacle.gameObject.GetComponent<Rotate>().enabled = false;
                //}
                else
                {
                    Debug.Log("There is no animator on " + obstacle.name);
                }
            }
        }
        yield return new WaitForSeconds(dur);
        foreach (GameObject obstacle in obstacles)
        {
            if (obstacle != null)
            {
                if (obstacle.gameObject.GetComponent<Obstacle>() != null)
                {
                    obstacle.gameObject.GetComponent<Obstacle>().frozen = false;
                }
                //if (obstacle.gameObject.GetComponent<Animator>() != null)
                //{
                //    obstacle.gameObject.GetComponent<Animator>().enabled = true;
                //}
                //if (obstacle.gameObject.GetComponent<Rotate>() != null)
                //{
                //    obstacle.gameObject.GetComponent<Rotate>().enabled = true;
                //}
            }
        }
        obstacleSpawner.gameObject.SetActive(true);
        frozenScreenEffect.SetActive(false);
        snow.Stop();
    }

    public void clearFrozen(){
        frozenScreenEffect.SetActive(false);
        snow.Stop();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Crystal"))
        {
            //Debug.Log("collision is a block");
            Destroy(collision.gameObject);
            gemsCollected++;
            if (PlayerPrefs.GetInt("isVibrate") < 1)
            {
                Vibration.Vibrate(20);
            }
            Instantiate(gemEffect, transform.position, Quaternion.identity);

        }

        if (collision.gameObject.CompareTag("BlueCrystal"))
        {
            Destroy(collision.gameObject);
            gemsCollected+=10;
            if (PlayerPrefs.GetInt("isVibrate") < 1)
            {
                Vibration.Vibrate(20);
            }
            Instantiate(blueGemEffect, transform.position, Quaternion.identity);

        }
        if (collision.gameObject.CompareTag("block"))
        {
            gm.endGame();
        }
        if(collision.gameObject.CompareTag("freezeTime")){
            foreach (GameObject i in GameObject.FindGameObjectsWithTag("freezeTime"))
            {
                Destroy(i);//this makes sure you don't bump into two freeze times
            }
            StartCoroutine(freezeTime(10));
            obstacleSpawner.timeToSpawn += 10;
        }
    }


}

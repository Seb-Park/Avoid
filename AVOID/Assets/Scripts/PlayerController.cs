using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Vector3 touchPosition;
    public Vector3 pixelTouchPosition;
    public Camera camera;
    public GameManager gm;
    public GameObject gemEffect;
    public GameObject[] skins;
    public int gemsCollected;
    public bool frozen;
    public ObstacleSpawner obstacleSpawner;
    public GameObject frozenScreenEffect;

    // Use this for initialization
    void Start () {
        Instantiate(skins[PlayerPrefs.GetInt("skin")],GetComponent<Transform>());
	}
	
	// Update is called once per frame
	void Update () {
        if (gm.isStarted)
        {
            brackeysMove();
            //debugMove();
        }
    }

    public void brackeysMove(){
        if(Input.touchCount>0){
            Touch touch = Input.GetTouch(0);
            touchPosition = camera.ScreenToWorldPoint(touch.position);
            pixelTouchPosition = touch.position;
            touchPosition.z = 0f;
            transform.position = Vector3.Slerp(transform.position, touchPosition, 0.5f); ;
        }
    }

    public void debugMove(){
        if (Input.GetMouseButton(0))
        {
            touchPosition = camera.ScreenToWorldPoint(Input.mousePosition);
            touchPosition.z = 0f;
            transform.position = Vector3.Slerp(transform.position, touchPosition, 0.5f); ;
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
        frozenScreenEffect.SetActive(true);
        frozenScreenEffect.GetComponent<FrozenTimeCanvas>().startTime = Time.time;
        obstacleSpawner.gameObject.SetActive(false);
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("block");
        foreach (GameObject obstacle in obstacles){
            if (obstacle.gameObject.GetComponent<Obstacle>() != null)
            {
                obstacle.gameObject.GetComponent<Obstacle>().frozen = true;
            }
        }
        yield return new WaitForSeconds(dur);
        foreach (GameObject obstacle in obstacles)
        {
            if (obstacle.gameObject.GetComponent<Obstacle>() != null)
            {
                obstacle.gameObject.GetComponent<Obstacle>().frozen = false;
            }
        }
        obstacleSpawner.gameObject.SetActive(true);
        frozenScreenEffect.SetActive(false);

    }

    public void stopTimeFor(float dur)
    {
        float start = Time.time;
        obstacleSpawner.gameObject.SetActive(false);
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("block");
        Debug.Log(blocks.Length + " is the number of obstacles currently in scene.");
        //foreach (GameObject obstacle in blocks)
        //{
        //    if(obstacle.gameObject.GetComponent<Obstacle>()==null){
        //        Debug.Log("this obstacle lacks an obstacle component");
        //    }
        //    obstacle.gameObject.GetComponent<Obstacle>().frozen = true;
        //}
        for (int i = 0; i < blocks.Length; i++){
            if (blocks[i].gameObject.GetComponent<Obstacle>() == null)
            {

                Debug.Log("this obstacle " + i + " lacks an obstacle component");
            }
            else
            {
                blocks[i].GetComponent<Obstacle>().frozen = true;
            }
        }
        while(Time.time - start >= dur){
            Debug.Log("Waiting...");
        }
        Debug.Log("I've waited " + dur + " seconds");
        foreach (GameObject obstacle in blocks)
        {
            if (obstacle.gameObject.GetComponent<Obstacle>() != null)
            {
                obstacle.gameObject.GetComponent<Obstacle>().frozen = true;
            }
        }
        obstacleSpawner.gameObject.SetActive(true);
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
        if (collision.gameObject.CompareTag("block"))
        {
            Debug.Log("collision is a block");
            gm.endGame();
        }
        if(collision.gameObject.CompareTag("freezeTime")){
            Destroy(collision.gameObject);
            StartCoroutine(freezeTime(10));
            obstacleSpawner.timeToSpawn += 10;
            //stopTimeFor(5);

        }
    }


}

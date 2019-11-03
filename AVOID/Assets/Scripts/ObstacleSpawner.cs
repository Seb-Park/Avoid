using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour {

    public Transform spawnPoint;
    public float timeToSpawn;
    public float delay;
    public GameObject[] obstacles;
    public GameObject crystal;
    public GameObject[] powerups;

	// Use this for initialization
	void Start () {
        spawnObstacles();
        timeToSpawn = Time.time + delay;
    }

    // Update is called once per frame
    void Update () {
        if(Time.time>=timeToSpawn){
            spawnObstacles();
            spawnCrystal();
            timeToSpawn = Time.time + delay;
        }
        //delay = 5 * Time.timeScale;
	}

    public void spawnCrystal(){
        int randomCrystal = Random.Range(0,5);
        if (randomCrystal == 4)
        {
            Instantiate(crystal, spawnPoint.transform.position, Quaternion.identity);

        }
        else
        {
            randomCrystal = Random.Range(0, 5);
            if(randomCrystal == 4){
                Instantiate(powerups[0], spawnPoint.transform.position, Quaternion.identity);
            }
            if (randomCrystal == 2)
            {
                Instantiate(powerups[1], spawnPoint.transform.position, Quaternion.identity);
            }

        }
    }

    public void spawnObstacles(){
        int randomObstacle = Random.Range(0,obstacles.Length);
        //Instantiate(obstacles[randomObstacle], new Vector3(0,15,0), Quaternion.identity);
        Instantiate(obstacles[randomObstacle], spawnPoint.transform.position,Quaternion.identity);

    }
}

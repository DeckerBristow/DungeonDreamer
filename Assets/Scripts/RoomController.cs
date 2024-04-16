using System.Collections;
using System.Collections.Generic;
using static System.Int32;
using UnityEngine;
using System;

public class RoomController : MonoBehaviour
{
    private int seed;
    public GameObject enemyPrefab;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateRoom(int newSeed) {

        if (newSeed == 0) {
            newSeed = UnityEngine.Random.Range(1,MaxValue);
        }
        Debug.Log("Generated new room: " + newSeed);
        this.seed = newSeed;
        System.Random random = new System.Random(this.seed);
        generateEnemies(ref random);
        generateHealthPickups(ref random);
    }

    private void generateEnemies(ref System.Random random) {
        GameObject[] existingEnemies = GameObject.FindGameObjectsWithTag("BrainSlayer");
        foreach (GameObject enemy in existingEnemies)
        {
            Destroy(enemy);
        }
        int enemies = random.Next(1, 5);
        for (int i = 0; i < enemies; i++) {
            float x = random.Next(-1465, 1530)/100.0f;
            float y = random.Next(-389, 483)/100.0f;
            GameObject enemyPrefab = Resources.Load<GameObject>("BrainSlayer");
            Instantiate(enemyPrefab, new Vector3(x, y, 0), Quaternion.identity);
        }
    }

    private void generateHealthPickups(ref System.Random random) {
        GameObject[] existingPickups = GameObject.FindGameObjectsWithTag("HealthPickup");
        foreach (GameObject pickup in existingPickups) {
            Destroy(pickup);
        }
        int val = random.Next(1, 100);
        int healthPickups = 0;
        if(val == 100) {
            healthPickups = 4;
        }
        else if (val > 92) {
            healthPickups = 3;
        }
        else if (val > 75) {
            healthPickups = 2;
        }
        else if (val > 55) {
            healthPickups = 1;
        }
        for (int i = 0; i < healthPickups; i++) {
            float x = random.Next(-1465, 1530)/100.0f;
            float y = random.Next(-389, 483)/100.0f;
            GameObject healthPrefab = Resources.Load<GameObject>("HealthPickup");
            Instantiate(healthPrefab, new Vector3(x, y, 0), Quaternion.identity);
        }
    }
}

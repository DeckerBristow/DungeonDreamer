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
        GameObject[] existingEnemies = GameObject.FindGameObjectsWithTag("BrainSlayer");
        Debug.Log(existingEnemies);
        foreach (GameObject enemy in existingEnemies)
        {
            Destroy(enemy);
        }
        System.Random random = new System.Random(this.seed);
        int enemies = random.Next(1, 5);
        // int enemies = 1;
        for (int i = 0; i < enemies; i++) {
        // Debug.Log(random.Next());
            float x = random.Next(-1465, 1530)/100.0f;
            float y = random.Next(-389, 483)/100.0f;
            GameObject enemyPrefab = Resources.Load<GameObject>("BrainSlayer");
            Instantiate(enemyPrefab, new Vector3(x, y, 0), Quaternion.identity);
        }
        // GameObject.Find("BrainSlayer").transform.position = new Vector3(x, y, 0);
    }
}

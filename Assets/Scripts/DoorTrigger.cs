using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public enum DoorLocation
    {
        Top,
        Left,
        Right,
        Bottom
    }

    // Public variable to select the door location in the inspector
    public DoorLocation doorLocation;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            Debug.Log("Player entered door!");
            switch(doorLocation) {
                case DoorLocation.Top:
                    player.transform.position = new Vector3(0.35f, -4.39f, 0f);
                    break;
                case DoorLocation.Left:
                    player.transform.position = new Vector3(15.3f, 0f, 0f);
                    break;
                case DoorLocation.Right: 
                    player.transform.position = new Vector3(-14.65f, 0f, 0f);
                    break;
                case DoorLocation.Bottom: 
                    player.transform.position = new Vector3(0.35f, 4.93f, 0f);
                    break;
                default: 
                    player.transform.position = new Vector3(0.33f, 0.24f, 0.0f);
                    Debug.Log("Error, door location not set!");
                    break;
            }
        }
    }

}

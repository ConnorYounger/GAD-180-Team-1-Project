using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStickToPlatform : MonoBehaviour
{
    public bool canStick = true;

    private GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject == player && canStick)
        {
            player.transform.parent = transform;

            player.GetComponent<PlayerPlatformMovement>().control = true;
        }
        

        /*
        if (other.GetComponent<Rigidbody>())
        {
            other.transform.parent = transform;
        }
        */
    }

    private void OnTriggerExit(Collider other)
    {
        
        if (other.gameObject == player && player.transform.parent == transform)
        {
            player.transform.parent = null;

            player.GetComponent<PlayerPlatformMovement>().control = false;
        }
        

        /*
        if(other.GetComponent<Rigidbody>() && other.transform.parent == transform)
        {
            other.transform.parent = null;
        }
        */
    }
}

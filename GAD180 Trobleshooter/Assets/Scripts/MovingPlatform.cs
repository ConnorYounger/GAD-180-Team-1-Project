﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float time = 1;

    public float ySpeed = 1;
    public float xSpeed = 0;
    public float zSpeed = 0;
    public float distance = 10;

    public int selectedOrb;

    private float currentDistance;
    private float timer;

    public bool timeOrbHit;
    private bool positiveMovement = true;

    public GameObject platform;

    void Start()
    {
        
    }

    void Update()
    {
        float speed = (time * Time.deltaTime);

        if (positiveMovement)
        {
            platform.transform.position = new Vector3(transform.position.x + (xSpeed * speed), transform.position.y + (ySpeed * speed), transform.position.z + (zSpeed * speed));

            currentDistance += 1 * speed;
        }
        else
        {
            platform.transform.position = new Vector3(transform.position.x - (xSpeed * speed), transform.position.y - (ySpeed * speed), transform.position.z - (zSpeed * speed));

            currentDistance -= 1 * speed;
        }

        if(currentDistance > distance && positiveMovement)
        {
            positiveMovement = false;
        }
        else if(currentDistance < 0 && !positiveMovement)
        {
            positiveMovement = true;
        }
    }

    public void OrbHit(int selOrb, float multiplier)
    {
        selectedOrb = selOrb;

        if (selOrb == 0)
        {
            time = 1 * multiplier;

            Debug.Log("Orb Hit animation * " + multiplier);
        }
        else if (selOrb == 1)
        {
            time = 1 / multiplier;

            Debug.Log("Orb Hit animation / " + multiplier);
        }

        timeOrbHit = true;
    }

    public void TimeOrbRelease()
    {
        time = 1;

        timeOrbHit = false;
    }
}

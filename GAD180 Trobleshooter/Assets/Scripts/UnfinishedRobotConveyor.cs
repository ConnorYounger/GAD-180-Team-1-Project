using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnfinishedRobotConveyor : MonoBehaviour
{
    public float spawnTime = 5;

    public GameObject unfinishedRobot;

    void Start()
    {
        Invoke("SpawnRobot", spawnTime);
    }

    public void SpawnRobot()
    {
        GameObject spawned = Instantiate(unfinishedRobot, transform.position, transform.rotation);
        spawned.transform.parent = transform;

        Invoke("SpawnRobot", spawnTime);
    }
}

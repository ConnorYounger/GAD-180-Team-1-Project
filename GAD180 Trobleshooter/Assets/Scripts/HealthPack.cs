using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    public int healthAmount = 4;

    private Animator animator;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<PlayerHealth>())
        {
            collision.collider.GetComponent<PlayerHealth>().AddHealth(healthAmount);

            animator.Play("HealRobotHeal");

            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
}

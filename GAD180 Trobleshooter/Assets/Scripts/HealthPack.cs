using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    public int healthAmount = 4;

    private Animator animator;
    private AudioSource audioSource;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerHealth>() && other.GetComponent<PlayerHealth>().health < other.GetComponent<PlayerHealth>().startingHealth)
        {
            other.GetComponent<PlayerHealth>().AddHealth(healthAmount);

            animator.Play("HealRobotHeal");

            if (audioSource = gameObject.GetComponent<AudioSource>())
            {
                audioSource.Play();
            }

            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
}

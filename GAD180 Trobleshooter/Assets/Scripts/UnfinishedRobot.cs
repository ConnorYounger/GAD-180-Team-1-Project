using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnfinishedRobot : MonoBehaviour
{
    public GameObject[] limbs;

    public int startingHealth = 1;
    private int health = 1;


    public bool isAlive = true;

    public GameObject destroyFx;
    private Animator animator;

    public AudioClip deathSound;
    public AudioClip[] otherDeathSounds;

    public Animator parentObject;

    public float startDespawnTime = 55;
    public float despawnTime = 10;

    private AudioSource audioSource;

    void Start()
    {
        StopRagdoll();

        if (gameObject.GetComponent<Animator>())
        {
            animator = gameObject.GetComponent<Animator>();
        }

        health = startingHealth;

        if (gameObject.GetComponent<AudioSource>())
        {
            audioSource = gameObject.GetComponent<AudioSource>();
        }

        if (parentObject)
        {
            Destroy(parentObject.gameObject, startDespawnTime);
        }
    }

    public void StopRagdoll()
    {
        foreach (GameObject limb in limbs)
        {
            if (limb.GetComponent<Rigidbody>())
            {
                limb.GetComponent<Rigidbody>().useGravity = false;
                limb.GetComponent<Rigidbody>().isKinematic = true;
            }

            if (limb.GetComponent<BoxCollider>())
            {
                limb.GetComponent<BoxCollider>().enabled = true;
                limb.GetComponent<BoxCollider>().isTrigger = true;
            }

            if (limb.GetComponent<SphereCollider>())
            {
                limb.GetComponent<SphereCollider>().enabled = true;
                limb.GetComponent<SphereCollider>().isTrigger = true;
            }

            if (limb.GetComponent<CapsuleCollider>())
            {
                limb.GetComponent<CapsuleCollider>().enabled = true;
                limb.GetComponent<CapsuleCollider>().isTrigger = true;
            }
        }
    }

    public void Ragdoll()
    {
        animator.enabled = false;

        if (gameObject.GetComponent<CapsuleCollider>())
        {
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
        }

        foreach (GameObject limb in limbs)
        {
            if (limb.GetComponent<Rigidbody>())
            {
                limb.GetComponent<Rigidbody>().useGravity = true;
                limb.GetComponent<Rigidbody>().isKinematic = false;
            }

            if (limb.GetComponent<BoxCollider>())
            {
                limb.GetComponent<BoxCollider>().enabled = true;
                limb.GetComponent<BoxCollider>().isTrigger = false;
            }

            if (limb.GetComponent<SphereCollider>())
            {
                limb.GetComponent<SphereCollider>().enabled = true;
                limb.GetComponent<SphereCollider>().isTrigger = false;
            }

            if (limb.GetComponent<CapsuleCollider>())
            {
                limb.GetComponent<CapsuleCollider>().enabled = true;
                limb.GetComponent<CapsuleCollider>().isTrigger = false;
            }
        }
    }

    public void TakeDamage(int dmg)
    {
        health -= dmg;

        CheckForDeath();
    }

    void CheckForDeath()
    {
        if (health <= 0 && isAlive)
        {
            Die();
        }
    }

    public void Die()
    {
        if (parentObject)
        {
            parentObject.enabled = false;

            Destroy(parentObject.gameObject, despawnTime);
        }

        isAlive = false;

        Ragdoll();

        if (destroyFx)
        {
            Instantiate(destroyFx, new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z), Quaternion.identity);
        }

        if (audioSource)
        {
            if (deathSound)
            {
                audioSource.clip = deathSound;

                audioSource.Play();
            }

            foreach (AudioClip clip in otherDeathSounds)
            {
                AudioSource.PlayClipAtPoint(clip, transform.position);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchPlatform : MonoBehaviour
{
    public float normalLaunchForce = 500;
    public float speedUpLaunchForce = 500;
    public float slowDownLaunchForce = 500;
    public float coolDown = 1;
    private float currentLaunchForce;
    private float particleSpeed;

    public int selectedOrb;

    private bool canLaunch = true;
    public bool timeOrbHit;

    public ParticleSystem particles;
    public Light light;

    private Color defultColour;
    private Color defultLightColour;
    public Color speedUpColor;
    public Color slowDownColor;

    public MeshRenderer meshRenderer;
    private Material defultMaterial;
    public Material speedUpMaterial;
    public Material slowDownMaterial;

    private AudioSource audioSource;

    void Start()
    {
        currentLaunchForce = normalLaunchForce;

        if (particles)
        {
            defultColour = particles.startColor;
            particleSpeed = particles.startSpeed;
        }
        if (meshRenderer)
        {
            defultMaterial = meshRenderer.material;
        }
        if (light)
        {
            defultLightColour = light.color;
        }

        if (gameObject.GetComponent<AudioSource>())
        {
            audioSource = gameObject.GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Rigidbody>() && canLaunch)
        {
            //collision.gameObject.transform.position = new Vector3(collision.gameObject.transform.position.x, collision.gameObject.transform.position.y + 10, collision.gameObject.transform.position.z);

            if(other.gameObject.name == "Player")
            {
                other.GetComponent<PlayerMovement>().Jump();
            }
            other.gameObject.GetComponent<Rigidbody>().AddForce(transform.up * currentLaunchForce);

            if (audioSource)
            {
                audioSource.Play();
            }

            canLaunch = false;

            Invoke("CoolDown", coolDown);
        }
    }

    /*
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody>() && canLaunch)
        {
            //collision.gameObject.transform.position = new Vector3(collision.gameObject.transform.position.x, collision.gameObject.transform.position.y + 10, collision.gameObject.transform.position.z);

            collision.gameObject.GetComponent<Rigidbody>().AddForce(transform.up * currentLaunchForce);

            canLaunch = false;

            Invoke("CoolDown", coolDown);
        }
    }
    */

    public void ChangeLaunchForce(int orbType)
    {
        selectedOrb = orbType;

        if (orbType == 0)
        {
            currentLaunchForce = speedUpLaunchForce;

            if (particles)
            {
                particles.startColor = speedUpColor;
                particles.startSpeed = particles.startSpeed * 2;
            }
            if (meshRenderer)
            {
                meshRenderer.material = speedUpMaterial;
            }
            if (light)
            {
                light.color = speedUpColor;
            }
        }
        else if(orbType == 1)
        {
            currentLaunchForce = slowDownLaunchForce;

            if (particles)
            {
                particles.startColor = slowDownColor;
                particles.startSpeed = particles.startSpeed * 0.5f;
            }
            if (meshRenderer)
            {
                meshRenderer.material = slowDownMaterial;
            }
            if (light)
            {
                light.color = slowDownColor;
            }
        }

        timeOrbHit = true;
    }

    public void TimeOrbRelease()
    {
        currentLaunchForce = normalLaunchForce;

        if (particles)
        {
            particles.startColor = defultColour;
            particles.startSpeed = particleSpeed;
        }
        if (meshRenderer)
        {
            meshRenderer.material = defultMaterial;
        }
        if (light)
        {
            light.color = defultLightColour;
        }

        timeOrbHit = false;
    }

    void CoolDown()
    {
        canLaunch = true;
    }
}

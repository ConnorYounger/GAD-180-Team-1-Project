using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentOpening : MonoBehaviour
{
    public bool isOpened;

    public bool breakIntoPieces;
    public GameObject ventPieces;

    private AudioSource audioSource;

    public bool addForce;
    private float force = 0.0001f;

    private void Start()
    {
        if (gameObject.GetComponent<AudioSource>())
        {
            audioSource = gameObject.GetComponent<AudioSource>();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.GetComponent<Rigidbody>() && collision.collider.GetComponent<Rigidbody>().velocity.magnitude > 3 && !isOpened)
        {
            Open(collision.collider.gameObject);
        }
    }

    public void Open(GameObject colliderObject)
    {
        if (!breakIntoPieces) {
            if (gameObject.GetComponent<Rigidbody>())
            {
                isOpened = true;

                gameObject.GetComponent<Rigidbody>().isKinematic = false;
            }
        }
        else
        {
            isOpened = true;

            GameObject pieces = Instantiate(ventPieces, transform.position, transform.rotation);

            if (addForce)
            {
                for(int i = 0; i < pieces.transform.childCount; i++)
                {
                    pieces.transform.GetChild(i).gameObject.GetComponent<Rigidbody>().AddForce(colliderObject.transform.forward * force);
                }
            }

            //gameObject.SetActive(false);

            gameObject.GetComponent<BoxCollider>().enabled = false;
            gameObject.GetComponent<MeshRenderer>().enabled = false;

            audioSource.Play();
        }
    }
}

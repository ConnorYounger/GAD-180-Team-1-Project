using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MonoBehaviour
{
    public float initialSpeed = 50.0f;
    private float speed;
    private float initialHeight = 0.5f;

    private bool isJumping = false;

    public float magnitude = 1.0f;

    private float x;
    private float y = 0.0f;
    private float z;

    private void Start()
    {
        y = initialHeight;
        speed = initialSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxis("Horizontal") * magnitude * Time.deltaTime;
        z = Input.GetAxis("Vertical") * magnitude * Time.deltaTime;

        if (Input.GetButtonDown("Fire1"))
        {
            //y = initialHeight;
            isJumping = true;

            speed = initialSpeed;

            Debug.Log(isJumping);
        }

        if (isJumping)
        {
            y += speed * Time.deltaTime;

            Vector3 position = new Vector3(transform.position.x, y, transform.position.z);

            transform.position = position;

            speed += Physics.gravity.y * Time.deltaTime;
        }

        if (y < initialHeight)
        {
            isJumping = false;

            y = initialHeight;

            transform.position = new Vector3(transform.position.x, initialHeight, transform.position.z);
            Debug.Log(isJumping);

        }

        transform.Translate(x, 0, z);

    }
}

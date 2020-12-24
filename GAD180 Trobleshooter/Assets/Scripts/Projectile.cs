using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float bulletSpeed = 10;
    public float bulletForce = 100;
    public float time = 1;

    public int bulletDamage = 1;

    public int pierceCount = 1;
    private bool gravity = false;

    public GameObject destroyFx;

    private void FixedUpdate()
    {
        if (!gravity)
        {
            transform.Translate(Vector3.forward * bulletSpeed * time * Time.deltaTime);
        }

        RaycastHit hit;

        if (Physics.SphereCast(transform.position, 0.1f,transform.forward, out hit, 0.5f))
        {
            Debug.DrawLine(transform.position, hit.point, Color.red);

            if (hit.collider.GetComponent<RobotAI>())
            {
                if (hit.collider.GetComponent<RobotCollisionBox>())
                {
                    if (hit.collider.GetComponent<RobotCollisionBox>().robotParent.GetComponent<RobotAI>())
                    {
                        hit.collider.GetComponent<RobotCollisionBox>().robotParent.GetComponent<RobotAI>().TakeDamage(bulletDamage);
                    }
                    else if (hit.collider.GetComponent<RobotCollisionBox>().robotParent.GetComponent<UnfinishedRobot>())
                    {
                        hit.collider.GetComponent<RobotCollisionBox>().robotParent.GetComponent<UnfinishedRobot>().TakeDamage(bulletDamage);
                    }
                    hit.collider.GetComponent<RobotCollisionBox>().BreakOff();
                }
                else
                {
                    hit.collider.GetComponent<RobotAI>().TakeDamage(bulletDamage);
                }

                Debug.Log("Hit Robot 2");

                ProjectileHit(hit.collider.gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!gravity)
        {
            if (collision.collider.GetComponent<VentOpening>() && !collision.collider.GetComponent<VentOpening>().isOpened)
            {
                Debug.Log("Hit Vent");

                collision.collider.GetComponent<VentOpening>().Open(gameObject);

                ProjectileHit(collision.gameObject);
            }
            else if (collision.collider.GetComponent<RobotCollisionBox>() && pierceCount > 0)
            {
                if (collision.collider.GetComponent<RobotCollisionBox>().robotParent.GetComponent<RobotAI>())
                {
                    collision.collider.GetComponent<RobotCollisionBox>().robotParent.GetComponent<RobotAI>().TakeDamage(bulletDamage);
                }
                else if (collision.collider.GetComponent<RobotCollisionBox>().robotParent.GetComponent<UnfinishedRobot>())
                {
                    collision.collider.GetComponent<RobotCollisionBox>().robotParent.GetComponent<UnfinishedRobot>().TakeDamage(bulletDamage);
                }
                collision.collider.GetComponent<RobotCollisionBox>().BreakOff();

                Debug.Log("Hit Robot 2");

                ProjectileHit(collision.collider.gameObject);
            }
            else if (collision.collider.GetComponent<PlayerHealth>())
            {
                collision.collider.GetComponent<PlayerHealth>().TakeDamage(bulletDamage);

                ProjectileHit(collision.gameObject);
            }
            else if (!gravity)
            {
                Debug.Log("Hit Collider");

                gravity = true;

                gameObject.GetComponent<Rigidbody>().useGravity = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!gravity)
        {
            if (other.GetComponent<RobotCollisionBox>() && pierceCount > 0)
            {
                if (other.GetComponent<RobotCollisionBox>().robotParent.GetComponent<RobotAI>())
                {
                    other.GetComponent<RobotCollisionBox>().robotParent.GetComponent<RobotAI>().TakeDamage(bulletDamage);
                }
                else if (other.GetComponent<RobotCollisionBox>().robotParent.GetComponent<UnfinishedRobot>())
                {
                    other.GetComponent<RobotCollisionBox>().robotParent.GetComponent<UnfinishedRobot>().TakeDamage(bulletDamage);
                }
                other.GetComponent<RobotCollisionBox>().BreakOff();

                Debug.Log("Hit Robot 2");

                ProjectileHit(other.gameObject);
            }
        }
    }

    void ProjectileHit(GameObject objectHit)
    {
        pierceCount--;

        if (objectHit.GetComponent<Rigidbody>())
        {
            objectHit.GetComponent<Rigidbody>().AddForceAtPosition(Vector3.forward * bulletForce, transform.position);
        }

        if (destroyFx)
        {
            GameObject fx = Instantiate(destroyFx, transform.position, transform.rotation);
            Destroy(fx, 1);
        }

        if (pierceCount <= 0)
        {
            Destroy(gameObject);
        }
    }
}

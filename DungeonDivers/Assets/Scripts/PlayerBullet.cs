using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float bulletSpeed = 7.5f;
    public Rigidbody2D theRigidBody;

    public GameObject bulletImpact;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        theRigidBody.velocity = transform.right * bulletSpeed;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Instantiate(bulletImpact, transform.position, transform.rotation);
        Destroy(gameObject);
    }
    private void OnBecameInvisible()
    {

        Destroy(gameObject);
    }
}

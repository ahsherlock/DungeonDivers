using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float bulletSpeed = 7.5f;
    public Rigidbody2D theRigidBody;

    public GameObject bulletImpact;
    public int enemyDamagedSound;
    public int bulletImpactSound;

    public GameObject enemyBulletImpact;

    public int bulletDamage = 50;
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


        if (other.tag == "Enemy")
        {
            Instantiate(enemyBulletImpact, transform.position, transform.rotation);
            other.GetComponent<EnemyController>().DamageEnemy(50);
            AudioManager.instance.PlaySFX(enemyDamagedSound);
            Destroy(gameObject);
        }
        else
        {
            Instantiate(bulletImpact, transform.position, transform.rotation);
            AudioManager.instance.PlaySFX(bulletImpactSound);
            Destroy(gameObject);
        }


    }
    private void OnBecameInvisible()
    {

        Destroy(gameObject);
    }
}



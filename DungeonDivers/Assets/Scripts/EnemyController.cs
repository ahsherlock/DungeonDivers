using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D theRigidBody2D;
    public float moveSpeed;

    public float rangeToChasePlayer;

    private Vector3 moveDirection;

    public Animator theAnimator;

    public GameObject[] deathSplats;

    public int health = 150;
    public bool isAShootah;

    public GameObject bullet;

    public Transform firePoint;

    public float fireRate;

    private float fireCounter;

    public SpriteRenderer theBody;

    public float shootingRange;

    public int enemyShootingSound, enemyDeathSound;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (theBody.isVisible && PlayerController.instance.gameObject.activeInHierarchy)
        {
            if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < rangeToChasePlayer)
            {
                moveDirection = PlayerController.instance.transform.position - transform.position;
            }
            else
            {
                moveDirection = Vector3.zero;
            }

            moveDirection.Normalize();
            if (theRigidBody2D != null)
            {
                theRigidBody2D.velocity = moveDirection * moveSpeed;
            }


            if (moveDirection != Vector3.zero)
            {
                theAnimator.SetBool("isMoving", true);
            }
            else
            {
                theAnimator.SetBool("isMoving", false);
            }
            // If character is a shootah and within range then shoot at em
            if (isAShootah && Vector3.Distance(transform.position, PlayerController.instance.transform.position) < shootingRange)
            {
                fireCounter -= Time.deltaTime;
                if (fireCounter <= 0)
                {
                    fireCounter = fireRate;
                    Instantiate(bullet, firePoint.position, firePoint.rotation);
                    AudioManager.instance.PlaySFX(enemyShootingSound);
                }
            }
        }
        else
        {
            if (theRigidBody2D != null)
            {
                theRigidBody2D.velocity = Vector2.zero;
            }

        }


    }
    public void DamageEnemy(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
            int selectedSplatter = Random.Range(0, deathSplats.Length);
            int rotation = Random.Range(0, 4);
            Instantiate(deathSplats[selectedSplatter], transform.position, Quaternion.Euler(0f, 0f, rotation * 90f));
            AudioManager.instance.PlaySFX(enemyDeathSound);
            // Instantiate(deathSplat, transform.position, transform.rotation);
        }
    }

}

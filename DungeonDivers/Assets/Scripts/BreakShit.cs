using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakShit : MonoBehaviour
{

    public GameObject[] brokenPieces;
    public int breakSound;
    public int maxPieces = 6;

    public bool doesItDropShit;
    public GameObject[] possibleDrops;

    public float itemDropPercentage;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Smash()
    {
        Destroy(gameObject);
        AudioManager.instance.PlaySFX(breakSound);
        int piecesToDrop = Random.Range(2, maxPieces);
        for (int i = 0; i <= piecesToDrop; i++)
        {
            int randomPiece = Random.Range(0, brokenPieces.Length);
            Instantiate(brokenPieces[randomPiece], transform.position, transform.rotation);
        }

        if (doesItDropShit)
        {
            float dropChance = Random.Range(0f, 100f);
            if (dropChance < itemDropPercentage)
            {
                int randomItem = Random.Range(0, possibleDrops.Length);
                Instantiate(possibleDrops[randomItem], transform.position, transform.rotation);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" || other.tag == "PlayerBullet")
        {

            Smash();

        }
    }
}


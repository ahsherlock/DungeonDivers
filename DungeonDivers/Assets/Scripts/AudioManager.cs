using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource levelMusic, gameOverMusic, gameWonMusic;
    public AudioSource[] SFX;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void PlayGameOver()
    {
        levelMusic.Stop();
        gameOverMusic.Play();
    }
    public void PlayGameWon()
    {
        levelMusic.Stop();
        gameWonMusic.Play();
    }
    public void PlaySFX(int sfx)
    {
        SFX[sfx].Stop();
        SFX[sfx].Play();
    }

}

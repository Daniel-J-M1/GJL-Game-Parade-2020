using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource MusicBase;
    public AudioSource BossMusic;

    bool BossStart = false;

    // Start is called before the first frame update
    void Start()
    {
        BaseMusic();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BaseMusic()
    {
        MusicBase.Play();

        if (BossStart == true)
        {
            BossMusic.Play();
            BossStart = false;
        }


    }

    public void PlayBoss()
    {
        MusicBase.Play();
        BossMusic.Play();
        BossStart = true;
    }
}

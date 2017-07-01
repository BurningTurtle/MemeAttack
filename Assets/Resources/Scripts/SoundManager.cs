using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    [SerializeField] private AudioSource soundSource;
    [SerializeField] private AudioSource musicSource1;
    [SerializeField] private AudioSource musicSource2;

    // Sounds
    [SerializeField] private AudioClip seitenbacherSound;
    [SerializeField] private AudioClip nikeVansSound;
    [SerializeField] private AudioClip[] softIceSounds;
    [SerializeField] private AudioClip dolanDropSound;
    [SerializeField] private AudioClip doritosSound;
    [SerializeField] private AudioClip mountainDewSound;
    [SerializeField] private AudioClip timeToStopSound;
    [SerializeField] private AudioClip placeHolderSound;
    [SerializeField] private AudioClip kleinesYenSound;
    [SerializeField] private AudioClip cottonDamageSound;

    // Music
    [SerializeField] private AudioClip zeldaMusic, tunnelMusic;

    [SerializeField] private AudioClip[] sounds;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void playTunnel()
    {
        soundSource.volume = 0.1f;
        StartCoroutine(fadeOut1());
        musicSource2.PlayOneShot(tunnelMusic, 1);
        musicSource2.loop = true;
        StartCoroutine(fadeIn2());
    }

    public void playZelda()
    {
        StartCoroutine(fadeOut1());
        musicSource2.PlayOneShot(zeldaMusic, 1);
        StartCoroutine(fadeIn2());
        StartCoroutine(waitForMain(218));
    }

    private IEnumerator fadeOut1()
    {
        float startVolume = musicSource1.volume;

        while (musicSource1.volume > 0)
        {
            musicSource1.volume -= startVolume * Time.deltaTime / 1.5f; // 2.5s

            yield return null;
        }
    }

    private IEnumerator fadeOut2()
    {
        float startVolume = musicSource2.volume;

        while (musicSource2.volume > 0)
        {
            musicSource2.volume -= startVolume * Time.deltaTime / 1.5f; // 2.5s

            yield return null;
        }
    }

    private IEnumerator fadeIn1()
    {
        while (musicSource1.volume < 1)
        {
            musicSource1.volume += 1 * Time.deltaTime / 2.5f; // 2.5s

            yield return null;
        }
    }

    private IEnumerator fadeIn2()
    {
        while (musicSource2.volume < 1)
        {
            musicSource2.volume += 1 * Time.deltaTime / 2.5f; // 2.5s

            yield return null;
        }
    }

    IEnumerator waitForMain(int s)
    {
        yield return new WaitForSeconds(s);
        StartCoroutine(fadeOut2());
        StartCoroutine(fadeIn1());
    }

    public void playSeitenbacher()
    {
        soundSource.PlayOneShot(seitenbacherSound, 1);
    }

    public void playNikeVans()
    {
        soundSource.PlayOneShot(nikeVansSound, 1);
    }

    public void playSoftIce()
    {
        int i = Random.Range(0, 6);

        soundSource.PlayOneShot(softIceSounds[i], 1);
    }

    public void playDolanDrop()
    {
        soundSource.PlayOneShot(dolanDropSound, 1);
    }

    public void playDoritos()
    {
        soundSource.PlayOneShot(doritosSound, 1);
    }

    public void playMountainDew()
    {
        soundSource.PlayOneShot(mountainDewSound, 1);
    }

    public void playPlaceholder()
    {
        soundSource.PlayOneShot(placeHolderSound, 1);
    }

    public void playItsTimeToStop()
    {
        soundSource.PlayOneShot(timeToStopSound, 1);
    }

    public void playKleinesYen()
    {
        soundSource.PlayOneShot(kleinesYenSound, 1);
    }

    public void playCottonDamage()
    {
        soundSource.PlayOneShot(cottonDamageSound, 1);
    }

    public void playAudioClip(string clipToPlay)
    {
        foreach (AudioClip clip in sounds)
        {
            if(clip.name == clipToPlay)
            {
                // Some sounds are too silent, so their volumes have to be adjusted. 
                if (clip.name == "DogeWoof")
                {
                    soundSource.PlayOneShot(clip, 15);
                }
                else if(clip.name == "CryForHelp")
                {
                    soundSource.PlayOneShot(clip, 15);
                }
                else if(clip.name == "Feather")
                {
                    soundSource.PlayOneShot(clip, 7);
                }
                else
                {
                    soundSource.PlayOneShot(clip, 5);
                }
            }
        }
    }
}

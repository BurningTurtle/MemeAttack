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

    // Music
    [SerializeField] private AudioClip zeldaMusic;


    private void Start()
    {
        
    }

    public void playZelda()
    {
        StartCoroutine(fadeOut1());
        musicSource2.PlayOneShot(zeldaMusic, 1);
        StartCoroutine(fadeIn2());
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

    private IEnumerator fadeIn2()
    {
        while (musicSource2.volume < 1)
        {
            musicSource2.volume += 1 * Time.deltaTime / 2.5f; // 2.5s

            yield return null;
        }
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
}

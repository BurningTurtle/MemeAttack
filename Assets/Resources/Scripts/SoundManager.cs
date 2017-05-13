using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    private AudioSource audioSource;

    // Sounds
    [SerializeField] private AudioClip seitenbacherSound;
    [SerializeField] private AudioClip nikeVansSound;
    [SerializeField] private AudioClip[] softIceSounds;
    [SerializeField] private AudioClip dolanDropSound;
    [SerializeField] private AudioClip doritosSound;
    [SerializeField] private AudioClip mountainDewSound;
    [SerializeField] private AudioClip timeToStopSound;
    [SerializeField] private AudioClip placeHolderSound;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void playSeitenbacher()
    {
        audioSource.PlayOneShot(seitenbacherSound, 1);
    }

    public void playNikeVans()
    {
        audioSource.PlayOneShot(nikeVansSound, 1);
    }

    public void playSoftIce()
    {
        int i = Random.Range(0, 6);

        audioSource.PlayOneShot(softIceSounds[i], 1);
    }

    public void playDolanDrop()
    {
        audioSource.PlayOneShot(dolanDropSound, 1);
    }

    public void playDoritos()
    {
        audioSource.PlayOneShot(doritosSound, 1);
    }

    public void playMountainDew()
    {
        audioSource.PlayOneShot(mountainDewSound, 1);
    }

    public void playPlaceholder()
    {
        audioSource.PlayOneShot(placeHolderSound, 1);
    }

    public void playItsTimeToStop()
    {
        audioSource.PlayOneShot(timeToStopSound, 1);
    }
}

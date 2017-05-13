using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    private AudioSource audioSource;

    // Sounds
    [SerializeField] private AudioClip seitenbacherSound;
    [SerializeField] private AudioClip nikeVansSound;
    [SerializeField] private AudioClip softIceSound1, softIceSound2, softIceSound3, softIceSound4, softIceSound5, softIceSound6, softIceSound7;
    [SerializeField] private AudioClip dolanDropSound;
    [SerializeField] private AudioClip doritosSound;
    [SerializeField] private AudioClip mountainDewSound;
    [SerializeField] private AudioClip timeToStopSound;

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
        int i = Random.Range(1, 7);

        switch (i)
        {
            case 0:
                audioSource.PlayOneShot(softIceSound1, 1);
                break;

            case 1:
                audioSource.PlayOneShot(softIceSound2, 1);
                break;

            case 2:
                audioSource.PlayOneShot(softIceSound3, 1);
                break;

            case 3:
                audioSource.PlayOneShot(softIceSound4, 1);
                break;

            case 4:
                audioSource.PlayOneShot(softIceSound5, 1);
                break;

            case 5:
                audioSource.PlayOneShot(softIceSound6, 1);
                break;

            case 6:
                audioSource.PlayOneShot(softIceSound7, 1);
                break;
                
        }
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

    public void playTimeToStop()
    {
        audioSource.PlayOneShot(timeToStopSound, 1);
    }
}

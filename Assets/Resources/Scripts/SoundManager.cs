using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    private AudioSource audioSource;

    // Sounds
    [SerializeField] private AudioClip seitenbacherSound;
    [SerializeField] private AudioClip nikeVansSound;
    [SerializeField] private AudioClip softIceSound;

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
        audioSource.PlayOneShot(softIceSound, 1);
    }
}

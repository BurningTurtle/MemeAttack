using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountainDew : MonoBehaviour {

    private GameObject player;
    private UIController uic;
    private SoundManager soundMan;
    private SpriteRenderer sr;
    private bool pickedUp = false;

    void Start()
    {
        player = GameObject.Find("Player");
        uic = FindObjectOfType<UIController>();
        soundMan = FindObjectOfType<SoundManager>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player && uic.passiveItems < 3)
        {
            // Make Mountain Dew invisible.
            Color colour = sr.material.color;
            colour.a = 0;
            sr.color = colour;

            if (!pickedUp)
            {
                uic.GetComponent<UIController>().mountainDew();
                soundMan.playMountainDew();

                // Gives speedup + half a heart regeneration every second for five seconds.
                StartCoroutine(theDew());

                pickedUp = true;
            }
        }
    }

    IEnumerator theDew()
    {
        player.GetComponent<Player>().speed += 1;
        for(int i = 0; i < 5; i++)
        {
            player.GetComponent<Player>().health += 5;
            yield return new WaitForSeconds(1);
        }
        player.GetComponent<Player>().speed -= 1;
    }
}

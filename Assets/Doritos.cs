using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doritos : MonoBehaviour {

    private GameObject player;
    private UIController uic;
    private SpriteRenderer sr;
    private SoundManager soundMan;
    private bool pickedUp = false;

    private void Start()
    {
        player = GameObject.Find("Player");
        uic = FindObjectOfType<UIController>();
        sr = GetComponent<SpriteRenderer>();
        soundMan = FindObjectOfType<SoundManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == player && uic.passiveItems < 3)
        {
            // Make Dorito bag invisible.
            Color colour = sr.material.color;
            colour.a = 0;
            sr.color = colour;

            if (!pickedUp)
            {
                uic.GetComponent<UIController>().doritos();
                soundMan.playDoritos();

                // Gives him half a heart a second for five seconds.
                StartCoroutine(regeneration());

                pickedUp = true;
            }
        }
    }

    IEnumerator regeneration()
    {
        for(int i = 0; i < 5; i++)
        {
            player.GetComponent<Player>().health += 5;
            yield return new WaitForSeconds(1f);
        }
        Destroy(this.gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seitenbacher : MonoBehaviour {

    private GameObject player;
    private GameObject uic;
    private SpriteRenderer sr;
    private SoundManager soundMan;
    private bool pickedUp = false;

    void Start()
    {
        player = GameObject.Find("Player");
        uic = GameObject.Find("UIController");
        sr = GetComponent<SpriteRenderer>();
        soundMan = FindObjectOfType<SoundManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Only do this if Seitenbacher was hit by player. Otherwise, a Projectile for example could trigger this.
        if (collision.gameObject == player)
        {
            // Make Nike Vans invisible
            Color colour = sr.material.color;
            colour.a = 0;
            sr.material.color = colour;

            if (!pickedUp)
            {
                soundMan.playSeitenbacher();

                // Temporarily increase player's damage by 0.5.
                StartCoroutine(temporaryDmgUp());

                // 
                uic.GetComponent<UIController>().seitenbacher();

                pickedUp = true;
            }
        }
    }

    IEnumerator temporaryDmgUp()
    {
        player.GetComponent<Player>().damage += 1;
        yield return new WaitForSeconds(10f);
        player.GetComponent<Player>().damage -= 1;
        Destroy(gameObject);
    }
}

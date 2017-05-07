using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NikeVans : MonoBehaviour {

    private GameObject player;
    private GameObject uic;
    private SpriteRenderer sr;
    private bool pickedUp = false;
    private SoundManager soundMan;

    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player");
        uic = GameObject.Find("UIController");
        sr = GetComponent<SpriteRenderer>();
        soundMan = FindObjectOfType<SoundManager>();

	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player && uic.GetComponent<UIController>().passiveItems < 3)
        {
            // Make Nike Vans invisible
            Color colour = sr.material.color;
            colour.a = 0;
            sr.material.color = colour;

            if (!pickedUp)
            {
                soundMan.playNikeVans();

                // Temporarily increase player's damage by 0.5.
                StartCoroutine(temporarySpeedUp());

                // 
                uic.GetComponent<UIController>().nikeVans();

                pickedUp = true;
            }
        }
    }

    IEnumerator temporarySpeedUp()
    {
        player.GetComponent<Player>().speed += 2f;
        yield return new WaitForSeconds(10f);
        player.GetComponent<Player>().speed -= 2f;
        Destroy(gameObject);
    }
}

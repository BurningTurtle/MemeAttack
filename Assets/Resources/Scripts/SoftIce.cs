using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoftIce : MonoBehaviour {

    private GameObject player;
    private SoundManager soundMan;
    private SpriteRenderer sr;
    private bool pickedUp = false;
    private GameObject uic;

    void Start()
    {
        player = GameObject.Find("Player");
        soundMan = FindObjectOfType<SoundManager>();
        sr = GetComponent<SpriteRenderer>();
        uic = GameObject.Find("UIController");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == player && uic.GetComponent<UIController>().passiveItems < 3)
        {
            Color colour = sr.material.color;
            colour.a = 0;
            sr.material.color = colour;
            if (!pickedUp)
            {
                soundMan.playSoftIce();

                // Activate cold shock
                StartCoroutine(coldShock());

                // Show softice in the UI
                uic.GetComponent<UIController>().softIce();

                pickedUp = true;
            }
        }
    }

    IEnumerator coldShock()
    {
        player.GetComponent<Player>().speed += 3f;
        for (int i = 0; i < 10; i++)
        {
            player.GetComponent<Player>().health += 5;
            yield return new WaitForSeconds(1f);
        }
        player.GetComponent<Player>().speed -= 3f;
        Destroy(this.gameObject);
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoftIce : MonoBehaviour {

    private GameObject player;
    private SoundManager soundMan;

    void Start()
    {
        player = GameObject.Find("Player");
        soundMan = FindObjectOfType<SoundManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == player && player.GetComponent<Player>().health <= 95)
        {
            soundMan.playSoftIce();
            player.GetComponent<Player>().health += 10;
            Destroy(this.gameObject);
        }
    }
}

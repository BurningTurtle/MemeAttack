using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PileOfCotton : MonoBehaviour {

    private GameObject player;
    private int damage = 20;
    private SoundManager soundMan;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
        soundMan = FindObjectOfType<SoundManager>();
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            if (other.GetComponent<Player>().readyForDamage)
            {
                other.GetComponent<Player>().health -= damage;
                other.GetComponent<Player>().GetReadyForDamage();
                soundMan.playCottonDamage();
            }
            Destroy(gameObject);
        }
    }
}

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
            other.GetComponent<Player>().health -= damage;
            soundMan.playCottonDamage();
            Destroy(gameObject);
        }
    }
}

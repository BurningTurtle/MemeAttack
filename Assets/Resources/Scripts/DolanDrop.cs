using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DolanDrop : MonoBehaviour {

    private GameObject player;
    private SoundManager soundMan;

    // Use this for initialization
    void Start ()
    {
        player = GameObject.Find("Player");
        soundMan = FindObjectOfType<SoundManager>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            soundMan.playDolanDrop();
            player.GetComponent<Player>().health += 25;
            Destroy(gameObject);
        }
    }
}

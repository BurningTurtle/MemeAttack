using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DolanDrop : MonoBehaviour {

    private GameObject player;

    // Use this for initialization
    void Start ()
    {
        player = GameObject.Find("Player");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            player.GetComponent<Player>().health += 25;
            Destroy(gameObject);
        }
    }
}

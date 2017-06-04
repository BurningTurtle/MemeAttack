using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour {

    private GameObject player;
    [SerializeField]
    private GameObject otherLock1, otherLock2;


    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject == player && player.GetComponent<Player>().hasKey == true)
        {
            Destroy(gameObject);
            Destroy(otherLock1);
            Destroy(otherLock2);
            player.GetComponent<Player>().hasKey = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour {

    private GameObject player;
    [SerializeField]
    private GameObject otherLock1, otherLock2;
    private SoundManager soundMan;


    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player");
        soundMan = GameObject.FindObjectOfType<SoundManager>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject == player && player.GetComponent<Player>().hasKey == true)
        {
            soundMan.playAudioClip("Key");
            Destroy(gameObject);
            Destroy(otherLock1);
            Destroy(otherLock2);
            player.GetComponent<Player>().hasKey = false;
        }
    }
}

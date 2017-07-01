using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalTrigger : MonoBehaviour {

    GameObject player;
    private bool triggered = false;
    public bool canTrigger;
    private SoundManager soundMan;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
        canTrigger = false;
        soundMan = FindObjectOfType<SoundManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && player.GetComponent<Player>().hasKey == false && !triggered && this.tag != "alternateTrigger")
        {
            triggered = true;
            player.GetComponent<Player>().finalDialogue();
        }
        else if(this.tag == "alternateTrigger" && !triggered && canTrigger)
        {
            triggered = true;
            player.GetComponent<Player>().triedToCollectKey = true;
        }
        else if(this.tag != "alternateTrigger" && player.GetComponent<Player>().openedPortalRoom)
        {
            player.transform.position = new Vector2(13, 342);
            soundMan.playAudioClip("Teleport");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalTrigger : MonoBehaviour {

    GameObject player;
    [SerializeField] GameObject keyPrefab;
    private GameObject key;
    private bool triggered = false;
    public bool canTrigger;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
        canTrigger = false;
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
    }
}

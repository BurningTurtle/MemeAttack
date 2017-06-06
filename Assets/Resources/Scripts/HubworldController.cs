using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubworldController : MonoBehaviour {

    public string area;
    [SerializeField]
    private GameObject parentController;
    [SerializeField]
    private GameObject HUD;
    [SerializeField]
    private GameObject special1HUD;

    private Bubble bubble;

	// Use this for initialization
	void Start () {
        HUD.SetActive(false);
        special1HUD.SetActive(false);
        bubble = FindObjectOfType<Bubble>();
    }
	
	// Update is called once per frame
	void Update () {
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.name == "Player")
        {   
            switch (this.tag)
            {
                case "arena1Trigger":
                    parentController.GetComponent<HubworldController>().area = "arena1";
                    HUD.SetActive(true);
                    bubble.playerInArena = true;
                    bubble.fadein();
                    Debug.Log(area);
                    break;
                case "hubworldTrigger":
                    parentController.GetComponent<HubworldController>().area = "hubworld";
                    HUD.SetActive(false);
                    bubble.playerInArena = false;
                    bubble.fadeout();
                    Debug.Log(area);
                    break;
                case "special1Trigger":
                    parentController.GetComponent<HubworldController>().area = "special1";
                    HUD.SetActive(false);
                    special1HUD.SetActive(true);
                    bubble.playerInArena = true;
                    bubble.fadein();
                    break;
            }
        }
    }
}

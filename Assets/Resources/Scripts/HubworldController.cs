using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubworldController : MonoBehaviour {

    public string area;
    [SerializeField]
    private GameObject parentController;
    [SerializeField]
    GameObject HUD;

	// Use this for initialization
	void Start () {
        HUD.SetActive(false);
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
                    Debug.Log(area);
                    break;
                case "hubworldTrigger":
                    parentController.GetComponent<HubworldController>().area = "hubworld";
                    HUD.SetActive(false);
                    Debug.Log(area);
                    break;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubworldController : MonoBehaviour
{

    public string area;
    [SerializeField]
    private GameObject parentController;
    [SerializeField]
    private GameObject HUD;
    [SerializeField]
    private GameObject special1HUD;

    //public string currentArea;

    private Bubble bubble = null;

    // Use this for initialization
    void Start()
    {
        HUD.SetActive(false);
        special1HUD.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.name == "Player")
        {
            switch (this.tag)
            {
                case "arena1Trigger":
                    if (parentController.GetComponent<HubworldController>().area != "arena1")
                    {
                        parentController.GetComponent<HubworldController>().area = "arena1";
                        HUD.SetActive(true);
                        if (bubble != null)
                        {
                            bubble.playerInArena = true;
                            bubble.fadein();
                        }
                        Debug.Log(area);
                    }
                    break;
                case "hubworldTrigger":
                    if (parentController.GetComponent<HubworldController>().area != "hubworld")
                    {
                        parentController.GetComponent<HubworldController>().area = "hubworld";
                        HUD.SetActive(false);
                        if (bubble != null)
                        {
                            bubble.playerInArena = false;
                            bubble.fadeout();
                        }
                        Debug.Log(area);
                    }
                    break;
                case "special1Trigger":
                    if (parentController.GetComponent<HubworldController>().area != "special1")
                    {
                        parentController.GetComponent<HubworldController>().area = "special1";
                        HUD.SetActive(false);
                        if (bubble != null)
                        {
                            bubble.playerInArena = true;
                            bubble.fadein();
                        }
                    }
                    break;
                case "arena2Trigger":
                    if (parentController.GetComponent<HubworldController>().area != "arena2")
                    {
                        parentController.GetComponent<HubworldController>().area = "arena2";
                        HUD.SetActive(true);
                        if (bubble != null)
                        {
                            bubble.playerInArena = true;
                            bubble.fadein();
                        }
                    }
                    break;
            }
        }
    }

    public void getBubble()
    {
        bubble = FindObjectOfType<Bubble>();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubworldController : MonoBehaviour
{

    public string area;
    [SerializeField]
    private GameObject parentController;
    [SerializeField]
    private GameObject HUD, itemslots, persistentCanvas;
    [SerializeField]
    private GameObject special1HUD;

    private Bubble bubble = null;

    private ArenaController arena1Ctrl;
    private Arena2Controller arena2Ctrl;
    private Special1Controller special1Ctrl;
    private Arena3Controller arena3Ctrl;
    private TunnelController tunnelCtrl;

    // Changed in respective Arena Scripts
    public bool resetting = false;

    // Use this for initialization
    void Start()
    {
        HUD.SetActive(false);
        special1HUD.SetActive(false);
        arena1Ctrl = FindObjectOfType<ArenaController>();
        arena2Ctrl = FindObjectOfType<Arena2Controller>();
        arena3Ctrl = FindObjectOfType<Arena3Controller>();
        special1Ctrl = FindObjectOfType<Special1Controller>();
        tunnelCtrl = FindObjectOfType<TunnelController>();
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
                        if (!resetting)
                        {
                            // Prevent bugging out of closed Arena (but only if Player didn't just die)
                            if (arena1Ctrl.GetComponent<ArenaController>().cantEscapeActivated)
                            {
                                other.transform.position = new Vector2(12.5f, 2);
                                break;
                            }
                            if (special1Ctrl.GetComponent<Special1Controller>().cantEscapeActivated)
                            {
                                other.transform.position = new Vector2(34, 12.5f);
                                break;
                            }
                            if (arena2Ctrl.GetComponent<Arena2Controller>().cantEscapeActivated)
                            {
                                other.transform.position = new Vector2(24, 50);
                                break;
                            }
                            if (arena3Ctrl.GetComponent<Arena3Controller>().cantEscapeActivated)
                            {
                                other.transform.position = new Vector2(1, 90);
                                break;
                            }
                            if (tunnelCtrl.GetComponent<TunnelController>().cantEscapeActivated)
                            {
                                other.transform.position = new Vector2(0, 0);
                                break;
                            }
                        }

                        parentController.GetComponent<HubworldController>().area = "hubworld";
                        HUD.SetActive(false);
                        special1HUD.SetActive(false);
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
                case "special2Trigger":
                    if(parentController.GetComponent<HubworldController>().area != "special2")
                    {
                        parentController.GetComponent<HubworldController>().area = "special2";
                        HUD.SetActive(false);
                        if (bubble != null)
                        {
                            bubble.playerInArena = true;
                            bubble.fadein();
                        }
                    }
                    break;
                case "arena3Trigger":
                    if (parentController.GetComponent<HubworldController>().area != "arena3")
                    {
                        parentController.GetComponent<HubworldController>().area = "arena3";
                        HUD.SetActive(true);
                        if (bubble != null)
                        {
                            bubble.playerInArena = true;
                            bubble.fadein();
                        }
                    }
                    break;
                case "tunnelTrigger":
                    if (parentController.GetComponent<HubworldController>().area != "tunnel")
                    {
                        parentController.GetComponent<HubworldController>().area = "tunnel";
                        itemslots.SetActive(false);
                        persistentCanvas.SetActive(false);
                        HUD.SetActive(true);
                        if (bubble != null)
                        {
                            bubble.playerInArena = true;
                            bubble.fadein();
                        }
                    }
                    break;
                case "finalRoomTrigger":
                    if (parentController.GetComponent<HubworldController>().area != "finalRoom")
                    {
                        parentController.GetComponent<HubworldController>().area = "finalRoom";
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

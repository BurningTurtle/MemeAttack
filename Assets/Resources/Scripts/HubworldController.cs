﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.PostProcessing;
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
    private VillainArenaController villainCtrl;

    // Changed in respective Arena Scripts
    public bool resetting = false;
    public string deathArea;

    // So that the Player doesn't get teleported into the Arena while it's still closed and Player hits Hubworld Trigger
    public bool mentorIntroductionFinished = false;

    [SerializeField]
    private PostProcessingProfile grayscale;

    private GameObject player, villain;

    [SerializeField]
    private SoundManager soundMan;

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
        villainCtrl = FindObjectOfType<VillainArenaController>();
        player = GameObject.Find("Player");
        villain = GameObject.Find("Villain");
    }

    // Update is called once per frame
    void Update()
    {
        if(area == "tunnel")
        {
            float progress = player.transform.position.y - 107;
            float saturation = 1 - progress / 205;
            var satSetting = grayscale.colorGrading.settings;
            satSetting.basic.saturation = saturation;
            grayscale.colorGrading.settings = satSetting;
            Debug.Log(saturation);
        }
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
                    }
                    break;
                case "hubworldTrigger":
                    if (parentController.GetComponent<HubworldController>().area != "hubworld")
                    {
                        if (!GameObject.Find("Player").GetComponent<Player>().dead && mentorIntroductionFinished)
                        {
                            // Prevent bugging out of closed Arena (but only if Player didn't just die)
                            if (arena1Ctrl.GetComponent<ArenaController>().cantEscapeActivated)
                            {
                                other.transform.position = new Vector2(12.5f, 2);
                                resetting = false;
                                break;
                            }
                            if (special1Ctrl.GetComponent<Special1Controller>().cantEscapeActivated)
                            {
                                other.transform.position = new Vector2(34, 12.5f);
                                resetting = false;
                                break;
                            }
                            if (arena2Ctrl.GetComponent<Arena2Controller>().cantEscapeActivated)
                            {
                                other.transform.position = new Vector2(24, 50);
                                resetting = false;
                                break;
                            }
                            if (arena3Ctrl.GetComponent<Arena3Controller>().cantEscapeActivated)
                            {
                                other.transform.position = new Vector2(1, 90);
                                resetting = false;
                                break;
                            }
                        }
                        else
                        {
                            parentController.GetComponent<HubworldController>().area = "hubworld";
                            HUD.SetActive(false);
                            special1HUD.SetActive(false);
                            if (bubble != null)
                            {
                                bubble.playerInArena = false;
                                bubble.fadeout();
                            }
                        }
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
                        if (tunnelCtrl.GetComponent<TunnelController>().cantEscapeActivated)
                        {
                            other.transform.position = new Vector2(12, 107.5f);
                            resetting = false;
                            break;
                        }
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
                        soundMan.playTunnel();
                        if (villainCtrl.GetComponent<VillainArenaController>().cantEscapeActivated)
                        {
                            other.transform.position = new Vector2(12, 315);
                            resetting = false;
                            break;
                        }
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
                        villain.GetComponent<Villain>().startTalking();
                        parentController.GetComponent<HubworldController>().area = "finalRoom";
                        if (bubble != null)
                        {
                            bubble.playerInArena = true;
                            bubble.fadein();
                        }
                        foreach (GameObject mE in arena1Ctrl.GetComponent<ArenaController>().mainEnemiesInScene)
                        {
                            Destroy(mE.gameObject);
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

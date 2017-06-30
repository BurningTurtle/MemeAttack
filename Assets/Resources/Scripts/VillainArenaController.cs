using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillainArenaController : MonoBehaviour {

    public GameObject UIcontroller;
    [SerializeField]
    GameObject hubworldController;
    [SerializeField]
    private GameObject cantEscape, HUD;
    private GameObject arena1Controller;
    [SerializeField] GameObject villainPrefab;

    public bool cantEscapeActivated = false;


    // Use this for initialization.

    void Start()

    {
        arena1Controller = GameObject.Find("ArenaController");
        cantEscape.SetActive(false);
        hubworldController = GameObject.Find("HubworldController");
    }



    // Update is called once per frame

    void Update()
    {
        if (hubworldController.GetComponent<HubworldController>().area == "finalRoom")
        {
            if (!cantEscape.activeSelf)
            {
                StartCoroutine(activateCantEscapeCoroutine());
            }
        }
    }

    IEnumerator activateCantEscapeCoroutine()
    {
        cantEscapeActivated = true;
        yield return new WaitForSeconds(.5f);
        cantEscape.SetActive(true);
    }

    public void activateCantEscape()
    {
        StartCoroutine(activateCantEscapeCoroutine());
    }

    public void deactivateCantEscape()
    {
        cantEscapeActivated = false;
        cantEscape.SetActive(false);
    }

    public void diedInVillainArena()
    {
        hubworldController.GetComponent<HubworldController>().resetting = true;
        cantEscape.SetActive(false);
        cantEscapeActivated = false;

        GameObject[] playerProjectiles = GameObject.FindGameObjectsWithTag("PlayerProjectile");
        GameObject[] mainEnemyProjectiles = GameObject.FindGameObjectsWithTag("MainEnemyProjectile");
        GameObject[] enemyProjectiles = GameObject.FindGameObjectsWithTag("EnemyBullet");

        foreach (GameObject enemy in arena1Controller.GetComponent<ArenaController>().mainEnemiesInScene)
        {
            Destroy(enemy.gameObject);
        }
        foreach (GameObject enemy in arena1Controller.GetComponent<ArenaController>().dolansInScene)
        {
            Destroy(enemy.gameObject);
        }
        foreach (GameObject enemy in arena1Controller.GetComponent<ArenaController>().datBoisInScene)
        {
            Destroy(enemy.gameObject);
        }
        foreach (GameObject playerProjectile in playerProjectiles)
        {
            Destroy(playerProjectile.gameObject);
        }
        foreach (GameObject mainEP in mainEnemyProjectiles)
        {
            Destroy(mainEP.gameObject);
        }
        foreach (GameObject enemyP in enemyProjectiles)
        {
            Destroy(enemyP.gameObject);
        }
        foreach (GameObject money in arena1Controller.GetComponent<ArenaController>().moneyInScene)
        {
            Destroy(money.gameObject);
        }

        HUD.SetActive(false);
        GameObject[] items = GameObject.FindGameObjectsWithTag("item");
        foreach (GameObject item in items)
        {
            Destroy(item.gameObject);
        }

        GameObject turtle = GameObject.FindGameObjectWithTag("Turtle");
        if(turtle != null)
        {
            Destroy(turtle.gameObject);
        }

        GameObject villain = Instantiate(villainPrefab) as GameObject;
        villain.transform.position = new Vector2(12, 318);
    }
}

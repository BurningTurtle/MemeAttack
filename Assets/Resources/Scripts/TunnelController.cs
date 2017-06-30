using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelController : MonoBehaviour {

    public int gridRows;

    public int gridCols;

    public float offset = 1;

    public Grass grass1, grass2, grass3, grass4;


    // For not endlessly calling IEnumerator NewWave()

    private bool alreadyCalled = false;


    public GameObject UIcontroller;

    [SerializeField]
    GameObject hubworldController;

    [SerializeField]
    private GameObject cantEscape, HUD;

    private GameObject player;

    // Enemies' Prefabs  

    [SerializeField]
    private GameObject mainEnemyPrefab, dolanPrefab, datBoiPrefab, nyanCatPrefab, dogePrefab, trollfacePrefab;

    [SerializeField] private GameObject arena1Controller;

    public bool cantEscapeActivated = false;

    // Use this for initialization
    void Start () {
        cantEscape.SetActive(false);
        hubworldController = GameObject.Find("HubworldController");
        player = GameObject.Find("Player");

        Vector2 startPos = new Vector2(7.5f, 105);

        // Iterate through each row...

        for (float i = 1; i <= gridRows; i++)

        {

            // ...while adding columns.

            for (float j = 1; j <= gridCols; j++)

            {

                Grass grass = null;



                // A random grass tile gets chosen.

                int id = Random.Range(1, 4);

                switch (id)

                {

                    case 1:

                        grass = Instantiate(grass1) as Grass;

                        break;

                    case 2:

                        grass = Instantiate(grass2) as Grass;

                        break;

                    case 3:

                        grass = Instantiate(grass3) as Grass;

                        break;

                    case 4:

                        grass = Instantiate(grass4) as Grass;

                        break;

                }



                // Set the X and Y coordinate of the new tile to the start position + offset (length & width of one tile) * the number of iterations.

                float posX = (offset * i) + startPos.x;

                float posY = (offset * j) + startPos.y;

                grass.transform.position = new Vector2(posX, posY);

            }

        }
    }
	
	// Update is called once per frame
	void Update () {
        if (hubworldController.GetComponent<HubworldController>().area == "tunnel")
        {
            if (!cantEscape.activeSelf && !hubworldController.GetComponent<HubworldController>().resetting)
            {
                StartCoroutine(activateCantEscapeCoroutine());
                Debug.Log("starting cantactiveatasd");
            }

            if (!alreadyCalled)

            {
                // ... spawn the new wave.
                StartCoroutine(NewWave());
            }
        }
    }

    IEnumerator activateCantEscapeCoroutine()
    {
        cantEscapeActivated = true;
        yield return new WaitForSeconds(.5f);
        cantEscape.SetActive(true);
    }

    IEnumerator NewWave()
    {
        alreadyCalled = true;
        for (int i = 1; i <= 5; i++)
        {
            Instantiate(mainEnemyPrefab, new Vector2(player.transform.position.x, player.transform.position.y + 10), Quaternion.identity);
        }
        yield return new WaitForSeconds(2.5f);
        alreadyCalled = false;
    }

    public void diedInTunnel()
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
    }
}


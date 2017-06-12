using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Special1Controller : MonoBehaviour {

    public int gridRows;
    public int gridCols;
    public float offset = 1;
    public Grass grass1, grass2, grass3, grass4;

    [SerializeField]
    private GameObject hubworldController, cantEscape, exit;
    private GameObject player;

    [SerializeField]
    private GameObject mainEnemyPrefab, nyanCatPrefab, dogePrefab;

    private bool started = false;

    [SerializeField]
    private Text critText;

    public float crits;
    public float nonCrits;
    public float critRate;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player");
        cantEscape.SetActive(false);

        // Starting point for creating the arena.

        Vector2 startPos = new Vector2(31.77f, -0.5f);

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

                grass.transform.position = new Vector3(posX, posY, 100);

            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        if(hubworldController.GetComponent<HubworldController>().area == "special1")
        {
            if(!cantEscape.activeSelf && !started)
            {
                StartCoroutine(cantEscapeCoroutine());
            }

            if (player.GetComponent<Player>().isLink)
            {
                if (!started)
                {
                    StartCoroutine(bassEnemies());
                    started = true;
                }
                
            }
            if(crits+nonCrits != 0)
            {
                critRate = crits / (crits + nonCrits);
            }
            if (cantEscape.activeSelf)
            {
                critText.text = "CRIT RATE: " + critRate;
            }
            else
            {
                critText.text = "GAINED " + Mathf.RoundToInt(critRate * 2000) + " KLEINES YEN"; 
            }
            
        }
    }

    IEnumerator bassEnemies()
    {
        bool canSpawn = true;
        while((player.GetComponent<Player>().isLink || player.GetComponent<Player>().isDarkLink))
        {
            if (canSpawn)
            {
                canSpawn = false;

                Vector2 spawn1 = new Vector2(33.1f, 2);
                Vector2 spawn2 = new Vector2(33.1f, 46);

                for (int i = 1; i <= 10; i++)
                {
                    Instantiate(mainEnemyPrefab, new Vector2(spawn1.x + i, spawn1.y), Quaternion.identity);
                }

                for (int i = 1; i <= 10; i++)
                {
                    Instantiate(mainEnemyPrefab, new Vector2(spawn2.x + i, spawn2.y), Quaternion.identity);
                }

                yield return new WaitForSeconds(3);
                canSpawn = true;
            }
        }
    }

    IEnumerator cantEscapeCoroutine()
    {
        yield return new WaitForSeconds(.5f);
        cantEscape.SetActive(true);
    }

    public void canEscape()
    {
        cantEscape.SetActive(false);
    }

    public void activateExit()
    {
        GameObject[] mainEnemiesInScene = GameObject.FindGameObjectsWithTag("MainEnemy");
        foreach(GameObject enemy in mainEnemiesInScene)
        {
            Destroy(enemy.gameObject);
        }
        exit.SetActive(false);
    }
}

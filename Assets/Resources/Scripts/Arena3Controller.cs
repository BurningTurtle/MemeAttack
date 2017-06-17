using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arena3Controller : MonoBehaviour
{

    public int gridRows;

    public int gridCols;

    public float offset = 1;

    public Grass grass1, grass2, grass3, grass4;

    public int wave = 1;

    private string[] waves;

    public bool wavesAreActive = true;





    // For not endlessly calling IEnumerator NewWave()

    private bool alreadyCalled = false;


    public GameObject UIcontroller;
    [SerializeField]
    GameObject hubworldController;
    [SerializeField]
    private GameObject cantEscape, HUD;


    // Enemies' Prefabs  

    [SerializeField]

    private GameObject mainEnemyPrefab, dolanPrefab, datBoiPrefab, nyanCatPrefab, dogePrefab, trollfacePrefab;


    // Item's prefabs

    [SerializeField]

    private GameObject seitenbacherPrefab, nikeVansPrefab, softIcePrefab, timeToStopPrefab, doritosPrefab, mountainDewPrefab;

    [SerializeField]
    private GameObject keyPrefab;

    private ArenaController arena1Controller;

    public bool cantEscapeActivated = false;

    // Use this for initialization.

    void Start()

    {
        arena1Controller = FindObjectOfType<ArenaController>();

        cantEscape.SetActive(false);
        hubworldController = GameObject.Find("HubworldController");

        waves = new string[]

        { "001. 003main000dolan000datboi000nyan000doge000troll", "002. 005main000dolan000datboi000nyan001doge000troll",
          "003. 005main001dolan000datboi000nyan000doge001troll", "004. 005main000dolan000datboi001nyan001doge000troll",
          "005. 007main001dolan001datboi000nyan000doge000troll", "006. 007main002dolan000datboi000nyan000doge000troll",
          "007. 007main001dolan001datboi001nyan000doge001troll", "008. 010main001dolan000datboi000nyan003doge000troll",
          "009. 010main001dolan001datboi001nyan002doge000troll", "010. 015main002dolan000datboi000nyan003doge000troll",
          "011. 015main001dolan001datboi001nyan002doge002troll", "012. 015main002dolan001datboi001nyan002doge000troll",
          "013. 015main002dolan002datboi001nyan003doge000troll", "014. 015main002dolan002datboi001nyan000doge003troll",
          "015. 020main003dolan001datboi001nyan003doge000troll" };



        // Starting point for creating the arena.

        Vector2 startPos = new Vector2(-0.5f, 79.45f);

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

    void Update()
    {
        if (hubworldController.GetComponent<HubworldController>().area == "arena3")
        {
            if (!cantEscape.activeSelf)
            {
                StartCoroutine(activateCantEscapeCoroutine());
            }

            // If there is no enemy in the scene (anymore)...

            if ((arena1Controller.mainEnemiesInScene.Length + arena1Controller.datBoisInScene.Length + arena1Controller.dolansInScene.Length + arena1Controller.nyanCatsInScene.Length + arena1Controller.dogesInScene.Length) < 1 && !alreadyCalled && wavesAreActive)

            {
                // ... spawn the new wave.
                StartCoroutine(NewWave());

                // Avoid infinite calling of IEnumerator NewWave() and thus spawning infinitely.

                alreadyCalled = true;

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

    IEnumerator NewWave()

    {
        wave++;
        UIcontroller.GetComponent<UIController>().startNewWaveAnimation();

        yield return new WaitForSeconds(1.25f);

        try
        {
            // Parse the array of strings to get the amount of enemies to spawn
            int spawningMainEnemies = int.Parse(waves[wave - 2].Substring(5, 3));
            int spawningDolans = int.Parse(waves[wave - 2].Substring(12, 3));
            int spawningDatBois = int.Parse(waves[wave - 2].Substring(20, 3));
            int spawningNyanCats = int.Parse(waves[wave - 2].Substring(29, 3));
            int spawningDoges = int.Parse(waves[wave - 2].Substring(36, 3));
            int spawningTrollfaces = int.Parse(waves[wave - 2].Substring(43, 3));

            // Spawn them.

            for (int i = 1; i <= spawningMainEnemies; i++)
            {
                GameObject mainEnemy = Instantiate(mainEnemyPrefab, new Vector2(12.5f, 92), Quaternion.identity) as GameObject;
            }

            for (int i = 1; i <= spawningDolans; i++)
            {
                GameObject dolan = Instantiate(dolanPrefab, new Vector2(13, 92 + i), Quaternion.identity) as GameObject;
            }

            for (int i = 1; i <= spawningDatBois; i++)
            {
                GameObject datboi = Instantiate(datBoiPrefab, new Vector2(13.5f, 92 + i), Quaternion.identity) as GameObject;
            }

            for (int i = 1; i <= spawningNyanCats; i++)
            {
                GameObject nyan = Instantiate(nyanCatPrefab, new Vector2(13.5f, 92 + i), Quaternion.identity) as GameObject;
            }

            for (int i = 1; i <= spawningDoges; i++)
            {
                GameObject doge = Instantiate(dogePrefab, new Vector2(13.5f, 92 + i), Quaternion.identity) as GameObject;
            }

            for (int i = 1; i <= spawningTrollfaces; i++)
            {
                GameObject trollface = Instantiate(trollfacePrefab, new Vector2(13.5f, 92 + i), Quaternion.identity) as GameObject;
            }
        }

        // Output in log if there is no more wave (i.e. our array has ended).

        catch (System.IndexOutOfRangeException)
        {
            wavesAreActive = false;
            cantEscapeActivated = false;
            cantEscape.SetActive(false);
            Debug.Log("Keine weiteren Wellen mehr vorhanden");
            Instantiate(keyPrefab, new Vector2(13, 96), Quaternion.identity);
        }

        // Spawn items (same chances as in Arena 1

        if (Random.value < .5)
        {
            float ranX = Random.Range(5, 20);
            float ranY = Random.Range(87, 100);
            float ran = Random.value;

            // 5% Soft Ice, 25% Nike Vans, 30% Seitenbacher, 25% Doritos, 15% Mountain Dew
            if (ran < .05)
            {
                GameObject softIce = Instantiate(softIcePrefab, new Vector2(ranX, ranY), Quaternion.identity) as GameObject;
            }
            else if (ran < .10)
            {
                GameObject timeToStop = Instantiate(timeToStopPrefab, new Vector2(ranX, ranY), Quaternion.identity) as GameObject;

            }
            else if (ran < .50)
            {
                GameObject seitenbacher = Instantiate(seitenbacherPrefab, new Vector2(ranX, ranY), Quaternion.identity) as GameObject;
            }
            else if (ran < .75)
            {
                GameObject doritos = Instantiate(doritosPrefab, new Vector2(ranX, ranY), Quaternion.identity) as GameObject;
            }
            else if (ran < .90)
            {
                GameObject mountainDew = Instantiate(mountainDewPrefab, new Vector2(ranX, ranY), Quaternion.identity) as GameObject;
            }
            else
            {
                GameObject nikeVans = Instantiate(nikeVansPrefab, new Vector2(ranX, ranY), Quaternion.identity) as GameObject;
            }
        }
        alreadyCalled = false;
    }

    public void resetWaves()
    {
        hubworldController.GetComponent<HubworldController>().resetting = true;
        cantEscape.SetActive(false);
        cantEscapeActivated = false;
        wavesAreActive = false;

        GameObject[] playerProjectiles = GameObject.FindGameObjectsWithTag("PlayerProjectile");
        GameObject[] mainEnemyProjectiles = GameObject.FindGameObjectsWithTag("MainEnemyProjectile");
        GameObject[] enemyProjectiles = GameObject.FindGameObjectsWithTag("EnemyBullet");

        wave = 1;
        foreach (GameObject enemy in arena1Controller.mainEnemiesInScene)
        {
            Destroy(enemy.gameObject);
        }
        foreach (GameObject enemy in arena1Controller.dolansInScene)
        {
            Destroy(enemy.gameObject);
        }
        foreach (GameObject enemy in arena1Controller.datBoisInScene)
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

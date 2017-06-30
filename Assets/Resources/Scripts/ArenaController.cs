using System.Collections;

using System.Collections.Generic;

using UnityEngine;



public class ArenaController : MonoBehaviour

{



    public int gridRows;

    public int gridCols;

    public float offset = 1;

    public Grass grass1, grass2, grass3, grass4;



    // For ending wave (Keep track of how many enemies there are in the scene)
    public GameObject[] mainEnemiesInScene, nyanCatsInScene, dogesInScene, dolansInScene, datBoisInScene, trollfacesInScene;

    // Kleines Yen adds itself to the list in void Start()
    public List<GameObject> moneyInScene;

    public int wave = 1;

    private string[] waves;

    public bool wavesAreActive = true;

    public bool bossIsActive;



    // For not endlessly calling IEnumerator NewWave()

    private bool alreadyCalled = false;


    public GameObject UIcontroller;
    [SerializeField]
    GameObject hubworldController;
    [SerializeField]
    private GameObject cantEscape, HUD;


    // Enemies' Prefabs  

    [SerializeField]

    private GameObject mainEnemyPrefab, nyanCatPrefab, dogePrefab, nyanDogePrefab;


    // Item's prefabs

    [SerializeField]

    private GameObject seitenbacherPrefab, nikeVansPrefab, softIcePrefab, timeToStopPrefab, doritosPrefab, mountainDewPrefab;

    public bool cantEscapeActivated = false;


    // Use this for initialization.

    void Start()

    {
        cantEscape.SetActive(false);
        hubworldController = GameObject.Find("HubworldController");

        waves = new string[]

        { "001. 001main000doge000nyan", "002. 003main000doge000nyan", "003. 003main001doge000nyan", "004. 005main001doge000nyan", "005. 001main001doge001nyan",
          "006. 005main001doge001nyan", "007. 010main002doge001nyan", "008. 010main002doge001nyan", "009. 010main003doge001nyan", "010. 010main004doge000nyan",
          "011. 015main002doge001nyan", "012. 020main002doge001nyan", "013. 020main003doge001nyan", "014. 025main003doge001nyan", "015. 030main004doge001nyan"};



        // Starting point for creating the arena.

        Vector2 startPos = new Vector2(-0.5f, -0.5f);



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
        //Debug.Log(bossIsActive);
        //Debug.Log(mainEnemiesInScene.Length + nyanCatsInScene.Length + dogesInScene.Length + dolansInScene.Length + datBoisInScene.Length + trollfacesInScene.Length);

        // Keep track of enemies in scene
        mainEnemiesInScene = GameObject.FindGameObjectsWithTag("MainEnemy");
        nyanCatsInScene = GameObject.FindGameObjectsWithTag("NyanCat");
        dogesInScene = GameObject.FindGameObjectsWithTag("Doge");
        dolansInScene = GameObject.FindGameObjectsWithTag("Dolan");
        datBoisInScene = GameObject.FindGameObjectsWithTag("DatBoi");
        trollfacesInScene = GameObject.FindGameObjectsWithTag("Trollface");

        if (hubworldController.GetComponent<HubworldController>().area == "arena1")
        {
            if (!cantEscape.activeSelf && !bossIsActive)
            {
                StartCoroutine(activateCantEscapeCoroutine());
            }

            // If there is no enemy in the scene (anymore)...

            if (!bossIsActive && (mainEnemiesInScene.Length + nyanCatsInScene.Length + dogesInScene.Length) < 1 && !alreadyCalled && wavesAreActive)

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
        cantEscape.SetActive(false);
        cantEscapeActivated = false;
    }

    IEnumerator spawnMain(int number)
    {
        for (int i = 1; i <= number; i++)
        {
            GameObject mainEnemy = Instantiate(mainEnemyPrefab, new Vector2(12.5f, 13), Quaternion.identity) as GameObject;
            yield return new WaitForSeconds(0.1f);
        }

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

            int spawningDoges = int.Parse(waves[wave - 2].Substring(12, 3));

            int spawningNyanCats = int.Parse(waves[wave - 2].Substring(19, 3));

            // Spawn them.

            StartCoroutine(spawnMain(spawningMainEnemies));

            for (int i = 1; i <= spawningDoges; i++)

            {

                GameObject mainEnemy = Instantiate(dogePrefab, new Vector2(13, 13 + i), Quaternion.identity) as GameObject;

            }

            for (int i = 1; i <= spawningNyanCats; i++)

            {

                GameObject mainEnemy = Instantiate(nyanCatPrefab, new Vector2(13.5f, 13 + i), Quaternion.identity) as GameObject;

            }
        }

        // Output in log if there is no more wave (i.e. our array has ended).

        catch (System.IndexOutOfRangeException)
        {
            wavesAreActive = false;
            bossIsActive = true;
            cantEscapeActivated = false;
            cantEscape.SetActive(false);
            Instantiate(nyanDogePrefab, new Vector2(13,13), Quaternion.identity);
        }

        // Spawn items

        if (Random.value < .33)
        {
            float ranX = Random.Range(5, 22);
            float ranY = Random.Range(5, 16);
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

        GameObject[] playerProjectiles = GameObject.FindGameObjectsWithTag("PlayerProjectile");
        GameObject[] mainEnemyProjectiles = GameObject.FindGameObjectsWithTag("MainEnemyProjectile");
        GameObject[] enemyProjectiles = GameObject.FindGameObjectsWithTag("EnemyBullet");

        if (!bossIsActive)
        {
            wave = 1;
        }

        foreach (GameObject enemy in mainEnemiesInScene)
        {
            Destroy(enemy.gameObject);
        }
        foreach (GameObject enemy in nyanCatsInScene)
        {
            Destroy(enemy.gameObject);
        }
        foreach (GameObject enemy in dogesInScene)
        {
            Destroy(enemy.gameObject);
        }
        foreach(GameObject playerProjectile in playerProjectiles)
        {
            Destroy(playerProjectile.gameObject);
        }
        foreach(GameObject mainEP in mainEnemyProjectiles)
        {
            Destroy(mainEP.gameObject);
        }
        foreach(GameObject enemyP in enemyProjectiles)
        {
            Destroy(enemyP.gameObject);
        }
        foreach(GameObject money in moneyInScene)
        {
            Destroy(money.gameObject);
        }

        HUD.SetActive(false);
        GameObject[] items = GameObject.FindGameObjectsWithTag("item");
        foreach (GameObject item in items)
        {
            Destroy(item.gameObject);
        }

        if (!wavesAreActive && bossIsActive)
        {
            Destroy(GameObject.FindGameObjectWithTag("NyanDoge").gameObject);

            GameObject nyanDoge = Instantiate(nyanDogePrefab) as GameObject;
            nyanDoge.transform.position = new Vector2(13, 13);
        }
    }

}
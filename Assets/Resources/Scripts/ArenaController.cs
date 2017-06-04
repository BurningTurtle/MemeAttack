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

    public GameObject[] dolansInScene;

    public GameObject[] mainEnemiesInScene;

    public GameObject[] datBoisInScene, nyanCatsInScene, dogesInScene, trollfacesInScene;

    public int wave = 1;

    private string[] waves;

    public bool wavesAreActive = true;



    // For not endlessly calling IEnumerator NewWave()

    private bool alreadyCalled = false;


    public GameObject UIcontroller;
    [SerializeField]
    GameObject hubworldController;


    // Enemies' Prefabs  

    [SerializeField]

    private GameObject mainEnemyPrefab, nyanCatPrefab, dogePrefab;


    // Item's prefabs

    [SerializeField]

    private GameObject seitenbacherPrefab, nikeVansPrefab, softIcePrefab, timeToStopPrefab, doritosPrefab, mountainDewPrefab;


    // Use this for initialization.

    void Start()

    {

        hubworldController = GameObject.Find("HubworldController");

        waves = new string[]

        { "001. 001main000doge000nyan", "002. 003main000doge000nyan", "003. 003main001doge000nyan", "004. 005main001doge000nyan", "005. 001main000doge000nyan",
          "006. 001main000doge000nyan", "007. 001main000doge000nyan", "008. 001main000doge000nyan", "009. 001main000doge000nyan", "010. 001main000doge000nyan",
          "011. 001main000doge000nyan", "012. 001main000doge000nyan", "013. 001main000doge000nyan", "014. 001main000doge000nyan", "015. 001main000doge000nyan"};



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
        if (hubworldController.GetComponent<HubworldController>().area == "arena1")
        {
            Debug.Log("arena1 drin");
            // Keep track of enemies in scene

            mainEnemiesInScene = GameObject.FindGameObjectsWithTag("MainEnemy");

            datBoisInScene = GameObject.FindGameObjectsWithTag("DatBoi");

            dolansInScene = GameObject.FindGameObjectsWithTag("Dolan");

            nyanCatsInScene = GameObject.FindGameObjectsWithTag("NyanCat");

            dogesInScene = GameObject.FindGameObjectsWithTag("Doge");

            trollfacesInScene = GameObject.FindGameObjectsWithTag("Trollface");



            // If there is no enemy in the scene (anymore)...

            if ((mainEnemiesInScene.Length + datBoisInScene.Length + dolansInScene.Length + nyanCatsInScene.Length + dogesInScene.Length) < 1 && !alreadyCalled && wavesAreActive)

            {
                // ... spawn the new wave.

                StartCoroutine(NewWave());

                // Avoid infinite calling of IEnumerator NewWave() and thus spawning infinitely.

                alreadyCalled = true;

            }
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

            for (int i = 1; i <= spawningMainEnemies; i++)

            {

                GameObject mainEnemy = Instantiate(mainEnemyPrefab, new Vector2(12.5f, 25), Quaternion.identity) as GameObject;

            }

            for (int i = 1; i <= spawningDoges; i++)

            {

                GameObject mainEnemy = Instantiate(dogePrefab, new Vector2(11.5f, 25 + i), Quaternion.identity) as GameObject;

            }

            for (int i = 1; i <= spawningNyanCats; i++)

            {

                GameObject mainEnemy = Instantiate(nyanCatPrefab, new Vector2(13.5f, 25 + i), Quaternion.identity) as GameObject;

            }
        }

        // Output in log if there is no more wave (i.e. our array has ended).

        catch (System.IndexOutOfRangeException)
        {
            wavesAreActive = false;
            Debug.Log("Keine weiteren Wellen mehr vorhanden");
        }

        // Spawn items

        if (Random.value < .3)
        {
            float ranX = Random.Range(6, 18);
            float ranY = Random.Range(10, 17);
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

}
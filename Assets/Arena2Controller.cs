﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arena2Controller : MonoBehaviour {

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

    public bool bossIsActive;



    // For not endlessly calling IEnumerator NewWave()

    private bool alreadyCalled = false;


    public GameObject UIcontroller;
    [SerializeField]
    GameObject hubworldController;
    [SerializeField]
    private GameObject cantEscape;


    // Enemies' Prefabs  

    [SerializeField]

    private GameObject mainEnemyPrefab, dolanPrefab, datBoiPrefab, datDolanPrefab;


    // Item's prefabs

    [SerializeField]

    private GameObject seitenbacherPrefab, nikeVansPrefab, softIcePrefab, timeToStopPrefab, doritosPrefab, mountainDewPrefab;


    // Use this for initialization.

    void Start()

    {
        cantEscape.SetActive(false);
        hubworldController = GameObject.Find("HubworldController");

        waves = new string[]

        { "001. 001main000dolan000datboi", "002. 003main000dolan000datboi", "003. 003main001dolan000datboi", "004. 005main001dolan000datboi", "005. 001main001dolan001datboi",
          "006. 005main001dolan001datboi", "007. 010main002dolan001datboi", "008. 010main002dolan001datboi", "009. 010main003dolan001datboi", "010. 010main004dolan000datboi",
          "011. 015main002dolan001datboi", "012. 020main002dolan001datboi", "013. 020main003dolan001datboi", "014. 025main003dolan001datboi", "015. 030main004dolan001datboi"};



        // Starting point for creating the arena.

        Vector2 startPos = new Vector2(-0.5f, 37.1f);



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
        if (hubworldController.GetComponent<HubworldController>().area == "arena2")
        {
            if (!bossIsActive && !cantEscape.activeSelf)
            {
                StartCoroutine(activateCantEscapeCoroutine());
            }
            Debug.Log("arena2 drin");
            // Keep track of enemies in scene

            mainEnemiesInScene = GameObject.FindGameObjectsWithTag("MainEnemy");

            datBoisInScene = GameObject.FindGameObjectsWithTag("DatBoi");

            dolansInScene = GameObject.FindGameObjectsWithTag("Dolan");


            // If there is no enemy in the scene (anymore)...

            if ((mainEnemiesInScene.Length + datBoisInScene.Length + dolansInScene.Length) < 1 && !alreadyCalled && wavesAreActive)

            {
                // ... spawn the new wave.

                StartCoroutine(NewWave());

                // Avoid infinite calling of IEnumerator NewWave() and thus spawning infinitely.

                alreadyCalled = true;

            }
        }
    }

    IEnumerator activateCantEscapeCoroutine()
    {
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

            // Spawn them.

            for (int i = 1; i <= spawningMainEnemies; i++)

            {

                GameObject mainEnemy = Instantiate(mainEnemyPrefab, new Vector2(12.5f, 43), Quaternion.identity) as GameObject;

            }

            for (int i = 1; i <= spawningDolans; i++)

            {

                GameObject mainEnemy = Instantiate(dolanPrefab, new Vector2(13, 43 + i), Quaternion.identity) as GameObject;

            }

            for (int i = 1; i <= spawningDatBois; i++)

            {

                GameObject mainEnemy = Instantiate(datBoiPrefab, new Vector2(13.5f, 43 + i), Quaternion.identity) as GameObject;

            }
        }

        // Output in log if there is no more wave (i.e. our array has ended).

        catch (System.IndexOutOfRangeException)
        {
            wavesAreActive = false;
            bossIsActive = true;
            cantEscape.SetActive(false);
            Instantiate(datDolanPrefab, new Vector2(13, 50), Quaternion.identity);
            Debug.Log("Keine weiteren Wellen mehr vorhanden");
        }

        // Spawn items (same chances as in Arena 1

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
}
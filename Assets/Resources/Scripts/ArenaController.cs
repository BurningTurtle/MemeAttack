using System.Collections;

using System.Collections.Generic;

using UnityEngine;



public class ArenaController : MonoBehaviour

{



    public int gridRows;

    public int gridCols;

    public int offset = 1;

    public Grass grass1, grass2, grass3, grass4;



    // For ending wave (Keep track of how many enemies there are in the scene)

    private GameObject[] dolansInScene;

    private GameObject[] mainEnemiesInScene;

    private GameObject[] datBoisInScene;

    public int wave = 1;

    private string[] waves;



    // For not endlessly calling IEnumerator NewWave()

    private bool alreadyCalled = false;


    public GameObject UIcontroller;


    // Enemies' Prefabs  

    [SerializeField]

    private GameObject mainEnemyPrefab;

    [SerializeField]

    private GameObject dolanPrefab;

    [SerializeField]

    private GameObject datBoiPrefab;



    // Use this for initialization.

    void Start()

    {

        waves = new string[]

        { "001. 001main000dolan000datboi", "002. 003main000dolan000datboi", "003. 010main000dolan000datboi", "004. 000main001dolan000datboi", "005. 003main001dolan000datboi",
          "006. 010main001dolan000datboi", "007. 010main001dolan000datboi", "008. 000main000dolan001datboi", "009. 005main001dolan001datboi", "010. 010main002dolan003datboi",
          "011. 010main003dolan004datboi", "012. 015main003dolan001datboi", "013. 015main004dolan002datboi", "014. 020main005dolan003datboi", "015. 025main005dolan005datboi"};



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

	void Update () {



        // Keep track of enemies in scene

        mainEnemiesInScene = GameObject.FindGameObjectsWithTag("MainEnemy");

        datBoisInScene = GameObject.FindGameObjectsWithTag("DatBoi");

        dolansInScene = GameObject.FindGameObjectsWithTag("Dolan");



        // If there is no enemy in the scene (anymore)...

        if((mainEnemiesInScene.Length + datBoisInScene.Length + dolansInScene.Length) < 1 && !alreadyCalled)

        {
            // ... spawn the new wave.

            StartCoroutine(NewWave());

            // Avoid infinite calling of IEnumerator NewWave() and thus spawning infinitely.

            alreadyCalled = true;

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

            int spawningDolans = int.Parse(waves[wave - 2].Substring(12, 3));

            int spawningDatBois = int.Parse(waves[wave - 2].Substring(20, 3));

            // Spawn them.

            for (int i = 1; i <= spawningMainEnemies; i++)

            {

                GameObject mainEnemy = Instantiate(mainEnemyPrefab, new Vector2(11, 24), Quaternion.identity) as GameObject;

            }



            for (int i = 1; i <= spawningDolans; i++)

            {

                GameObject mainEnemy = Instantiate(dolanPrefab, new Vector2(12, 24 + i), Quaternion.identity) as GameObject;

            }



            for (int i = 1; i <= spawningDatBois; i++)

            {

                GameObject mainEnemy = Instantiate(datBoiPrefab, new Vector2(13, 24 + i), Quaternion.identity) as GameObject;

            }
        }

        // Output in log if there is no more wave (i.e. our array has ended).

        catch(System.IndexOutOfRangeException)
        {
            Debug.Log("Keine weiteren Wellen mehr vorhanden");
        }

        
        

        alreadyCalled = false;
    }

}
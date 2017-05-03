using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaController : MonoBehaviour {

    public int gridRows;
    public int gridCols;
    public int offset = 1;
    public Grass grass1, grass2, grass3, grass4;

    // For ending wave (Keep track of how many enemies there are in the scene)
    private GameObject[] dolansInScene;
    private GameObject[] mainEnemiesInScene;
    private GameObject[] datBoisInScene;
    private int wave = 1;

    // For not endlessly calling IEnumerator NewWave()
    private bool alreadyCalled = false;

    // Enemies' Prefabs  
    [SerializeField] private GameObject mainEnemyPrefab;
    [SerializeField] private GameObject dolanPrefab;
    [SerializeField] private GameObject datBoiPrefab;

	// Use this for initialization.
	void Start ()
    {
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
                switch(id)
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

        // If there is no enemy in the scene (anymore)
        if((mainEnemiesInScene.Length + datBoisInScene.Length + dolansInScene.Length) < 1 && !alreadyCalled)
        {
            StartCoroutine(NewWave());

            // Avoid infinite calling of IEnumerator NewWave() and thus spawning infinitely
            alreadyCalled = true;
        }

	}

     IEnumerator NewWave()
    {
        switch (wave)
        {
            case 1:

                // Cool animation here
                Debug.Log("Wave 1");

                // Wait for animation to disappear
                yield return new WaitForSeconds(5f);

                // Spawn 5 Main Enemies at (12,24)
                for(int i = 0; i < 6; i++)
                {
                    GameObject mainEnemy = Instantiate(mainEnemyPrefab) as GameObject;
                    mainEnemy.transform.position = new Vector2(12, 24);
                }

                // So that the next wave "is" case 2
                wave++;

                // Allow if statement in Update() to call this function again
                alreadyCalled = false;

                break;

            case 2:

                // Cool animation here
                Debug.Log("Wave2");

                yield return new WaitForSeconds(5f);

                for(int i = 0; i <5; i++)
                {
                    GameObject mainEnemy = Instantiate(mainEnemyPrefab) as GameObject;
                    mainEnemy.transform.position = new Vector2(12, 24);
                }

                GameObject dolan = Instantiate(dolanPrefab) as GameObject;
                dolan.transform.position = new Vector2(12, 24);

                wave++;
                alreadyCalled = false;
                break;

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Special2Controller : MonoBehaviour {

    private int gridRows = 25;
    private int gridCols = 25;
    private float offset = 0.9917f;
    [SerializeField] private Grass grass1, grass2, grass3, grass4;

    private GameObject slot1, slot2, slot3, slot4, slot5;
    public bool solved = false;

    private GameObject[] enemiesInScene;

    [SerializeField] GameObject MainEnemySpecialPrefab;
    private bool canSpawn = true;
    [SerializeField] private GameObject hwCtrl;

    // Use this for initialization
    void Start () {

        // Starting point for creating the arena.

        Vector2 startPos = new Vector2(-50, -70);

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

        slot1 = GameObject.Find("Slot1");
        slot2 = GameObject.Find("Slot2");
        slot3 = GameObject.Find("Slot3");
        slot4 = GameObject.Find("Slot4");
        slot5 = GameObject.Find("Slot5");
    }
	
	// Update is called once per frame
	void Update () {
        enemiesInScene = GameObject.FindGameObjectsWithTag("MainEnemySpecial");
        if(hwCtrl.GetComponent<HubworldController>().area == "special2" && enemiesInScene.Length < 1)
        {
            if (canSpawn)
            {
                canSpawn = false;
                StartCoroutine(spawnEnemy());
            }
        }
    }

    IEnumerator spawnEnemy()
    {
        GameObject enemy = Instantiate(MainEnemySpecialPrefab) as GameObject;
        enemy.transform.position = new Vector2(-28, -45);

        yield return new WaitForSeconds(3);
        canSpawn = true;
    }

    public void updateSolved()
    {
        if (slot1.GetComponent<Special2Slot>().slotNumber == 1 && slot2.GetComponent<Special2Slot>().slotNumber == 0 && slot3.GetComponent<Special2Slot>().slotNumber == 1 && slot4.GetComponent<Special2Slot>().slotNumber == 0 && slot5.GetComponent<Special2Slot>().slotNumber == 0)
        {
            solved = true;
        }
    }
}

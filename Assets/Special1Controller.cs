using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Special1Controller : MonoBehaviour {

    public int gridRows;
    public int gridCols;
    public float offset = 1;
    public Grass grass1, grass2, grass3, grass4;

    // Use this for initialization
    void Start()
    {
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
        void Update () {
		
	    }
}

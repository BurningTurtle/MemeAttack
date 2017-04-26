using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaController : MonoBehaviour {

    public int gridRows;
    public int gridCols;
    public int offset = 1;
    public Grass grass1, grass2, grass3, grass4;

	// Use this for initialization
	void Start ()
    {
        Vector2 startPos = new Vector2(-0.5f, -0.5f);

        for (float i = 1; i <= gridRows; i++)
        {
            for (float j = 1; j <= gridCols; j++)
            {
                Grass grass = Instantiate(grass1);
                int id = Random.Range(1, 4);
                switch(id)
                {
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

                float posX = (offset * i) + startPos.x;
                float posY = (offset * j) + startPos.y;
                grass.transform.position = new Vector3(posX, posY, 100);
            }
        }

        GameObject camera = GameObject.Find("Main Camera");
        camera.transform.position = new Vector3(gridRows / 2, gridCols / 2, camera.transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

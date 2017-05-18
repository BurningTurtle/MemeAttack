using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeToStop : MonoBehaviour {

    private GameObject player;
    private UIController uic;
    private SoundManager soundMan;
    private bool pickedUp = false;
    private SpriteRenderer sr;

    private bool used = false;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
        uic = FindObjectOfType<UIController>();
        soundMan = FindObjectOfType<SoundManager>();
        sr = GetComponent<SpriteRenderer>();
	}

    private void Update()
    {
        if (Input.GetKeyDown("space") && !used && pickedUp)
        {
            used = true;
            StartCoroutine(itsTimeToStop());
            uic.timeToStop();
            soundMan.playItsTimeToStop();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player && uic.activeItems < 1)
        {
            Color colour = sr.color;
            colour.a = 0;
            sr.color = colour;

            if (!pickedUp)
            {
                soundMan.playPlaceholder();
                uic.timeToStopNotUsed();

                pickedUp = true;
            }
        }
    }

    IEnumerator itsTimeToStop()
    {
        for(int i = 0; i < FindObjectOfType<ArenaController>().GetComponent<ArenaController>().dolansInScene.Length; i++)
        {
            FindObjectOfType<ArenaController>().GetComponent<ArenaController>().dolansInScene[i].GetComponent<Dolan>().stop = true;
        }
        for(int i = 0; i < FindObjectOfType<ArenaController>().GetComponent<ArenaController>().mainEnemiesInScene.Length; i++)
        {
            FindObjectOfType<ArenaController>().GetComponent<ArenaController>().mainEnemiesInScene[i].GetComponent<MainEnemy>().stop = true;
        }
        for(int i = 0;  i < FindObjectOfType<ArenaController>().GetComponent<ArenaController>().datBoisInScene.Length; i++)
        {
            FindObjectOfType<ArenaController>().GetComponent<ArenaController>().datBoisInScene[i].GetComponent<DatBoi>().stop = true;
        }
        for (int i = 0; i < FindObjectOfType<ArenaController>().GetComponent<ArenaController>().nyanCatsInScene.Length; i++)
        {
            FindObjectOfType<ArenaController>().GetComponent<ArenaController>().nyanCatsInScene[i].GetComponent<NyanCat>().stop = true;
        }

        yield return new WaitForSeconds(3f);

        for (int i = 0; i < FindObjectOfType<ArenaController>().GetComponent<ArenaController>().dolansInScene.Length; i++)
        {
            FindObjectOfType<ArenaController>().GetComponent<ArenaController>().dolansInScene[i].GetComponent<Dolan>().stop = false;
        }
        for (int i = 0; i < FindObjectOfType<ArenaController>().GetComponent<ArenaController>().mainEnemiesInScene.Length; i++)
        {
            FindObjectOfType<ArenaController>().GetComponent<ArenaController>().mainEnemiesInScene[i].GetComponent<MainEnemy>().stop = false;
        }
        for (int i = 0; i < FindObjectOfType<ArenaController>().GetComponent<ArenaController>().datBoisInScene.Length; i++)
        {
            FindObjectOfType<ArenaController>().GetComponent<ArenaController>().datBoisInScene[i].GetComponent<DatBoi>().stop = false;
        }
        for (int i = 0; i < FindObjectOfType<ArenaController>().GetComponent<ArenaController>().nyanCatsInScene.Length; i++)
        {
            FindObjectOfType<ArenaController>().GetComponent<ArenaController>().nyanCatsInScene[i].GetComponent<NyanCat>().stop = false;
        }
    }
}

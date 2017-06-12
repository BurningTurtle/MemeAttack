using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeToStop : MonoBehaviour
{

    private GameObject player;
    private UIController uic;
    private SoundManager soundMan;
    private bool pickedUp = false;
    private SpriteRenderer sr;

    private bool used = false;

    // Use this for initialization
    void Start()
    {
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
        // Arena3Controller because it keeps track of all enemies. Doing it in ArenaController would result in unnecessary storage of enemies unnecessary arrays.

        for (int i = 0; i < FindObjectOfType<Arena3Controller>().GetComponent<Arena3Controller>().dolansInScene.Length; i++)
        {
            FindObjectOfType<Arena3Controller>().GetComponent<Arena3Controller>().dolansInScene[i].GetComponent<Dolan>().stop = true;
        }
        for (int i = 0; i < FindObjectOfType<Arena3Controller>().GetComponent<Arena3Controller>().mainEnemiesInScene.Length; i++)
        {
            FindObjectOfType<Arena3Controller>().GetComponent<Arena3Controller>().mainEnemiesInScene[i].GetComponent<MainEnemy>().stop = true;
        }
        for (int i = 0; i < FindObjectOfType<Arena3Controller>().GetComponent<Arena3Controller>().datBoisInScene.Length; i++)
        {
            FindObjectOfType<Arena3Controller>().GetComponent<Arena3Controller>().datBoisInScene[i].GetComponent<DatBoi>().stop = true;
        }
        for (int i = 0; i < FindObjectOfType<Arena3Controller>().GetComponent<Arena3Controller>().nyanCatsInScene.Length; i++)
        {
            FindObjectOfType<Arena3Controller>().GetComponent<Arena3Controller>().nyanCatsInScene[i].GetComponent<NyanCat>().stop = true;
        }
        for (int i = 0; i < FindObjectOfType<Arena3Controller>().GetComponent<Arena3Controller>().dogesInScene.Length; i++)
        {
            FindObjectOfType<Arena3Controller>().GetComponent<Arena3Controller>().dogesInScene[i].GetComponent<Doge>().stop = true;   
        }
        for (int i = 0; i < FindObjectOfType<Arena3Controller>().GetComponent<Arena3Controller>().trollfacesInScene.Length; i++)
        {
            FindObjectOfType<Arena3Controller>().GetComponent<Arena3Controller>().trollfacesInScene[i].GetComponent<Trollface>().stop = true;
        }

        yield return new WaitForSeconds(3f);

        for (int i = 0; i < FindObjectOfType<Arena3Controller>().GetComponent<Arena3Controller>().dolansInScene.Length; i++)
        {
            FindObjectOfType<Arena3Controller>().GetComponent<Arena3Controller>().dolansInScene[i].GetComponent<Dolan>().stop = false;
        }
        for (int i = 0; i < FindObjectOfType<ArenaController>().GetComponent<ArenaController>().mainEnemiesInScene.Length; i++)
        {
            FindObjectOfType<Arena3Controller>().GetComponent<Arena3Controller>().mainEnemiesInScene[i].GetComponent<MainEnemy>().stop = false;
        }
        for (int i = 0; i < FindObjectOfType<Arena3Controller>().GetComponent<Arena3Controller>().datBoisInScene.Length; i++)
        {
            FindObjectOfType<Arena3Controller>().GetComponent<Arena3Controller>().datBoisInScene[i].GetComponent<DatBoi>().stop = false;
        }
        for (int i = 0; i < FindObjectOfType<Arena3Controller>().GetComponent<Arena3Controller>().nyanCatsInScene.Length; i++)
        {
            FindObjectOfType<Arena3Controller>().GetComponent<Arena3Controller>().nyanCatsInScene[i].GetComponent<NyanCat>().stop = false;
        }
        for (int i = 0; i < FindObjectOfType<Arena3Controller>().GetComponent<Arena3Controller>().dogesInScene.Length; i++)
        {
            FindObjectOfType<Arena3Controller>().GetComponent<Arena3Controller>().dogesInScene[i].GetComponent<Doge>().stop = false;
        }
        for (int i = 0; i < FindObjectOfType<Arena3Controller>().GetComponent<Arena3Controller>().trollfacesInScene.Length; i++)
        {
            FindObjectOfType<Arena3Controller>().GetComponent<Arena3Controller>().trollfacesInScene[i].GetComponent<Trollface>().stop = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterSwordItem : MonoBehaviour
{

    private GameObject player;
    private SoundManager soundMan;
    private SpriteRenderer sr;
    private bool pickedUp = false;
    private GameObject uic;

    void Start()
    {
        player = GameObject.Find("Player");
        soundMan = FindObjectOfType<SoundManager>();
        sr = GetComponent<SpriteRenderer>();
        uic = GameObject.Find("UIController");
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            Color colour = sr.material.color;
            colour.a = 0;
            sr.material.color = colour;
            if (!pickedUp)
            {
                soundMan.playZelda();

                // Do something
                player.GetComponent<Player>().transformToLink();

                // Show MasterSword in the UI
                //uic.GetComponent<UIController>().masterSword();

                pickedUp = true;
            }
        }
    }

    
}

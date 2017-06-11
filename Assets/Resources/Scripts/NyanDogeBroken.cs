using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NyanDogeBroken : MonoBehaviour {

    private Rigidbody2D rb;

    private int xVelocity;
    private int yVelocity;

    public int health = 100;

    private GameObject player;

    private bool alive = true;

    [SerializeField] GameObject yen500, yen100;

    private SoundManager soundMan;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        if(this.gameObject.tag == "NyanDogeDoge")
        {
            xVelocity = 7;
            yVelocity = 3;
        }
        if(this.gameObject.tag == "NyanDogeCat")
        {
            xVelocity = -5;
            yVelocity = 7;
        }

        player = GameObject.Find("Player");

        soundMan = FindObjectOfType<SoundManager>();
    }
	
	// Update is called once per frame
	void Update () {
        rb.velocity = new Vector2(xVelocity, yVelocity);
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "BorderVertical")
        {
            xVelocity = -xVelocity;
        }
        else if (collision.gameObject.tag == "Border")
        {
            yVelocity = -yVelocity;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "PlayerProjectile")
        {
            health -= player.GetComponent<Player>().damage;

            if (health <= 0 && alive)
            {
                alive = false;
                soundMan.playAudioClip("KeksSound");

                int ran = Random.Range(0, 100);
                if(ran <= 50)
                {
                    GameObject fivehundredyen = Instantiate(yen500) as GameObject;
                    fivehundredyen.transform.position = transform.position;
                }
                else
                {
                    GameObject hundredyen = Instantiate(yen100) as GameObject;
                    hundredyen.transform.position = transform.position;
                }
                Destroy(this.gameObject);
            }

            Destroy(collision.gameObject);
        }
    }
}

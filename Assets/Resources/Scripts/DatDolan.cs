using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatDolan : MonoBehaviour {

    [SerializeField] GameObject PileOfCottonPrefab;
    private GameObject player;
    [SerializeField] private int speed;
    private float deltaX;
    private float lastposition;
    private SpriteRenderer sr;
    private bool canDrop = true;
    private int health = 1000;
    private bool alive = true;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
        sr = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (alive)
        {
            Vector2 playerVector = new Vector2(player.transform.position.x - this.transform.position.x, player.transform.position.y - this.transform.position.y);
            playerVector.Normalize();
            GetComponent<Rigidbody2D>().velocity = playerVector * speed * Time.deltaTime;

            deltaX = transform.position.x - lastposition;
            lastposition = transform.position.x;

            if (deltaX < 0)
            {
                sr.flipX = false;
            }
            else if (deltaX > 0)
            {
                sr.flipX = true;
            }

            if (canDrop)
            {
                StartCoroutine(drop());
            }
        }
    }


    IEnumerator drop()
    {
        canDrop = false;
        GameObject pileOfCotton = Instantiate(PileOfCottonPrefab) as GameObject;
        if (deltaX < 0)
        {
            pileOfCotton.transform.position = new Vector2(transform.position.x - 1f, transform.position.y - 2);
        }
        else if (deltaX > 0)
        {
            pileOfCotton.transform.position = new Vector2(transform.position.x + 1f, transform.position.y - 2);
        }
        else if (deltaX == 0)
        {
            pileOfCotton.transform.position = new Vector2(transform.position.x, transform.position.y - 2);
        }
        yield return new WaitForSeconds(5);
        canDrop = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerProjectile")
        {
            health = health - FindObjectOfType<Player>().damage;
            if (health <= 0)
            {
                StartCoroutine(die());
            }
            Destroy(collision.gameObject);
        }
    }

    IEnumerator die()
    {
        for (float f = 1; f >= 0; f -= 0.1f)
        {
            Color colour = sr.material.color;
            colour.a = f;
            sr.material.color = colour;
            yield return null;
        }

        alive = false;

        GameObject pileOfCotton = Instantiate(PileOfCottonPrefab) as GameObject;
        pileOfCotton.transform.position = new Vector2(transform.position.x - 0.2f, transform.position.y - 2);

        Destroy(this.gameObject);
    }
}

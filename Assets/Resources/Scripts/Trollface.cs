using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trollface : MonoBehaviour
{

    private GameObject player;
    private bool alive = true;
    public int health = 15;
    public int speed;
    private bool activated = false;

    // Keep track of which direction Doge is moving to. Important for animation.
    private float deltaX;
    private float lastPosition;

    private SpriteRenderer sr;

    // For It's time to stop
    public bool stop = false;

    [SerializeField]
    private GameObject yen1, yen5, yen10, yen50;

    [SerializeField]
    private Sprite[] itemSprites;
    [SerializeField]
    private Sprite trollfaceSprite;
    private int spriteInt;

    private GameObject statue;
    [SerializeField]
    private Sprite statueActivated;

    // Use this for initialization
    void Start()
    {
        float ranX = Random.Range(5, 20);
        float ranY = Random.Range(87, 100);
        transform.position = new Vector2(ranX, ranY);
        player = GameObject.Find("Player");
        lastPosition = transform.position.x;
        sr = GetComponent<SpriteRenderer>();
        spriteInt = Random.Range(0, itemSprites.Length);
        statue = GameObject.Find("trollfaceStatue1");
        sr.sprite = itemSprites[spriteInt];

        // Adjust scale
        switch(spriteInt)
        {
            case 0: // Clock
                transform.localScale = new Vector2(0.6f, 0.6f);
                break;
            case 1: // Doritos
                transform.localScale = new Vector2(0.45f, 0.45f);
                break;
            case 2: // Seitenbacher
                transform.localScale = new Vector2(0.36f, 0.36f);
                break;
            case 3: // Nike Vans
                transform.localScale = new Vector2(0.36f, 0.36f);
                break;
            case 4: // SoftIce
                transform.localScale = new Vector2(0.29f, 0.29f);
                break;
            case 5: // Mountain Dew
                transform.localScale = new Vector2(0.45f, 0.45f);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (alive && !stop && activated)
        {
            Vector2 playerVector = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
            playerVector.Normalize();
            GetComponent<Rigidbody2D>().velocity = playerVector * speed * Time.deltaTime;

            // Get deltaX for current position.
            deltaX = lastPosition - transform.position.x;

            // Last position for the next frame is the current position from now.
            lastPosition = transform.position.x;

            // If Doge is moving right
            if (deltaX < 0)
            {
                // Flip Doge
                sr.flipX = false;
            }
            else if (deltaX > 0)
            {
                // Flip Doge back to normal
                sr.flipX = true;
            }
        }

        if (stop)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" && !activated)
        {
            transform.localScale = new Vector2(0.25f, 0.25f);
            activated = true;
            sr.sprite = trollfaceSprite;
            player.GetComponent<Player>().health -= 20;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(activated)
        {
            if (collision.gameObject.tag == "PlayerProjectile")
            {
                health -= FindObjectOfType<Player>().GetComponent<Player>().damage;
                Destroy(collision.gameObject);
            }

            if (health <= 0 && alive)
            {
                StartCoroutine(die());
            }
        }
    }

    IEnumerator die()
    {
        for (float f = 1f; f >= 0; f -= 0.1f)
        {
            // The colour of Doge's sprite.
            Color colour = sr.material.color;

            // Reduce colour's alpha by 0.1f for every f >= 0.
            colour.a = f;

            // Apply colour with new alpha value to DatBoi's SpriteRenderer Component.
            sr.material.color = colour;

            // Wait until next frame.
            yield return null;
        }

        // Kill Doge.
        alive = false;

        int ran = Random.Range(0, 100);
        if (ran < 50)
        {
            int ran1 = Random.Range(0, 100);

            if (ran1 < 90)
            {
                GameObject oneYen = Instantiate(yen1) as GameObject;
                oneYen.transform.position = transform.position;
            }
            else if (ran1 < 93)
            {
                GameObject fiveYen = Instantiate(yen5) as GameObject;
                fiveYen.transform.position = transform.position;
            }
            else if (ran1 < 96)
            {
                GameObject tenYen = Instantiate(yen10) as GameObject;
                tenYen.transform.position = transform.position;
            }
            else if (ran1 < 100)
            {
                GameObject fiftyYen = Instantiate(yen50) as GameObject;
                fiftyYen.transform.position = transform.position;
            }
        }

        statue.GetComponent<SpriteRenderer>().sprite = statueActivated;
        Destroy(this.gameObject);
    }
}

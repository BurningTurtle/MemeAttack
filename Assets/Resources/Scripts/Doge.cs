using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Doge : MonoBehaviour
{

    private GameObject player;
    private bool alive = true;
    public int health = 15;
    public int speed;

    // Keep track of which direction Doge is moving to. Important for animation.
    private float deltaX;
    private float lastPosition;

    private SpriteRenderer sr;
    private Animator anim;

    // For It's time to stop
    public bool stop = false;

    // For woof
    [SerializeField] private GameObject woofPrefab;
    private int range = 20;
    private bool canShootNext = true;
    [SerializeField]
    private float shootSpeed;

    [SerializeField]
    private GameObject yen1, yen5, yen10, yen50;

    private GameObject statue;
    [SerializeField]
    private Sprite statueActivated;

    private GameObject woofProjectile;

    private void Start()
    {
        player = GameObject.Find("Player");
        lastPosition = transform.position.x;
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        statue = GameObject.Find("dogeStatue1");
    }

    private void FixedUpdate()
    {
        if (alive && !stop)
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

            // Distance between Doge and Player
            float combinedDistance = Mathf.Abs(playerVector.x) + Mathf.Abs(playerVector.y);

            // If Doge's distance to the player is smaller than range
            if (combinedDistance < range && canShootNext)
            {
                StartCoroutine(woof());
            }

        }

        if (stop)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            anim.SetBool("moving", false);
            try
            {
                FindObjectOfType<Woof>().GetComponent<Woof>().stop = true;
            }
            catch (NullReferenceException)
            {
            }
        }
    }

    IEnumerator woof()
    {
        canShootNext = false;

        // Instantiate woof on doge
        woofProjectile = Instantiate(woofPrefab) as GameObject;
        woofProjectile.transform.position = this.transform.position;

        // Get Vector to the player
        Vector2 playerVector = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
        playerVector = Vector2.ClampMagnitude(playerVector, shootSpeed);
        woofProjectile.GetComponent<Rigidbody2D>().velocity = playerVector * shootSpeed;

        yield return new WaitForSeconds(3f);

        canShootNext = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerProjectile")
        {
            health -= FindObjectOfType<Player>().GetComponent<Player>().damage;
            Destroy(collision.gameObject);
        }

        if (health <= 0 && alive)
        {
            Destroy(woofProjectile.gameObject);
            StartCoroutine(die());
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

        int ran = UnityEngine.Random.Range(0, 100);
        if (ran < 50)
        {
            int ran1 = UnityEngine.Random.Range(0, 100);

            if (ran1 < 10)
            {
                GameObject oneYen = Instantiate(yen1) as GameObject;
                oneYen.transform.position = transform.position;
            }
            else if (ran1 < 20)
            {
                GameObject tenYen = Instantiate(yen5) as GameObject;
                tenYen.transform.position = transform.position;
            }
            else if (ran1 < 80)
            {
                GameObject fiveYen = Instantiate(yen10) as GameObject;
                fiveYen.transform.position = transform.position;
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

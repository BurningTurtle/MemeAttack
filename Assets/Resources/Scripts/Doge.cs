using System.Collections;
using System.Collections.Generic;
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

    [SerializeField]
    private GameObject money;

    private void Start()
    {
        player = GameObject.Find("Player");
        lastPosition = transform.position.x;
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
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

        }

        if (stop)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            anim.SetBool("moving", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerProjectile")
        {
            health -= FindObjectOfType<Player>().GetComponent<Player>().damage;
            Destroy(collision.gameObject);
        }

        if (health <= 0)
        {
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
        Instantiate(money, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        Destroy(this.gameObject);
    }
}

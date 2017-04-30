using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dolan : MonoBehaviour {

    [SerializeField] private GameObject player;
    private bool alive = true;
    private float minimalSpeed = 2f;
    private int health = 10;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        if (alive)
        {
            Vector2 playerVector = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
            GetComponent<Rigidbody2D>().velocity = playerVector * minimalSpeed * Time.deltaTime;

            // Accelerate Dolan if he's too slow.
            while (GetComponent<Rigidbody2D>().velocity.magnitude < minimalSpeed)
            {
                GetComponent<Rigidbody2D>().velocity *= 1.1f;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Subtract one healthpoint if Dolan gets hit by the PlayerProjectile. Delete him if he has no more healthpoints.
        if (collision.tag == "PlayerProjectile")
        {
            health -= 1;
            if(health <= 0)
            {
                Destroy(gameObject);
            }
            Destroy(collision.gameObject);
        }
    }
}

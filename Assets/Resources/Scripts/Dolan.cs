using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dolan : MonoBehaviour {

    private GameObject player;
    private bool alive = true;
    private float minimalSpeed = 3f;
    private int health = 10;

    private Animator anim;

    [SerializeField] private GameObject knifePrefab;
    private GameObject knife;
    private bool canShootNext;

    private void Start()
    {
        player = GameObject.Find("Player");
        anim = GetComponent<Animator>();
        canShootNext = true;
    }

    private void FixedUpdate()
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

            // Distance between Dolan and Player
            float distance = Mathf.Abs(playerVector.x + playerVector.y);

            // If Dolan's distance is smaller than 7 etc., shoot
            if (distance < 7 && knife == null && canShootNext)
            {
                StartCoroutine(shoot(playerVector));
            }
        }
    }

    IEnumerator shoot(Vector2 playerVector)
    {
        canShootNext = false;

        // Instantiate new knife at Dolan's position
        knife = Instantiate(knifePrefab) as GameObject;
        knife.transform.position = this.transform.position;

        // Launch and rotate the knife towards the player
        knife.GetComponent<Rigidbody2D>().AddForce(playerVector / 50);
        float angle = Mathf.Atan2(playerVector.y, playerVector.x) * Mathf.Rad2Deg;
        knife.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);


        // Wait and Destroy the knife
        yield return new WaitForSeconds(2f);
        Destroy(knife.gameObject);

        canShootNext = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Subtract one healthpoint if Dolan gets hit by the PlayerProjectile.
        if (collision.tag == "PlayerProjectile")
        {
            health -= 1;
            if(health <= 0)
            {
                // Coroutine because Wait Time is necessary
                StartCoroutine(die());
            }
            Destroy(collision.gameObject);
        }
    }

    IEnumerator die()
    {
        // Activate Death Animation (Animator)
        anim.SetBool("isDead", true);

        // Wait for animation to run ONCE (!) If waiting for longer than 0.3s, animation will restart
        yield return new WaitForSeconds(0.3f);

        // Destroy that duckling
        alive = false;
        Destroy(this.gameObject);
    }
}

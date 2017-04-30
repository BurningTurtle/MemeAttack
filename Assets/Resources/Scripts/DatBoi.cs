using UnityEngine;
using System.Collections;

public class DatBoi : MonoBehaviour {

    private GameObject player;
    private bool alive = true;
    public float speed = 1f;
    private int health = 10;

    private Animator anim;

    // Keep track of which direction DatBoi is moving to. Important for animation.
    private float deltaX;
    private float lastPosition;

    private void Start()
    {
        player = GameObject.Find("Player");
        anim = GetComponent<Animator>();
        lastPosition = transform.position.x;
        anim.SetBool("isDead", false);
    }

    private void Update()
    {
        if (alive)
        {
            Vector2 playerVector = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
            GetComponent<Rigidbody2D>().velocity = playerVector * speed * Time.deltaTime;

            // Accelerate DatBoi if he is too slow
            while(GetComponent<Rigidbody2D>().velocity.magnitude < speed)
            {
                GetComponent<Rigidbody2D>().velocity *= 1.1f;
            }

            // Get deltaX for current position.
            deltaX = lastPosition - transform.position.x;

            // Last position for the next frame is the current position from now.
            lastPosition = transform.position.x;

            // If DatBoi is moving right
            if(deltaX < 0)
            {
                // DatBoi Mirrored animation is set active (Animator)
                anim.SetBool("negativeDeltaX", true);
            }
            else if (deltaX > 0)
            {
                // Normal DatBoi animation is set active (Animator)
                anim.SetBool("negativeDeltaX", false);
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Substract 1 health if hit by PlayerProjectile GameObject.
        if (collision.gameObject.tag == "PlayerProjectile")
        {
            health -= 1;

            if (health <= 0)
            {
                // Coroutine because Wait Time is necessary
                StartCoroutine(die());
            }
        }

        Destroy(collision.gameObject);
    }

    IEnumerator die()
    {
        anim.SetBool("isDead", true);

        // Wait for animation to run ONCE (!) If waiting for longer than 0.3s, animation will restart
        yield return new WaitForSeconds(0.3f);

        // Then kill him
        alive = false;
        Destroy(this.gameObject);
    } 
}

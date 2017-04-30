using UnityEngine;
using System.Collections;

public class DatBoi : MonoBehaviour {

    private GameObject player;
    private bool alive = true;
    public float speed = 3f;
    private int health = 10;

    private Animator anim;

    // Keep track of which direction DatBoi is moving to. Important for animation.
    private float deltaX;
    private float lastPosition;

    [SerializeField] private GameObject oshitwaddupPrefab;
    [SerializeField] private GameObject oshitwaddup;
    public bool canShootNext;

    private void Start()
    {
        player = GameObject.Find("Player");
        anim = GetComponent<Animator>();
        lastPosition = transform.position.x;
        anim.SetBool("isDead", false);
        canShootNext = true;
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

            // Distance between DatBoi and player
            float distance = Mathf.Abs(playerVector.x + playerVector.y);

            // If DatBoi's distance to the player is smaller than 7 [...] then shoot
            if(distance < 7 && oshitwaddup == null && canShootNext)
            {
                StartCoroutine(shoot(playerVector));
            }
        }
        
    }

    IEnumerator shoot(Vector2 playerVector)
    {
        canShootNext = false;

        // Instantiate oshitwaddup on Datboi
        oshitwaddup = Instantiate(oshitwaddupPrefab) as GameObject;
        oshitwaddup.transform.position = transform.position;

        // Move oshitwaddup to Player
        oshitwaddup.GetComponent<Rigidbody2D>().AddForce(playerVector / 50);

        // Destroy after 2s if it didn't hit anything
        yield return new WaitForSeconds(2f);
        Destroy(oshitwaddup.gameObject);

        canShootNext = true;
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

            Destroy(collision.gameObject);
        }
    }

    IEnumerator die()
    {
        // Activate Death Animation (Animator)
        anim.SetBool("isDead", true);

        // Wait for animation to run ONCE (!) If waiting for longer than 0.3s, animation will restart
        yield return new WaitForSeconds(0.3f);

        // Then kill him
        alive = false;
        Destroy(this.gameObject);
    } 
}

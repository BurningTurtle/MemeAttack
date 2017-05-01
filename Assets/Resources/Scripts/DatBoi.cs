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
    private GameObject oshitwaddup;
    public bool canShootNext;

    // Stuff for Spawning bois
    GameObject[] bois;
    [SerializeField] GameObject datBoiPrefab;
    GameObject boi1, boi2, boi3, boi4;
    bool canSpawnNext = true;

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
        bois = GameObject.FindGameObjectsWithTag("DatBoi");

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

            if (bois.Length < 2 && canSpawnNext == true)
            {
                StartCoroutine(SpawnDemBois());
                canSpawnNext = false;
            }
        }
        
    }

    IEnumerator SpawnDemBois()
    {
        // Give Player time to kill single DatBoi. Prevents Endless Spawning.
        yield return new WaitForSeconds(5);

        // Cool animation here
        Debug.Log("Here Come Dem Bois");

        // Instantiate them
        boi1 = Instantiate(datBoiPrefab) as GameObject;
        boi2 = Instantiate(datBoiPrefab) as GameObject;
        boi3 = Instantiate(datBoiPrefab) as GameObject;
        boi4 = Instantiate(datBoiPrefab) as GameObject;

        // Later they're spawned at the Enemy's entrance. These coordinates for now.
        boi1.transform.position = new Vector2(5, 10);
        boi2.transform.position = new Vector2(5, 8);
        boi3.transform.position = new Vector2(5, 6);
        boi4.transform.position = new Vector2(5, 4);
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
